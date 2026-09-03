using System.Text.Json.Serialization;

namespace SolarSystem;

public class Program
{
    static void Main(string[] args)
    {
        Node<CelestialBody> solarSystem = CreateSolarSystem();
        PrintSolarSystem(solarSystem);
    }

    static void PrintSolarSystem(Node<CelestialBody> node, int level = 0)
    {
        Console.WriteLine(new string(' ', level * 2) + $"{node.Value.Name} kreist um {(node.Parent != null ? node.Parent.Value.Name : "nichts")}");
        foreach (var child in node.Children)
        {
            PrintSolarSystem(child, level + 1);
        }
    }

    public static Node<CelestialBody> CreateSolarSystem()
    {
        //CelestialBody sun = new CelestialBody("Sun", CelestialBodyType.Star);
        //Node<CelestialBody> sunNode = new Node<CelestialBody>(sun);

        //CelestialBody earth = new CelestialBody("Earth", CelestialBodyType.Planet);
        //var earthNode = new Node<CelestialBody>(earth, sunNode);
        //sunNode.AddChildNode(earthNode);

        //var mars = earth with { Name = "Mars" };
        //sunNode.AddChild(mars);

        //CelestialBody moon = new CelestialBody("Moon", CelestialBodyType.Moon);
        //earthNode.AddChild(moon);
        Node<CelestialBody>.UseDynamic = false;
        CelestialBody sun = new CelestialBody("Sun", CelestialBodyType.Star);
        Node<CelestialBody> sunNode = new Node<CelestialBody>(sun);

        CelestialBody earth = new CelestialBody("Earth", CelestialBodyType.Planet);
        var mars = earth with { Name = "Mars" };
        var jupiter = earth with { Name = "Jupiter" };
        CelestialBody moon = new CelestialBody("Moon", CelestialBodyType.Moon);
        var deimos = moon with { Name = "Deimos" };
        var phobos = moon with { Name = "Phobos" };

        sunNode.AddChildTo(sun, earth);
        sunNode.AddChildTo(sun, mars);
        sunNode.AddChildTo(earth, moon);
        sunNode.AddChildTo(mars, deimos);
        sunNode.AddChildTo(mars, phobos);
        sunNode.AddChildTo(sun, jupiter);


        return sunNode;
    }
}

public class Node<T>
{
    public T Value { get; }

    // [JsonIgnore] - löst das Problem
    public Node<T>? Parent { get; } = default;
    public IEnumerable<Node<T>> Children { get; } = new List<Node<T>>();
    
    public IEnumerable<Node<T>> FlatChildrenDynamic 
    { 
        get
        {
            yield return this;
            foreach (var child in Children)
            {
                foreach (var descendant in child.FlatChildrenDynamic)
                {
                    yield return descendant;
                }
            }
        }
    }

    public IList<Node<T>> FlatChildrenStatic { get; private set; } = new List<Node<T>>();
    public static bool UseDynamic { get; set; } = false;
    public Node<T> Root => Parent == null ? this : Parent.Root;

    public Node(T value)
    {
        Value = value;
    }

    [JsonConstructor]
    public Node(T value, Node<T> parent) : this(value)
    {
        Parent = parent;
    }

    public void AddChildNode(Node<T> child)
    {
        // ArgumentNullException.ThrowIfNull(child, nameof(child)); // Kürzer, aber nicht .NetStandard
        if (child == null)
        {
            throw new ArgumentNullException(nameof(child));
        }
        ((List<Node<T>>)Children).Add(child);
    }

    public void AddChild(T value)
    {
        var childNode = new Node<T>(value, this);
        AddChildNode(childNode);
    }

    public void RemoveChildNode(Node<T> child)
    {
        // ArgumentNullException.ThrowIfNull(child, nameof(child)); // Kürzer, aber nicht .NetStandard
        if (child == null)
        {
            throw new ArgumentNullException(nameof(child));
        }
        ((List<Node<T>>)Children).Remove(child);
    }

    public bool AddChildToDynamic(T parentValue, T childValue)
    {
        foreach (var node in FlatChildrenDynamic)
        {
            if(node.Value!.Equals(parentValue))
            {
                node.AddChild(childValue);
                return true;
            }
        }
        return false;
    }
    internal bool AddChildTo(T parentValue, T childValue)
    {
        return Node<T>.UseDynamic ? AddChildToDynamic(parentValue, childValue) : AddChildToStatic(parentValue, childValue);
    }

    internal bool AddChildToStatic(T parentValue, T childValue)
    {
        var root = Root;
        bool checkAndAdd(Node<T> node)
        {
            if (node.Value!.Equals(parentValue))
            {
                node.AddChild(childValue);
                root.RebuildFlatChildrenStatic();
                return true;
            }
            return false;
        }

        if(checkAndAdd(root))
        {
            return true;
        }
        foreach (var node in root.FlatChildrenStatic)
        {
            if (checkAndAdd(node))
            {
                return true;
            }
        }
        return false;
    }

    private void RebuildFlatChildrenStatic()
    {
        Root.FlatChildrenStatic= new List<Node<T>>(Root.FlatChildrenDynamic);
    }
}

//public class CelestialBody
//{
//    public CelestialBody(string name, CelestialBodyType type)
//    {
//        Name = name;
//        Type = type;
//    }

//    public string Name { get; init; }
//    public CelestialBodyType Type { get; init; }
//}

public record class CelestialBody(string Name, CelestialBodyType Type);

public enum CelestialBodyType
{
    Star,
    Planet,
    Moon
}

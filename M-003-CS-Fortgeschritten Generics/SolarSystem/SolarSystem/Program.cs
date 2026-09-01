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
        CelestialBody sun = new CelestialBody("Sun", CelestialBodyType.Star);
        Node<CelestialBody> sunNode = new Node<CelestialBody>(sun);

        CelestialBody earth = new CelestialBody("Earth", CelestialBodyType.Planet);
        var earthNode = new Node<CelestialBody>(earth, sunNode);
        sunNode.AddChildNode(earthNode);

        var mars = earth with { Name = "Mars" };
        sunNode.AddChild(mars);

        CelestialBody moon = new CelestialBody("Moon", CelestialBodyType.Moon);
        earthNode.AddChild(moon);

        return sunNode;
    }
}

public class Node<T>
{
    public T Value { get; }

    // [JsonIgnore] - löst das Problem
    public Node<T>? Parent { get; } = default;
    public IEnumerable<Node<T>> Children { get; } = new List<Node<T>>();
 
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

    //public bool AddChildTo(T existingNode, T newNode)
    //{
    //    var childNode = FindChildNode(existingNode);
    //    if(childNode == null)
    //    {
    //        return false;
    //    }
    //    childNode.AddChild(moon);
    //}

    //protected Node<T>? FindChildNode(Node<T> node)
    //{
    //    var result = node.Children.FirstOrDefault(c => c.Value!.Equals(node.Value));
    //    if(result != null)
    //    {
    //        return result;
    //    }
    //    foreach (var child in node.Children)
    //    {
    //        if(child.FindChildNode(child) is Node<T> found)
    //        {
    //            return found;
    //        }
    //    }
    //}
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

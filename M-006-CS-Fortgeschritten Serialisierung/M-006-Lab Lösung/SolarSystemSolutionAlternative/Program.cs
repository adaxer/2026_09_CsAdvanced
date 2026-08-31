using SolarSystemSolutionAlternative;
using System.Text.Json;

Node<SolarItem> sunNode = new Node<SolarItem>(new SolarItem("Sonne", SolarItemType.Star));

Node<SolarItem> merkur = new Node<SolarItem>(new SolarItem("Merkur", SolarItemType.Planet));
Node<SolarItem> venus = new Node<SolarItem>(new SolarItem("Venus", SolarItemType.Planet));

Node<SolarItem> erde = new Node<SolarItem>(new SolarItem("Erde", SolarItemType.Planet));
Node<SolarItem> mond = new Node<SolarItem>(new SolarItem("Mond", SolarItemType.Trabant));
erde.AddChild(mond);

Node<SolarItem> mars = new Node<SolarItem>(new SolarItem("Mars", SolarItemType.Planet));
Node<SolarItem> phobos = new Node<SolarItem>(new SolarItem("Phobos", SolarItemType.Trabant));
Node<SolarItem> deimos = new Node<SolarItem>(new SolarItem("Deimos", SolarItemType.Trabant));
mars.AddChild(phobos);
mars.AddChild(deimos);



sunNode.AddChild(merkur);
sunNode.AddChild(venus);
sunNode.AddChild(erde);
sunNode.AddChild(mars);



NodeCollection<SolarItem> solarItemCollection = new NodeCollection<SolarItem>();
solarItemCollection.Add(sunNode);
solarItemCollection.Add(merkur);
solarItemCollection.Add(venus);
solarItemCollection.Add(erde);
solarItemCollection.Add(mond);
solarItemCollection.Add(mars);
solarItemCollection.Add(phobos);
solarItemCollection.Add(deimos);



DisplaySolarSystem(solarItemCollection.GetRootNode());

void DisplaySolarSystem(Node<SolarItem> solarNode)
{

    if (solarNode.Item.Type == SolarItemType.Star)
        Console.WriteLine($"Sonne: {solarNode.Item.Description}");

    if (solarNode.Item.Type == SolarItemType.Planet)
        Console.WriteLine($"\tPlanet: {solarNode.Item.Description} - kreist um {solarItemCollection.GetNode(solarNode.ParentId).Item.Description}");

    if (solarNode.Item.Type == SolarItemType.Trabant)
        Console.WriteLine($"\t\t -Trabant: {solarNode.Item.Description} - kreist um {solarItemCollection.GetNode(solarNode.ParentId).Item.Description}");

    if (solarNode.Item.Type != SolarItemType.Trabant)
    {
        if (solarNode.Item.Description == "Erde")
        {

        }

        foreach (Guid currentChildId in solarNode.Children)
        {
            Node<SolarItem> node = solarItemCollection.GetNode(currentChildId);
            DisplaySolarSystem(node);
        }
    }

}

Console.WriteLine("------ JSON ----------");

string json = SaveWithJsonSerializer(solarItemCollection);

NodeCollection<SolarItem> solarItemCollectionAfterLoading = LoadWithJsonSerilizer(json);

DisplaySolarSystem(solarItemCollectionAfterLoading.GetRootNode());



string SaveWithJsonSerializer(NodeCollection<SolarItem> collection)
{
    JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions();
    //jsonSerializerOptions.WriteIndented = true;
    return JsonSerializer.Serialize<NodeCollection<SolarItem>>(collection, jsonSerializerOptions);
}

NodeCollection<SolarItem>? LoadWithJsonSerilizer(string input)
{
    NodeCollection<SolarItem>? collection = new NodeCollection<SolarItem>();
   
    try
    {
        collection = JsonSerializer.Deserialize<NodeCollection<SolarItem>>(input);
    }
    catch (InvalidOperationException ex)
    {

    }
    catch (JsonException ex)
    {

    }

    return collection;
}









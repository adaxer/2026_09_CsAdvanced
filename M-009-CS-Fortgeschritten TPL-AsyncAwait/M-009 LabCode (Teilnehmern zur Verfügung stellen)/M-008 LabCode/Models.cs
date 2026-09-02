using System.Text.Json.Serialization;

namespace JsonSplit;

public class CityEntry
{
    [JsonPropertyName("id")]
    [JsonConverter(typeof(MongoLongConverter))]
    public long Id { get; set; }

    [JsonPropertyName("city")]
    public City City { get; set; }
}

public class City
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("country")]
    public string Country { get; set; }
}


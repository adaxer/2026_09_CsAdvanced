using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace JsonSplit;

public partial class MainWindow : Window
{
    /// <summary>
    /// Datei herunterladen, an einen beliebigen Ort speichern, entpacken und danach mit SaveSplitJson einlesen und bearbeiten.
    /// http://bulk.openweathermap.org/sample/history.city.list.min.json.gz
    /// </summary>
    public MainWindow() => InitializeComponent();

    /// <summary>
    /// Diese Methode soll die originale Json Datei laden, aufteilen und in die einzelnen Dateien speichern.
    /// Diese Methode ist mit dem linken Button (Split Json) in der UI verbunden
    /// </summary>
    private async void SplitJsonFileAndSaveFiles(object sender, EventArgs e)
    {
        (sender as Button)?.IsEnabled = false;  // Aktion nicht mehrfach!
        WriteText("Download JsonData...");
        List<string> elements;
        try
        {
            // Download zipped file and decompress it to a local file
            var client = new HttpClient();
            using var gzStream = await client.GetStreamAsync("http://bulk.openweathermap.org/sample/history.city.list.min.json.gz");
            var outputFileStream = File.Create("cities.json");
            using var decompressor = new GZipStream(gzStream, CompressionMode.Decompress);
            await decompressor.CopyToAsync(outputFileStream);
            await gzStream.FlushAsync();
            outputFileStream.Close();
            List<CityEntry> cityEntries = JsonSerializer.Deserialize<List<CityEntry>>(await File.ReadAllTextAsync("cities.json"));

            // Read the decompressed JSON file and split it into individual JSON objects
            WriteText("Parse JsonData...");
            using var inputFileStream = File.OpenRead("cities.json");
            JsonDocument jsonDoc = await JsonDocument.ParseAsync(inputFileStream);
            var root = jsonDoc.RootElement;
            List<City> cities = new();
            elements = new List<string>();
            foreach (var jElement in root.EnumerateArray())
            {
                elements.Add(jElement.ToString());
                //Trace.TraceInformation(elements.Last());
                var cityEntry = JsonSerializer.Deserialize<CityEntry>(jElement.GetRawText());
                var cityName = jElement.GetProperty("city").GetProperty("name").GetString();
                var countryName = jElement.GetProperty("city").GetProperty("country").GetString();
                City city = new City { Name = cityName, Country = countryName };
                cities.Add(city);
            }
            inputFileStream.Close();


        }
        catch (Exception ex)
        {
            WriteText(ex.Message);
        }
        (sender as Button)?.IsEnabled = true;
    }

    /// <summary>
    /// Verwende diese Methode, um einen Text in der TextBox anzuzeigen.
    /// </summary>
    /// <param name="text"></param>
    private void WriteText(string text)
    {
        Dispatcher.Invoke(() =>
        {
            Output.Text += text;
            Output.Text += Environment.NewLine;
            Scroll.ScrollToEnd();
        });
    }

    //Es gibt keine Methode um aus einer Liste von JsonElements ein JsonArray zu generieren
    private string JsonListToJson(IEnumerable<JsonElement> jsons)
    {
        return jsons.Aggregate(new StringBuilder("[\n"), (sb, je) =>
            sb.Append('\t')
              .Append(je.GetRawText())
              .Append(",\n"))
              .ToString()
              .TrimEnd(',', '\n') + "\n]";
    }
}

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

public class MongoLongConverter : JsonConverter<long>
{
    public override long Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Number)
        {
            // Try to read as Int64 first
            if (reader.TryGetInt64(out var int64Value))
                return int64Value;

            // If that fails, get as double and cast to long (handles decimals like 123.0)
            var doubleValue = reader.GetDouble();
            return (long)doubleValue;
        }

        if (reader.TokenType == JsonTokenType.StartObject)
        {
            using var document = JsonDocument.ParseValue(ref reader);

            if (document.RootElement.TryGetProperty("$numberLong", out var value))
                return long.Parse(value.GetString()!);
        }

        throw new JsonException($"Cannot deserialize long from {reader.TokenType}");
    }

    public override void Write(
        Utf8JsonWriter writer,
        long value,
        JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value);
    }
}

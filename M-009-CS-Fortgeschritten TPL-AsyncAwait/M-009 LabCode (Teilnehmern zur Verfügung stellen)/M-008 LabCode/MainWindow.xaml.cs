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
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace JsonSplit;

public partial class MainWindow : Window
{
    private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

    /// <summary>
    /// Datei herunterladen, an einen beliebigen Ort speichern, entpacken und danach mit SaveSplitJson einlesen und bearbeiten.
    /// http://bulk.openweathermap.org/sample/history.city.list.min.json.gz
    /// </summary>
    public MainWindow() => InitializeComponent();

    /// <summary>
    /// Diese Methode soll die originale Json Datei laden, aufteilen und in die einzelnen Dateien speichern.
    /// Diese Methode ist mit dem linken Button (Split Json) in der UI verbunden
    /// </summary>
    private async void ButtonClick(object sender, EventArgs e)
    {
        (sender as Button)?.IsEnabled = false;  // Aktion nicht mehrfach!
        _cancellationTokenSource = new CancellationTokenSource();
        try
        {
            var theTask = SplitJsonFileAndSaveFilesAsync(_cancellationTokenSource.Token);
            await Task.Delay(2000);
            //_cancellationTokenSource.Cancel();
            await theTask;
        }
        catch (Exception ex) 
        {
            WriteText($"Error: {ex.Message}");
        }
        finally
        {
            (sender as Button)?.IsEnabled = true;
        }
    }
    
    private async Task SplitJsonFileAndSaveFilesAsync(CancellationToken cancellationToken)
    {
        Progress.IsIndeterminate = false;
        Progress.Value = 0;

        try
        {
            // Download zipped file and decompress it to a local file
            WriteText("Download JsonData...");
            var client = new HttpClient();
            cancellationToken.ThrowIfCancellationRequested();
            using var gzStream = await client.GetStreamAsync("http://bulk.openweathermap.org/sample/history.city.list.min.json.gz");
            var outputFileStream = File.Create("cities.json");
            using var decompressor = new GZipStream(gzStream, CompressionMode.Decompress);
            Progress.Value = 20;
            WriteText("Decompress and save JsonData...");
            cancellationToken.ThrowIfCancellationRequested();
            await decompressor.CopyToAsync(outputFileStream);
            await gzStream.FlushAsync();
            outputFileStream.Close();
            Progress.Value = 40;

            // Read the decompressed JSON file and split it into individual JSON objects
            WriteText("Read JsonData...");
            cancellationToken.ThrowIfCancellationRequested();
            List<CityEntry> cityEntries = JsonSerializer.Deserialize<List<CityEntry>>(await File.ReadAllTextAsync("cities.json"));

            var groups = cityEntries.GroupBy(c => c.City.Country);
            double delta = 60.0 / groups.Count();
            // Group into countries and save the files
            foreach (var group in groups)
            {
                cancellationToken.ThrowIfCancellationRequested();
                WriteText($"Save JsonData for country {group.Key}...");
                var country = group.Key;
                var cities = group.Select(c => c.City).ToList();
                var json = JsonSerializer.Serialize(cities, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync($"{country}.json", json);
                Progress.Value = Math.Min(Progress.Value += delta, 100);
            }
        }
        catch(OperationCanceledException)
        {
            WriteText("Operation was canceled.");
            // Optionally, you can clean up any partially written files here
            return;
        }
        catch (Exception ex)
        {
            WriteText(ex.Message);
        }
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
}

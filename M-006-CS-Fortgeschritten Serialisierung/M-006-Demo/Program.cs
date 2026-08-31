using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Xml.Serialization;
using static System.Environment;

namespace Serialisierung
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string desktop = GetFolderPath(SpecialFolder.DesktopDirectory);

			string folderPath = Path.Combine(desktop, "Serialisierung");

			if (!Directory.Exists(folderPath))
				Directory.CreateDirectory(folderPath);

			string filePath = Path.Combine(folderPath, "Test.txt");
			//using StreamWriter sw = new StreamWriter(filePath) { AutoFlush = true }; //Wird am Ende der Methode geschlossen
			//sw.WriteLine("Test");
			//sw.WriteLine("Test");
			//sw.WriteLine("Test");
			//sw.WriteLine("Test");

			List<Fahrzeug> fahrzeuge = new List<Fahrzeug>
			{
				new Fahrzeug(0, 251, FahrzeugMarke.BMW),
				new Fahrzeug(1, 274, FahrzeugMarke.BMW),
				new Fahrzeug(2, 146, FahrzeugMarke.BMW),
				new Fahrzeug(3, 208, FahrzeugMarke.Audi),
				new Fahrzeug(4, 189, FahrzeugMarke.Audi),
				new Fahrzeug(5, 133, FahrzeugMarke.VW),
				new Fahrzeug(6, 253, FahrzeugMarke.VW),
				new Fahrzeug(7, 304, FahrzeugMarke.BMW),
				new Fahrzeug(8, 151, FahrzeugMarke.VW),
				new Fahrzeug(9, 250, FahrzeugMarke.VW),
				new Fahrzeug(10, 217, FahrzeugMarke.Audi),
				new Fahrzeug(11, 125, FahrzeugMarke.Audi)
			};

			#region Json
			string json = System.Text.Json.JsonSerializer.Serialize(fahrzeuge[0]); //Hier Json string zwischenspeichern
			File.WriteAllText(filePath, json);

			using Stream s = new FileStream(filePath, FileMode.Create);
			System.Text.Json.JsonSerializer.Serialize(s, fahrzeuge); //Hier direkt auf die Festplatte schreiben

			using StreamReader sr = new StreamReader(filePath);
			List<Fahrzeug> read = System.Text.Json.JsonSerializer.Deserialize<List<Fahrzeug>>(sr.BaseStream);

			//Newtonsoft.Json
			string json2 = JsonConvert.SerializeObject(fahrzeuge);
			File.WriteAllText(filePath, json2);

			string read2 = File.ReadAllText(filePath);
			List<Fahrzeug> read2Fzg = JsonConvert.DeserializeObject<List<Fahrzeug>>(read2);
			#endregion

			#region XML
			XmlSerializer xml = new XmlSerializer(typeof(List<Fahrzeug>));
			using Stream xmlStream = new FileStream(filePath, FileMode.Create);
			xml.Serialize(xmlStream, fahrzeuge);

			using Stream xmlReadStream = new FileStream(filePath, FileMode.Open);
			List<Fahrzeug> xmlReadFzg = xml.Deserialize(xmlReadStream) as List<Fahrzeug>;
			#endregion

			#region Binary
			BinaryFormatter formatter = new BinaryFormatter();
			using Stream binStream = new FileStream(filePath, FileMode.Create);
			formatter.Serialize(binStream, fahrzeuge); //Klasse muss als Serializable gekennzeichnet sein

			using Stream binReadStream = new FileStream(filePath, FileMode.Open);
			List<Fahrzeug> binReadFzg = formatter.Deserialize(binReadStream) as List<Fahrzeug>;
			#endregion

			#region CSV
			File.WriteAllText(filePath, fahrzeuge.Aggregate(string.Empty, (agg, fzg) => agg + $"{fzg.ID};{fzg.MaxGeschwindigkeit};{fzg.Marke}\n"));

			TextFieldParser tfp = new TextFieldParser(filePath);
			tfp.SetDelimiters(";");

			string[] header = tfp.ReadFields(); //Header wenn existieren

			List<Fahrzeug> csvRead = new();
			while (!tfp.EndOfData)
			{
				string[] fields = tfp.ReadFields(); //Eine Zeile lesen
				csvRead.Add(new Fahrzeug(int.Parse(fields[0]), int.Parse(fields[1]), Enum.Parse<FahrzeugMarke>(fields[2])));
			}
			#endregion
		}
	}
}

//public record Fahrzeug(int ID, int MaxGeschwindigkeit, FahrzeugMarke Marke);

[Serializable]
public class Fahrzeug
{
	[field: NonSerialized] //BinaryIgnore
	[JsonIgnore] //Nicht schreiben bei Json (bei beiden Frameworks)
	[XmlIgnore] //Xml
	public int ID;

	public int MaxGeschwindigkeit;

	public FahrzeugMarke Marke;

	public Fahrzeug(int id, int v, FahrzeugMarke fm)
	{
		ID = id;
		MaxGeschwindigkeit = v;
		Marke = fm;
	}

	public Fahrzeug() { }
}

public enum FahrzeugMarke { Audi, BMW, VW }
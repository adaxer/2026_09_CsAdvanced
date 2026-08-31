using System.Diagnostics;
using System.Text.Json;

internal class Program
{
	static void Main(string[] args)
	{
		string readJson = File.ReadAllText(@"..\..\..\Personen.json");
		List<Person> personen = JsonSerializer.Deserialize<List<Person>>(readJson);

		personen.Where(e => e.Alter >= 60);
		personen.Where(e => e.Geburtsdatum.Year == 1977);
		personen.Where(e => e.Job.Gehalt > 5000);
		personen.OrderBy(e => e.Job.Titel).ThenBy(e => e.Job.Gehalt);

		personen.Count(e => e.Vorname.Length >= 10);
		personen.Where(e => e.Job.Titel == "Softwareentwickler").Average(e => e.Job.Gehalt);
		personen.Count(e => e.Hobbies.Count == 2);
		personen.Where(e => e.Hobbies.Contains("Radfahren") && e.Hobbies.Contains("Laufen"));

		personen.Where(e => e.Vorname[0] == 'M' && e.Nachname[0] == 'S');
		personen.Where(e => e.Vorname[0] == e.Nachname[0]);
		personen.Where(e => e.Alter > personen.Average(x => x.Alter));
		personen.Where(e => e.Alter > 60 && (e.Hobbies.Contains("Laufen") || e.Hobbies.Contains("Radfahren") || e.Hobbies.Contains("Ballsport") || e.Hobbies.Contains("Fitness")));

		personen.Where(e => e.Alter > 60 && e.Hobbies.Intersect(new string[] { "Laufen", "Radfahren", "Ballsport", "Fitness" }).Any());
		
		personen.Select(e => e.Job.Titel).Distinct().Count();
		personen.Where(e => e.Job.Titel == "Tischler").Max(e => e.Job.Gehalt);
		personen.All(e => e.Alter > 50 && e.Job.Gehalt > 2000);
		personen.Select(e => e.Vorname + " " + e.Nachname).OrderBy(e => e.Length);

		personen.OrderByDescending(e => e.Job.Gehalt).Take(5);
		personen.Where(e => (int) (DateTime.Today.Subtract(e.Job.Einstellungsdatum).TotalDays / 365.25) >= 20);
		personen.GroupBy(e => e.Job.Titel).ToDictionary(e => e.Key, e => e.Max(x => x.Job.Gehalt)).OrderBy(e => e.Key);
		personen.GroupBy(e => e.Vorname).ToDictionary(e => e.Key, e => e.Count()).OrderByDescending(e => e.Value);
		
		personen.SelectMany(e => e.Hobbies).GroupBy(e => e).ToDictionary(e => e.Key, e => e.Count()).OrderByDescending(e => e.Value).Take(1);
		personen.GroupBy(e => e.Job.Titel).ToDictionary(e => e.Key, e => e.OrderByDescending(x => x.Job.Gehalt).Take(3));
	}
}

///////////////////////////////////////////////////////////////////////////////

[DebuggerDisplay("Person - ID: {ID}, Vorname: {Vorname}, Nachname: {Nachname}, GebDat: {Geburtsdatum.ToString(\"yyyy.MM.dd\")}, Alter: {Alter}, " +
	"Jobtitel: {Job.Titel}, Gehalt: {Job.Gehalt}, Einstellungsdatum: {Job.Einstellungsdatum.ToString(\"yyyy.MM.dd\")}")]
public record Person(int ID, string Vorname, string Nachname, DateTime Geburtsdatum, int Alter, Beruf Job, List<string> Hobbies);

public record Beruf(string Titel, int Gehalt, DateTime Einstellungsdatum);

///////////////////////////////////////////////////////////////////////////////
using System.Collections;
using System.Diagnostics;
using System.Text.Json;

namespace M012;

internal class Program
{
	static void Main(string[] args)
	{
		#region File lesen
		string readJson = File.ReadAllText(@"..\..\..\Personen.json");
		List<Person> personen = JsonSerializer.Deserialize<List<Person>>(readJson)!;
		#endregion
		

		var names = from person in personen.Take(100)
					where person.Alter > 30
					select new { person.Vorname, person.Nachname };

		var firstNamed = names.First();
		Type anonType = firstNamed.GetType();

		// Order() aus C# 11, var ordered = personen.Select(p=>p.ID).Order().ToList();

		(int Id, string Name) passenger = (1, "Maier");
		PrintPassenger(passenger);
		IEnumerable<Person> somePersons = personen;

		// var isEmpty = somePersons.IsEmpty? - Offenbar noch keine Extension Properties in Linq

        //Hier eigenen Code schreiben
        var aged = personen.Where(p=>p.Alter>=60).ToList();

		var riches1 = personen.Where(p => p.Job.Gehalt > 5000);
		var riches2 = riches1.Select(p => p.ID);
		var riches3 = riches2.ToList();

		var jobsSalaries = personen.OrderBy(p => p.Job.Titel).ThenBy(p => p.Job.Gehalt).ToList();

		var longPrenames = personen.Where(p => p.Vorname.Length > 10).Select(p => p.Vorname).ToList();

		var swSalary = personen.Where(p => p.Job.Titel.Contains("Software")).Select(p => p.Job.Gehalt).Average();

		var distinctPersons = personen.DistinctBy(p=>p.Job.Titel).ToList();
		var distinctJobs = personen.Select(p => p.Job.Titel).Distinct().ToList(); 

        var oldGoodSalary = personen.Where(p=>p.Alter>50).All(p => p.Job.Gehalt*12 > 25000);

		// ...
		personen.GroupBy(p=>p.Job.Titel) 
						   .Select(g => new KeyValuePair<string, List<Person>>(g.Key, g.OrderByDescending(h => h.Job.Gehalt).Take(3).ToList()))
						   .ToDictionary(k => k.Key, v => v.Value)
						   .Print();
    }

    private static void PrintPassenger((int Id, string Name) passenger)
    {
        Console.WriteLine($"Passenger ID: {passenger.Id}, Name: {passenger.Name}");
    }
}

public static class LinqExtensions
{
    // .Net 10 kann auch Properties und hat extension Blocks,
    // Siehe: https://dev.to/rasheedmozaffar/exploring-extension-blocks-in-net-10-ijo

    public static void Print(this IDictionary<string, List<Person>> collection)
    {
		Console.WriteLine("Top-Verdiener nach Berufsgruppe:");
        foreach (var item in collection)
        {
            Console.WriteLine(item.Key);
            foreach (var person in item.Value)
            {
                Console.WriteLine($"  {person.Vorname} {person.Nachname}\t{person.Job.Gehalt}");
            }
        }
    }
}

///////////////////////////////////////////////////////////////////////////////

[DebuggerDisplay("Person - ID: {ID}, Vorname: {Vorname}, Nachname: {Nachname}, GebDat: {Geburtsdatum.ToString(\"yyyy.MM.dd\")}, Alter: {Alter}, " +
	"Jobtitel: {Job.Titel}, Gehalt: {Job.Gehalt}, Einstellungsdatum: {Job.Einstellungsdatum.ToString(\"yyyy.MM.dd\")}")]
public record Person(int ID, string Vorname, string Nachname, DateTime Geburtsdatum, int Alter, Beruf Job, List<string> Hobbies);

public record Beruf(string Titel, int Gehalt, DateTime Einstellungsdatum);

///////////////////////////////////////////////////////////////////////////////
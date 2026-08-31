namespace DelegatesEvents;

public class Program
{
	public delegate void Vorstellungen(string name); //Speichert Referenzen zu Methoden, können zur Laufzeit hinzugefügt oder weggenommen werden

	public static void Main(string[] args)
	{
		Vorstellungen vorstellungen; //Deklaration
		vorstellungen = new Vorstellungen(VorstellungDE); //Delegate erstellen mit einer Methode am Anfang
		vorstellungen += new Vorstellungen(VorstellungEN); //weitere Methode dranhängen
		vorstellungen -= new Vorstellungen(VorstellungDE); //Methode abziehen
		//vorstellungen -= new Vorstellungen(VorstellungDE); //kein Fehler wenn keine Methode dran
		//vorstellungen -= new Vorstellungen(VorstellungEN); //Delegate ab hier null (keine Methoden dran)

		if (vorstellungen != null)
			vorstellungen("Max"); //Aufruf
		else
			Console.WriteLine("Keine Methoden am Delegate");

		vorstellungen?.Invoke("Max1"); //hier mit ? überprüfen ob das Delegate Methoden hat

		foreach (Delegate dg in vorstellungen.GetInvocationList()) //Delegate iterieren
		{
			Console.WriteLine(dg.Method.Name);
		}

		vorstellungen = null; //Alle Methoden entfernen
	}

	public static void VorstellungDE(string name)
	{
		Console.WriteLine($"Hallo mein Name ist {name}");
	}

	public static void VorstellungEN(string name)
	{
		Console.WriteLine($"Hello my name is {name}");
	}
}
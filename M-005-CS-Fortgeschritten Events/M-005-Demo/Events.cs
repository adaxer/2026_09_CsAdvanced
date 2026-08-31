namespace DelegatesEvents;

/// <summary>
/// Event
/// Statischer Punkt, an den eine Methode angehängt wird
/// Diese Methode wird in der selben Klasse wie das Event ausgeführt
/// Der User der Klasse hängt die Methode an (definiert den Code, der bei feuern des Events ausgeführt wird)
/// 
/// Beispiel: Button Click
/// Zweigeteilte Programmierung: Entwicklerseite (Button Programmierung), Anwenderseite (Definition der Click-Methode)
/// Entwickler: Wann wird das Event gefeuert; Cursor ist auf dem Button, Linksklick, keine anderen Komponente über dem Button, Button muss aktiviert sein, ...
/// Anwender: Beim Klick soll X passieren
/// </summary>
internal class Events
{
	//Definition des Eventpunktes (mit einem beliebigen Delegate)
	//Entwicklerseite
	public event EventHandler TestEvent;

	public event EventHandler<CustomEventArgs> ArgsEvent;

	/////////////////////////////////////////////////////////////

	private event EventHandler accessorEvent;

	public event EventHandler AccessorEvent
	{
		add
		{
			accessorEvent += value;
			Console.WriteLine("Event hinzugefügt");
		}
		remove
		{
			accessorEvent -= value;
			Console.WriteLine("Event entfernt");
		}
	}

	static void Main(string[] args) => new Events().Start();

	public void Start()
	{
		TestEvent += Events_TestEvent; //Methode anhängen (Anwenderseite)

		TestEvent?.Invoke(this, EventArgs.Empty); //Feuern des Events (Entwicklerseite)

		/////////////////////////////////////////////////////////////

		ArgsEvent += Events_ArgsEvent;

		ArgsEvent?.Invoke(this, new CustomEventArgs("Erfolg"));
	}

	private void Events_TestEvent(object? sender, EventArgs e)
	{
		Console.WriteLine("TestEvent gefeuert");
	}

	private void Events_ArgsEvent(object? sender, CustomEventArgs e)
	{
		Console.WriteLine(e.Status);
	}
}

public class CustomEventArgs(string status) : EventArgs
{
	public readonly string Status = status;
}
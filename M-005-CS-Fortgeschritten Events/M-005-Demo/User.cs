namespace DelegatesEvents;

/// <summary>
/// Anwenderseite
/// </summary>
internal class User
{
	static void Main(string[] args)
	{
		Component comp = new();

		comp.Start += Comp_Start;
		comp.Progress += Comp_Progress;
		comp.End += Comp_End;

		comp.DoWork();
	}

	private static void Comp_Start(object? sender, EventArgs e)
	{
		Console.WriteLine("Arbeit gestartet");
	}

	private static void Comp_Progress(object? sender, int e)
	{
		Console.WriteLine($"Fortschritt: {e}");
	}

	private static void Comp_End(object? sender, EventArgs e)
	{
		Console.WriteLine("Arbeit beendet");
	}
}

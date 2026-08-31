using System.Text;

public class Program
{
	static void Main(string[] args)
	{
		Console.OutputEncoding = Encoding.UTF8;

		List<Task> tasks = new List<Task>();
		for (int i = 0; i < 50; i++) //Hier 50 Tasks erzeugen die alle jeweils 500 Transaktionen machen
			tasks.Add(Task.Run(() => KontoUpdate()));

		Task.WaitAll(tasks.ToArray());
	}

	static void KontoUpdate() //Random Einzahlungen und Auszahlungen ausführen
	{
		Random random = new Random();
		for (int i = 0; i < 500; i++)
		{
			Task.Run(() =>
			{
				int betrag = random.Next(0, 1000);
				bool einzahlen = random.Next() % 2 == 0;

				if (einzahlen)
					Konto.Einzahlen(betrag);
				else
				{
					Konto.Auszahlen(betrag);
					betrag *= -1;
				}

				return betrag;

			}).ContinueWith(task => Ausgabe(task.Result));
		}
	}

	static void Ausgabe(int betrag)
	{
		Console.WriteLine($"Es wurden {(betrag < 0 ? Math.Abs(betrag) + "€ \tabgehoben" : betrag + "€ \teingezahlt")}." +
			$" \tAktueller Kontostand: {Konto.Kontostand}€");
	}

	public static class Konto
	{
		public static int Kontostand { get; set; } = 0;

		public static void Einzahlen(int betrag) => Kontostand += betrag;

		public static void Auszahlen(int betrag) => Kontostand -= betrag;
	}
}
namespace KontoSimulator
{
    internal class Program
    {
		static void Main(string[] args)
		{
			Thread t = null;

			for (int i = 0; i < 500; i++) //Hier 500 Threads erzeugen die alle jeweils 10000 Transaktionen machen
			{
				t = new Thread(KontoUpdate);
				t.Start();
			}

			Console.WriteLine("Fertig");
		}

		static void KontoUpdate() //Random Einzahlungen und Auszahlungen ausführen
		{
			Random random = new Random();
			for (int i = 0; i < 10000; i++)
			{
				int betrag = random.Next(0, 1000);

				if (random.Next() % 2 == 0)
					Konto.Einzahlen(betrag);
				else
					Konto.Auszahlen(betrag);
			}
		}

		public static class Konto
		{
			public static int Kontostand { get; set; } = 0;
			public static int TransactionCount { get; set; } = 0;

			public static object LockFlagEinzahlen = new object(); //LockObject um Lock Status zu halten
			public static object LockFlagAuszahlen = new object();

			public static void Einzahlen(int betrag)
			{
				try
				{
					//Variablen werden gesperrt wenn Thread drauf zugreifen möchte
					//Threads die auf gelockte Blöcke zugreifen wollen müssen warten
					lock (LockFlagEinzahlen)
					{
						Kontostand += betrag;
						TransactionCount++;
						Console.WriteLine($"Kontostand: {Kontostand}");
					}
				}
				catch (Exception) //Wenn 2 Threads zum genau gleichen Zeitpunkt zugreifen wollen
				{
					Console.WriteLine("Deadlock");
				}
			}

			public static void Auszahlen(int betrag)
			{
				try
				{
					lock (LockFlagAuszahlen)
					{
						Kontostand -= betrag;
						TransactionCount++;
						Console.WriteLine($"Kontostand: {Kontostand}");
					}
				}
				catch (Exception)
				{
					Console.WriteLine("Deadlock");
				}
			}
		}
	}
}
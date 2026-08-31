// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

int numberOfThreads = 50;

Thread[] thread = new Thread[numberOfThreads];
WaitHandle[] waitHandles = new WaitHandle[numberOfThreads];

for (int i = 0; i < numberOfThreads; i++)
{
	EventWaitHandle handle = new EventWaitHandle(false, EventResetMode.ManualReset);
	waitHandles[i] = handle;

	ParameterizedThreadStart threadStart = new ParameterizedThreadStart(KontoUpdate);
	thread[i] = new Thread(threadStart);
	thread[i].Start(handle);
}

WaitHandle.WaitAll(waitHandles);
Console.WriteLine("Anwendung fertig");


static void KontoUpdate(object obj) //Random Einzahlungen und Auszahlungen ausführen
{
	EventWaitHandle handle = (EventWaitHandle)obj;

	Random random = new Random();
	for (int i = 0; i < 500; i++)
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

		Ausgabe(betrag);
	}
	handle.Set();
}


static void Ausgabe(int betrag)
  => Console.WriteLine($"Transaktionsnummer: {Konto.TransactionCounter} - \t Es wurden {(betrag < 0 ? Math.Abs(betrag) + "€ \tabgehoben" : betrag + "€ \teingezahlt")}." 
  +$" \tAktueller Kontostand: {Konto.Kontostand}€");

public static class Konto
{
	public static int TransactionCounter = 0;
	public static int Kontostand { get; set; } = 0;
	public static void Einzahlen(int betrag)
	{
		TransactionCounter++;
		Kontostand += betrag;
	}
	public static void Auszahlen(int betrag)
	{
		TransactionCounter++;
		Kontostand -= betrag;
	}
}

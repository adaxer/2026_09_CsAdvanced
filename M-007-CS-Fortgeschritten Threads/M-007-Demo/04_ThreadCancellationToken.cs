namespace Multithreading
{
	internal class _04_ThreadCancellationToken
	{
		static void Main(string[] args)
		{
			CancellationTokenSource cts = new CancellationTokenSource(); //Source erstellen
			CancellationToken ct = cts.Token; //Token aus Source entnehmen

			ParameterizedThreadStart pt = new ParameterizedThreadStart(Run);
			Thread t = new Thread(pt);
			t.Start(ct); //Token als Parameter übergeben

			Thread.Sleep(2000);
			cts.Cancel(); //Cancellation anordnen
		}

		static void Run(object o) //CancellationToken übergeben
		{
			try
			{
				if (o is CancellationToken ct)
				{
					for (int i = 0; i < 100; i++)
					{
						if (ct.IsCancellationRequested)
							ct.ThrowIfCancellationRequested(); //OperationCanceledException werfen

						Console.WriteLine(i);
						Thread.Sleep(100);
					}
				}
			}
			catch (OperationCanceledException) //Muss auch wieder hier unten gemacht werden
			{
				Console.WriteLine("Thread wurde mit Token beendet");
			}
		}
	}
}

namespace Multithreading
{
	internal class _03_ThreadBeenden
	{
		static void Main(string[] args)
		{
			try
			{
				Thread t = new Thread(Run);
				t.Start();

				Thread.Sleep(3000);
				t.Interrupt(); //Wirft ThreadInterruptedException
				//t.Abort(); Deprecated
			}
			catch (ThreadInterruptedException)
			{
				//Funktioniert hier nicht
			}
		}

		static void Run()
		{
			try
			{
				for (int i = 0; i < 100; i++) //100*100 = 10000ms = 10s
				{
					Console.WriteLine(i);
					Thread.Sleep(100);
				}
			}
			catch (ThreadInterruptedException)
			{
				Console.WriteLine("Thread wurde interrupted");
			}
		}
	}
}

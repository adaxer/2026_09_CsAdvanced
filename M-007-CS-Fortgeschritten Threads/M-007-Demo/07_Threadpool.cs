namespace Multithreading
{
	internal class _07_Threadpool
	{
		static void Main(string[] args)
		{
			new Thread(() => Thread.Sleep(500)).Start(); //Vordergrundthread (hält Programm auf bis er fertig ist)
			ThreadPool.QueueUserWorkItem(Methode1); //Hintergrundthread (wird abgebrochen wenn alle Vordergrundthreads zu Ende)
			ThreadPool.QueueUserWorkItem(Methode2);
			ThreadPool.QueueUserWorkItem(Methode3);
			Thread.Sleep(100);
			//Hintergrundthreads abbrechen
		}

		public static void Methode1(object o)
		{
			for (int i = 0; i < 100; i++)
			{
				Thread.Sleep(25);
				Console.WriteLine("Methode1");
			}
		}

		public static void Methode2(object o)
		{
			for (int i = 0; i < 100; i++)
			{
				Thread.Sleep(25);
				Console.WriteLine("Methode2");
			}
		}

		public static void Methode3(object o)
		{
			for (int i = 0; i < 100; i++)
			{
				Thread.Sleep(25);
				Console.WriteLine("Methode3");
			}
		}
	}
}

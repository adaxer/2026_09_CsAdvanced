namespace Multithreading
{
	internal class _08_Lock
	{
		static int Zahl { get; set; } = 0;

		static object LockObject = new object();

		static void Main(string[] args)
		{
			for (int i = 0; i < 500; i++)
			{
				new Thread(() => ZahlPlus1()).Start();
			}
		}

		public static void ZahlPlus1()
		{
			for (int i = 0; i < 100; i++)
			{
				lock (LockObject) //Zahl sperren damit mehrere Threads nacheinander zugreifen können
				{
					Zahl++;
					Console.WriteLine(Zahl);
				}

				Monitor.Enter(LockObject); //Monitor und lock haben den selben Code, genau das gleiche wie oben
				Zahl++;
				Console.WriteLine(Zahl);
				Monitor.Exit(LockObject);
			}
		}
	}
}

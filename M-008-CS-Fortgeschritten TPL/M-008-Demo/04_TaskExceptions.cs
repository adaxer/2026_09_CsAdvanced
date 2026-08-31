namespace Multitasking
{
	internal class _04_TaskExceptions
	{
		static void Main(string[] args)
		{
			Task t1, t2, t3, t4;
			try
			{
				t1 = Task.Run(Exception1);
				t2 = Task.Run(Exception2);
				t3 = Task.Run(Exception3);
				t4 = Task.Run(KeineException);

				Task.WaitAll(t1, t2, t3, t4);

				if (t1.IsFaulted) { } //Wenn eine unhandled Exception aufgetreten ist

				if (t2.IsCanceled) { } //Canceled mit Token

				if (t3.IsCompleted) { } //Wenn Task fertig (erfolgreich oder nicht)

				if (t4.IsCompletedSuccessfully) { } //Wenn Task erfolgreich
			}
			catch (AggregateException ex) //Sammelt alle Exceptions
			{
				foreach (Exception e in ex.InnerExceptions) //Alle Exceptions die aufgetreten sind
					Console.WriteLine(e.Message);
			}
		}

		public static void Exception1()
		{
			Thread.Sleep(1000);
			throw new DivideByZeroException();
		}

		public static void Exception2()
		{
			Thread.Sleep(2000);
			throw new StackOverflowException();
		}

		public static void Exception3()
		{
			Thread.Sleep(3000);
			throw new OutOfMemoryException();
		}

		public static void KeineException()
		{
			Console.WriteLine("Alles OK");
		}
	}
}

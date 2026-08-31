namespace Multitasking
{
	internal class _01_TaskStart
	{
		static void Main(string[] args)
		{
			Task t = new Task(DoSomething); //Genau wie bei Thread
			t.Start();

			Task t2 = Task.Run(DoSomething); //t.Start() automatisch dabei

			Task t3 = Task.Factory.StartNew(DoSomething); //Genau gleicher Code wie Task.Run

			t2.Wait(); //t.Join in Tasks (warten bis Task fertig ist)

			for (int i = 0; i < 100; i++)
				Console.WriteLine($"Main Thread {i}");

			Console.ReadKey();
		}

		static void DoSomething()
		{
			for (int i = 0; i < 100; i++)
				Console.WriteLine($"Task {i}");
		}
	}
}
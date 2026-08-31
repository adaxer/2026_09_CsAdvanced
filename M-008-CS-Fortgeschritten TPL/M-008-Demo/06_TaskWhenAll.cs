namespace Multitasking
{
	internal class _06_TaskWhenAll
	{
		static void Main(string[] args)
		{
			List<Task<int>> tasks = new();
			for (int i = 0; i < 1000; i++)
				tasks.Add(Task.Run(() =>
				{
					return Square(i);
				}));

			Task<int[]> ergebnisse = Task.WhenAll(tasks); //WaitAll mit Ergebnis als Array
			int[] ergebnis = ergebnisse.Result; //Ergebnis aus Task rausholen
		}

		static int Square(int x)
		{
			return x * x;
		}
	}
}

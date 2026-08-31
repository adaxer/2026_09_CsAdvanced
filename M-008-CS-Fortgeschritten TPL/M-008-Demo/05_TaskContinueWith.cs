namespace Multitasking
{
	internal class _05_TaskContinueWith
	{
		static void Main(string[] args)
		{
			Task<int> t = Task.Run(() =>
			{
				return 0;
			});

			t.ContinueWith(task => Folgetask(task.Result)); //Tasks verketten, weitermachen wenn originaler Task fertig
			t.ContinueWith(task => Fehlertask(), TaskContinuationOptions.OnlyOnFaulted); //Verzweigungen
			t.ContinueWith(task => Erfolgstask(), TaskContinuationOptions.OnlyOnRanToCompletion);
		}

		static void Folgetask(int x)
		{

		}

		static void Fehlertask()
		{

		}

		static void Erfolgstask()
		{

		}
	}
}

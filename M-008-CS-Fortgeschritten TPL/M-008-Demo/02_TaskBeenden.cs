using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multitasking
{
	internal class _02_TaskBeenden
	{
		static void Main(string[] args)
		{
			CancellationTokenSource cts = new CancellationTokenSource();
			CancellationToken ct = cts.Token;

			Task t = new Task(Print, ct); //Hier Token direkt als Parameter übergeben
			t.Start();

			Thread.Sleep(500);

			cts.Cancel(); //auf der Source canceln

			Console.ReadKey();
		}

		static void Print(object token)
		{
			if (token is CancellationToken ct)
			{
				for (int i = 0; i < 100; i++)
				{
					if (ct.IsCancellationRequested)
						ct.ThrowIfCancellationRequested(); //Task wirft Exception aber ist nicht sichtbar

					Console.WriteLine($"Task {i}");
					Thread.Sleep(50);
				}
			}
		}
	}
}

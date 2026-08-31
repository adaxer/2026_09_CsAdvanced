using System.Diagnostics;

namespace Multitasking
{
	internal class ParallelDemo
	{
		static void Main(string[] args)
		{
			int[] durchgänge = { 1_000, 10_000, 50_000, 100_000, 250_000, 500_000, 1_000_000, 5_000_000, 10_000_000, 100_000_000 };
			for (int i = 0; i < durchgänge.Length; i++)
			{
				Stopwatch sw = Stopwatch.StartNew();
				RegularFor(durchgänge[i]);
				sw.Stop();
				Console.WriteLine($"For {durchgänge[i]}: {sw.ElapsedMilliseconds}");

				Stopwatch sw2 = Stopwatch.StartNew();
				ParallelFor(durchgänge[i]);
				sw2.Stop();
				Console.WriteLine($"Parallel For {durchgänge[i]}: {sw2.ElapsedMilliseconds}");
			}

			/*  For 1000: 1
				Parallel For 1000: 220
				For 10000: 5
				Parallel For 10000: 37
				For 50000: 31
				Parallel For 50000: 19
				For 100000: 61
				Parallel For 100000: 26
				For 250000: 193
				Parallel For 250000: 74
				For 500000: 306
				Parallel For 500000: 88
				For 1000000: 613
				Parallel For 1000000: 262
				For 5000000: 3652
				Parallel For 5000000: 896
				For 10000000: 2977
				Parallel For 10000000: 844
				For 100000000: 26637
				Parallel For 100000000: 13675
			 */
		}

		static void RegularFor(int iterations)
		{
			double[] erg = new double[iterations];
			for (int i = 0; i < iterations; i++)
			{
				erg[i] = (Math.Pow(i, 0.333333333333) * Math.Sin(i + 2) / Math.Exp(i) + Math.Log(i + 1)) * Math.Sqrt(i + 100);
			}
		}

		static void ParallelFor(int iterations)
		{
			double[] erg = new double[iterations];
			Parallel.For(0, iterations, i =>
			{
				erg[i] = (Math.Pow(i, 0.333333333333) * Math.Sin(i + 2) / Math.Exp(i) + Math.Log(i + 1)) * Math.Sqrt(i + 100);
			});
		}
	}
}

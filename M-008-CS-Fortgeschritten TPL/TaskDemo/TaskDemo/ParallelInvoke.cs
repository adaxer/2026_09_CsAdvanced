using System.Diagnostics;

namespace Multitasking;

internal class ParallelInvoke
{
	static string[] Words;

	internal static void Main(string[] args)
	{
		Words = GetWordArray(@"http://www.gutenberg.org/files/54700/54700-0.txt");
        GetLongestWord();
        GetMostCommonWords();
        GetCountForWord("sleep"); //Tasks nacheinander starten



        var watch = Stopwatch.StartNew();
        Parallel.Invoke(GetLongestWord, GetMostCommonWords, () => GetCountForWord("sleep")); //Mehrere Tasks gleichzeitig starten
        watch.Stop();
        Console.WriteLine($"Elapsed Time for ParallelInvoke: {watch.ElapsedMilliseconds} ms");

		watch.Restart();
        GetLongestWord();
        GetMostCommonWords();
        GetCountForWord("sleep"); //Tasks nacheinander starten
        watch.Stop();
        Console.WriteLine($"Elapsed Time for Sequential Execution: {watch.ElapsedMilliseconds} ms");
        Console.ReadKey();
	}

	static void GetLongestWord()
	{
		Console.WriteLine(Words.OrderByDescending(e => e.Length).First());
	}

	static void GetCountForWord(string word)
	{
		Console.WriteLine(Words.Count(e => e.Equals(word, StringComparison.OrdinalIgnoreCase)));
	}

	static void GetMostCommonWords()
	{
		Console.WriteLine
		(
			Words
			.Where(e => e.Length >= 7)
			.GroupBy(e => e.ToLower())
			.ToDictionary(e => e.Key, e => e.Count())
			.OrderByDescending(e => e.Value)
			.Take(10)
			.Aggregate("", (agg, kv) => agg + $"{kv.Key}: {kv.Value}\n")
		);
	}

	static string[] GetWordArray(string url)
	{
		HttpClient client = new HttpClient();
		string full = client.GetStringAsync(url).Result;
		return full.Split(new char[] { ' ', '\n', ',', '.', ';', ':', '-', '_', '/' }, StringSplitOptions.RemoveEmptyEntries);
	}
}

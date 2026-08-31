namespace Multitasking;

internal class ParallelInvoke
{
	static string[] Words;

	static void Main(string[] args)
	{
		Words = GetWordArray(@"http://www.gutenberg.org/files/54700/54700-0.txt");

		Parallel.Invoke(GetLongestWord, GetMostCommonWords, () => GetCountForWord("sleep")); //Mehrere Tasks gleichzeitig starten

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

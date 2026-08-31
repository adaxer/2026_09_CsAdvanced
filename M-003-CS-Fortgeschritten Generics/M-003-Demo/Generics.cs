using System.Collections;

namespace Generics
{
	internal class Program
	{
		static void Main(string[] args)
		{
			List<string> list = new(); //Generic: T wird nach unten übernommen
			list.Add("Max"); //Zum Beispiel in Add(T) -> Add(string)

			Dictionary<string, string> dictionary = new Dictionary<string, string>(); //Hier 2 Generics: TKey, TValue
			dictionary.Add("Max", "Mustermann");

			foreach (KeyValuePair<string, string> kv in dictionary) { }

			DataStore<string> ds = new DataStore<string>();
			ds.Add(1, "Test");
			ds.DisplayDefault<long>();
		}
	}

	public class DataStore<T> : IEnumerable<int>, IProgress<T> //int oder T nach unten weitergeben
	{
		private T[] data = new T[10]; //Array vom Typ T

		public List<T> Data => data.ToList(); //Generic weitergeben in List

		public void Add(int index, T item) //T mit Parameter
		{
			data[index] = item;
		}

		public T GetIndex(int index)
		{
			if (index < 0 || index >= data.Length)
				return default(T); //default(T): Standardwert von T zurückgeben
			return data[index];
		}

		public void DisplayDefault<MyType>() where MyType : struct //Weiteres Generic, bei Methode constrained
		{
			Console.WriteLine(default(MyType));
		}

		public IEnumerator<int> GetEnumerator() //int von Vererbung
		{
			throw new NotImplementedException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		public void Report(T value) //T weitergeben von Progress<T>
		{
			throw new NotImplementedException();
		}
	}

	public class DataStore2<T> : DataStore<T>
	{

	}
}
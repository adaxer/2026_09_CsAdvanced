namespace M009_Linq;

public static class ExtensionMethods //Klasse muss statisch sein
{
	public static int Quersumme(this int zahl) //mit this angeben auf welchen Typen sich diese Methode bezieht
	{
		return zahl.ToString().ToCharArray().Sum(e => (int) char.GetNumericValue(e));
	}
	
	public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> list) //Erweiterungsmethode mit Generic, wird hier verwendet wie eine normale Linq-Methode
	{
		return list.OrderBy(e => Random.Shared.Next());
	}
}

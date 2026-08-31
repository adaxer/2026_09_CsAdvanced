namespace Generics;

internal class GenericConstraints
{
	static void Main(string[] args)
	{
		//DataStore4<TestClass> ds4; //Nicht möglich
	}

	public class DataStore1<T> where T : class { } //Referenztyp erzwingen

	public class DataStore2<T> where T : struct { } //Wertetyp erzwingen

	public class DataStore3<T> where T : GenericConstraints { } //Bestimmte Vererbungshierarchie

	public class DataStore4<T> where T : new() { } //Nur Typen die einen Default Konstruktor haben

	public class DataStore5<T> where T : Enum { } //Nur Enum Werte

	public class DataStore6<T> where T : Delegate { } //Nur Delegate

	public class DataStore7<T> where T : unmanaged { } //Bestimmte Typen, https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/unmanaged-types

	public class DataStore8<T1, T2> //Mehrere Constraints auf mehrere Generics
		where T1 : class, new() //Mehrere Constraints auf T1
		where T2 : struct
	{

	}
}

public class TestClass
{
	public TestClass(int i) { } //Default Konstruktor entfernt durch einen Konstruktor mit Parameter
}

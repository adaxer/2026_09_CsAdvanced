namespace DelegatesEvents;

internal class ActionFuncPredicate
{
	static void Main(string[] args)
	{
		Action<int, int> action = Addiere; //Action: Methode mit void und bis zu 16 Parametern
		action += Subtrahiere; //Methode dranhängen wie bei Delegate
		action(3, 5); //Aufruf ohne null-check
		action?.Invoke(4, 6); //Aufruf mit null-check

		DoSomething(4, 2, Addiere); //Methode mit Action Parameter
		DoSomething(4, 2, Subtrahiere);

		Predicate<int> predicate = CheckForZero; //Predicate: Methode mit bool als return Wert und genau einem Parameter
		predicate += CheckForOne;
		bool b = predicate(0);
		bool? b2 = predicate?.Invoke(0); //nullable boolean, da Invoke null zurück geben könnte

		DoSomething(0, CheckForZero);
		DoSomething(0, CheckForOne);

		Func<int, int, double> func = Multipliziere; //Func: Methode mit Rückgabewert, letzter Generic ist Rückgabetyp (double)
		func += Dividiere;
		double? ergebnis = func?.Invoke(4, 5); //Rückgabewert der letzten Funktion genommen

		DoSomething(4, 5, Multipliziere);
		DoSomething(4, 5, Dividiere);

		func += delegate (int x, int y) //Anonyme Methode
		{ 
			return x * y;
		};

		func += (int x, int y) => { return x * y; };
		func += (x, y) => { return x * y; };
		func += (x, y) => x * y; //kürzeste Form

		DoSomething(3, 5, (zahl1, zahl2) => Console.WriteLine(zahl1 * zahl2)); //Anonyme Methoden direkt als Parameter
		DoSomething(3, (zahl1) => zahl1 != 0);
		DoSomething(3, 5, (zahl1, zahl2) => zahl1 * zahl2);
	}

	static void Addiere(int x, int y) => Console.WriteLine(x + y);

	static void Subtrahiere(int x, int y) => Console.WriteLine(x - y);

	static bool CheckForZero(int x) => x == 0;

	static bool CheckForOne(int x) => x == 1;

	static double Multipliziere(int x, int y) => x * y;

	static double Dividiere(int x, int y) => (double) x / y;

	//Methoden mit Delegate
	static void DoSomething(int x, int y, Action<int, int> action) => action(x, y);

	static void DoSomething(int x, Predicate<int> predicate) => predicate(x);

	static void DoSomething(int x, int y, Func<int, int, double> func) => func(x, y);
}

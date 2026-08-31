using System.Diagnostics;
using System.Text;

public class Program
{
	async static Task Main(string[] args)
	{
		Stopwatch sw = Stopwatch.StartNew();
		ToastToasten();
		GeschirrHerrichten();
		KaffeeZubereiten();
		sw.Stop(); //8s
		Console.WriteLine(sw.ElapsedMilliseconds);

		Stopwatch swAsync = Stopwatch.StartNew();
		ToastToastenAsync(); //Async void Methoden
		GeschirrHerrichtenAsync();
		KaffeeZubereitenAsync();
		swAsync.Stop();
		Console.WriteLine(swAsync.ElapsedMilliseconds); //Die Methoden sind im Hintergrund, Stopwatch im Main Thread
		Console.ReadKey();

		Stopwatch swAsyncAwait = Stopwatch.StartNew();
		Task<Toast> toast = ToastToastenTaskAsync(); //Start: 4 Sekunden
		Task<Tasse> geschirr = GeschirrHerrichtenTaskAsync(); //Start: 2 Sekunden
		Task<Kaffee> kaffee = KaffeeZubereitenTaskAsync(await geschirr); //Warte auf Geschirr, danach 2 Sekunden
		Toast t = await toast; //await um auf Task zu warten
		Kaffee k = await kaffee;
		swAsyncAwait.Stop(); //4s
		Console.WriteLine(swAsyncAwait.ElapsedMilliseconds);
	}

	static void ToastToasten()
	{
		Thread.Sleep(4000);
		Console.WriteLine("Toast fertig");
	}

	static void GeschirrHerrichten()
	{
		Thread.Sleep(2000);
		Console.WriteLine("Besteck und Geschirr hergerichtet");
	}

	static void KaffeeZubereiten()
	{
		Thread.Sleep(2000);
		Console.WriteLine("Kaffee zubereitet");
	}

	static async void ToastToastenAsync()
	{
		await Task.Delay(4000);
		Console.WriteLine("Toast fertig");
	}

	async static void GeschirrHerrichtenAsync()
	{
		await Task.Delay(2000);
		Console.WriteLine("Besteck und Geschirr hergerichtet");
	}

	static async void KaffeeZubereitenAsync()
	{
		await Task.Delay(2000);
		Console.WriteLine("Kaffee zubereitet");
	}

	static async Task<Toast> ToastToastenTaskAsync()
	{
		await Task.Delay(4000);
		Console.WriteLine("Toast fertig");
		return new Toast();
	}

	async static Task<Tasse> GeschirrHerrichtenTaskAsync()
	{
		await Task.Delay(2000);
		Console.WriteLine("Besteck und Geschirr hergerichtet");
		return new Tasse();
	}

	static async Task<Kaffee> KaffeeZubereitenTaskAsync(Tasse tasse)
	{
		await Task.Delay(2000);
		Console.WriteLine("Kaffee zubereitet");
		return new Kaffee();
	}
}

public class Toast { }

public class Tasse { }

public class Kaffee { }
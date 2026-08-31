namespace DelegateDemo;

internal class Program
{
    public delegate string MyDelegate(int value);


    static void Main(string[] args)
    {
        // Alles aus Kap 05
        EventSource.MyEventAsAction = (sender, e) => Console.WriteLine("MyEventAsAction triggered");

        EventHandler writeMessage = (sender, e) => Console.WriteLine("MyEventAsAction triggered again");
        EventSource.MyEventAsAction += writeMessage;

        EventSource.MyEventAsAction.Invoke(null, EventArgs.Empty);
        EventSource.MyEventAsAction -= writeMessage;

        EventSource.MyEventAsAction(null, EventArgs.Empty);

        // EventSource.MyEventAsEvent = (sender, e) => Console.WriteLine("MyEventAsEvent triggered"); // geht nicht, da MyEventAsEvent ein Event ist und nicht direkt zugewiesen werden kann
        EventSource.MyEventAsEvent += (sender, e) => Console.WriteLine("MyEventAsEvent triggered");
        // EventSource.MyEventAsEvent.Invoke(null, EventArgs.Empty); // geht nicht, da MyEventAsEvent ein Event ist und nicht direkt aufgerufen werden kann

        EventSource.MyActionAsEvent += () => Console.WriteLine("MyActionAsEvent triggered");

        EventSource.TriggerEvents();

        List<int> numbers = Enumerable.Range(0, 100).ToList();

        ForEach<int>(numbers, i => Console.WriteLine(i));
        var resultList = ForEachResult<int, bool>(numbers, i => i % 2 == 0);

        Console.WriteLine(resultList.Where(b => true).Count());
        Console.WriteLine(resultList.Where(b => false).Count());

        Func<int, string> HalfIt = i =>
        {
            string result = $"Die Hälfte von {i} ist: {(i / 2)}";
            Console.WriteLine(result);
            return result;
        };
        MyDelegate del = PrintInt;
        del += i => (2 * i).ToString();
        del += new MyDelegate(HalfIt);
        del(43);

        del = PrintInt;
        del(44);
    }

    static void ForEach<T>(List<T> collection, Action<T> callBack)
    {
        for (int i = 0; i < collection.Count; i++)
        {
            callBack(collection[i]);
        }
    }

    static List<R> ForEachResult<T, R>(List<T> collection, Func<T, R> callBack)
    {
        List<R> results = new List<R>();
        for (int i = 0; i < collection.Count; i++)
        {
            var value = callBack(collection[i]);
        }
        return results;
    }

    private static string PrintInt(int value)
    {
        string result = $"Die Antwort ist: {value}";
        Console.WriteLine(result);
        return result;
    }


}

public static class EventSource
{
    public static EventHandler MyEventAsAction = (sender, e) => { };

    // Aus .Net-History
    public static event EventHandler MyEventAsEvent;

    public static event Action MyActionAsEvent;

    internal static void TriggerEvents()
    {
        MyEventAsAction(null, EventArgs.Empty);
        MyEventAsEvent?.Invoke(null, EventArgs.Empty);
        MyActionAsEvent?.Invoke();
    }
}


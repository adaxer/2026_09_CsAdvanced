namespace DelegateDemo;

internal class Program
{
    public delegate string MyDelegate(int value);

    static void Main(string[] args)
    {
        Func<int, string> HalfIt = i =>
        {
            string result = $"Die Hälfte von {i} ist: {(i / 2)}";
            Console.WriteLine(result);
            return result;
        };
        MyDelegate del = PrintInt;
        del += i=> (2*i).ToString();
        del += new MyDelegate(HalfIt);
        del(43);

        del = PrintInt;
        del(44);
    }

    private static string PrintInt(int value)
    {
        string result = $"Die Antwort ist: {value}";
        Console.WriteLine(result);
        return result;
    }


}

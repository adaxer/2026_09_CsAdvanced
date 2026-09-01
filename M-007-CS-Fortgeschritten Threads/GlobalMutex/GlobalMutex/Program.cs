namespace GlobalMutex;

internal class Program
{
    static void Main(string[] args)
    {
        Mutex mutex = new Mutex(false, "MeinMutex");
        for (int i = 0; i < 100; i++)
        {
            mutex.WaitOne();
            Thread.Sleep(300);
            Console.WriteLine(i);
            mutex.ReleaseMutex();
        }
        Console.ReadKey();

    }

}

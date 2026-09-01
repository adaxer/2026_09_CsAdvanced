namespace ThreadingDemo;

internal class Program
{
    static int externalCounter = 0;
    private static Thread t1;
    private static Thread t2;

    static void Main(string[] args)
    {
        t1 = new Thread(Count);
        t1.IsBackground = true;
        t1.Start();

        t2 = new Thread(Count);
        t2.Start();
        Console.ReadLine();
    }

    private static void Count(object? obj)
    {
        int internalCounter = 0;
        for (int i = 0; i < 1000000; i++)
        {
            lock (typeof(Program))
            {
                internalCounter++;
                externalCounter++;
                Console.WriteLine($"Id: {Thread.CurrentThread.ManagedThreadId}, Int: {internalCounter}, Ext: {externalCounter}");
            }
            Thread.Sleep(100);
            var otherThread = Thread.CurrentThread == t1 ? t2 : t1;
            otherThread.Join();
        }
    }
}

namespace TaskDemo;

internal class Program
{
    static void Main(string[] args)
    {
        //Multitasking.ParallelInvoke.Main(args);
        //Multitasking.ParallelDemo.Main(args);
        Action callback=() => Console.WriteLine("Task completed");
        var task = Task.Run(() => Thread.Sleep(10000));
        task.ContinueWith(t => Task.Run(callback));
        Console.WriteLine("Waiting");
        Console.WriteLine("Waiting");
        Console.WriteLine("Waiting");
        Console.WriteLine("Waiting");
        Console.ReadLine();
    }
}

using Microsoft.EntityFrameworkCore;

namespace PartialsDemo;

internal class Program
{
    static void Main(string[] args)
    {
        var context = new SomeContext(new DbContextOptions<SomeContext>());
        Console.WriteLine("Hello, World!");
    }
}

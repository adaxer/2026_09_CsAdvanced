using System;

namespace AClassLib
{
    public class Class1
    {
        public void DoSomething(SomeService service)
        {
            // ArgumentNullException.ThrowIfNull(service, nameof(service)); // Kürzer, aber nicht .NetStandard
            Console.WriteLine("Hello from Class1 in AClassLib!");
        }
    }

    public class SomeService
    {
    }
}

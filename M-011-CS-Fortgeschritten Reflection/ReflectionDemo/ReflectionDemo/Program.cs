using M012;
using SolarSystem;
using System.Diagnostics.Tracing;
using System.Reflection;

namespace ReflectionDemo;

internal class Program
{
    static void Main(string[] args)
    {
        var myAssemblies = new List<Assembly> { typeof(CelestialBody).Assembly, typeof(Person).Assembly };
        var allAssemblies = AppDomain.CurrentDomain.GetAssemblies();
        myAssemblies.Add(Assembly.LoadFile(Path.Combine(Directory.GetCurrentDirectory(), "DelegateDemo.dll")));

#if DEBUG_SPECIAL
        var allTypes = myAssemblies.SelectMany(a => a.GetTypes()).ToList();

        //Type instType = Type.GetType("SolarSystem.CelestialBodyType");
        //instType = Type.GetType(nameof(CelestialBodyType));

        var celestialBodyType = allTypes.Single(t => t.Name == "CelestialBody");//typeof(CelestialBody);
        var props = celestialBodyType.GetProperties();
        var methods = celestialBodyType.GetMethods();
        var ctors = celestialBodyType.GetConstructors();
        var parameters = ctors.FirstOrDefault()!.GetParameters();

#endif

        var pluto = Activator.CreateInstance(celestialBodyType, "Pluto", CelestialBodyType.Planet);
        string plutosName = (string)celestialBodyType.GetProperty("Name").GetValue(pluto);
        plutosName = (string)celestialBodyType.InvokeMember("Name", BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty, null, pluto, new object[] { })!;

        var eventSourceType = allTypes.Single(t => t.Name == "EventSource");

        EventHandler handler = (sender, e) => Console.WriteLine("Event!");

        eventSourceType.InvokeMember(
            "add_MyEventAsEvent",
            BindingFlags.InvokeMethod |
            BindingFlags.Static |
            BindingFlags.Public,
            binder: null,
            target: null,
            args: new object[] { handler });

        eventSourceType.InvokeMember("TriggerEvents", BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.NonPublic, null, null, null);
        

    }
}

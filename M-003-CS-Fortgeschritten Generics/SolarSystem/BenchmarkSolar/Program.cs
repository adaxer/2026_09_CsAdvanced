using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using SolarSystem;

namespace BenchmarkSolar;

// See https://benchmarkdotnet.org/articles/guides/getting-started.html

internal class Program
{
    static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<SolarBenchmarks>();
    }
}

public class SolarBenchmarks
{
    [Benchmark]
    public Node<CelestialBody> CreateSolarSystemStatic()
    {
        Node<CelestialBody>.UseDynamic = false;
        var system = SolarSystem.Program.CreateSolarSystem();
        return system;
    }

    [Benchmark]
    public Node<CelestialBody> CreateSolarSystemDynamic()
    {
        Node<CelestialBody>.UseDynamic = true;
        var system = SolarSystem.Program.CreateSolarSystem();
        return system;
    }

}
using Microsoft.EntityFrameworkCore;

namespace PartialsDemo;

public partial class SomeContext
{
    private string _importantValue;

    partial void OnConstructor()
    {
        _importantValue = "Wichtiger Wert";
    }

    public void SeedData()
    {
        // Testdaten initialisieren
    }

    partial string SomeProperty { get => _importantValue; set => _importantValue = value; }

}

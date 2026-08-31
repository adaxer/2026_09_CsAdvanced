namespace Varianz;

record class Animal(string Name) { }
record class Dog : Animal
{
    public Dog(string name) : base(name) { }

    public void Bark()
    {
        Console.WriteLine($"{Name} barks!");
    }
}

class Program
{
    static void Main()
    {
        // Kovarianz: Dog → Animal
        IEnumerable<Dog> dogs = new List<Dog> { new Dog("Bello") };
        foreach (Dog dog in dogs)
        {
            dog.Bark();
        }

        IEnumerable<Animal> animals = dogs;
        foreach (var animal in animals)
        {
            Console.WriteLine(animal.Name);
        }
        //dogs = animals;

        // Kontravarianz: Animal → Dog
        Action<Animal> handleAnimal = a => Console.WriteLine(a.Name);
        Action<Dog> handleDog = handleAnimal;

        //handleAnimal = handleDog;

        handleDog(new Dog("Fiffi"));

        Console.ReadLine();
    }
}
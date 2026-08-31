record class Animal(string Name) { }
record class Dog : Animal {
    public Dog(string name) : base(name) { }
}

class Program
{
    static void Main()
    {
        // Kovarianz: Dog → Animal
        IEnumerable<Dog> dogs = new List<Dog>{ new Dog("Bello")};
        IEnumerable<Animal> animals = dogs;
        foreach(var animal in animals)
        {
            Console.WriteLine(animal.Name);
        }

        // Kontravarianz: Animal → Dog
        Action<Animal> handleAnimal = a => Console.WriteLine(a.Name);
        Action<Dog> handleDog = handleAnimal;

        handleDog(new Dog("Fiffi"));

        Console.ReadLine();
    }
}
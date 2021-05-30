using System;
using FunConsole.CarDataModel;
using FunConsole.Extensions;
using LanguageExt;
using static System.Console;

using static LanguageExt.Prelude;

namespace FunConsole.FunctionalAreas
{
    public static class LazyComputation
    {
        public static void Run()
        {
            // Lazy Computation - OrElse
            var volvoFactory = new CarFactory("Volvo");
            var toyotaFactory = new CarFactory("Toyota");
            volvoFactory.ProduceCars(2, "red");
            toyotaFactory.ProduceCars(2, "blue");

            volvoFactory.BuyCar("red")
                .OrElse(toyotaFactory.BuyCar("blue")) // Not lazy - both will be called - two cars bought
                .Match(
                    newCar => WriteLine($"Bought {newCar.Brand} with color {newCar.Color}"),
                    () => WriteLine("No matching car"));

            WriteLine($"Volvo factory have {volvoFactory.Cars.Count} red cars for sale");
            WriteLine($"Toyota factory have {volvoFactory.Cars.Count} blue cars for sale");

            // Lazy Computation - GetOrElse
            volvoFactory.BuyCar("red")
                .OrElse(() => toyotaFactory.BuyCar("blue")) // () => Lazy, the second will not be evaluated - only one car bought
                .Match(
                    newCar => WriteLine($"Bought {newCar.Brand} with color {newCar.Color}"),
                    () => WriteLine("No matching car"));
            
            WriteLine($"Volvo factory have {volvoFactory.Cars.Count} red cars for sale");
            WriteLine($"Toyota factory have {toyotaFactory.Cars.Count} blue cars for sale");

            WriteLine("GetOrElse(defaultValue): Try to buy new blue car or else I will paint my old car blue");
            var oldCar = new Car("Volvo", "red", "old red car");
            var car = volvoFactory.BuyCar("blue")
                .GetOrElse(() => oldCar with {Color = "blue", Description = "old car painted blue"});
            WriteLine("Result: " + car.Description);

            Option<Car> optCar = None;

            // Lazy Computation - Map
            Func<Car> lazyRedCar = () => new Car("Volvo", "Red");
            Func<Car, Car> turnBlue = c => c with {Color = "Blue"};
            Func<Car> lazyBlueCar = lazyRedCar.Map(turnBlue);
            Out.WriteLine(lazyBlueCar());
        }
    }
}
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
            var volvoFactory = new CarFactory(Brand.Volvo);
            var toyotaFactory = new CarFactory(Brand.Volvo);
            volvoFactory.ProduceCars(2, Color.Red);
            toyotaFactory.ProduceCars(2, Color.Red);

            volvoFactory.BuyCar(Color.Red)
                .OrElse(toyotaFactory.BuyCar(Color.Blue)) // Not lazy - both will be called - two cars bought
                .Match(
                    newCar => WriteLine($"Bought {newCar.Brand} with color {newCar.Color}"),
                    () => WriteLine("No matching car"));

            WriteLine($"Volvo factory have {volvoFactory.Cars.Count} red cars for sale");
            WriteLine($"Toyota factory have {volvoFactory.Cars.Count} blue cars for sale");

            // Lazy Computation - GetOrElse
            volvoFactory.BuyCar(Color.Red)
                .OrElse(() => toyotaFactory.BuyCar(Color.Blue)) // () => Lazy, the second will not be evaluated - only one car bought
                .Match(
                    newCar => WriteLine($"Bought {newCar.Brand} with color {newCar.Color}"),
                    () => WriteLine("No matching car"));
            
            WriteLine($"Volvo factory have {volvoFactory.Cars.Count} red cars for sale");
            WriteLine($"Toyota factory have {toyotaFactory.Cars.Count} blue cars for sale");

            WriteLine("GetOrElse(defaultValue): Try to buy new blue car or else I will paint my old car blue");
            var oldCar = new Car(Brand.Volvo, Color.Red, 2021, 4000, "old car");
            var car = volvoFactory.BuyCar(Color.Blue)
                .GetOrElse(() => oldCar with {Color = Color.Blue, Description = "old car painted blue"});
            WriteLine("Result: " + car.Description);

            Option<Car> optCar = None;

            // Lazy Computation - Map
            Func<Car> lazyRedCar = () => new Car(Brand.Volvo, Color.Red, 2021, 4000);
            Func<Car, Car> turnBlue = c => c with {Color = Color.Blue};
            Func<Car> lazyBlueCar = lazyRedCar.Map(turnBlue);
            Out.WriteLine(lazyBlueCar());
        }
    }
}
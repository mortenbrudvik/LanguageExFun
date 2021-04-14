using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Security.Cryptography.X509Certificates;
using DomainModel;
using LanguageExt;
using LanguageExt.ClassInstances;
using LanguageExt.Common;
using Microsoft.FSharp.Collections;
using static System.Console;
using static System.Decimal;
using static System.Linq.Enumerable;
using static LanguageExt.Prelude;
using Error = LanguageExt.Common.Error;

namespace FunConsole
{
    /// <summary>
    /// https://github.com/louthy/language-ext
    /// </summary>
    class Program
    {
        private delegate int ExampleDelegate(int left, int right);
        
        static void Main(string[] args)
        {
            PartialApplicationAndCurrying.Run();

            MultiArgumentFunctions.Run();

            DataFunctionality.Run();
            
            ValidationFun.Run();
            
            // Lazy Computation
            var volvoFactory = new CarFactory("Volvo");
            var toyotaFactory = new CarFactory("Toyota");
            volvoFactory.ProduceCars(2, "red");
            toyotaFactory.ProduceCars(2, "blue");

            volvoFactory.BuyCar("red")
                .OrElse(toyotaFactory.BuyCar("blue")) // Not lazy - both will be called - two cars bought
                .Match(
                    car => WriteLine($"Bought {car.Brand} with color {car.Color}"),
                    () => WriteLine("No matching car"));

            WriteLine($"Volvo factory have {volvoFactory.Cars.Count} red cars for sale");
            WriteLine($"Toyota factory have {volvoFactory.Cars.Count} blue cars for sale");

            volvoFactory.BuyCar("red")
                .OrElse(() => toyotaFactory.BuyCar("blue")) // () => Lazy, the second will not be evaluated - only one car bought
                .Match(
                    car => WriteLine($"Bought {car.Brand} with color {car.Color}"),
                    () => WriteLine("No matching car"));
            
            WriteLine($"Volvo factory have {volvoFactory.Cars.Count} red cars for sale");
            WriteLine($"Toyota factory have {toyotaFactory.Cars.Count} blue cars for sale");

            ReadLine();
        }

        private static Option<Car> NoCar => None;
        private static Option<Car> VolvoRed => Some(new Car("Volvo", "red"));
    }
    
    public static class OptionExt
    {
        public static Option<T> OrElse<T>(this Option<T> left, Option<T> right)
            => left.Match( 
                () => right,
                _ => left);

        public static Option<T> OrElse<T>(this Option<T> left, Func<Option<T>> right)
            => left.Match(
                right, 
                _ => left);
    }

    internal record Car(string Brand, string Color);

    internal class CarFactory
    {
        private readonly string _brand;
        readonly List<Car> _cars = new();

        public CarFactory(string brand) => _brand = brand;
        public IReadOnlyCollection<Car> Cars => _cars;

        public void ProduceCars(int number, string withColor)
        {
            _cars.AddRange(Repeat(_brand, number)
                .Select(brand => new Car(brand, withColor)));
        }

        public Option<Car> BuyCar(string withColor)
            => _cars.Find(x => x.Color == withColor) switch
            {
                { } car => _cars.Remove(car) ? car : None,
                _ => None
            };
    }
}

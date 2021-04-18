using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DomainModel;
using Flurl.Http;
using LanguageExt;
using LanguageExt.ClassInstances;
using LanguageExt.Common;
using Microsoft.FSharp.Collections;
using static System.Console;
using static System.Decimal;
using static System.Linq.Enumerable;
using static LanguageExt.Prelude;
using Error = LanguageExt.Common.Error;
using Newtonsoft.Json.Linq;

namespace FunConsole
{
    /// <summary>
    /// https://github.com/louthy/language-ext
    /// </summary>
    internal  static class Program
    {
        private delegate int ExampleDelegate(int left, int right);

        private static async Task Main(string[] args)
        {
            PartialApplicationAndCurrying.Run();

            MultiArgumentFunctions.Run();

            DataFunctionality.Run();
            
            ValidationFun.Run();
            
            Misc.Run();
 
            
            
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

            // Lazy Computation - Map
            Func<Car> lazyRedCar = () => new Car("Volvo", "Red");
            Func<Car, Car> turnBlue = c => c with {Color = "Blue"};
            Func<Car> lazyBlueCar = lazyRedCar.Map(turnBlue);
            Out.WriteLine(lazyBlueCar());

            // Exception handling
            // "https://api.chucknorris.io/jokes/random"
            Func<string, Try<Uri>> createUri = uri => Try(() => new Uri(uri));
            Func<string, TryAsync<string>> getJsonFromUrl = url => TryAsync<string>(async () => await url.GetJsonAsync());
            Func<Uri, TryAsync<string>> getJsonFromUri = uri => TryAsync<string>(async () => await uri.GetJsonAsync());
            Func<string,string, Try<string>> getJsonValue = (json, valueName) => Try(() => JObject.Parse(json)[valueName].ToString());
            

            
            ReadLine();
        }
    }
    

    public static class FuncExt
    {
        public static Func<R> Map<T, R>
            (this Func<T> f, Func<T, R> g)
            => () => g(f());

        public static Func<R> Bind<T, R>
            (this Func<T> f, Func<T, Func<R>> g)
            => () => g(f())();

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

        public static T GetOrElse<T>(this Option<T> opt, T defaultValue)
            => opt.Match(
                x => x,
                defaultValue);
        
        public static T GetOrElse<T>(this Option<T> opt, Func<T> fallback)
            => opt.Match(
                x => x,
                fallback());
    }

    internal record Car(string Brand, string Color, string Description = "");

    internal class CarFactory
    {
        private readonly string _brand;
        private readonly List<Car> _cars = new();

        public CarFactory(string brand) => _brand = brand;
        public IReadOnlyCollection<Car> Cars => _cars;

        public void ProduceCars(int number, string withColor) =>
            _cars.AddRange(Repeat(_brand, number)
                .Select(brand => new Car(brand, withColor)));
        public Option<Car> BuyCar(string withColor) =>
            _cars.Find(x => x.Color == withColor) switch
            {
                { } car => _cars.Remove(car) ? car : None,
                _ => None
            };
    }
}

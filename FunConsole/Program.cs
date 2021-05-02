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
            LazyComputation.Run();
            

            // Exception handling
            // The Try monad captures exceptions and uses them to cancel the computation.
            // Primarily useful for expression based processing of errors.
            // "https://api.chucknorris.io/jokes/random"
            Func<string, TryAsync<Uri>> createUri = url => TryAsync(Task.Run(() => new Uri(url)));
            Func<Uri, TryAsync<string>> getJsonFromUri = uri => TryAsync(async () => await uri.GetStringAsync());
            Func<string,string, TryAsync<string>> getJsonValue = (json, valueName) => TryAsync(Task.Run(() => "" + JObject.Parse(json)[valueName]));

            await createUri("https://api.chucknorris.io/jokes/random")
                .Bind(x => getJsonFromUri(x))
                .Bind(y => getJsonValue(y, "value"))
                .Match(
                    joke => WriteLine("Chuck Norris says: " + joke),
                    ex => WriteLine("Error: " + ex.Message));
            
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

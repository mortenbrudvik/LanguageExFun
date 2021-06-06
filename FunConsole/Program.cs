using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FunConsole.FunctionalAreas;
using static System.Console;
using FunConsole.Utils;
using String = FunConsole.Utils.String;
using LanguageExt;
using static LanguageExt.Prelude;
using Double = FunConsole.Utils.Double;

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
            await ExceptionHandling.Run();

            await Out.WriteLineAsync("Traverse - Flipping it inside out");
            var result = "1.1,2.3,1.6"
                .Split(',')
                .Map(String.Trim)
                .Map(Double.Parse)
                .Traverse(x => x)
                .Map(Enumerable.Sum)
                .Match(
                    sum => Out.WriteLine("Sum: " + sum),
                    () => Out.WriteLine("Some inputs could not be parsed"));
            
            ReadLine();
        }
    }
    
    static class CurrencyLayer
    {
        public static async Task<double> GetRate(string ccyPair)
        {
            return await Task.FromResult(0.82);
        }
    }
}

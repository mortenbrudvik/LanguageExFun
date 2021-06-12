using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FunConsole.CarDataModel;
using FunConsole.Extensions;
using FunConsole.FunctionalAreas;
using static System.Console;
using FunConsole.Utils;
using String = FunConsole.Utils.String;
using LanguageExt;
using static LanguageExt.Prelude;
using Double = FunConsole.Utils.Double;
using static FunConsole.Fun.TaskUtils;

namespace FunConsole
{
    /// <summary>
    /// https://github.com/louthy/language-ext
    ///
    /// Pending: Seq and Map
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

            var processItems = await Task.Run(() => Process.GetProcesses().ToSeq())
                .Map(processes =>
                    processes.Map(process => process.ProcessName)
                        .Map(name => new ProcessItem(name)));

            var mobileCarDealer = new MobileCarDealer();

            await Out.WriteLineAsync("Cheapest car: " + await mobileCarDealer.Cheapest(2021));
            
            
                
            
            await Out.WriteLineAsync("Traverse - Flipping it inside out");
            "1.1,2.3,1.6"
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

    public record ProcessItem(string Name);
}

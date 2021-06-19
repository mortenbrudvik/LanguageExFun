using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
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
            await ThePromiseOfAFuture.Run();
            

            
        }


    }

    public record ProcessItem(string Name);
}

using System;
using System.Diagnostics.Contracts;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DomainModel;
using Flurl.Http;
using FunConsole.FunctionalAreas;
using LanguageExt;
using LanguageExt.ClassInstances;
using LanguageExt.Common;
using Microsoft.FSharp.Collections;
using static System.Console;
using static System.Decimal;
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
            await ExceptionHandling.Run();
            

            
            ReadLine();
        }
    }
}

using System;
using DomainModel;
using LanguageExt;
using Microsoft.FSharp.Collections;
using static System.Console;
using static System.Decimal;
using static LanguageExt.Prelude;

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

            ReadLine();
        }
    }
}

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

            ReadLine();
        }


    }
}

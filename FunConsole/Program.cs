using System;
using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using static System.Console;

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
            
            // Lists
            var emptyList = List<string>();
            var letters = List("a", "b");
            var abc = letters.Add("c");
            WriteLine(abc);
            var ac = abc.Remove("b");
            WriteLine(ac);

            ReadLine();
        }
    }
}

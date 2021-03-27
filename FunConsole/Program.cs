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
            //PartialApplicationAndCurrying.Run();

            MultiArgumentFunctions.Run();
            
            // Arrow Notation in C#
            // int -> int -> int 
            Func<int, int, int> sum = (x, y) => x + y;

            ReadLine();

        }

        private static int Sum(int x, int y)
        {
            return x + y;
        }
    }
}

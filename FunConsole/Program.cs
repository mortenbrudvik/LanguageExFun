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
        static void Main(string[] args)
        {
            PartialApplicationAndCurrying.Run();
            
            // Nomad examples

            // Bind multiple parameters
            static Option<int> Multiply(int x, int y) => Some(x * y);
            static Option<int> MultiplicationWithBind(string strX, string strY)
                => parseInt(strX).Bind(x => parseInt(strY)
                    .Bind(y => Multiply(x,y)));
            var multiplicationResult = MultiplicationWithBind("4", "2");
            Out.WriteLine("4*2 = " + multiplicationResult);
            


            ReadLine();

        }
    }
}

using System;
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

            ReadLine();

        }
    }
}

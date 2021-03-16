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
            var names = new[] {"Bob", "Lisa", "Bill", "Joe"};

            Func<string, string, string> greet = (greeting, name) => $"{greeting} {name}!";
            WriteLine("Greet Normal");
            names.Map(x=>greet("Hello", x)).ToList().ForEach(WriteLine);

            WriteLine("\nGreet with partial applications");
            var greetSimple = par(greet, "Hi");
            names.Map(x=>greetSimple(x)).ToList().ForEach(WriteLine);

            WriteLine("\nGreet with partial application, currying");
            var greetCowboy = curry(greet)("Howdy");
            names.Map(x=>greetCowboy(x)).ToList().ForEach(WriteLine);

            WriteLine("\nAdd method, currying");

            var add = curry((int x, int y) => x + y);
            var res = add(2)(2);
            WriteLine("2+2 = " + res);



            ReadLine();

        }
    }
}

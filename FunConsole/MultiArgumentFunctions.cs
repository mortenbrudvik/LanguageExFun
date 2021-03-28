using System;
using LanguageExt;
using System.Linq;
using static System.Console;

using static LanguageExt.Prelude;

namespace FunConsole
{
    public static class MultiArgumentFunctions
    {
        public static void Run()
        {
            // Multi Argument Functions
            
            // Map - Function Application
            Func<int, Func<int, int>> multiply = x => y => x * y;
            Some(4).Map(multiply)
                .Apply(3)
                .IfSome(WriteLine);

            // Lifting functions
            Some(multiply)
                .Apply(Some(4))
                .Apply(Some(5))
                .IfSome(WriteLine);
            
            // Functor, Applicative, Nomad 
            
            // Functor - Map
            var mapEx = Some(3).Map(x => x * 3);
            
            // Applicative - Apply
            Func<int, int, int> mul = (x,y) => x * y;
            var applyEx = par(mul, 3)(3);   
            
            // Nomad - Bind 
            var bindEx = Some(3).Bind(x => Some(x * 3));
            
            // Nomad right Identity
            var m = Some(2);
            var right = m == m.Bind(x => Some(m));
            Out.WriteLine("\nm == m.Bind(x => Some(m))");
            Out.WriteLine("Right: " + right);
            
            // Nomad left identity
            Func<int, int> @double = (x) => x*2;
            var left = @double(2) == Some(2).Bind(x => Some(@double(x)));
            Out.WriteLine("\n@double(2) == Some(2).Bind(x => Some(@double(x)))");
            Out.WriteLine("Left: " + left);
            
            // Nomad associativity
            Func<int,Option<int>> triple = (x) => Some(x* 3);
            var m2 = Some("2");
            var associativity = m2.Bind(parseInt).Bind(triple) == m2.Bind(x => parseInt(x).Bind(triple));;
            Out.WriteLine("\nm2.Bind(parseInt).Bind(triple) == m2.Bind(x => parseInt(x).Bind(triple))");
            Out.WriteLine("Associativity: " + associativity);


            // Bind using multiple parameters
            static Option<int> Multiply(int x, int y) => Some(x * y);
            static Option<int> MultiplicationWithBind(string strX, string strY)
                => parseInt(strX).Bind(x => parseInt(strY)
                    .Bind(y => Multiply(x,y)));

            var multiplicationResult = MultiplicationWithBind("4", "2");
            Out.WriteLine("4*2 = " + multiplicationResult);

            // Using LINQ with arbitrary monads
            var chars = new[] { 'a', 'b', 'c' };
            var ints = new [] { 2, 3 };

            var r1 = from c in chars
                from i in ints
                select (c, i);
            Out.WriteLine(string.Join(',', r1));
            // Above is equal to
            var r2 = chars.SelectMany(c => ints, (c, i) => (c, i));
            Out.WriteLine(string.Join(',', r2));

            // Linq provide a better syntax when applying multiple parameters.
            var s1 = "2";
            var s2 = "3";
            var sum = from a in parseInt(s1)
                from b in parseInt(s2)
                select a + b;
            Out.WriteLine($"{s1} + {s2} = {sum}");

            // Using where and let
            var res = from a in parseInt(s1)
                where a > 0
                let aa = a * a

                from b in parseInt(s2)
                where b > 0
                let bb = b * b

                select Math.Sqrt(aa + bb);
            Out.WriteLine($"Math.Sqrt(a*a + b*b) = {res}");

        }
    }
}
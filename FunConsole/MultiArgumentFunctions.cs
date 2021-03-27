using System;
using LanguageExt;
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
            var multiplyBy4 = Some(4).Map(multiply);

            // Map - Using built in currying support
            Func<int, int, int> multiplyV2 = (x, y) => x * y;
            var multiplyBy7 = Some(7).Map(multiply);

            multiplyBy7.Apply(Some(2))
                .IfSome(x => Out.WriteLine("" + x));

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
            
            // Right Identity
            var m = Some(2);
            var right = m == m.Bind(x => Some(m));
            Out.WriteLine("Right: " + right);
            
            



            // Bind multiple parameters
            static Option<int> Multiply(int x, int y) => Some(x * y);
            static Option<int> MultiplicationWithBind(string strX, string strY)
                => parseInt(strX).Bind(x => parseInt(strY)
                    .Bind(y => Multiply(x,y)));
            var multiplicationResult = MultiplicationWithBind("4", "2");
            Out.WriteLine("4*2 = " + multiplicationResult);
        }
    }
}
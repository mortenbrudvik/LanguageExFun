using System;
using LanguageExt;
using static System.Console;
using static LanguageExt.Prelude;

namespace FunConsole.FunctionalAreas
{
    public static class Misc
    {
        public static void Run()
        {
            //parseInt, ifNone and match
            WriteLine("parseInt, ifNone and match");
            int res = parseInt("2").IfNone(0);
            res = ifNone(parseInt("k"), 0);
            res = parseInt("4").Match(
                x => x * 2,
                () => 0);
            res = match(parseInt("3"),
                x => x * 2,
                () => 0);
            
            // Function composition
            WriteLine("Functional composition");
            Func<int, int> add2 = x => x + 2;
            Func<int, int> add3 = x => x + 3;
            Func<int, int> add5 = x => add2.Compose(add3)(x);
            WriteLine("add2.Compose(add3)(5) = " + add5(2));
            
            // Memoization
            WriteLine("Memoization");
            Func<string, string> CreateUserGuid = userName => userName + ":" + Guid.NewGuid();
            Func<string, string> CreateUserGuidMemo = memo(CreateUserGuid);
            WriteLine(memo(CreateUserGuidMemo("bob")));
            WriteLine(memo(CreateUserGuidMemo("bob")));
            
            // Fold
            WriteLine("Fold");
            var foldedSum = List(1, 2, 3, 4, 5)
            .Map(x => x * 10)
            .Fold(10m, (x, s) => s + x); // 160m
            WriteLine("Folded sum: " + foldedSum);
            
            // Reduce
            WriteLine("Reduce");
            var reducedSum = List(1, 2, 3, 4, 5)
                .Map(x => x * 10)
                .Reduce((x, s) => s + x); // 150
            WriteLine("Reduced sum: " + reducedSum);

                
            
        }
    }
}
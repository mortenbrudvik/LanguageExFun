using System;
using System.Linq;
using System.Collections.Generic;
using DomainModel;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.FSharp.Collections;
using static System.Console;
using static System.Decimal;
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

            Func<int, Validation<CustomError, int>> isPositive = number =>
                number switch
                {
                    > 0 => Success<CustomError, int>(number),
                    _ => Fail<CustomError, int>(CustomError.New("Not positive number!"))
                };

            var isAllPositive = Enumerable.Range(1, 10)
                .Map(isPositive)
                .All(x => x.IsSuccess);
            Out.WriteLine($"All Positive (1..10): {isAllPositive}" );

            isAllPositive = Enumerable.Range(-1, 10)
                .Map(isPositive)
                .All(x=>x.IsSuccess);
            Out.WriteLine($"All Positive (-1..10): {isAllPositive}" );

            ReadLine();
        }

        public class CustomError : NewType<CustomError, string>
        {
            public CustomError(string error) : base(error)
            {
            }
        }
    }
}

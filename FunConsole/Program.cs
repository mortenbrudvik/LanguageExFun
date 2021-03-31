using System;
using System.Linq;
using System.Collections.Generic;
using DomainModel;
using LanguageExt;
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

            Func<int, Validation<CustomError, int>> isPositive = number =>
                number switch
                {
                    > 0 => Success<CustomError, int>(number),
                    _ => Fail<CustomError, int>(CustomError.New("Not positive number!"))
                };
            
            var isAllPositive = Range(1, 10)
                .Map(isPositive)
                .All(x => x.IsSuccess);
            Out.WriteLine($"All Positive (1..10): {isAllPositive}" );

            isAllPositive = Range(-1, 12)
                .Map(isPositive)
                .All(x=>x.IsSuccess);
            Out.WriteLine($"All Positive (-1..10): {isAllPositive}" );

            var totalPositive = Range(-1, 12)
                .Map(isPositive)
                .Bind(x => x)
                .Sum(x=>x.Success);
            Out.WriteLine($"Sum of positive number (-1..10): {totalPositive}" );
            
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

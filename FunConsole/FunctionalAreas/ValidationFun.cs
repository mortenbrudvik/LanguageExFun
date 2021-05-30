using System;
using System.Linq;
using LanguageExt;
using static System.Linq.Enumerable;
using static LanguageExt.Prelude;
using static System.Console;
using Error = LanguageExt.Common.Error;

namespace FunConsole.FunctionalAreas
{
    public static class ValidationFun
    {
        public static void Run()
        {
            Func<int, Validation<CustomError, int>> validatePositive = number =>
                number switch
                {
                    > 0 => Success<CustomError, int>(number),
                    _ => Fail<CustomError, int>(CustomError.New("Not positive number!"))
                };
            Func<int, Validation<CustomError, int>> validateOdd = number =>
                (number%2==0) switch
                {
                    false => Success<CustomError, int>(number),
                    _ => Fail<CustomError, int>(CustomError.New("Not odd number!"))
                };
            Func<int, Validation<CustomError, int>> validateEven = number =>
                (number%2==0) switch
                {
                    true => Success<CustomError, int>(number),
                    _ => Fail<CustomError, int>(CustomError.New("Not even number!"))
                };
            
            var isAllPositive = Range(1, 10)
                .Map(validatePositive)
                .All(x => x.IsSuccess);
            WriteLine($"All Positive (1..10): {isAllPositive}" );

            isAllPositive = Range(-1, 12)
                .Map(validatePositive)
                .All(x=>x.IsSuccess);
            Out.WriteLine($"All Positive (-1..10): {isAllPositive}" );

            var totalPositive = Range(-1, 12)
                .Map(validatePositive)
                .Bind(x => x)
                .Sum(x=>x.Success);
            Out.WriteLine($"Sum of positive number (-1..10): {totalPositive}" );

            validatePositive(1)
                .Do(result => WriteLine($"Number {result} is positive"))
                .Bind(validateOdd)
                .Do(result => WriteLine($"Number {result} is positive and odd"))
                .Match(x =>
                {
                    WriteLine("Validation success");
                    return x;
                }, errors =>
                {
                    WriteLine("Validation failed");
                    errors.ToList().ForEach(err => WriteLine("Error: " + err.Value));
                    return 0;
                });
            
            // validate using either
            WriteLine("validate using either");
            Func<int, Either<Error, int>> validateOddEither = number =>
                (number%2==0) switch
                {
                    false => Right(number),
                    _ => Left(Error.New("Not odd number"))
                };

            validateOddEither(0)
                .Do(result => WriteLine($"number {result} is odd"))
                .Match(_ => WriteLine("Validation success"), error => WriteLine(error.Message));
        }
        
        public class CustomError : NewType<CustomError, string>
        {
            public CustomError(string error) : base(error)
            {
            }
        }
    }
}
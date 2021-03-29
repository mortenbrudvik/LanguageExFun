using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
            PartialApplicationAndCurrying.Run();

            MultiArgumentFunctions.Run();
            
            // Lists
            var emptyList = List<string>();
            var letters = List("a", "b");
            var abc = letters.Add("c");
            WriteLine(abc);
            var ac = abc.Remove("b");
            WriteLine(ac);
            
            // Immutable object copying
            var account = new Account(AccountStatus.Active, Currency.EUR, new []{"xx"});
            var redFlaggedAccount = account.WithStatus(AccountStatus.RedFlag);
            var frozenAccount = account.Frozen();
            var addedTransaction = account.Add("transaction: xxx");

            ReadLine();
        }
    }

    public sealed class Account
    {
        private IEnumerable<string> TransactionHistory { get; }
        public Account(AccountStatus status, Currency currency, IEnumerable<string> transactions)
        {
            TransactionHistory = ImmutableList.CreateRange(
                transactions ?? Enumerable.Empty<string>() );
            Status = status;
            Currency = currency;
            TransactionHistory = transactions;
        }
        public AccountStatus Status { get; private set; }
        public Currency Currency { get; private set; }

        public Account WithStatus(AccountStatus status)
        {
            return new Account(status, Currency, TransactionHistory);
        }
        
        public Account Add(string transaction)
        {
            return new Account(Status, Currency, TransactionHistory.Prepend(transaction));
        }
    }

    public static class AccountExt
    {
        public static Account Frozen(this Account account)
            => account.WithStatus(AccountStatus.Frozen);

    }
    
    public enum AccountStatus{Active, Frozen, RedFlag}
    public enum Currency{NOK, EUR}
    
}

using System;
using DomainModel;
using FunConsole.AccountingDataModel;
using static System.Console;
using static System.Decimal;
using static LanguageExt.Prelude;
using static Microsoft.FSharp.Collections.ListModule;
using AccountStatus = FunConsole.AccountingDataModel.AccountStatus;
using Transaction = FunConsole.AccountingDataModel.Transaction;

namespace FunConsole.FunctionalAreas
{
    public static class DataFunctionality
    {
        public static void Run()
        {
            // Lists
            var emptyList = List<string>();
            var letters = List("a", "b");
            var abc = letters.Add("c");
            WriteLine(abc);
            var ac = abc.Remove("b");
            WriteLine(ac);

            // Immutable object copying (plain class - would be more sensible to do with records)
            var account = new Account(AccountStatus.Active, Currency.EUR, new[] { new Transaction(10, "Salary", DateTime.Now) });
            var dormantAccount = account.WithStatus(AccountStatus.Dormant);
            var frozenAccount = account.Frozen();
            var addedTransaction = account.Add(new Transaction(-5, "Spotify Inc.", DateTime.Now));

            // Immutable object copying (F# - Domain model)
            var bankAccount = new BankAccount(DomainModel.AccountStatus.Active, "NOK", Zero, OfSeq(new[] {new DomainModel.Transaction(10, "", DateTime.Now)}));
            var closedBankAccount = bankAccount.WithStatus(DomainModel.AccountStatus.Closed);
            var addedTransactionBankAccount = bankAccount.Add(new DomainModel.Transaction(10, "", DateTime.Now));
        }
    }
}
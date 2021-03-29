using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace FunConsole.DataFunctionality_DataModel
{
    public sealed class Account
    {
        private IEnumerable<Transaction> TransactionHistory { get; }
        public Account(AccountStatus status, Currency currency, IEnumerable<Transaction> transactions)
        {
            TransactionHistory = ImmutableList.CreateRange(transactions ?? Enumerable.Empty<Transaction>() );
            Status = status;
            Currency = currency;
            TransactionHistory = transactions;
        }
        public AccountStatus Status { get; }
        public Currency Currency { get; }

        public Account WithStatus(AccountStatus status) => new(status, Currency, TransactionHistory);

        public Account Add(Transaction transaction) => new(Status, Currency, TransactionHistory.Prepend(transaction));
    }
}
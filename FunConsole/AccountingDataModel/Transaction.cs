using System;

namespace FunConsole.AccountingDataModel
{
    public sealed class Transaction
    {
        public Transaction(decimal amount, string description, DateTime date)
        {
            Amount = amount;
            Description = description;
            Date = date;
        }

        public decimal Amount { get; }
        public string Description { get; }
        public DateTime Date { get; }
    }
}
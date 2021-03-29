namespace DomainModel
open System

type AccountStatus = 
    Requested | Active | Frozen | Dormant | Closed

type CurrrencyCode = string

type Transaction = {
    Amount: decimal
    Description: string
    Date: DateTime
}

type BankAccount = {
    Status: AccountStatus
    Currency: CurrrencyCode
    AllowedOverdraft: decimal
    TransactionHistory: Transaction list
}

type BankAccount with 

member this.WithStatus(status) = 
    {this with Status = Active }

member this.Add(transaction) = 
    { this with TransactionHistory = transaction::this.TransactionHistory}
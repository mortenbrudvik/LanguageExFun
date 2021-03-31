namespace FunConsole.DataFunctionality_DataModel
{
    internal static class AccountExt
    {
        public static Account Frozen(this Account account)
            => account.WithStatus(AccountStatus.Frozen);
    }
}
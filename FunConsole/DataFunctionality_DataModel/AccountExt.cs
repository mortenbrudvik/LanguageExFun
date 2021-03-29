namespace FunConsole.DataFunctionality_DataModel
{
    public static class AccountExt
    {
        public static Account Frozen(this Account account)
            => account.WithStatus(AccountStatus.Frozen);
    }
}
namespace FunConsole.DataFunctionalityDataModel
{
    internal static class AccountExt
    {
        public static Account Frozen(this Account account)
            => account.WithStatus(AccountStatus.Frozen);
    }
}
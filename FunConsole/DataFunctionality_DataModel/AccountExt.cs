namespace FunConsole.DomainModel
{
    public static class AccountExt
    {
        public static Account Frozen(this Account account)
            => account.WithStatus(AccountStatus.Frozen);
    }
}
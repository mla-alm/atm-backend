namespace AtmBackend.Services;

internal class AccountService : IAccountService
{
    public Task<bool> HasSufficientFunds(ushort withdrawalAmount) =>
        Task.FromResult(true);

    public Task MarkWithdrawal(ushort withdrawalAmount) =>
        Task.CompletedTask;
}

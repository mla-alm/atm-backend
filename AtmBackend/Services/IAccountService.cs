namespace AtmBackend.Services;

internal interface IAccountService
{
    Task<bool> HasSufficientFunds(ushort withdrawalAmount);
    Task MarkWithdrawal(ushort withdrawalAmount);
}

using AtmBackend.Models.Requests;
using AtmBackend.Models.Responses;

namespace AtmBackend.Services;

internal class WithdrawalService(
    IAccountService accountService,
    IDenominationService denominationService) : IWithdrawalService
{
    public async Task<List<DenominationResponse>> Withdraw(WithdrawalRequest request)
    {
        var withdrawalAmount = request.GetAmount();

        if (!await accountService.HasSufficientFunds(withdrawalAmount))
        {
            // ToDo: Give user better error message
            throw new InvalidOperationException("Insufficient funds");
        }

        var denominations = denominationService.SplitIntoPayableDenominations(withdrawalAmount);
        await accountService.MarkWithdrawal(withdrawalAmount);

        return
        [
            .. denominations
                .Where(d => d.Amount > 0)
                .Select(DenominationResponse.FromDenominationAmount)
        ];
    }
}

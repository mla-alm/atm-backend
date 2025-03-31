using AtmBackend.Models.Requests;
using AtmBackend.Models.Responses;

namespace AtmBackend.Services;

public interface IWithdrawalService
{
    Task<List<DenominationResponse>> Withdraw(WithdrawalRequest request);
}

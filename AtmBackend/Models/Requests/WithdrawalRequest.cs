using System.ComponentModel.DataAnnotations;

namespace AtmBackend.Models.Requests;

public record WithdrawalRequest
{
    [Range(1, 65000, ErrorMessage = "Withdrawal amount must be between 1 and 65000")]
    [Required(ErrorMessage = "Withdrawal amount is required")]
    public int? WithdrawalAmount { get; init; }

    internal ushort GetAmount()
    {
        if (WithdrawalAmount is null ||
            WithdrawalAmount < 1 ||
            WithdrawalAmount > ushort.MaxValue)
        {
            throw new InvalidOperationException($"Withdrawal amount invalid, {WithdrawalAmount}");
        }

        return (ushort)WithdrawalAmount;
    }
}

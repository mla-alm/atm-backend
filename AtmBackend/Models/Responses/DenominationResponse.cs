using AtmBackend.Denominations;

namespace AtmBackend.Models.Responses;

public record DenominationResponse(ushort Amount, string DenominationName, PayBox PayBox, ushort Value)
{
    internal static DenominationResponse FromDenominationAmount(DenominationAmount denominationAmount) =>
        new(denominationAmount.Amount, denominationAmount.Denomination.Name, denominationAmount.Denomination.PayBox, denominationAmount.Denomination.Value);
}

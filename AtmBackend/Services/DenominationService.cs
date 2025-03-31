using System.Collections.Immutable;
using AtmBackend.Denominations;
using AtmBackend.Models;

namespace AtmBackend.Services;

internal class DenominationService : IDenominationService
{
    public IEnumerable<DenominationAmount> SplitIntoPayableDenominations(ushort withdrawalAmount) =>
        _availableDenominations.Aggregate(
            Enumerable.Empty<DenominationAmount>(),
            (denominationAmounts, denomination) =>
            {
                ushort amount = denomination.GetMaxAmountOfThisCurrencyLessThan(withdrawalAmount);

                denominationAmounts = denominationAmounts.Append(new DenominationAmount(denomination, amount));
                withdrawalAmount %= denomination.Value;

                return denominationAmounts;
            });

    private static readonly ImmutableList<Denomination> _availableDenominations =
        [.. new List<Denomination>
        {
            Note.Note1000,
            Note.Note500,
            Note.Note200,
            Note.Note100,
            Note.Note50,
            Coin.Coin20,
            Coin.Coin10,
            Coin.Coin5,
            Coin.Coin2,
            Coin.Coin1
        }
            .OrderByDescending(d => d.Value)];
}

using AtmBackend.Models;

namespace AtmBackend.Services;

internal interface IDenominationService
{
    IEnumerable<DenominationAmount> SplitIntoPayableDenominations(ushort withdrawalAmount);
}

using AtmBackend.Denominations;

namespace AtmBackend.Models;

public record DenominationAmount(Denomination Denomination, ushort Amount);

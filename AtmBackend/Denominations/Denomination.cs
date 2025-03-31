namespace AtmBackend.Denominations;

public abstract record Denomination
{
    public abstract PayBox PayBox { get; }

    public abstract ushort Value { get; }

    public abstract string Name { get; }

    public ushort GetMaxAmountOfThisCurrencyLessThan(ushort requestAmount) =>
        (ushort)(requestAmount / Value);
}

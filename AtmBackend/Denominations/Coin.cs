namespace AtmBackend.Denominations;

internal record Coin : Denomination
{
    public static Coin Coin1 => new(1, 10);
    public static Coin Coin2 => new(2, 30);
    public static Coin Coin5 => new(5, 50);
    public static Coin Coin10 => new(10, 20);
    public static Coin Coin20 => new(20, 40);

    private Coin(ushort value, ushort diameter)
    {
        Value = value;
        Diameter = diameter;
    }

    public sealed override ushort Value { get; }

    public ushort Diameter { get; }

    public sealed override PayBox PayBox => Diameter > 20 ? PayBox.CoinsGreaterThan20mm : PayBox.CoinsLessThanEqual20mm;

    public sealed override string Name => $"{Value} coin";
}

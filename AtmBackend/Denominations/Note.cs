namespace AtmBackend.Denominations;

internal record Note : Denomination
{
    public static Note Note50 => new(50);
    public static Note Note100 => new(100);
    public static Note Note200 => new(200);
    public static Note Note500 => new(500);
    public static Note Note1000 => new(1000);

    private Note(ushort value) : base()
    {
        Value = value;
    }

    public sealed override ushort Value { get; }

    public sealed override PayBox PayBox => PayBox.Notes;

    public sealed override string Name => $"{Value} note";
}

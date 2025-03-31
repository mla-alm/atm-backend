using AtmBackend.Denominations;
using AtmBackend.Models;
using AtmBackend.Services;
using Xunit;

namespace AtmBackend.Test.Services;

public class DenominationServiceTests
{
    private static DenominationService GetService() => new();

    [Theory]
    [MemberData(nameof(GetDenominationAmounts))]
    public void SplitIntoPayableDenominations(ushort input, List<DenominationAmount> expected)
    {
        // Arrange
        var service = GetService();

        // Act
        var result = service.SplitIntoPayableDenominations(input);

        // Assert
        Assert.Equal(expected, result);
    }

    public static IEnumerable<object[]> GetDenominationAmounts() =>
    [
        [
            (ushort)0,
            new List<DenominationAmount>
            {
                new(Note.Note1000, 0),
                new(Note.Note500, 0),
                new(Note.Note200, 0),
                new(Note.Note100, 0),
                new(Note.Note50, 0),
                new(Coin.Coin20, 0),
                new(Coin.Coin10, 0),
                new(Coin.Coin5, 0),
                new(Coin.Coin2, 0),
                new(Coin.Coin1, 0)
            }
        ],

        [
            (ushort)578,
            new List<DenominationAmount>
            {
                new(Note.Note1000, 0),
                new(Note.Note500, 1),
                new(Note.Note200, 0),
                new(Note.Note100, 0),
                new(Note.Note50, 1),
                new(Coin.Coin20, 1),
                new(Coin.Coin10, 0),
                new(Coin.Coin5, 1),
                new(Coin.Coin2, 1),
                new(Coin.Coin1, 1)
            }
        ],

        [
            (ushort)64000,
            new List<DenominationAmount>
            {
                new(Note.Note1000, 64),
                new(Note.Note500, 0),
                new(Note.Note200, 0),
                new(Note.Note100, 0),
                new(Note.Note50, 0),
                new(Coin.Coin20, 0),
                new(Coin.Coin10, 0),
                new(Coin.Coin5, 0),
                new(Coin.Coin2, 0),
                new(Coin.Coin1, 0)
            }
        ],

        [
            (ushort)499,
            new List<DenominationAmount>
            {
                new(Note.Note1000, 0),
                new(Note.Note500, 0),
                new(Note.Note200, 2),
                new(Note.Note100, 0),
                new(Note.Note50, 1),
                new(Coin.Coin20, 2),
                new(Coin.Coin10, 0),
                new(Coin.Coin5, 1),
                new(Coin.Coin2, 2),
                new(Coin.Coin1, 0)
            }
        ],

        [
            (ushort)110,
            new List<DenominationAmount>
            {
                new(Note.Note1000, 0),
                new(Note.Note500, 0),
                new(Note.Note200, 0),
                new(Note.Note100, 1),
                new(Note.Note50, 0),
                new(Coin.Coin20, 0),
                new(Coin.Coin10, 1),
                new(Coin.Coin5, 0),
                new(Coin.Coin2, 0),
                new(Coin.Coin1, 0)
            }
        ],

        [
            (ushort)231,
            new List<DenominationAmount>
            {
                new(Note.Note1000, 0),
                new(Note.Note500, 0),
                new(Note.Note200, 1),
                new(Note.Note100, 0),
                new(Note.Note50, 0),
                new(Coin.Coin20, 1),
                new(Coin.Coin10, 1),
                new(Coin.Coin5, 0),
                new(Coin.Coin2, 0),
                new(Coin.Coin1, 1)
            }
        ],
    ];
}



using AtmBackend.Denominations;
using AtmBackend.Models;
using AtmBackend.Models.Requests;
using AtmBackend.Models.Responses;
using AtmBackend.Services;
using Moq;
using Xunit;

namespace AtmBackend.Test.Services;

public class WithdrawalServiceTests
{
    private IAccountService? _accountService;

    private IDenominationService? _denominationService;

    private WithdrawalService GetService() =>
        new(_accountService!, _denominationService!);

    [Fact]
    public async Task Withdraw()
    {
        // Arrange
        const ushort withdrawalAmount = 100;
        var note = Note.Note500;
        var coin = Coin.Coin1;
        var zeroCoin = Coin.Coin20;

        var request = new WithdrawalRequest
        {
            WithdrawalAmount = withdrawalAmount
        };

        _accountService = Mock.Of<IAccountService>(i =>
            i.HasSufficientFunds(withdrawalAmount) == Task.FromResult(true) &&
            i.MarkWithdrawal(withdrawalAmount) == Task.CompletedTask);

        List<DenominationAmount> denominations =
        [
            new(note, 1),
            new(zeroCoin, 0),
            new(coin, 4),
        ];

        _denominationService = Mock.Of<IDenominationService>(i =>
            i.SplitIntoPayableDenominations(withdrawalAmount) == denominations);

        var service = GetService();

        // Act
        var actual = await service.Withdraw(request);

        // Assert
        List<DenominationResponse> expected = [
            new(1, note.Name, note.PayBox, note.Value),
            new(4, coin.Name, coin.PayBox, coin.Value)
        ];

        Assert.Equal(expected, actual);
    }
}

using Bank.Cards.Infrastructure.Services;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace Bank.Cards.Tests.Mocks;

public class CardServiceMock
{
    public ICardService Mock { get; } = Substitute.For<ICardService>();

    public void SetupGetCardDetails(string userId, string cardNumber, CardType cardType, CardStatus cardStatus, bool isPinSet)
    {
        Mock.GetCardDetails(userId, cardNumber).Returns(new CardDetails(cardNumber, cardType, cardStatus, isPinSet));
    }

    public void SetupGetCardDetailsReturnsNull()
    {
        Mock.GetCardDetails(Arg.Any<string>(), Arg.Any<string>()).Returns((CardDetails)null);
    }

    public void SetupThrowsException(Exception exception)
    {
        Mock.GetCardDetails(Arg.Any<string>(), Arg.Any<string>()).Throws(exception);
    }
}
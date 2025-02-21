using System.Diagnostics;
using System.Net;
using Bank.Cards.Api;
using Bank.Cards.Application.UsersCards.GetAvailableActions;
using Bank.Cards.Infrastructure.Services;
using Bank.Cards.Tests.Extensions;
using Bank.Cards.Tests.Infrastructure;
using FluentAssertions;
using Xunit;

namespace Bank.Cards.Tests.Controllers.UserCards;

public class GetAvailableActionsTests : TestBase<Program>
{
    [Theory]
    [InlineData(CardType.Prepaid, CardStatus.Closed, true)]
    public async Task ShouldReturnAvailableActions(CardType cardType, CardStatus cardStatus, bool isPinSet)
    {
        // arrange
        var expectedStatusCode = HttpStatusCode.OK;

        var userId = Guid.NewGuid().ToString();
        var cardNumber = Guid.NewGuid().ToString();

        CardService.SetupGetCardDetails(userId, cardNumber, cardType, cardStatus, isPinSet);

        var uri = $"users/{userId}/cards/{cardNumber}";

        // act
        var (httpResponse, response) = await HttpClient.GetAsync<GetAvailableActionsResponse>(uri);

        // assert
        httpResponse.IsSuccessStatusCode.Should().BeTrue();
        httpResponse.StatusCode.Should().Be(expectedStatusCode);

        response.Should().NotBeNull();
        response.Actions.Should().NotBeEmpty();
    }

    [Fact]
    public async Task ShouldReturnValidResult()
    {
        // arrange
        var expectedStatusCode = HttpStatusCode.OK;

        var userId = Guid.NewGuid().ToString();
        var cardNumber = Guid.NewGuid().ToString();

        var cardTypes = Enum.GetValues(typeof(CardType)).As<IList<CardType>>();
        var cardStatuses = Enum.GetValues(typeof(CardStatus)).As<IList<CardStatus>>();
        var pinSets = new[] { true, false };

        foreach (var cardType in cardTypes)
        foreach (var cardStatus in cardStatuses)
        foreach (var pinSet in pinSets)
        {
            CardService.SetupGetCardDetails(userId, cardNumber, cardType, cardStatus, pinSet);

            var uri = $"users/{userId}/cards/{cardNumber}";

            // act
            var (httpResponse, response) = await HttpClient.GetAsync<GetAvailableActionsResponse>(uri);

            // assert
            httpResponse.IsSuccessStatusCode.Should().BeTrue();
            httpResponse.StatusCode.Should().Be(expectedStatusCode);

            response.Should().NotBeNull();
            response.Actions.Should().NotBeEmpty();

            Debug.WriteLine($"{cardType} {cardStatus} {pinSet}");
        }
    }

    [Theory]
    [InlineData(" ", " ")]
    [InlineData(" ", "cardid")]
    [InlineData("userid", " ")]
    public async Task ShouldReturnBadRequest(string userId, string cardNumber)
    {
        // arrange
        var uri = $"users/{userId}/cards/{cardNumber}/";

        // act
        var (httpResponse, response) = await HttpClient.GetAsync<GetAvailableActionsResponse>(uri);

        // assert
        httpResponse.IsSuccessStatusCode.Should().BeFalse();
        httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldReturnNotFound_WhenCardDetailsNotFound()
    {
        // arrange
        CardService.SetupGetCardDetailsReturnsNull();
        var uri = $"users/notExistingUserId/cards/notExistingCardNumber/";

        // act
        var (httpResponse, response) = await HttpClient.GetAsync<GetAvailableActionsResponse>(uri);

        // assert
        httpResponse.IsSuccessStatusCode.Should().BeFalse();
        httpResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ShouldReturnInternalServerError_WhenExceptionOccured()
    {
        // arrange
        CardService.SetupThrowsException(new Exception());
        var uri = $"users/userId/cards/cardNumber/";

        // act
        var (httpResponse, response) = await HttpClient.GetAsync<GetAvailableActionsResponse>(uri);

        // assert
        httpResponse.IsSuccessStatusCode.Should().BeFalse();
        httpResponse.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }
}
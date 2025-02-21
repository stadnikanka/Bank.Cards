using Bank.Cards.Api.Extensions;
using Bank.Cards.Application.UsersCards.GetAvailableActions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Cards.Api.Controllers;

[ApiController]
public class UserCardsController
{
    [HttpGet("users/{userId}/cards/{cardNumber}")]
    [ProducesResponseType(typeof(GetAvailableActionsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAvailableActions(
        [FromRoute] string userId,
        [FromRoute] string cardNumber,
        [FromServices] IMediator mediator)
        => (await mediator.Send(new GetAvailableActionsRequest(userId, cardNumber))).AsActionResult();
}
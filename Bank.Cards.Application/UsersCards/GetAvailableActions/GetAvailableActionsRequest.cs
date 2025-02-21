using Bank.Cards.Application.Infrastructure;
using MediatR;

namespace Bank.Cards.Application.UsersCards.GetAvailableActions;

public record GetAvailableActionsRequest(string UserId, string CardNumber) : IRequest<OperationResult<GetAvailableActionsResponse>>;

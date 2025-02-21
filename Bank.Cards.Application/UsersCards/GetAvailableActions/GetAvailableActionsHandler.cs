using Bank.Cards.Application.Infrastructure;
using Bank.Cards.Application.Services;
using Bank.Cards.Infrastructure.Services;
using MediatR;

namespace Bank.Cards.Application.UsersCards.GetAvailableActions;

public class GetAvailableActionsHandler(
    ICardService cardService,
    ICardActionsService userCardActionsProvider)
    : IRequestHandler<GetAvailableActionsRequest, OperationResult<GetAvailableActionsResponse>>
{
    public async Task<OperationResult<GetAvailableActionsResponse>> Handle(GetAvailableActionsRequest request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.UserId))
            return OperationResult<GetAvailableActionsResponse>.BadRequest($"Invalid {nameof(request.UserId)}");

        if (string.IsNullOrWhiteSpace(request.CardNumber))
            return OperationResult<GetAvailableActionsResponse>.BadRequest($"Invalid {nameof(request.CardNumber)}");

        var cardDetails = await cardService.GetCardDetails(request.UserId, request.CardNumber);

        if (cardDetails == null)
            return OperationResult<GetAvailableActionsResponse>.NotFound("User or card not found.");

        var availableActions = userCardActionsProvider.GetAvailableActions(cardDetails);

        return OperationResult<GetAvailableActionsResponse>.Success(new GetAvailableActionsResponse(availableActions));
    }
}
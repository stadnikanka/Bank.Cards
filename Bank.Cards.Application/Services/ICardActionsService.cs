using Bank.Cards.Infrastructure.Services;

namespace Bank.Cards.Application.Services;

public interface ICardActionsService
{
    IEnumerable<string> GetAvailableActions(CardDetails cardDetails);
}
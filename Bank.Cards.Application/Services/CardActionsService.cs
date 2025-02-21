using Bank.Cards.Application.Configuration;
using Microsoft.Extensions.Options;
using Bank.Cards.Infrastructure.Services;

namespace Bank.Cards.Application.Services;

public class CardActionsService(IOptions<CardActionsConfiguration> cardActionsConfigurationOptions)
    : ICardActionsService
{
    private readonly CardActionsConfiguration _cardActionsConfiguration = cardActionsConfigurationOptions.Value;

    public IEnumerable<string> GetAvailableActions(CardDetails cardDetails)
    {
        if (_cardActionsConfiguration?.Rules == null || !_cardActionsConfiguration.Rules.Any())
            return [];

        var availableActions = new List<string>();

        foreach (var rule in _cardActionsConfiguration.Rules)
        {
            if (!IsAllowed(rule.Value.TypeRule, cardDetails.CardType))
                continue;

            if (!IsAllowed(rule.Value.StatusRule, cardDetails.CardStatus, cardDetails.IsPinSet))
                continue;

            availableActions.Add(rule.Key);
        }

        return availableActions.AsEnumerable();
    }

    private static bool IsAllowed(TypeRule? typeRule, CardType cardType)
    {
        if (typeRule == null)
            return true;

        return typeRule.RuleEffect switch
        {
            RuleEffect.Allow => typeRule.CardTypes.Contains(cardType),
            RuleEffect.Deny => !typeRule.CardTypes.Contains(cardType),
            _ => throw new InvalidDataException($"Invalid {nameof(CardActionsConfiguration)}. {typeRule.RuleEffect} is not supported")
        };
    }

    private static bool IsAllowed(StatusRule? statusRule, CardStatus cardStatus, bool isPinSet)
    {
        if (statusRule == null)
            return true;

        return statusRule.RuleEffect switch
        {
            RuleEffect.Allow => statusRule.CardStatusPinRules.Any(o => o.CardStatus == cardStatus && (!o.IsPinSet.HasValue || o.IsPinSet.Value == isPinSet)),
            RuleEffect.Deny => !statusRule.CardStatusPinRules.Any(o => o.CardStatus == cardStatus && (!o.IsPinSet.HasValue || o.IsPinSet.Value == isPinSet)),
            _ => throw new InvalidDataException($"Invalid {nameof(CardActionsConfiguration)}. {statusRule.RuleEffect} is not supported")
        };
    }
}
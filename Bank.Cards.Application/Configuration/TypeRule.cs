using Bank.Cards.Infrastructure.Services;

namespace Bank.Cards.Application.Configuration;

public class TypeRule
{
    public RuleEffect RuleEffect { get; set; }

    public required CardType[] CardTypes { get; set; }
}
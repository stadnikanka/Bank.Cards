namespace Bank.Cards.Application.Configuration;

public class StatusRule
{
    public RuleEffect RuleEffect { get; set; }

    public required IEnumerable<CardStatusPinRule> CardStatusPinRules { get; set; }
}
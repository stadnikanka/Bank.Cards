namespace Bank.Cards.Application.Configuration;

public class CardActionsConfiguration
{
    public required IReadOnlyDictionary<string, CardRules> Rules { get; set; }
}
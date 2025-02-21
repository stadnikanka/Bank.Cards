using Bank.Cards.Infrastructure.Services;

namespace Bank.Cards.Application.Configuration;

public class CardStatusPinRule
{
    public CardStatus CardStatus { get; set; }

    public bool? IsPinSet { get; set; }
}
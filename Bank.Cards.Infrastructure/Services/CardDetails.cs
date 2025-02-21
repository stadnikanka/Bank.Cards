namespace Bank.Cards.Infrastructure.Services;

public record CardDetails(string CardNumber, CardType CardType, CardStatus CardStatus, bool IsPinSet);

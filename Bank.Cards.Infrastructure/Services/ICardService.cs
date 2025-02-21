namespace Bank.Cards.Infrastructure.Services;

public interface ICardService
{
    Task<CardDetails?> GetCardDetails(string userId, string cardNumber);
}
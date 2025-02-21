using Bank.Cards.Application.Configuration;
using Bank.Cards.Application.Services;
using Bank.Cards.Application.UsersCards.GetAvailableActions;
using Bank.Cards.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Cards.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.Configure<CardActionsConfiguration>(configuration.GetSection("CardActionsConfiguration"));

        serviceCollection.AddMediatR(o => o.RegisterServicesFromAssemblies(typeof(GetAvailableActionsRequest).Assembly));

        serviceCollection.AddTransient<ICardService, CardService>();
        serviceCollection.AddTransient<ICardActionsService, CardActionsService>();

        return serviceCollection;
    }
}
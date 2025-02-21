using Bank.Cards.Tests.Mocks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Cards.Tests.Infrastructure;

public class TestApplicationFactory<TStartup>(CardServiceMock cardServiceMock) : WebApplicationFactory<TStartup>
    where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(serviceCollection => { serviceCollection.AddTransient(_ => cardServiceMock.Mock); });
    }
}
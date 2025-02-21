using Bank.Cards.Tests.Mocks;

namespace Bank.Cards.Tests.Infrastructure;

public class TestBase<TStartup> : IDisposable
    where TStartup : class
{
    private readonly TestApplicationFactory<TStartup>? _factory;

    private readonly HttpClient? _httpClient;

    protected readonly CardServiceMock CardService;

    public TestBase()
    {
        CardService = new CardServiceMock();
        _factory = new(CardService);
        _httpClient = _factory.CreateClient();
    }

    protected HttpClient HttpClient => _httpClient!;

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _factory?.Dispose();
        _httpClient?.Dispose();
    }
}
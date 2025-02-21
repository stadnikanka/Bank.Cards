using Newtonsoft.Json;

namespace Bank.Cards.Tests.Extensions;

public static class HttpClientExtensions
{
    public static async Task<(HttpResponseMessage HttpResponse, TResponse? Response)> GetAsync<TResponse>(
        this HttpClient client,
        string url)
    {
        var httpResponse = await client.GetAsync(url);
        var json = await httpResponse.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<TResponse>(json);

        return (httpResponse, response);
    }
}
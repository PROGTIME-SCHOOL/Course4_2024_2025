using System;
using Newtonsoft.Json.Linq;

using lesson_261024.Services.Interfaces;
using System.Text.Json.Nodes;

namespace lesson_261024.Services;

public class CurrencyService : ICurrencyService
{
    private readonly HttpClient httpClient;
    private readonly string apiKey = "43eca60876f5a8f186645363";

    public CurrencyService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<decimal> GetExchangeRateAsync(string currencyCode)
    {
        var request = $"https://v6.exchangerate-api.com/v6/{apiKey}/latest/USD";

        var response = await httpClient.GetStringAsync(request);

        var json = JObject.Parse(response);

        var rate = json["conversion_rates"]?[currencyCode];

        if (rate != null)
        {
            return rate.Value<decimal>();
        }

        // если не найден возвращаем
        return 0.0m;
    }
}

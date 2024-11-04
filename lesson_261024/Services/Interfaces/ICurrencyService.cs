using System;

namespace lesson_261024.Services.Interfaces;

public interface ICurrencyService
{
    Task<decimal> GetExchangeRateAsync(string currencyCode);  // RUB
}

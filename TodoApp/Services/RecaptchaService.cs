using System;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TodoApp.Models;

namespace TodoApp.Services;

public class RecaptchaService
{
    private RecaptchaSettings settings;

    public RecaptchaService(IOptions<RecaptchaSettings> settings)
    {
        this.settings = settings.Value;
    }

    public async Task<bool> VerifyAsync(string token)
    {
        using var httpClient = new HttpClient();   // fix

        var response = await httpClient.PostAsync(
        $"https://www.google.com/recaptcha/api/siteverify?secret={settings.SecretKey}&response={token}", 
        null);

        if (!response.IsSuccessStatusCode)
        {
            return false;
        }

        var jsonString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<RecaptchaResponse>(jsonString);

        return result != null && result.Success;
    }
}

class RecaptchaResponse
{
    public bool Success { get; set; }
}

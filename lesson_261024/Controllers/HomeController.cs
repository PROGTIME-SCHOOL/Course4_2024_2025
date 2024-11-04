using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using lesson_261024.Models;
using lesson_261024.Services.Interfaces;

namespace lesson_261024.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHttpClientFactory httpClientFactory;
    private readonly IMessageService messageService;
    private readonly ICurrencyService currencyService;

    public HomeController(ILogger<HomeController> logger, IConfiguration configuration,
       IHttpClientFactory httpClientFactory, IMessageService messageService,
       HttpClient httpClient, ICurrencyService currencyService)
    {
        _logger = logger;

        var res = configuration["AllowedHosts"];
        _logger.LogInformation(res);

        var res2 = configuration["Logging:LogLevel:Default"];
        _logger.LogInformation(res2);

        this.httpClientFactory = httpClientFactory;
        this.messageService = messageService;
        this.currencyService = currencyService;
    }

    public async Task<IActionResult> GetExchangeRate(string currencyCode)
    {
        var exchangeRate = await currencyService.GetExchangeRateAsync(currencyCode);

        return Content(exchangeRate.ToString());
    }

    public string UseMessageService()
    {
        return messageService.GetMessage();
    }

    public async Task<string> UseHttpFactory()
    {
        HttpClient httpClient = httpClientFactory.CreateClient();
        string response = await httpClient.GetStringAsync("https://adventures-lab.ru");

        return response;
    }

    public IActionResult Index()
    {
        _logger.LogInformation("Index успешно запустился!");
        
        _logger.LogWarning("Внимание! Что-то может пойти не так!");

        _logger.LogError("Error");

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

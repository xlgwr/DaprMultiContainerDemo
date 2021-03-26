using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapr.Client;
using System.Net.Http;

namespace DaprFrontEnd.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly DaprClient _daprClient;

        public IndexModel(ILogger<IndexModel> logger, DaprClient daprClient)
        {
            _logger = logger;
            _daprClient = daprClient ?? throw new ArgumentNullException(nameof(daprClient));
        }

        public async Task OnGet()
        {
            var forecasts = await _daprClient.InvokeMethodAsync<IEnumerable<WeatherForecast>>(
                HttpMethod.Get,
                "daprbackend",
                "weatherforecast");
            ViewData["WeatherForecastData"] = forecasts;
        }
    }
}

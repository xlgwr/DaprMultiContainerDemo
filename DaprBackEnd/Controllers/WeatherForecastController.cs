using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapr;
using Dapr.Client;

namespace DaprBackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly DaprClient _daprClient;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private static List<Order> Orders = new List<Order>(){
            new Order(){ orderId="001", amount="0", productId="001"}
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, DaprClient daprClient)
        {
            _logger = logger;
            _daprClient = daprClient ?? throw new ArgumentNullException(nameof(daprClient));
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpGet("/orders")]
        public IEnumerable<Order> GetOrders()
        {
            return Orders;
        }
        /// <summary>
        ///  
        ///  post http://localhost:3502/v1.0/publish/pubsub/newOrder
        ///  data { "orderId": "123678", "productId": "5678", "amount": "2" }
        /// </summary>
        [HttpPost("/publishorder")]
        public async Task<ActionResult> PublishOrderAsync(Order order)
        {
            await _daprClient.PublishEventAsync<Order>("pubsub", "newOrder", order);
            return Ok();
        }
        /// <param name="order"></param>
        /// <returns></returns>
        [Topic("pubsub", "newOrder")]
        [HttpPost("/orders")]
        public async Task<ActionResult> CreateOrder(Order order)
        {
            Orders.Add(order);
            await Task.Delay(0);
            return Ok();
        }
    }
}

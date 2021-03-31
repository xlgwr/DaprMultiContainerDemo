using System;

namespace DaprBackEnd
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }
    public class Order
    {
        public string orderId { get; set; }
        public string productId { get; set; }
        public string amount { get; set; }
    }
}

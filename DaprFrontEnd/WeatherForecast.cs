using System;

namespace DaprFrontEnd
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF { get; set; }

        public string Summary { get; set; }
    }
     public class Order
    {
        public string orderId { get; set; }
        public string productId { get; set; }
        public string amount { get; set; }
    }
}
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace Weather
{
    public class JsnReceive
    {
        public coord Coord { get; set; }
        public Weather[] Weather { get; set; }
        [JsonProperty("base")]
        public string Bace { get; set; }
        public Weathermain Main { get; set; }
        public int Visibility { get; set; }
        public Wind Wind { get; set; }
        public Clouds Clouds { get; set; }
        public double Dt { get; set; }
        public Sys Sys { get; set; }
        public int Id { get; set; }
   
        public string Name { get; set; }
        public int Code { get; set; }

    }
    public class coord
    {
        public float Lon { get; set; }
        public float Lat { get; set; }
    }

    public class Sys
    {
        public int Type { get; set; }
        public float Id { get; set; }
        public float Message { get; set; }
        public string Country { get; set; }
        public double Sunrise { get; set; }
        public double Sunset { get; set; }
    }
    public class Clouds
    {
        public int All { get; set; }
    }
    public class Wind
    {
        public float Speed { get; set; }
        public string Name { get; set; }
        public float Deg { get; set; }
    }
    public class Weather
    {
        public float Id { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }
    public class Weathermain
    {
        public float Temp { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }

        public float Temp_min { get; set; }
        public float Temp_max { get; set; }
    }
}

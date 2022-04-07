namespace Domain.Entities
{
    public class CityWeather
    {
        public int Id { get; set; }
        public double Temperature { get; set; }
        public int Precipitation { get; set; }
        public string Weather { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
    }
}

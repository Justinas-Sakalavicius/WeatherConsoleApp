namespace Domain.Entities
{
    public class City
    {
        public City()
        {
            CityWeathers = new List<CityWeather>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        ICollection<CityWeather> CityWeathers { get; set; }
    }
}

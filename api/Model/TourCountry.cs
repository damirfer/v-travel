namespace Model
{
    public class TourCountry
    {
        public int TourId { get; set; }
        public Tour Tour { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}
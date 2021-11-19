namespace Model
{
    public class BookingTraveler
    {
        public int BookingId { get; set; }
        public Booking Booking { get; set; }

        public int TravelerId { get; set; }
        public Traveler Traveler { get; set; }
    }
}
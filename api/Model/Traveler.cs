using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Traveler
    {
        public int TravelerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public virtual IList<BookingTraveler> BookingTraveler { get; set; }
    }
}

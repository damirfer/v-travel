using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Booking
    {
        public int BookingId { get; set; }
        public string Name { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int GuideId { get; set; }
        public virtual Guide Guide { get; set; }

        public int TourId { get; set; }
        public virtual Tour Tour { get; set; }

        public virtual IList<BookingTraveler> BookingTraveler { get; set; }
        public IList<Message> Messages { get; set; }

    }
}

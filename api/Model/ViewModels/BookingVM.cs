using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ViewModels
{
    public class BookingVM
    {
        public class Single
        {
            public int BookingId { get; set; }
            public string Name { get; set; }
            public string DateFrom { get; set; }
            public string DateTo { get; set; }
            public int GuideId { get; set; }
            public string GuideName { get; set; }
            public int TourId { get; set; }
            public string TourName { get; set; }

        }
        public class List
        {
            public List<Single> Bookings { get; set; }
        }
    }
}

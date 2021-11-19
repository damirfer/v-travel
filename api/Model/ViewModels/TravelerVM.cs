using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ViewModels
{
    public class TravelerVM
    {
        public class DeleteModel
        {
            public int TravelerId { get; set; }
            public int BookingId { get; set; }
        }
    }
}

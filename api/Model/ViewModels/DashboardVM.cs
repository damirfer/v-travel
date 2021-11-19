using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ViewModels
{
    public class DashboardVM
    {
        public string UpcommingBookings { get; set; }
        public string TotalTravelers { get; set; }
        public string ThisMonthsBookings { get; set; }
        public string ActiveTours { get; set; }

        public Dictionary<string, int> Data { get; set; }
        public Dictionary<string, int> BookingData { get; set; }


    }
}

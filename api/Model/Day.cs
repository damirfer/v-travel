using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Model
{
    public class Day
    {
        public int DayId { get; set; }
        public int TourId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }

        public bool IsBIncluded { get; set; }
        public bool IsLIncluded { get; set; }
        public bool IsDIncluded { get; set; }

        public int? AccommodationId { get; set; }
        public Accommodation Accommodation { get; set; }

        public int? ScheduleId { get; set; }
        public Schedule Schedule { get; set; }

        public virtual IList<DayCountry> DayCountry { get; set; }
        public virtual IList<DayCity> DayCity { get; set; }


    }
}
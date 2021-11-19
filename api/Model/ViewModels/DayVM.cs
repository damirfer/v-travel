using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ViewModels
{
    public class DayVM
    {
        public class Update
        {
            public int TourId { get; set; }
            public int DayId { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string PhotoUrl { get; set; }
            public bool IsBIncluded { get; set; }
            public bool IsLIncluded { get; set; }
            public bool IsDIncluded { get; set; }
            public int? AccommodationId { get; set; }
            public List<int> Countries { get; set; }
            public List<int> Cities { get; set; }
        }
        public class ListItem
        {
            public int TourId { get; set; }
            public int DayId { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string PhotoUrl { get; set; }
            public bool IsBIncluded { get; set; }
            public bool IsLIncluded { get; set; }
            public bool IsDIncluded { get; set; }
            public int? ScheduleId { get; set; }
            public AccommodationVM.Single Accommodation { get; set; }
            public List<CountryVM.Info> Countries { get; set; }
            public List<CityVM.Info> Cities { get; set; }
        }
    }
}

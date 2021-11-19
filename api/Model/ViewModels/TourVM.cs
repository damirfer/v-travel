using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ViewModels
{
    public class TourVM
    {
        public class Mobile
        {
            public int TourId { get; set; }
            public string Name { get; set; }
            public int Duration { get; set; }
            public string Description { get; set; }
            public string PhotoUrl { get; set; }
            public AccommodationVM.Single Accommodation { get; set; }
            public List<CityVM.Single> Cities { get; set; }
            public List<CountryVM.Single> Countries { get; set; }
            public List<AmenityVM> HotelAmenities { get; set; }
            public List<AmenityVM> RoomAmenities { get; set; }
            public MealsVM IncludedMeals { get; set; }
            public ScheduleVM.List Schedule { get; set; }

        }
        public class Update
        {
            public int TourId { get; set; }
            public int Duration { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string PhotoUrl { get; set; }

            public List<int> TourCountry { get; set; }
            public List<TourSection> Sections { get; set; }
            public List<DayVM.ListItem> Days { get; set; }

        }
        public class List
        {
            public int TourId { get; set; }
            public string Name { get; set; }
            public int Duration { get; set; }
        }

        public class Info
        {
            public string PhotoUrl { get; set; }
            public string Name { get; set; }
            public string DateFrom { get; set; }
            public string DateTo { get; set; }
            public string TourDetails { get; set; }
            public List<TourSection> TourSections { get; set; }
            public List<CountryVM.Info> Countries { get; set; }
            public GuideVM.Single Guide { get; set; }

        }
    }
}

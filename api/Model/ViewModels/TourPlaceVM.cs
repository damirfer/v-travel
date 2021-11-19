using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ViewModels
{
    public class TourPlaceVM
    {
        public class Single
        {
            public string CityName { get; set; }
            public List<TourPlaceVM.List> PlaceList { get; set; }

        }

        public class List
        {
            public int PlaceId { get; set; }
            public string Name { get; set; }
            public string WorkingHours { get; set; }
            public string Description { get; set; }
            public string PhotoUrl { get; set; }
            public string Type { get; set; }

        }
    }
}

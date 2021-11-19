using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ViewModels
{
   public class PlaceVM
    {
        public class Single
        {
            public int PlaceId { get; set; }
            public string Name { get; set; }
            public string WorkingHours { get; set; }
            public string Description { get; set; }
            public string PhotoUrl { get; set; }
            public string CityName { get; set; }
            public string Type { get; set; }
            public int CityId { get; set; }
            public int CountryId { get; set; }
            public int PlaceTypeId { get; set; }
        }


        public class List
        {
            public int PlaceId { get; set; }
            public string Name { get; set; }
            public string WorkingHours { get; set; }
            public string Description { get; set; }
            public string PhotoUrl { get; set; }
            public string CityName { get; set; }
            public string Type { get; set; }

        }
    }
}

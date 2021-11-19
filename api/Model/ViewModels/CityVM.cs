using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ViewModels
{
    public class CityVM
    {
        public class Single
        {
            public int CityId { get; set; }
            public string Name { get; set; }
            public float Latitude { get; set; }
            public float Longitude { get; set; }
        }
        public class List
        {
            public int CityId { get; set; }
            public string Name { get; set; }
            public string Country { get; set; }

        }
        public class Info
        {
            public int CityId { get; set; }
            public string Name { get; set; }
            public string GeneralInfo { get; set; }
            public string PhotoUrl { get; set; }

        }
    }
}

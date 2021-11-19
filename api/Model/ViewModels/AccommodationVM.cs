using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ViewModels
{
    public class AccommodationVM
    {
        public class Single
        {
            public int? AccommodationId { get; set; }
            public string Name { get; set; }
        }
        public class List
        {
            public int AccommodationId { get; set; }
            public string Name { get; set; }
            public string City { get; set; }
            public string Country { get; set; }
            public string Type { get; set; }
        }
    }
}

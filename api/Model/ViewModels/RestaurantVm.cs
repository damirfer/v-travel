using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ViewModels
{
   public class RestaurantVm
    {
        public class Single
        {
            public int RestaurantId { get; set; }
            public string Name { get; set; }
            public string WorkingHours { get; set; }
            public string Description { get; set; }
            public string PhotoUrl { get; set; }
            public string CityName { get; set; }
        }


        public class List
        {
            public int RestaurantId { get; set; }
            public string Name { get; set; }
            public string WorkingHours { get; set; }
            public string Description { get; set; }
            public string PhotoUrl { get; set; }
            public string CityName { get; set; }
        }
    }
}

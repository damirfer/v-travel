using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
   public class Place
    {
        public int PlaceId { get; set; }
        public string Name { get; set; }
        public string WorkingHours { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }
        public int PlaceTypeId { get; set; }
        public virtual PlaceType PlaceType { get; set; }
    }
}

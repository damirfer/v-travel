using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class City
    {
        public int CityId { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public string GeneralInfo { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }

        public IList<CitySection> Sections { get; } = new List<CitySection>();

        public virtual IList<DayCity> DayCity { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Model
{
    public class Accommodation
    {
        public int AccommodationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string WebsiteUrl { get; set; }
        public string PhotoUrl { get; set; }

        public int CityId { get; set; }
        public virtual City City { get; set; }


        public int AccommodationTypeId { get; set; }
        public virtual AccommodationType AccommodationType { get; set; }

        public virtual IList<AccommodationAmenity> AccommodationAmenity { get; set; }


    }
}

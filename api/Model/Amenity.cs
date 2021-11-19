using System.Collections.Generic;

namespace Model
{
    public class Amenity
    {
        public int AmenityId { get; set; }
        public string Name { get; set; }

        public int AmenityTypeId { get; set; }
        public virtual AmenityType AmenityType { get; set; }

        public virtual IList<AccommodationAmenity> AccommodationAmenity { get; set; }

    }
}
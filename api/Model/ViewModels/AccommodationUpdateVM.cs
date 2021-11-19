using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model;

namespace Model.ViewModels
{
    public class AccommodationUpdateVM
    {
        public int AccommodationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string WebsiteUrl { get; set; }
        public string PhotoUrl { get; set; }
        public int AccommodationTypeId { get; set; }

        public int CityId { get; set; }


        public List<int> AccommodationAmenity { get; set; }

        public class Mobile
        {
            public int AccommodationId { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Address { get; set; }
            public string Telephone { get; set; }
            public string WebsiteUrl { get; set; }
            public string PhotoUrl { get; set; }
            public string CityName { get; set; }
            public string AccommodationType { get; set; }
            public List<AmenityVM> HotelAmenities { get; set; }
            public List<AmenityVM> RoomAmenities { get; set; }
        }


    }
}

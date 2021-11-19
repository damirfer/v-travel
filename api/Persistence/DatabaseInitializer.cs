using System;
using System.Collections.Generic;
using System.Text;
using Model;
using Persistence;

namespace Persistence
{
    public class DatabaseInitializer
    {
        public static void SeedCountry(LastaContext context)
        {

            context.Country.Add(new Country
            {
                Name = "Bosnia and Herzegovina",
                CapitalCity = "Sarajevo",
                Currency = "BAM",
                FlagUrl = "FlagUrl",
                GeneralInfo = "Bosnia and Herzegovina is awesome country",
                NationalDay = DateTime.Now,
                OfficialLanguage = "Bosnian/Croatian/Serbian",
                Polity = "Republic",
                Population = "1.5 mill",
                Area = 300,
            });
            context.CountrySection.Add(new CountrySection()
            {
                CountryId = 1,
                Title = "History",
                Content = "There is quite some history",
            });


            context.SaveChanges();
            }

        public static void SeedCity(LastaContext context)
        {

            context.City.Add(new City
            {
                Name = "Sarajevo",
                CountryId = 1,
                PhotoUrl = "UrlSlike"
            });

            context.CitySection.Add(new CitySection()
            {
                Title = "History",
                Content = "Sarajevo throught history was quite important city.",
            });

            context.City.Add(new City
            {
                Name = "Mostar",
                CountryId = 1,
                PhotoUrl = "UrlSlike"
            });

            context.CitySection.Add(new CitySection()
            {
                CityId = 2,
                Title = "History",
                Content = "Mostar has a rich history.",
            });

        }

        public static void SeedAccommodationType(LastaContext context)
        {


            context.AccommodationType.AddRange(new List<AccommodationType> {

                new AccommodationType(){Name="Hotel"},
                new AccommodationType(){Name="Hostel"},
                new AccommodationType(){Name="Motel"},
                new AccommodationType(){Name="Apartment"}


            });


            context.SaveChanges();
        }

        public static void SeedPlaceType(LastaContext context)
        {


            context.PlaceType.AddRange(new List<PlaceType> {

                new PlaceType(){PlaceTypeName="See"},
                new PlaceType(){PlaceTypeName="Do"},
                new PlaceType(){PlaceTypeName="Buy"},
                new PlaceType(){PlaceTypeName="Eat"},
                new PlaceType(){PlaceTypeName="Drink"},
                new PlaceType(){PlaceTypeName="Sleep"},
                new PlaceType(){PlaceTypeName="Other"},



            });


            context.SaveChanges();
        }

        public static void SeedAmenityType(LastaContext context)
        {


            context.AmenityType.AddRange(new List<AmenityType> {

                new AmenityType(){Name="Hotel"},
                new AmenityType(){Name="Room"},


            });


            context.SaveChanges();
        }

        public static void SeedAmenity(LastaContext context)
        {


            context.Amenity.AddRange(new List<Amenity> {

                new Amenity(){Name="Spa",AmenityTypeId=1},
                new Amenity(){Name="24-hour front desk",AmenityTypeId=1},
                new Amenity(){Name="Elevators",AmenityTypeId=1},
                new Amenity(){Name="Air conditioning",AmenityTypeId=2},
                new Amenity(){Name="Alarm clock",AmenityTypeId=2},
                new Amenity(){Name="Cable television",AmenityTypeId=2},

            });


            context.SaveChanges();
        }

        public static void SeedLanguages(LastaContext context)
        {


            context.Language.AddRange(new List<Language> {

                new Language(){ Name = "English"},
                new Language(){ Name = "German"},
                new Language(){ Name = "Spanish"},
                new Language(){ Name = "Italian"},

            });


            context.SaveChanges();
        }

        public static void SeedTransportTypes(LastaContext context)
        {


            context.TransportType.AddRange(new List<TransportType> {

                new TransportType(){ Name = "Taxi"},
                new TransportType(){ Name = "Bus"},
                new TransportType(){ Name = "Train"},
                new TransportType(){ Name = "Tram"},

            });


            context.SaveChanges();
        }

        public static void SeedTransport(LastaContext context)
        {


            //context.Transport.AddRange(new List<Transport> {

            //    new Transport(){ Name = "Info - Taxi", TransportTypeId = 1, CityId = 1,Phone = "043/666-333"}

            //});


            context.SaveChanges();
        }


    }
}

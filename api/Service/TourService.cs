using Microsoft.EntityFrameworkCore;
using Model;
using Model.ViewModels;
using Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Service
{
    public interface ITourService
    {
        IEnumerable<Tour> GetAll();
        bool Add(Tour model);
        bool Delete(int id);
        bool Update(Tour model);
        TourVM.Update Get(int id);
        List<TourVM.Mobile> ByBookingForMobile(int tourId);
        IEnumerable<TourVM.List> GetByIndex(int index);
        IEnumerable<TourVM.List> GetByName(string name);
        int GetCount();
        TourVM.Info GetInfo(int bookingId);

    }


    public class TourService : ITourService
    {
        private readonly LastaContext _TourDbContext;

        public TourService(
            LastaContext TourDbContext
        )
        {
            _TourDbContext = TourDbContext;
        }

        public IEnumerable<Tour> GetAll()
        {
            var result = new List<Tour>();

            try
            {
                result = _TourDbContext.Tour
                    .Where(x => x.IsTemplate)
                    .ToList();
            }
            catch (System.Exception e)
            {
                throw e;
            }

            return result;
        }

        public TourVM.Update Get(int id)
        {
            var result = new TourVM.Update();
            result.Sections = new List<TourSection>();

            try
            {
                result = _TourDbContext.Tour
                    .Include(x => x.Sections)
                    .Select(x => new TourVM.Update()
                    {
                        TourId = x.TourId,
                        Name = x.Name,
                        Duration = x.Duration,
                        PhotoUrl = Helper.Helper.GetImageUrl(x.PhotoUrl),
                        Description = x.Description,
                        Sections = x.Sections.ToList()

                    }).FirstOrDefault(x => x.TourId == id);

                result.TourCountry = new List<int>();

                foreach (var item in _TourDbContext.TourCountry.Where(x => x.TourId == id).ToList())
                    result.TourCountry.Add(item.CountryId);

                result.Days = _TourDbContext.Day.Where(x => x.TourId == id)
                    .Select(x => new DayVM.ListItem()
                    {
                        TourId = x.TourId,
                        DayId = x.DayId,
                        Name = x.Name,
                        Description = x.Description,
                        PhotoUrl = Helper.Helper.GetImageUrl(x.PhotoUrl),
                        IsBIncluded = x.IsBIncluded,
                        IsDIncluded = x.IsDIncluded,
                        IsLIncluded = x.IsLIncluded,
                        ScheduleId = x.ScheduleId

                    }).ToList();


                foreach (var day in result.Days)
                {
                    day.Countries = new List<CountryVM.Info>();
                    day.Cities = new List<CityVM.Info>();
                    day.Accommodation = new AccommodationVM.Single();



                    foreach (var item in _TourDbContext.DayCountry.Where(x => x.DayId == day.DayId).Select(x => x.Country).ToList())
                        day.Countries.Add(new CountryVM.Info
                        {
                            CountryId = item.CountryId,
                            Name = item.Name,
                            FlagUrl = Helper.Helper.GetImageUrl(item.FlagUrl)
                        });

                    foreach (var item in _TourDbContext.DayCity.Where(x => x.DayId == day.DayId).Select(x => x.City).ToList())
                        day.Cities.Add(new CityVM.Info
                        {
                            CityId = item.CityId,
                            Name = item.Name,
                            PhotoUrl = Helper.Helper.GetImageUrl(item.PhotoUrl)
                        });
                    day.Accommodation = _TourDbContext.Day.Where(x => x.DayId == day.DayId)
                        .Include(x => x.Accommodation)
                        .Select(x => new AccommodationVM.Single
                        {
                            AccommodationId = x.Accommodation.AccommodationId,
                            Name = x.Accommodation.Name
                        }).FirstOrDefault();
                }
            }
            catch (System.Exception test)
            {

            }

            return result;

        }

        public IEnumerable<TourVM.List> GetByIndex(int index)
        {
            var result = new List<TourVM.List>();

            try
            {
                result = _TourDbContext.Tour
                    .Where(x => x.IsTemplate)
                    .Skip(index * 10).Take(10)
                    .Select(x => new TourVM.List()
                    {
                        TourId = x.TourId,
                        Duration = x.Duration,
                        Name = x.Name
                    })
                    .ToList();
            }
            catch (System.Exception)
            {

            }

            return result;
        }

        public IEnumerable<TourVM.List> GetByName(string name)
        {
            var result = new List<TourVM.List>();

            try
            {
                result = _TourDbContext
                           .Tour
                         .Where(x => x.Name.ToLower().StartsWith(name.ToLower()) && x.IsTemplate)
                         .Select(x => new TourVM.List()
                         {
                             TourId = x.TourId,
                             Duration = x.Duration,
                             Name = x.Name
                         })
                         .ToList();
            }
            catch (System.Exception)
            {

            }

            return result;
        }

        public bool Add(Tour model)
        {
            try
            {
                for (int i = 0; i < model.Duration; i++)
                    model.Day.Add(new Day()
                    {
                        Schedule = new Schedule()
                    });

                var imageName = model.PhotoUrl;
                model.IsTemplate = true;
                _TourDbContext.Tour.Add(model);
                _TourDbContext.SaveChanges();



            }
            catch (System.Exception test)
            {
                return false;
            }

            return true;
        }

        public bool Update(Tour model)
        {
            try
            {
                var originalModel = _TourDbContext.Tour
                    .Include(x => x.TourCountry)
                    .Single(x =>
                    x.TourId == model.TourId
                );

                originalModel.Name = model.Name;
                originalModel.Duration = model.Duration;
                if (model.PhotoUrl != string.Empty)
                    originalModel.PhotoUrl = model.PhotoUrl;
                originalModel.Description = model.Description;

                var sections = _TourDbContext.TourSection.Where(x => x.TourId == originalModel.TourId).ToList();
                if (sections.Any())
                {
                    _TourDbContext.TourSection.RemoveRange(sections);
                }
                _TourDbContext.SaveChanges();

                if (model.Sections != null)
                    foreach (var item in model.Sections)
                    {
                        _TourDbContext.TourSection.Add(new TourSection()
                        {
                            TourId = originalModel.TourId,
                            Content = item.Content,
                            Title = item.Title
                        });
                    }


                if (originalModel.TourCountry.Any())
                {
                    _TourDbContext.TourCountry.RemoveRange(originalModel.TourCountry);
                    _TourDbContext.SaveChanges();
                }

                if (model.TourCountry != null)
                    foreach (var item in model.TourCountry)
                    {
                        originalModel.TourCountry.Add(new TourCountry()
                        {
                            TourId = originalModel.TourId,
                            CountryId = item.CountryId,
                        });
                    }

                _TourDbContext.Tour.Update(originalModel);
                _TourDbContext.SaveChanges();
            }
            catch (System.Exception test)
            {
                return false;
            }

            return true;
        }

        public bool Delete(int id)
        {
            try
            {
                var Tour = _TourDbContext.Tour.Find(id);
                _TourDbContext.Entry(Tour).State = EntityState.Deleted;

                _TourDbContext.SaveChanges();
            }
            catch (System.Exception)
            {
                return false;
            }

            return true;
        }

        public List<TourVM.Mobile> ByBookingForMobile(int tourId)
        {
            List<TourVM.Mobile> result = new List<TourVM.Mobile>();

            var days = _TourDbContext.Day
                .Include(x => x.Accommodation)
                    .ThenInclude(y => y.AccommodationAmenity)
                .Include(x => x.Schedule)
                    .ThenInclude(y => y.ScheduleItems)
                .Include(x => x.DayCountry)
                .Include(x => x.DayCity)
                .Where(x => x.TourId == tourId)
                .OrderBy(x=> x.DayId)
                .ToList();

            TourVM.Mobile dayMobile;
            foreach (var day in days)
            {
                dayMobile = new TourVM.Mobile();
                dayMobile.TourId = day.TourId;
                dayMobile.Description = day.Description;
                dayMobile.Duration = days.Count;
                dayMobile.PhotoUrl = Helper.Helper.GetImageUrl(day.PhotoUrl);
                dayMobile.Name = day.Name;

                if(day.Accommodation != null)
                {
                    dayMobile.Accommodation = new AccommodationVM.Single
                    {
                        AccommodationId = day.Accommodation.AccommodationId,
                        Name = day.Accommodation.Name
                    };
                }

                dayMobile.HotelAmenities = new List<AmenityVM>();
                dayMobile.RoomAmenities = new List<AmenityVM>();

                foreach (var amenity in day.Accommodation.AccommodationAmenity)
                {
                    var a = _TourDbContext.Amenity
                                          .Include(x => x.AmenityType)
                                          .FirstOrDefault(x => x.AmenityId == amenity.AmenityId);

                    if (a.AmenityType.Name == "Hotel")
                    {
                        dayMobile.HotelAmenities.Add(new AmenityVM
                        {
                            AmenityId = a.AmenityId,
                            Name = a.Name
                        });
                    }
                    else
                    {
                        dayMobile.RoomAmenities.Add(new AmenityVM
                        {
                            AmenityId = a.AmenityId,
                            Name = a.Name
                        });
                    }


                }

                dayMobile.Cities = new List<CityVM.Single>();
                foreach (var city in day.DayCity)
                {
                    var c = _TourDbContext.City
                                          .FirstOrDefault(x => x.CityId == city.CityId);

                    dayMobile.Cities.Add(new CityVM.Single
                    {
                        CityId = c.CityId,
                        Name = c.Name,
                        Latitude = c.Latitude,
                        Longitude = c.Longitude
                    });
                }

                dayMobile.Countries = new List<CountryVM.Single>();
                foreach (var city in day.DayCountry)
                {
                    var c = _TourDbContext.City
                                          .FirstOrDefault(x => x.CountryId == city.CountryId);

                    dayMobile.Countries.Add(new CountryVM.Single
                    {
                        CountryId = c.CountryId,
                        Name = c.Name
                    });
                }

                dayMobile.IncludedMeals = new MealsVM
                {
                    IsBreakfastIncluded = day.IsBIncluded,
                    IsLunchIncluded = day.IsLIncluded,
                    IsDinnerIncluded = day.IsDIncluded
                };

                dayMobile.Schedule = new ScheduleVM.List();

                dayMobile.Schedule.ScheduleId = day.Schedule.ScheduleId;

                dayMobile.Schedule.ScheduleItems = new List<ScheduleVM.Item>();
                foreach (var scheduleItem in day.Schedule.ScheduleItems.OrderBy(x => x.TimeStamp))
                {
                    dayMobile.Schedule.ScheduleItems.Add(new ScheduleVM.Item
                    {
                        ScheduleItemId = scheduleItem.ScheduleItemId,
                        Content = scheduleItem.Content,
                        TimeStamp = scheduleItem.TimeStamp.ToString("HH:mm"),
                        Url = scheduleItem.Url,
                        PlaceId = scheduleItem.PlaceId,
                        AccommodationId = scheduleItem.AccommodationId

                    });
                }


                result.Add(dayMobile);

            }
            return result;
        }
        public int GetCount()
        {
            decimal temp = 0;

            try
            {
                temp = _TourDbContext.Tour
                    .Where(x => x.IsTemplate)
                    .Count();
            }
            catch (System.Exception)
            {

            }
            temp /= 10;
            //Gets decimal of number - if number is 10.45 dec = 0.45
            var dec = temp - Math.Truncate(temp);

            int result = (int)temp;

            if (dec != 0)
                result++;

            return result;
        }
        public TourVM.Info GetInfo(int BookingId)
        {
            TourVM.Info info = new TourVM.Info();
            Tour tour = _TourDbContext.Booking.Where(x => x.BookingId == BookingId).Select(x => x.Tour)
                .Include(x => x.TourCountry)
                .Include(x => x.Sections)
                .FirstOrDefault();



            Booking book = _TourDbContext.Booking.Where(x => x.BookingId == BookingId)
                  .Include(x => x.Guide).FirstOrDefault();
            info.PhotoUrl = Helper.Helper.GetImageUrl(tour.PhotoUrl);
            info.DateTo = book.DateTo.ToString("dd-MM-yyyy");
            info.DateFrom = book.DateFrom.ToString("dd-MM-yyyy");
            info.Guide = new GuideVM.Single
            {
                Bio = book.Guide.Bio,
                FirstName = book.Guide.FirstName,
                LastName = book.Guide.LastName,
                PhotoUrl = Helper.Helper.GetImageUrl(book.Guide.PhotoUrl)
            };
            info.TourSections = tour.Sections.ToList();
            info.Name = tour.Name;
            info.TourDetails = tour.Description;
            info.Countries = new List<CountryVM.Info>();
            foreach (var country in tour.TourCountry)
            {
                var c = _TourDbContext.Country.Where(x => x.CountryId == country.CountryId).Select(x => new CountryVM.Info
                {
                    CountryId = x.CountryId,
                    FlagUrl = Helper.Helper.GetImageUrl(x.FlagUrl),
                    Name = x.Name
                }).FirstOrDefault();
                info.Countries.Add(c);
            }


            return info;


        }
    }
}

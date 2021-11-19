using Model;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Model.ViewModels;

namespace Service
{
    public interface IBookingService
    {
        bool Add(Booking model);
        IEnumerable<BookingVM.Single> GetAll();
        bool Delete(int id);
        int GetCount();
        bool Update(Booking model);
        BookingVM.Single Get(int id);
        IEnumerable<BookingVM.Single> GetByIndex(int index);
        IEnumerable<BookingVM.Single> GetByName(string name);
        DataVM GetData(int travelerId);
        List<CityVM.Single> GetCitiesByDay(int bookingId);


    }
    public class BookingService : IBookingService
    {
        public readonly LastaContext _BookingDbContext;
        public BookingService(LastaContext BookingDbContext)
        {
            _BookingDbContext = BookingDbContext;
        }

        public List<CityVM.Single> GetCitiesByDay(int bookingId)
        {
            List<CityVM.Single> cities = new List<CityVM.Single>();
            int tourId = _BookingDbContext.Booking.Where(x => x.BookingId == bookingId).Select(x => x.TourId).FirstOrDefault();

            DateTime bookingStartingDate = _BookingDbContext.Booking.Where(x => x.BookingId == bookingId).Select(x => x.DateFrom).FirstOrDefault();
            DateTime currentDate = DateTime.Now;
            var currentTourDay = currentDate.DayOfYear - bookingStartingDate.DayOfYear;

            var dayId = _BookingDbContext.Day
                        .Where(x => x.TourId == tourId)
                        .Skip(currentTourDay)
                        .Take(1)
                        .Select(x => x.DayId)
                        .FirstOrDefault();

            cities = _BookingDbContext.DayCity
                        .Where(x => x.DayId == dayId)
                        .Include(x => x.City)
                        .Select(x=> new CityVM.Single
                        {
                            CityId = x.CityId,
                            Name = x.City.Name
                        }).ToList();

            return cities;
        }

        public IEnumerable<BookingVM.Single> GetAll()
        {
            var result = new List<BookingVM.Single>();

            try
            {
                result = _BookingDbContext.Booking
                                    .Include(x => x.Guide)
                                    .Include(x => x.Tour)
                                    .Select(x => new BookingVM.Single()
                                    {
                                        Name=x.Name,
                                        BookingId = x.BookingId,
                                        DateFrom = x.DateFrom.ToString("yyyy-MM-dd"),
                                        DateTo = x.DateTo.ToString("yyyy-MM-dd"),
                                        GuideId = x.GuideId,
                                        GuideName = x.Guide.FirstName + " " + x.Guide.LastName,
                                        TourId = x.TourId,
                                        TourName = x.Tour.Name
                                    }).ToList();
            }
            catch (System.Exception)
            {

            }

            return result;
        }

        public IEnumerable<BookingVM.Single> GetByIndex(int index)
        {
            var result = new List<BookingVM.Single>();

            try
            {
                result = _BookingDbContext.Booking
                    .Include(x => x.Tour)
                    .Include(x=> x.Guide)
                    .Skip(index * 10).Take(10)
                    .Select(x => new BookingVM.Single()
                    {
                        Name = x.Name,
                        BookingId = x.BookingId,
                        DateFrom = x.DateFrom.ToString("yyyy-MM-dd"),
                        DateTo = x.DateTo.ToString("yyyy-MM-dd"),
                        GuideId = x.GuideId,
                        GuideName = x.Guide.FirstName + " " + x.Guide.LastName,
                        TourId = x.TourId,
                        TourName = x.Tour.Name


                    })
                    .ToList();
            }
            catch (System.Exception)
            {

            }

            return result;
        }

        public IEnumerable<BookingVM.Single> GetByName(string name)
        {
            var result = new List<BookingVM.Single>();

            try
            {
                result = _BookingDbContext.Booking
                    .Include(x => x.Tour)
                    .Include(x => x.Guide)
                    .Where(x=> x.Name.ToLower().StartsWith(name.ToLower()))
                    .Select(x => new BookingVM.Single()
                    {
                        Name = x.Name,
                        BookingId = x.BookingId,
                        DateFrom = x.DateFrom.ToString("yyyy-MM-dd"),
                        DateTo = x.DateTo.ToString("yyyy-MM-dd"),
                        GuideId = x.GuideId,
                        GuideName = x.Guide.FirstName + " " + x.Guide.LastName,
                        TourId = x.TourId,
                        TourName = x.Tour.Name


                    })
                    .ToList();
            }
            catch (System.Exception e)
            {

            }

            return result;
        }

        public BookingVM.Single Get(int id)
        {
            return _BookingDbContext.Booking
                                    .Include(x => x.Guide)
                                    .Include(x => x.Tour)
                                    .Select(x => new BookingVM.Single()
                                    {
                                        Name=x.Name,
                                        BookingId = x.BookingId,
                                        DateFrom = x.DateFrom.ToString("yyyy-MM-dd"),
                                        DateTo = x.DateTo.ToString("yyyy-MM-dd"),
                                        GuideId = x.GuideId,
                                        GuideName = x.Guide.FirstName + " " + x.Guide.LastName,
                                        TourId = x.TourId,
                                        TourName = x.Tour.Name

                                    }).FirstOrDefault(x => x.BookingId == id);
        }

        public bool Add(Booking model)
        {
            try
            {

                CopyData(model);
                _BookingDbContext.Booking.Add(model);
                _BookingDbContext.SaveChanges();

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
                var booking = _BookingDbContext.Booking.Find(id);
                DeleteOldData(id);
                _BookingDbContext.BookingTraveler.RemoveRange(booking.BookingTraveler);
                _BookingDbContext.Entry(booking).State = EntityState.Deleted;
                _BookingDbContext.SaveChanges();
            }
            catch (System.Exception)
            {
                return false;
            }

            return true;

        }

        public bool Update(Booking model)
        {
            try
            {
                var originalModel = _BookingDbContext.Booking
                    .Include(x => x.Tour)
                        .ThenInclude(y=> y.Day)
                    .Single(x =>
                    x.BookingId == model.BookingId
                );
                var oldTourId = originalModel.TourId;
                originalModel.Name = model.Name;
                originalModel.DateFrom = model.DateFrom;
                originalModel.DateTo = CalculateDateTo(model.DateFrom, _BookingDbContext.Tour.Where(x=> x.TourId==model.TourId).FirstOrDefault().Day.Count());
                originalModel.GuideId = model.GuideId;
                if (model.TourId != originalModel.TourId)
                {
                    CopyData(model);
                    DeleteOldData(oldTourId);
                    originalModel.TourId = model.TourId;

                }

                _BookingDbContext.Booking.Update(originalModel);
                _BookingDbContext.SaveChanges();

                _BookingDbContext.Tour.Remove(_BookingDbContext.Tour.Single(x => x.TourId == oldTourId));
                _BookingDbContext.SaveChanges();
            }
            catch (System.Exception test)
            {
                return false;
            }

            return true;

        }

        public int GetCount()
        {
            decimal temp = 0;

            try
            {
                temp = _BookingDbContext.Booking.Count();
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
        private bool DeleteOldData(int tourId)
        {
            try
            {
                _BookingDbContext.TourCountry
                    .RemoveRange(_BookingDbContext.TourCountry.Where(x => x.TourId == tourId).ToList());


                List<Day> days = _BookingDbContext.Day
                    .Include(x => x.Schedule)
                        .ThenInclude(y => y.ScheduleItems)
                    .Include(x => x.DayCity)
                    .Include(x => x.DayCountry)
                    .Where(x => x.TourId == tourId).ToList();
                foreach (var day in days)
                {
                    _BookingDbContext.DayCity.RemoveRange(day.DayCity);
                    _BookingDbContext.DayCountry.RemoveRange(day.DayCountry);

                }

                _BookingDbContext.Day.RemoveRange(days);
                _BookingDbContext.SaveChanges();




            }
            catch (System.Exception test)
            {
                return false;
            }
            return true;

        }

        private DateTime CalculateDateTo(DateTime dateFrom, int tourLength)
        {
            return dateFrom.AddDays(tourLength);
        }

        private bool CopyData(Booking model)
        {
            try
            {
                Tour copy = _BookingDbContext.Tour
                    .AsNoTracking()
                    .Include(x => x.TourCountry)
                    .Include(x=> x.Sections)
                    .Where(x => x.TourId == model.TourId)
                    .FirstOrDefault();
                copy.TourId = 0;
                copy.IsTemplate = false;

                foreach (var tc in copy.TourCountry)
                {
                    tc.TourId = 0;
                }
                var sections = copy.Sections;
                var newSections = new List<TourSection>();
                copy.Sections = null;
              

                _BookingDbContext.Tour.Add(copy);
                _BookingDbContext.SaveChanges();
                var tour = _BookingDbContext.Tour.OrderByDescending(x => x.TourId).FirstOrDefault();
                foreach (var s in sections)
                {
                    var ns = new TourSection
                    {
                        Content = s.Content,
                        Title = s.Title,
                        TourId = tour.TourId
                    };
                    newSections.Add(ns);
                }

                tour.Sections = newSections;
                _BookingDbContext.SaveChanges();

                List<Day> copyDay = _BookingDbContext.Day
                    .AsNoTracking()
                    .Include(x => x.Schedule)
                        .ThenInclude(y => y.ScheduleItems)
                    .Include(x => x.DayCity)
                    .Include(x => x.DayCountry)
                    .Where(x => x.TourId == model.TourId).ToList();

                foreach (var item in copyDay)
                {
                    item.Schedule.ScheduleId = 0;
                    foreach (var scItem in item.Schedule.ScheduleItems)
                    {
                        scItem.ScheduleId = 0;
                        scItem.ScheduleItemId = 0;
                    }
                    _BookingDbContext.Schedule.Add(item.Schedule);
                    _BookingDbContext.SaveChanges();
                    item.ScheduleId = item.Schedule.ScheduleId;
                }



                foreach (var dc in copyDay)
                {
                    dc.TourId = copy.TourId;
                    dc.DayId = 0;
                    foreach (var item in dc.DayCity)
                    {
                        item.DayId = 0;
                    }
                    foreach (var item in dc.DayCountry)
                    {
                        item.DayId = 0;
                    }


                }

                _BookingDbContext.Day.AddRange(copyDay);
                _BookingDbContext.SaveChanges();
                model.TourId = copy.TourId;
            }
            catch (System.Exception e)
            {
                return false;
            }

            return true;
        }


        public DataVM GetData(int travelerId)
        {
            DataVM vm = new DataVM();
            try
            {
                Booking result = _BookingDbContext.Booking
                     .Where(x => x.BookingTraveler.Any(y => y.TravelerId == travelerId)).FirstOrDefault();
                Traveler t = _BookingDbContext.Traveler.FirstOrDefault(x => x.TravelerId == travelerId);

                vm.BookingId = result.BookingId;
                vm.TourId = result.TourId;
                vm.TravelerName = t.FirstName + " " + t.LastName;

            }
            catch (System.Exception test)
            {

            }
            return vm;
        }


    }
}

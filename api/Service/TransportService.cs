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
    public interface ITransportService
    {
        IEnumerable<TransportVM.List> GetAll();
        bool Add(Transport model);
        bool Delete(int id);
        bool Update(Transport model);
        TransportVM.Single Get(int id);
        IEnumerable<Transport> GetByCities(int[] cityId);
        IEnumerable<TransportVM.List> GetByIndex(int index);
        IEnumerable<TransportVM.List> GetByName(string name);
        int GetCount();
        List<TransportType> GetTransportTypes();
        List<TransportVM.Single> GetByCityAndType(int transportTypeId, int cityId);
        IEnumerable<TourTransportVM.Single> GetByTour(int tourId);

    }
    public class TransportService : ITransportService
    {
        private readonly LastaContext _TransportDbContext;

        public TransportService(LastaContext TransportDbContext)
        {
            _TransportDbContext = TransportDbContext;
        }

        public IEnumerable<TransportVM.List> GetAll()

        {
            var result = new List<TransportVM.List>();
            try
            {
                result = _TransportDbContext.Transport
                    .Include(x => x.City)
                    .Include(x => x.TransportType)

                    .Select(x => new TransportVM.List
                    {
                        Name = x.Name,
                        City = x.City.Name,
                        Phone = x.Phone,
                        PhotoUrl = x.PhotoUrl,
                        TransportId = x.TransportId,
                        TransportType = x.TransportType.Name,
                        WebsiteUrl = x.WebsiteUrl


                    })
                    .ToList();

            }
            catch (System.Exception)
            {

            }

            return result;
        }

        public TransportVM.Single Get(int id)
        {
            var result = new TransportVM.Single();

            try
            {
                result = _TransportDbContext.Transport.Include(x => x.City)
                    .Include(x => x.TransportType)
                    .Select(x => new TransportVM.Single()
                    {
                        Name = x.Name,
                        City = x.City.CityId.ToString(),
                        Phone = x.Phone,
                        PhotoUrl = Helper.Helper.GetImageUrl(x.PhotoUrl),
                        TransportId = x.TransportId,
                        TransportType = x.TransportType.Name,
                        TransportTypeId = x.TransportType.TransportTypeId,
                        WebsiteUrl = x.WebsiteUrl


                    })
                    .Single(x => x.TransportId == id);



            }
            catch (System.Exception)
            {

            }

            return result;
        }

        public bool Add(Transport model)
        {
            try
            {
                _TransportDbContext.Add(model);
                _TransportDbContext.SaveChanges();
            }
            catch (System.Exception e)
            {
                throw e;
            }

            return true;
        }

        public bool Update(Transport model)
        {
            try
            {
                _TransportDbContext.Transport.Update(model);
                _TransportDbContext.SaveChanges();
            }
            catch (System.Exception e)
            {
                throw e;
            }

            return true;
        }

        public bool Delete(int id)
        {
            try
            {
                var Transport = _TransportDbContext.Transport.Find(id);
                _TransportDbContext.Entry(Transport).State = EntityState.Deleted;

                _TransportDbContext.SaveChanges();
            }
            catch (System.Exception)
            {
                return false;
            }

            return true;
        }

        public IEnumerable<Transport> GetByCities(int[] cityId)
        {
            var result = new List<Transport>();
            try
            {
                foreach (var id in cityId)
                {
                    var Transports = _TransportDbContext.Transport.Where(x => x.CityId == id).ToList();
                    foreach (var transport in Transports)
                    {
                        result.Add(transport);
                    }
                }

            }
            catch (System.Exception)
            {

            }

            return result;
        }

        public IEnumerable<TransportVM.List> GetByIndex(int index)
        {
            var result = new List<TransportVM.List>();

            try
            {
                result = _TransportDbContext.Transport

                    .Include(x => x.City)
                    .Include(x => x.TransportType)
                    .Skip(index * 10).Take(10)
                    .Select(x => new TransportVM.List()
                    {

                        Name = x.Name,
                        City = x.City.Name,
                        Phone = x.Phone,
                        PhotoUrl = x.PhotoUrl,
                        TransportId = x.TransportId,
                        TransportType = x.TransportType.Name,
                        WebsiteUrl = x.WebsiteUrl

                    })
                    .ToList();
            }
            catch (System.Exception)
            {

            }

            return result;
        }

        public IEnumerable<TransportVM.List> GetByName(string name)
        {
            var result = new List<TransportVM.List>();

            try
            {
                result = _TransportDbContext
                           .Transport

                           .Include(x => x.City)
                           .Include(x => x.TransportType)

                         .Where(x => x.Name.ToLower().StartsWith(name.ToLower()))
                         .Select(x => new TransportVM.List()
                         {
                             Name = x.Name,
                             City = x.City.Name,
                             Phone = x.Phone,
                             PhotoUrl = x.PhotoUrl,
                             TransportId = x.TransportId,
                             TransportType = x.TransportType.Name,
                             WebsiteUrl = x.WebsiteUrl
                         })
                         .ToList();
            }
            catch (System.Exception)
            {

            }

            return result;
        }

        public int GetCount()
        {
            decimal temp = 0;

            try
            {
                temp = _TransportDbContext.Transport.Count();
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

        public List<TransportType> GetTransportTypes()
        {
            return _TransportDbContext.TransportType.ToList();
        }

        public List<TransportVM.Single> GetByCityAndType(int transportTypeId, int cityId)
        {
            List<TransportVM.Single> transports = new List<TransportVM.Single>();

            transports = _TransportDbContext.Transport
                        .Where(x => x.TransportTypeId == transportTypeId && x.CityId == cityId)
                        .Select(x => new TransportVM.Single
                        {
                            TransportId = x.TransportId,
                            Name = x.Name,
                            PhotoUrl = Helper.Helper.GetImageUrl(x.PhotoUrl),
                            Phone = x.Phone,
                            WebsiteUrl = x.WebsiteUrl
                        }).ToList();

            return transports;
        }
        public IEnumerable<TourTransportVM.Single> GetByTour(int tourId)
        {

            List<TourTransportVM.Single> res = new List<TourTransportVM.Single>();

            List<City> cities = _TransportDbContext.DayCity.Where(dc => dc.Day.TourId == tourId).Select(dc => dc.City).Distinct().ToList();

            foreach (var city in cities)
            {
                List<TourTransportVM.List> CurrentTransports = _TransportDbContext.Transport.Where(t => t.CityId == city.CityId)
                                                                .Select(t => new TourTransportVM.List
                                                                {
                                                                    TransportId = t.TransportId,
                                                                    Name = t.Name,
                                                                    Phone = t.Phone,
                                                                    PhotoUrl = Helper.Helper.GetImageUrl(t.PhotoUrl),
                                                                    TransportType = t.TransportType.Name
                                                                }).ToList();
                res.Add(new TourTransportVM.Single
                {
                    CityName = city.Name,
                    TransportList = CurrentTransports
                });

            }
            return res;
        }
    }
}

using Microsoft.AspNetCore.Hosting;
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
    public interface ICountryService
    {
        IEnumerable<Country> GetAll();
        IEnumerable<Country> GetByIndex(int index);
        IEnumerable<Country> GetByName(string name);
        int GetCount();
        bool Add(Country model);
        bool Delete(int id);
        bool Update(Country model);
        Country Get(int id);
        CountryVM.Details GetInfo(int id);
        bool DeleteSection(int countryId, int sectionId);
        IEnumerable<Country> GetByTour(int tourId);
    }

    public class CountryService : ICountryService
    {
        private readonly LastaContext _CountryDbContext;
        private readonly IHostingEnvironment _environment;
        public CountryService(
            LastaContext CountryDbContext,IHostingEnvironment environment
        )
        {
            _CountryDbContext = CountryDbContext;
            _environment = environment;
        }

        public IEnumerable<Country> GetAll()
        {
            var result = new List<Country>();

            try
            {
                result = _CountryDbContext.Country.Include(x => x.Sections).ToList();
            }
            catch (System.Exception e)
            {
                throw e;
            }

            return result;
        }

        public IEnumerable<Country> GetByIndex(int index)
        {
            var result = new List<Country>();

            try
            {
                result = _CountryDbContext.Country.Skip(index * 10).Take(10).ToList();
            }
            catch (System.Exception)
            {

            }

            return result;
        }

        public IEnumerable<Country> GetByName(string name)
        {
            var result = new List<Country>();

            try
            {
                result = _CountryDbContext.Country
                         .Where(x => x.Name.ToLower().StartsWith(name.ToLower())).ToList();
            }
            catch (System.Exception)
            {

            }

            return result;
        }

        public Country Get(int id)
        {
            var result = new Country();

            try
            {
                result = _CountryDbContext.Country.Include(x => x.Sections).Single(x => x.CountryId == id);
                result.FlagUrl = Helper.Helper.GetImageUrl(result.FlagUrl);
            }
            catch (System.Exception)
            {

            }

            return result;
        }
        public CountryVM.Details GetInfo(int id)
        {
            var result = new CountryVM.Details();
            try
            {
                result = _CountryDbContext.Country
                    .Where(x => x.CountryId == id)
                    .Select(x=> new CountryVM.Details {
                        Area = x.Area,
                        CapitalCity = x.CapitalCity,
                        Currency = x.Currency,
                        FlagUrl = Helper.Helper.GetImageUrl(x.FlagUrl),
                        GeneralInfo = x.GeneralInfo,
                        Name = x.Name,
                        NationalDay = x.NationalDay.ToString("dd-MM-yyyy"),
                        OfficialLanguage = x.OfficialLanguage,
                        Polity = x.Polity,
                        Population = x.Population,
                    }).FirstOrDefault();

                var countrySections = _CountryDbContext.CountrySection.Where(x => x.CountryId == id).ToList();
                result.Sections = new List<CountrySection>();

                foreach (var section in countrySections)
                {
                    result.Sections.Add(section);
                }

                result.Cities = _CountryDbContext.City.Where(x => x.CountryId == id)
                                .Select(x => new CityVM.Info
                                {
                                    CityId = x.CityId,
                                    Name = x.Name,
                                    GeneralInfo = x.GeneralInfo,
                                    PhotoUrl = Helper.Helper.GetImageUrl(x.PhotoUrl)
                                }).ToList();

            }
            catch (System.Exception e )
            {
                throw e;
            }

            return result;
        }

        public int GetCount()
        {
            decimal temp = 0;

            try
            {
                temp = _CountryDbContext.Country.Count();
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

        public bool Add(Country model)
        {
            try
            {
                _CountryDbContext.Country.Add(model);
                _CountryDbContext.SaveChanges();
            }
            catch (System.Exception)
            {
                return false;
            }

            return true;
        }

        public bool Update(Country model)
        {
            try
            {
                var originalModel = _CountryDbContext.Country.Single(x =>
                    x.CountryId == model.CountryId
                );

                originalModel.CapitalCity = model.CapitalCity;
                originalModel.Area = model.Area;
                originalModel.Currency = model.Currency;
                originalModel.OfficialLanguage = model.OfficialLanguage;
                originalModel.Polity = model.Polity;
                originalModel.Population = model.Population;
                originalModel.NationalDay = model.NationalDay;
                originalModel.GeneralInfo = model.GeneralInfo;
                if (model.FlagUrl != string.Empty)
                    originalModel.FlagUrl = model.FlagUrl;

                originalModel.Name = model.Name;
                var sections = _CountryDbContext.CountrySection.Where(x => x.CountryId == model.CountryId).ToList();
                if (sections.Any())
                    _CountryDbContext.CountrySection.RemoveRange(sections);

                foreach (var s in model.Sections)
                {
                    _CountryDbContext.CountrySection.Add(new CountrySection()
                    {
                        CountryId = model.CountryId,
                        Title = s.Title,
                        Content = s.Content
                    });
                }

                _CountryDbContext.Country.Update(originalModel);
                _CountryDbContext.SaveChanges();
            }
            catch (System.Exception)
            {
                return false;
            }

            return true;
        }

        public bool Delete(int id)
        {
            try
            {
                var country = _CountryDbContext.Country.Find(id);
                _CountryDbContext.Entry(country).State = EntityState.Deleted;
                _CountryDbContext.CountrySection.RemoveRange(country.Sections.ToList());
                _CountryDbContext.SaveChanges();
            }
            catch (System.Exception)
            {
                return false;
            }

            return true;
        }

        public bool DeleteSection(int countryId, int sectionId)
        {
            try
            {
                _CountryDbContext.Country.Where(x => x.CountryId == countryId).First().Sections.RemoveAt(sectionId);
                _CountryDbContext.Entry(new CountrySection { CountrySectionId = sectionId }).State = EntityState.Deleted;
                _CountryDbContext.SaveChanges();
            }
            catch (System.Exception)
            {
                return false;
            }

            return true;
        }
        public IEnumerable<Country> GetByTour(int tourId)
        {
            var result = new List<Country>();

            try
            {
                result = _CountryDbContext.TourCountry
                         .Where(x => x.TourId == tourId).Select(x => x.Country).ToList();

                foreach(var item in result)
                {
                    item.FlagUrl = Helper.Helper.GetImageUrl(item.FlagUrl);
                }
            }
            catch (System.Exception)
            {

            }

            return result;
        }
    }
}

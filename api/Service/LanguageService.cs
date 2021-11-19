using Model;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public interface ILanguageService
    {
        IEnumerable<Language> GetAll();
        IEnumerable<Language> GetByIndex(int index);
        IEnumerable<Language> GetByName(string name);
        int GetCount();
        bool Add(Language model);
        bool Delete(int id);
        bool Update(Language model);
        Language Get(int id);
    }

    public class LanguageService:ILanguageService
    {
        private readonly LastaContext _LanguageDbContext;

        public LanguageService(
            LastaContext LanguageDbContext
        )
        {
            _LanguageDbContext = LanguageDbContext;
        }

        public IEnumerable<Language> GetAll()
        {
            var result = new List<Language>();

            try
            {
                result = _LanguageDbContext.Language.ToList();
            }
            catch (System.Exception)
            {

            }

            return result;
        }

        public IEnumerable<Language> GetByIndex(int index)
        {
            var result = new List<Language>();

            try
            {
                result = _LanguageDbContext.Language
                    .Skip(index * 10).Take(10)
                    .ToList();
            }
            catch (System.Exception)
            {

            }

            return result;
        }

        public IEnumerable<Language> GetByName(string name)
        {
            var result = new List<Language>();

            try
            {
                result = _LanguageDbContext
                           .Language
                         .Where(x => x.Name.ToLower().StartsWith(name.ToLower()))
                         .ToList();
            }
            catch (System.Exception)
            {

            }

            return result;
        }

        public bool Add(Language model)
        {
            try
            {
                _LanguageDbContext.Add(model);
                _LanguageDbContext.SaveChanges();
            }
            catch (System.Exception)
            {
                return false;
            }

            return true;
        }

        public bool Update(Language model)
        {
            Language OriginalModel = _LanguageDbContext.Language.Where(x => x.LanguageId == model.LanguageId).FirstOrDefault();
            OriginalModel.Name = model.Name;

            _LanguageDbContext.Language.Update(OriginalModel);
            _LanguageDbContext.SaveChanges();
            return true;
        }

        public Language Get(int id)
        {
            var result = new Language();

            try
            {
                result = _LanguageDbContext.Language.Single(x => x.LanguageId == id);

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
                temp = _LanguageDbContext.Language.Count();
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

        public bool Delete(int id)
        {
            try
            {
                Language deleteModel = _LanguageDbContext.Language.Where(x => x.LanguageId == id).FirstOrDefault();
                if (deleteModel != null)
                    _LanguageDbContext.Language.Remove(deleteModel);
                else
                    return false;

            }catch(System.Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}

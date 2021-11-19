using Model;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service
{

    public interface ITransportTypeService
    {
        IEnumerable<TransportType> GetAll();
        IEnumerable<TransportType> GetByName(string name);
        IEnumerable<TransportType> GetByIndex(int index);
        int GetCount();
        bool Add(TransportType model);
        bool Delete(int id);
        bool Update(TransportType model);
        TransportType Get(int id);
    }

    public class TransportTypeService : ITransportTypeService
    {
        private readonly LastaContext _TransportTypeContext;

        public TransportTypeService(LastaContext TransportTypeService)
        {
            _TransportTypeContext = TransportTypeService;
        }

        public bool Add(TransportType model)
        {
            try
            {
                _TransportTypeContext.Add(model);
                _TransportTypeContext.SaveChanges();
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
                TransportType deleteModel = _TransportTypeContext.TransportType.Where(x => x.TransportTypeId == id).FirstOrDefault();
                if (deleteModel != null)
                    _TransportTypeContext.TransportType.Remove(deleteModel);
                else
                    return false;

            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        public TransportType Get(int id)
        {
            var result = new TransportType();

            try
            {
                result = _TransportTypeContext.TransportType.Single(x => x.TransportTypeId == id);

            }
            catch (System.Exception)
            {

            }

            return result;
        }

        public IEnumerable<TransportType> GetAll()
        {
            var result = new List<TransportType>();

            try
            {
                result = _TransportTypeContext.TransportType.ToList();
            }
            catch (System.Exception)
            {

            }

            return result;
        }

        public IEnumerable<TransportType> GetByName(string name)
        {
            var result = new List<TransportType>();

            try
            {
                result = _TransportTypeContext
                         .TransportType
                         .Where(x => x.Name.ToLower().StartsWith(name.ToLower()))
                         .ToList();
            }
            catch (System.Exception)
            {

            }

            return result;
        }


        public int GetCount()
        {
            int NumberOfTransportTypes = _TransportTypeContext.TransportType.Count();
            return NumberOfTransportTypes / 10 + Convert.ToInt32(NumberOfTransportTypes % 10 > 0);

        }

        public bool Update(TransportType model)
        {
            TransportType OriginalModel = _TransportTypeContext.TransportType.Find(model.TransportTypeId);
            OriginalModel.Name = model.Name;

            _TransportTypeContext.TransportType.Update(OriginalModel);
            _TransportTypeContext.SaveChanges();
            return true;
        }

        public IEnumerable<TransportType> GetByIndex(int index)
        {
            var result = new List<TransportType>();


            try
            {
                result = _TransportTypeContext.TransportType
                    .Skip(index * 10).Take(10)
                    .ToList();
            }
            catch (System.Exception)
            {

            }

            return result;
        }
    }
}

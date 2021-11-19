using Microsoft.EntityFrameworkCore;
using Model;
using Model.ViewModels;
using Persistence;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public interface ITravelerService
    {
        IEnumerable<Traveler> GetAll();
        bool Add(Traveler model);
        bool Delete(TravelerVM.DeleteModel model);
        bool Update(Traveler model);
        Traveler Get(int id);
        IEnumerable<Traveler> GetByBooking(int bookingId);
    }

    public class TravelerService :ITravelerService
    {
        private LastaContext _TravelerContext;

        public TravelerService(LastaContext context)
        {
            _TravelerContext = context;
        }

        public bool Add(Traveler model)
        {
            try
            {
                _TravelerContext.Add(model);
                _TravelerContext.SaveChanges();
            }
            catch (System.Exception)
            {
                return false;
            }

            return true;
        }

        public bool Update(Traveler model)
        {
            Traveler OriginalModel = _TravelerContext.Traveler.Where(x => x.TravelerId == model.TravelerId).FirstOrDefault();
            OriginalModel.FirstName = model.FirstName;
            OriginalModel.LastName = model.LastName;
            OriginalModel.Username = model.Username;
            OriginalModel.Password = model.Password;

            _TravelerContext.Traveler.Update(OriginalModel);
            _TravelerContext.SaveChanges();
            return true;
        }

        public bool Delete(TravelerVM.DeleteModel vm)
        {
            try
            {
                var Traveler = _TravelerContext.Traveler.Find(vm.TravelerId);
                _TravelerContext.Entry(Traveler).State = EntityState.Deleted;
                _TravelerContext.BookingTraveler.RemoveRange(_TravelerContext.BookingTraveler.Where(x => x.TravelerId == vm.TravelerId && x.BookingId == vm.BookingId));
                _TravelerContext.SaveChanges();
            }
            catch (System.Exception)
            {
                return false;
            }

            return true;
        }

        public Traveler Get(int id)
        {
            var result = new Traveler();

            try
            {
                result = _TravelerContext.Traveler.Single(x => x.TravelerId == id);

            }
            catch (System.Exception)
            {

            }

            return result;
        }

        public IEnumerable<Traveler> GetAll()
        {
            var result = new List<Traveler>();

            try
            {
                result = _TravelerContext.Traveler.ToList();
            }
            catch (System.Exception)
            {

            }

            return result;
        }


        public IEnumerable<Traveler> GetByBooking(int bookingId)
        {
            var result = new List<Traveler>();
            try
            {
                result = _TravelerContext.Traveler
                    .Where(x => x.BookingTraveler.Any(y => y.BookingId == bookingId))
                    .ToList();

            }catch(System.Exception ex)
            {
                
            }

            return result;
        }


    }
}

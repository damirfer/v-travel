using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.ViewModels;
using Persistence;

namespace Service
{
    public interface IScheduleService
    {
        IEnumerable<Schedule> GetAll();
        bool Add(Schedule model);
        bool Delete(int id);
        bool Update(Schedule model);
        ScheduleVM.List Get(int id);
        IEnumerable<ScheduleVM.ListMobile> GetByTour(int tourId);

    }
    public class ScheduleService : IScheduleService
    {
        private readonly LastaContext _ScheduleDbContext;

        public ScheduleService(LastaContext ScheduleDbContext)
        {
            _ScheduleDbContext = ScheduleDbContext;
        }


        public IEnumerable<Schedule> GetAll()
        {
            var result = new List<Schedule>();

            try
            {
                result = _ScheduleDbContext.Schedule.Include(x => x.ScheduleItems).ToList();
            }
            catch (System.Exception)
            {

            }

            return result;
        }

        public ScheduleVM.List Get(int id)
        {
            var result = new ScheduleVM.List();
            result.ScheduleItems = new List<ScheduleVM.Item>();

            try
            {
                var scheduleSingle = _ScheduleDbContext.Schedule.Include(x => x.ScheduleItems).Single(x => x.ScheduleId == id);
                scheduleSingle.ScheduleItems = scheduleSingle.ScheduleItems.OrderBy(x => x.TimeStamp).ToList();

                foreach (var item in scheduleSingle.ScheduleItems)
                {
                    result.ScheduleItems.Add(new ScheduleVM.Item
                    {
                        ScheduleItemId = item.ScheduleItemId,
                        Content = item.Content,
                        PlaceId=item.PlaceId,
                        TimeStamp = item.TimeStamp.ToShortTimeString(),
                        Url = item.Url,
                    });
                }
            }
            catch (System.Exception ex)
            {

            }

            return result;
        }

        public bool Add(Schedule model)
        {
            try
            {
                _ScheduleDbContext.Schedule.Add(model);
                _ScheduleDbContext.SaveChanges();
            }
            catch (System.Exception)
            {
                return false;
            }

            return true;
        }

        public bool Update(Schedule model)
        {
            foreach(var item in model.ScheduleItems)
            {
                item.ScheduleId = model.ScheduleId;
            }

            try
            {
                _ScheduleDbContext.Schedule.Update(model);
                _ScheduleDbContext.SaveChanges();
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
                var Schedule = _ScheduleDbContext.Schedule.Find(id);
                _ScheduleDbContext.Entry(Schedule).State = EntityState.Deleted;
                _ScheduleDbContext.ScheduleItem.RemoveRange(Schedule.ScheduleItems.ToList());
                _ScheduleDbContext.SaveChanges();
            }
            catch (System.Exception)
            {
                return false;
            }

            return true;
        }

        public IEnumerable<ScheduleVM.ListMobile> GetByTour(int tourId)
        {
            var result = new List<ScheduleVM.ListMobile>();


            try
            {
                result = _ScheduleDbContext.Day.Where(x => x.TourId == tourId)
                        .Include(x=> x.Schedule)
                            .ThenInclude(x=> x.ScheduleItems)
                        .Select(x=> new ScheduleVM.ListMobile
                        {
                            ScheduleId = x.ScheduleId,
                            ScheduleItems = x.Schedule.ScheduleItems
                        }).ToList();
            }
            catch (System.Exception)
            {

            }

            return result;
        }

    }
}

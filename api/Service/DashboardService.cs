using Microsoft.EntityFrameworkCore;
using Model;
using Model.ViewModels;
using Persistence;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IDashboardService
    {
        int GetUpcommingBookings();
        int GetTotalTravelers();
        int GetThisMonthsBookings();
        int GetActiveTours();
        DashboardVM GetData();
    }

    public class DashboardService:IDashboardService
    {

        private readonly LastaContext _DashboardDbContext;

        public DashboardService(
            LastaContext DashboardDbContext
        )
        {
            _DashboardDbContext = DashboardDbContext;
        }

        public int GetUpcommingBookings()
        {
            return _DashboardDbContext.Booking.Where(x => x.DateFrom > DateTime.Now).Count();
        }

        public int GetTotalTravelers()
        {
            var sum = 0;
            var bookings = _DashboardDbContext.Booking.Include(x => x.BookingTraveler).ToList();
            foreach(var b in bookings)
            {
                sum += b.BookingTraveler.Count();
            }

            return sum;
        }

        public int GetThisMonthsBookings()
        {
            return _DashboardDbContext.Booking.Where(x => (x.DateFrom.Month == DateTime.Now.Month) && (x.DateFrom.Year==DateTime.Now.Year)).Count();
        }

        public int GetActiveTours()
        {
            return _DashboardDbContext.Tour.Where(x => x.IsTemplate == true).Count();
        }

        public DashboardVM GetData()
        {
            DashboardVM vm = new DashboardVM();
            vm.ThisMonthsBookings = GetThisMonthsBookings().ToString();
            vm.TotalTravelers = GetTotalTravelers().ToString();
            vm.UpcommingBookings = GetUpcommingBookings().ToString();
            vm.ActiveTours = GetActiveTours().ToString();
            vm.Data = GetChartData();
            vm.BookingData = GetBookingsData();
            return vm;
        }

        public Dictionary<string,int> GetChartData()
        {
            var startMonth = DateTime.Now.Month - 2;
            var startYear = DateTime.Now.Year;
            if (startMonth < 1)
            {
                startMonth = 12 + startMonth;
                startYear--;
            }
            Dictionary<string, int> returnData=new Dictionary<string, int>();
            CultureInfo cultureInfo = new CultureInfo("en-US");
            for(int i = 0; i < 5; i++)
            {
                var key = cultureInfo.DateTimeFormat.GetMonthName(startMonth) + " " + startYear.ToString();
                var value = _DashboardDbContext.Booking.Where(x => x.DateFrom.Month == startMonth && x.DateFrom.Year == startYear).Count();
                returnData.Add(key, value);



                if (startMonth == 12)
                {
                    startYear++;
                }
                startMonth++;

                if (startMonth > 12)
                {
                    startMonth = 1;
                }

            }

            return returnData;
        }
    
        public Dictionary<string, int> GetBookingsData()
        {
            Dictionary<string, int> returnData = new Dictionary<string, int>();

            var value = _DashboardDbContext.Booking.Include(x => x.Tour).GroupBy(x => x.Tour.Name).Select(x => new { name = x.Key, count = x.Count() }).ToList();

            foreach(var result in  value)
            {
                returnData.Add(result.name, result.count);
            }


            return returnData;

        }


    }
}


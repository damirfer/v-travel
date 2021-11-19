using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Model
{
    public class DayCity
    {
        public int DayId { get; set; }
        public Day Day { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }

    }
}
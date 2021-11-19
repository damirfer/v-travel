using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Model
{
    public class DayCountry
    {
        public int DayId { get; set; }
        public Day Day { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }

    }
}
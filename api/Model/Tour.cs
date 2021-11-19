using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Tour
    {
        public int TourId { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
        public bool IsTemplate { get; set; }
        //public int TourTypeId { get; set; }
        //public virtual TourType TourType { get; set; }

        public virtual IList<TourCountry> TourCountry { get; set; }

        public IList<Day> Day { get;} = new List<Day>();
        public IList<TourSection> Sections { get; set; }

    }
}

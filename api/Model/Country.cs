using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Model
{
    public class Country
    {
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string CapitalCity { get; set; }
        public string OfficialLanguage { get; set; }
        public string Polity { get; set; }
        public string Population { get; set; }
        public string Currency { get; set; }
        public float Area { get; set; }
        public DateTime NationalDay { get; set; }
        public string GeneralInfo { get; set; }
        public string FlagUrl { get; set; }

        public IList<CountrySection> Sections { get; } = new List<CountrySection>();

        public virtual IList<TourCountry> TourCountry { get; set; }
        public virtual IList<DayCountry> DayCountry { get; set; }


    }
}

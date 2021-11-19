using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ViewModels
{
    public class CountryVM
    {
        public class Single
        {
            public int CountryId { get; set; }
            public string Name { get; set; }
        }
        public class Info
        {
            public int CountryId { get; set; }
            public string Name { get; set; }
            public string FlagUrl { get; set; }
        }
        public class Details
        {
            public string Name { get; set; }
            public string CapitalCity { get; set; }
            public string OfficialLanguage { get; set; }
            public string Polity { get; set; }
            public string Population { get; set; }
            public string Currency { get; set; }
            public float Area { get; set; }
            public string NationalDay { get; set; }
            public string GeneralInfo { get; set; }
            public string FlagUrl { get; set; }

            public List<CountrySection> Sections { get; set; } 
            public List<CityVM.Info> Cities { get; set; }

        }

    }
}

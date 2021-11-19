using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class CountrySection
    {

        public int CountrySectionId { get; set; }
        public int CountryId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}

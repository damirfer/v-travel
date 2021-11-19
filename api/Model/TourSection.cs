using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class TourSection
    {
        public int TourSectionId { get; set; }
        public int TourId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}

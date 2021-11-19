using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ViewModels
{
    public class TourTransportVM
    {
        public class Single
        {
            public string CityName { get; set; }
            public List<TourTransportVM.List> TransportList { get; set; }
        }
        public class List
        {
            public int TransportId { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public string PhotoUrl { get; set; }
            public string TransportType { get; set; }
        }
    }
}

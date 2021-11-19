using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ViewModels
{
    public class TransportVM
    {
        public class Single
        {
            public int TransportId { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public string PhotoUrl { get; set; }
            public string WebsiteUrl { get; set; }
            public string City { get; set; }
            public string TransportType { get; set; }
            public int TransportTypeId { get; set; }
        }
        
            public class List
            {
                public int TransportId { get; set; }
                public string Name { get; set; }
                public string Phone { get; set; }
                public string PhotoUrl { get; set; }
                public string WebsiteUrl { get; set; }

                public string City { get; set; }

                public string TransportType { get; set; }
            }
    }
}

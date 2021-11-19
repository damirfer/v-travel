using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Transport
    {
        public int TransportId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string PhotoUrl { get; set; }
        public string WebsiteUrl { get; set; }
        public virtual City City { get; set; }
        public int CityId { get; set; }
        public int TransportTypeId { get; set; }
        public virtual TransportType TransportType { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Message
    {
        public int MessageId { get; set; }
        public string Text { get; set; }
        public DateTime TimeStamp { get; set; }

        public int? TravelerId { get; set; }
        public virtual Traveler Traveler { get; set; }

        public int? GuideId { get; set; }
        public virtual Guide Guide { get; set; }
    }
}

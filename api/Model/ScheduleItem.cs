using System;

namespace Model
{
    public class ScheduleItem
    {
        public int ScheduleItemId { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
        public DateTime TimeStamp { get; set; }

        public virtual Schedule Schedule { get; set; }
        public int ScheduleId { get; set; }
        public virtual Place Place { get; set; }
        public int? PlaceId { get; set; }
        public virtual Accommodation Accommodation { get; set; }
        public int? AccommodationId { get; set; }
    }
}
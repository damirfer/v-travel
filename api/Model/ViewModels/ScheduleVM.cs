using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ViewModels
{
    public class ScheduleVM
    {
        public class List
        {
            public int? ScheduleId { get; set; }
            public List<Item> ScheduleItems { get; set; }
        }
        public class ListMobile
        {
            public int? ScheduleId { get; set; }
            public List<ScheduleItem> ScheduleItems { get; set; }
        }
        public class Item
        {
            public int ScheduleItemId { get; set; }
            public string Content { get; set; }
            public string Url { get; set; }
            public string TimeStamp { get; set; }
            public int? PlaceId { get; set; }
            public int? AccommodationId { get; set; }
        }
    }
}

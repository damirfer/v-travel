using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Schedule
    {
        public int ScheduleId { get; set; }
        public List<ScheduleItem> ScheduleItems { get; set; }
    }
}

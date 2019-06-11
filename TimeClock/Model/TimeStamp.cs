using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeClock.Model
{
    public class TimeStamp
    {
        //the source of so many problems
        public string InOut { get; set; }
        public DateTime Time { get; set; }

        public TimeStamp(string InOut, DateTime Time)
        {
            this.InOut = InOut;
            this.Time = Time;
        }
    }
}
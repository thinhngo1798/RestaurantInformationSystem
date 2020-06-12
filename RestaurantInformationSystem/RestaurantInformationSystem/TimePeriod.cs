using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantInformationSystem
{
    public class TimePeriod
    {
        private TimeSpan _time;
        private int _count;
        private string _name;

        /// <summary>
        /// Time period of the day.
        /// </summary>
        /// <param name="time"></param>
        /// <param name="name"></param>
        public TimePeriod(TimeSpan time, string name)
        {
            Time = time;
            Count = 0;
            Name = name;
        }

        public TimeSpan Time { get => _time; set => _time = value; }
        public int Count { get => _count; set => _count = value; }
        public string Name { get => _name; set => _name = value; }
    }
}

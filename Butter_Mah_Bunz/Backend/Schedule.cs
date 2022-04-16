using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    internal class Schedule
    {
        private List<ScheduleDay> _schedule = new List<ScheduleDay>();
        public ScheduleDay[] ScheduleDays
        {
            get
            {
                //sort the list so the days are in order (just in case)
                _schedule.Sort();
                
                //get DateTime objects representing today and tomorrow
                DateTime today = DateTime.Now;
                DateTime tomorrow = today.AddDays(1);

                //find the days that corrispond to tooday and tomorrow and mark them
                ScheduleDay? todaysSchedule = null;
                for (int i = 0; i < _schedule.Count; i++)
                {
                    if ((int)_schedule[i].Day - 2 == (int)today.DayOfWeek)
                    {
                        _schedule[i].Day = ScheduleDayOfWeek.Today;
                        todaysSchedule = _schedule[i];
                    }
                    else if ((int)_schedule[i].Day - 2 == (int)tomorrow.DayOfWeek)
                    {
                        _schedule[i].Day = ScheduleDayOfWeek.Tomorrow;
                    }

                }

                while (_schedule[0] != todaysSchedule)
                {
                    _schedule.Add(_schedule[0]);
                    _schedule.RemoveAt(0);
                }

                //return the now sorted schedule for presentation upstream
                return _schedule.ToArray();
            }
        }
        public ScheduleDay AddDay
        {
            set { _schedule.Add(value); }
        }
    }
}

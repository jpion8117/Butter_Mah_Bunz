using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    /// <summary>
    /// Enumeration to representing the day of the week
    /// </summary>
    public enum ScheduleDayOfWeek
    {
        ERROR = -3,
        Today,     //these will get assigned automatically based on the current day of the week
        Tomorrow,       //when the schedule property of Backend.Schedule is requested.
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday
    }
    /// <summary>
    /// Part of how the overall schedule component functions. This represents one day in the whole of the 
    /// schedule.
    /// </summary>
    internal class ScheduleDay : IComparable<ScheduleDay>
    {
        public ScheduleDay(ScheduleDayOfWeek day, string location, TimeOnly start, TimeOnly end)
        {
            _day = day;
            _location = location;
            _startTime = start;
            _endTime = end;
        }
        private ScheduleDayOfWeek _day;
        private string _location;
        private TimeOnly _startTime;
        private TimeOnly _endTime;

        public ScheduleDayOfWeek Day
        {
            set { _day = value; }
            get { return _day; }
        }
        public string Location
        {
            get { return _location; }
        }
        public TimeOnly StartTime
        {
            get { return _startTime; }
        }
        public TimeOnly EndTime
        {
            get { return _endTime; }
        }
        public string TimesStr
        {
            get 
            {
                // returns "closed" if start 
                if (_startTime == _endTime)
                {
                    return "Closed Today";
                }
                return _startTime.ToString("t") + " - " + _endTime.ToString("t"); 
            }
        }

        public int CompareTo(ScheduleDay? other)
        {
            if (other != null)
            {

                if (other._day > this._day)
                    return -1;
                else if (other._day < this._day)
                    return 1;
                else
                    return 0;
            }

            return 0;
        }
        public override bool Equals(object? obj)
        {
            ScheduleDay? other = obj as ScheduleDay;

            if (other == null)
                return false;

            return _day==other._day && _endTime==other._endTime 
                && _startTime==other._startTime && _location==other._location;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Backend
{
    enum ScheduleError
    {
        noErrors,               //schedule working as intended
        invalidNumberOfDays,    //schedule file failed to load every day
        scheduleNotFound        //no schedule file was found at given path
    }
    internal class Schedule
    {
        private List<ScheduleDay> _schedule = new List<ScheduleDay>();
        private DateTime? _todaysDate = null;
        private bool _scheduleReady = false;
        private ScheduleError _error = ScheduleError.noErrors;
        /// <summary>
        /// Property returns an ordered array of the week's schedule starting with the schedule for today.
        /// index 0 should always resolve to today's schedule.
        /// </summary>
        public ScheduleDay[] ScheduleDays
        {
            get
            {

                //sort the list so the days are in order (just in case)
                _schedule.Sort();
                
                //get DateTime objects representing today and tomorrow
                DateTime today = TodaysDate;
                DateTime tomorrow = today.AddDays(1);

                //find the days that corrispond to tooday and tomorrow and mark them
                ScheduleDay? todaysSchedule = null;
                for (int i = 0; i < _schedule.Count; i++)
                {
                    if ((int)_schedule[i].Day == (int)today.DayOfWeek)
                    {
                        _schedule[i].Day = ScheduleDayOfWeek.Today;
                        todaysSchedule = _schedule[i];
                    }
                    else if ((int)_schedule[i].Day == (int)tomorrow.DayOfWeek)
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
        public ScheduleError Error
        {
            get { return _error; }
        }

        /// <summary>
        /// Primarily used for unit testing the schedule object. Allows unit tests to change the internally recognized "current date"
        /// to test schedule sorting.
        /// </summary>
        public DateTime TodaysDate
        {
            get
            {
                if (_todaysDate == null) //if dateTime wans't set prior to call, TodaysDate returns the current system DateTime
                    return DateTime.Now;
                else
                    return (DateTime)_todaysDate; //will return custom set time as "Today"
            }
            set { _todaysDate = value; }
        }
        /// <summary>
        /// Prevents other components of the program from attempting to access the schedule before it's loaded.
        /// </summary>
        public bool ScheduleReady
        {
            get { return _scheduleReady; }
        }
        public bool loadMenu(string path)
        {
            //makes sure scheduleReady id only true IF the schedule is successfully loaded
            _scheduleReady = false;

            XmlDocument doc = new XmlDocument();
            try 
            { 
                doc.LoadXml(path); 
            }
            catch
            {
                _error = ScheduleError.scheduleNotFound;
                return _scheduleReady;
            }

            XmlNodeList days = doc.GetElementsByTagName("scheduleDay");
            foreach (XmlNode day in days)
            {
                ScheduleDayOfWeek dayOfWeek = ScheduleDayOfWeek.ERROR;
                TimeOnly startTime;
                TimeOnly endTime;
                string locationLine1 = "";
                string locationLine2 = "";

                XmlNodeList daySettings = day.ChildNodes;
                foreach (XmlNode setting in daySettings)
                {
                    if (setting.Name == "day")
                    {
                        switch(setting.InnerText.ToLower())
                        {
                            case "sunday":
                                dayOfWeek = ScheduleDayOfWeek.Sunday;
                                break;
                            case "monday":
                                dayOfWeek = ScheduleDayOfWeek.Monday;
                                break;
                            case "tuesday":
                                dayOfWeek = ScheduleDayOfWeek.Tuesday;
                                break;
                            case "wednesday":
                                dayOfWeek = ScheduleDayOfWeek.Wednesday;
                                break;
                            case "thursday":
                                dayOfWeek = ScheduleDayOfWeek.Thursday;
                                break;
                            case "friday":
                                dayOfWeek = ScheduleDayOfWeek.Friday;
                                break;
                            case "saturday":
                                dayOfWeek = ScheduleDayOfWeek.Saturday;
                                break;
                            default:
                                return false;
                        }
                    }
                    else if (setting.Name == "open")
                    {
                        int h = 0;
                        int m = 0;
                        foreach (XmlNode timeNode in setting.ChildNodes)
                        {
                            if (timeNode.Name == "h")
                                h = int.Parse(timeNode.InnerText);
                            else if (timeNode.Name == "m")
                                m = int.Parse(timeNode.InnerText);
                        }
                        startTime = new TimeOnly(h, m);
                    }
                    else if (setting.Name == "close")
                    {
                        int h = 0;
                        int m = 0;
                        foreach (XmlNode timeNode in setting.ChildNodes)
                        {
                            if (timeNode.Name == "h")
                                h = int.Parse(timeNode.InnerText);
                            else if (timeNode.Name == "m")
                                m = int.Parse(timeNode.InnerText);
                        }
                        endTime = new TimeOnly(h, m);
                    }
                    else if (setting.Name == "location")
                    {
                        foreach (XmlNode locationNode in setting.ChildNodes)
                        {
                            if (locationNode.Name == "line1")
                                locationLine1 = locationNode.InnerText;
                            else if (locationNode.Name == "line2")
                                locationLine2 = locationNode.InnerText;
                        }
                    }
                }

                _schedule.Add(new ScheduleDay(dayOfWeek, locationLine1 + "/n" + locationLine2, startTime, endTime));
            }

            if (_schedule.Count == 7) _scheduleReady = true; //will return false if schedule didn't load all days correctly.
            else _error = ScheduleError.invalidNumberOfDays;
            

            return _scheduleReady;
        }
    }
}

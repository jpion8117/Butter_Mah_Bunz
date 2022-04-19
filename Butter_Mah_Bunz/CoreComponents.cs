using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butter_Mah_Bunz
{
    static class CoreComponents
    {
        static private Backend.Schedule _schedule = new Backend.Schedule();
        static private Backend.Menu _menu = new Backend.Menu();

        /// <summary>
        /// Access the schedule
        /// </summary>
        public static Backend.Schedule Schedule
        {
            get { return _schedule; }
        }
        public static Backend.Menu Menu
        {
            get { return _menu; }
        }

        /// <summary>
        /// State flag indicating the schedule has been loaded and is ready for use
        /// </summary>
        public static bool ScheduleReady
        {
            get { return _schedule.ScheduleDays.Length == 7; }
        }

        /// <summary>
        /// Check if the menu is loaded and ready for use.
        /// </summary>
        public static bool MenuReady
        {
            get { return _menu.MenuReady; }
        }

        /// <summary>
        /// Load the schedule from file.
        /// </summary>
        public static void loadSchedule()
        {

        }
        public static void loadMenu()
        {
            _menu.LoadMenu();
        }
    }
}

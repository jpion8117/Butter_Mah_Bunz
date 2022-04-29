using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butter_Mah_Bunz
{
    class CoreComponents
    {
        //static private fields
        static private Backend.Item[] _menuItems = new Backend.Item[0];
        static private Backend.Enhancment[] _enhancments = new Backend.Enhancment[0];
        static private Backend.ScheduleDay[] _schedule = new Backend.ScheduleDay[0];
        static private Backend.Order _cart = new Backend.Order();
        static private bool _scheduleReady = false;
        static private bool _menuReady = false;

        //static public properties
        static public Backend.Item[] MenuItems
        {
            get
            {
                //load the menu on first access
                if(!_menuReady)
                {
                    Backend.Menu menu = new Backend.Menu();
                    menu.LoadMenu();

                    _menuItems = menu.MenuItems.ToArray();
                    _menuReady = menu.MenuReady;
                }
                
                return _menuItems;
            }
        }
        static public Backend.Enhancment[] Enhancments
        {
            get
            {
                //load the menu on first access
                if (!_menuReady)
                {
                    Backend.Menu menu = new Backend.Menu();
                    menu.LoadMenu();

                    _enhancments = menu.Enhancments.ToArray();
                    _menuReady = menu.MenuReady;
                }

                return _enhancments;
            }
        }
        static public Backend.ScheduleDay[] Schedule
        {
            get
            {
                if(!_scheduleReady)
                {
                    Backend.Schedule schedule = new Backend.Schedule();
                    schedule.load("Backend\\Schedule\\schedule.xml");

                    _schedule = schedule.ScheduleDays;
                    _scheduleReady = schedule.ScheduleReady;
                }

                return _schedule;
            }
        }
        static public bool ScheduleReady
        {
            get { return _scheduleReady; }
        }
        static public bool MenuReady
        {
            get { return _menuReady; }
        }
        
        //static methods
        static public bool initialize()
        {
            if (!_scheduleReady)
            {
                Backend.Schedule schedule = new Backend.Schedule();
                schedule.load("Backend\\Schedule\\schedule.xml");

                _schedule = schedule.ScheduleDays;
                _scheduleReady = schedule.ScheduleReady;
            }

            if (!_menuReady)
            {
                Backend.Menu menu = new Backend.Menu();
                menu.LoadMenu();

                _menuItems = menu.MenuItems.ToArray();
                _menuReady = menu.MenuReady;
            }

            return _menuReady && _scheduleReady;
        }

        public CoreComponents() { }
    }
}

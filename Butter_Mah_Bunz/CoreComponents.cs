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
        static public string[][] CartDetails
        {
            get
            {
                string[] detailsRaw = _cart.getOrderDetails();
                List<string[]> outter = new List<string[]>();

                for (int i = 0; i < detailsRaw.Length; i++)
                {
                    //holds inner array, using list since size is unknown
                    List<string> inner = new List<string>();

                    if (detailsRaw[i] == Backend.Item.ITEM_START) //Tells organizer where to start
                    {
                        ++i; //advances to next item
                        while (detailsRaw[i] != Backend.Item.ITEM_END) //keeps going until it finds end marker
                        {
                            inner.Add(detailsRaw[i]);
                            ++i; //advances to next item
                        }

                        outter.Add(inner.ToArray()); //adds inner array to outter array (list)
                    }
                }

                return outter.ToArray(); //converts outer to an array and returns it
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
        static public bool CartEmpty
        {
            get { return CartDetails.Length == 0; }
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
        static public void addToCart(Backend.Item newItem)
        {
            _cart.addToOrder(newItem);
        }
        static public void removeFromCart(int index)
        {
            _cart.removeFromOrder(CartDetails[index][0]); //uniqueID is always the first string in each of the inner item arrays
        }

        public CoreComponents() {} //instance constructor needed for XAML data binding...
    }
}

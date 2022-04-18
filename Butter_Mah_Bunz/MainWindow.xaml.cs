using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Butter_Mah_Bunz
{
    public struct CoreComponents
    {
        static private readonly Backend.Schedule _schedule = new Backend.Schedule();
        
        /// <summary>
        /// Access the schedule
        /// </summary>
        static Backend.Schedule Schedule
        {
            get { return _schedule; }
        }

        /// <summary>
        /// State flag indicating the schedule has been loaded and is ready for use
        /// </summary>
        static bool ScheduleReady 
        {
            get
            { return  _schedule.ScheduleDays.Length == 7; }
        }

        /// <summary>
        /// Load the schedule from file.
        /// </summary>
        static void loadSchedule()
        {

        }
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            frame.NavigationService.Navigate(new SchedulePage());
            
        }
    }
}

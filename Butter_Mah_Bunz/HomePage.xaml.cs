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
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();

            if (CoreComponents.ScheduleReady)
            {
                tmrwHours.Text = CoreComponents.Schedule[1].TimesStr;
                tmrwLocation.Text = CoreComponents.Schedule[1].Location.Replace("\n", " ");

                mid2kDisasterFilm_Hours.Text = CoreComponents.Schedule[2].TimesStr;
                mid2kDisasterFilm_Location.Text = CoreComponents.Schedule[2].Location.Replace("\n", " ");
            }
        }

        private void GoToSchedule(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new SchedulePage());
        }

        private void GoToMenu(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Menu());
        }
    }
}

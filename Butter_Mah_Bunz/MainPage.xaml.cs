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

            tmrwHours.Text = "8:00am - 3:00pm";
            tmrwLocation.Text = "69420 [ ]";

            mid2kDisasterFilm_day.Content = "Wednesday";
            mid2kDisasterFilm_Hours.Text = "8:00am - 3:00pm";
            mid2kDisasterFilm_Location.Text = "420 I Couldn't Come Up With Anything";
        }
    }
}

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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Butter_Mah_Bunz
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class SplashPage : Page
    {
        public SplashPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Storyboard spinAnimation = (Storyboard)FindResource("SpinFish");
            spinAnimation.RepeatBehavior = RepeatBehavior.Forever;
            spinAnimation.Begin();

            Task task = Task.Run(() =>
            {
                DateTime loadStart = DateTime.Now;

                CoreComponents.initialize();

                DateTime loadEnd = DateTime.Now;

                TimeSpan realLoadTime = loadEnd - loadStart;

                if (realLoadTime.TotalSeconds < 30)
                {
                    DateTime artificialLoadStart = DateTime.Now;
                    TimeSpan artificialLoad = artificialLoadStart - artificialLoadStart; //should = 0
                    int timeOfAL = new Random().Next(10);

                    while (artificialLoad.TotalSeconds < timeOfAL)
                    {
                        //lol
                        artificialLoad = DateTime.Now - artificialLoadStart;
                    }
                }

                this.Dispatcher.Invoke(() =>
                {
                    this.NavigationService.Navigate(new HomePage());
                });
            });
        }
    }
}

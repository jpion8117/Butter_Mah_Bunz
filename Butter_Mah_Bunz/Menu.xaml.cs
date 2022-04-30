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
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Page
    {
        public Menu()
        {
            InitializeComponent();

            //if (CoreComponents.ScheduleReady)
            //{
            //    Backend.ScheduleDay[] scheduleDays = CoreComponents.Schedule.ScheduleDays;
            //    headLocation.Text = scheduleDays[0].Location;
            //    headHours.Text = scheduleDays[0].TimesStr;
            //}

            if (CoreComponents.MenuReady)
            {
                Backend.Item[] menuItems = CoreComponents.MenuItems;
                foreach (Backend.Item item in menuItems)
                {
                    System.Windows.Controls.Label label = new System.Windows.Controls.Label();
                    System.Windows.Controls.Image img = new System.Windows.Controls.Image();
                    System.Windows.Controls.TextBlock description = new System.Windows.Controls.TextBlock();
                    System.Windows.Controls.TextBlock price = new System.Windows.Controls.TextBlock();
                    System.Windows.Controls.ListBox listbox = new System.Windows.Controls.ListBox();

                    listbox.MinWidth = 414;
                    label.Content = item.Name;
                    label.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                    label.Width = 414;
                    try
                    {
                        img.Source = new BitmapImage(new Uri("pack://application:,,,/Butter_Mah_Bunz;component/"+item.ImageURL));
                    }
                    catch
                    {
                        img.Source = new BitmapImage(new Uri("pack://application:,,,/Butter_Mah_Bunz;component/Media/bmb.png"));
                    }
                    img.Width = 150;
                    img.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                    description.Text = item.Description;
                    price.Text = item.Price.ToString();
                    listbox.Items.Add(label);
                    listbox.Items.Add(img);
                    listbox.Items.Add(description);
                    listbox.Items.Add(price);

                    MenuBox.Items.Add(listbox);
                }
            }
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void ToCart(object sender, RoutedEventArgs e)
        {
            if (!CoreComponents.CartEmpty)
                this.NavigationService.Navigate(new Cart());
            else
                System.Windows.MessageBox.Show("Thy buns remain barren (Cart is empty).", "Cart Empty");
        }
    }
}

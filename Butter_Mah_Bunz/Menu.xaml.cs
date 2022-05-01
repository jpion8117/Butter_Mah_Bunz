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
                for (int index = 0; index < menuItems.Length; index++)
                {
                    Backend.Item item = menuItems[index];
                    System.Windows.Controls.Label label = new System.Windows.Controls.Label();
                    System.Windows.Controls.Image img = new System.Windows.Controls.Image();
                    System.Windows.Controls.TextBlock description = new System.Windows.Controls.TextBlock();
                    System.Windows.Controls.TextBlock price = new System.Windows.Controls.TextBlock();
                    System.Windows.Controls.StackPanel superStacker9000 = new System.Windows.Controls.StackPanel();
                    System.Windows.Controls.Button fItButton = new System.Windows.Controls.Button();

                    superStacker9000.MinWidth = 414;
                    label.Content = item.Name;
                    label.Width = 414;
                    label.FontSize = 30;
                    label.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                    try
                    {
                        string url = "pack://application:,,,/Butter_Mah_Bunz;component/" + item.ImageURL;
                        Uri uri = new Uri(url);
                        img.Source = new BitmapImage(uri);
                    }
                    catch
                    {
                        img.Source = new BitmapImage(new Uri("pack://application:,,,/Butter_Mah_Bunz;component/Media/bmb.png"));
                    }
                    img.Width = 150;
                    description.Text = item.Description;
                    price.Text = item.Price.ToString("C");
                    superStacker9000.Children.Add(label);
                    superStacker9000.Children.Add(img);
                    superStacker9000.Children.Add(description);
                    superStacker9000.Children.Add(price);

                    fItButton.Content = superStacker9000;
                    fItButton.Margin = new System.Windows.Thickness(0, 0, 0, 15);
                    fItButton.Click += MenuItemClicked;
                    if (index % 2 == 0)
                        fItButton.Background = new SolidColorBrush(CoreComponents.Ketchup);
                    else
                        fItButton.Background = new SolidColorBrush(CoreComponents.Beef);
                    MenuBox.Height += fItButton.ActualHeight;
                    MenuBox.Children.Add(fItButton);
                }
            }
        }

        private void MenuItemClicked(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            StackPanel panel = (StackPanel)button.Content;

            //janky AF, but F*** it!
            Label label = (Label)panel.Children[0];
            string str = (string)label.Content;

            //update cart total.
            CartCount.Text = CoreComponents.CartCount.ToString();

            this.NavigationService.Navigate(new ItemDetailPage(str));
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

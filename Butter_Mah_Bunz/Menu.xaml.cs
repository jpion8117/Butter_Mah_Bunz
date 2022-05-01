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

            if (CoreComponents.MenuReady)
            {
                Backend.Item[] menuItems = CoreComponents.MenuItems;
                for (int index = 0; index < menuItems.Length; index++)
                {
                    Button fItButton = new ItemDetailsButton(menuItems[index]);
                    fItButton.Click += MenuItemClicked;
                    if (index % 2 == 0) //alternates button colors
                        fItButton.Background = new SolidColorBrush(CoreComponents.Ketchup);
                    else
                        fItButton.Background = new SolidColorBrush(CoreComponents.Beef);

                    //add newly created button to the menuBox stack panel
                    MenuBox.Height += fItButton.ActualHeight;
                    MenuBox.Children.Add(fItButton);
                }
            }
        }

        private void MenuItemClicked(object sender, RoutedEventArgs e)
        {
            ItemDetailsButton button = (ItemDetailsButton)sender;

            //update cart total.
            CartCount.Text = CoreComponents.CartCount.ToString();

            this.NavigationService.Navigate(new ItemDetailPage(button.Item.Name));
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
    class ItemDetailsButton : Button
    {
        private Backend.Item _item; 
        public Backend.Item Item
        {
            get { return _item; }
        }
        public ItemDetailsButton(Backend.Item item)
        {
            _item = item;

            //create and format item Label
            System.Windows.Controls.Label label = new System.Windows.Controls.Label();
            label.Content = item.Name;
            label.Width = 414;
            label.FontSize = 30;
            label.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;

            //create and format item image
            System.Windows.Controls.Image img = new System.Windows.Controls.Image();
            img.Width = 150;
            try //tries to load the image file
            {
                string url = "pack://application:,,,/Butter_Mah_Bunz;component/" + item.ImageURL;
                Uri uri = new Uri(url);
                img.Source = new BitmapImage(uri);
            }
            catch //if the file is not found this will catch the exception and load a defualt image
            {
                img.Source = new BitmapImage(new Uri("pack://application:,,,/Butter_Mah_Bunz;component/Media/bmb.png"));
            }

            //create and format description TextBlock
            System.Windows.Controls.TextBlock description = new System.Windows.Controls.TextBlock();
            description.Text = item.Description;

            //create and format price TextBlock
            System.Windows.Controls.TextBlock price = new System.Windows.Controls.TextBlock();
            price.Text = item.Price.ToString("C");

            //create and format stack pannel for item button components
            System.Windows.Controls.StackPanel superStacker9000 = new System.Windows.Controls.StackPanel();
            superStacker9000.Width = 414;
            superStacker9000.Children.Add(label);
            superStacker9000.Children.Add(img);
            superStacker9000.Children.Add(description);
            superStacker9000.Children.Add(price);

            this.Content = superStacker9000;
            this.Margin = new System.Windows.Thickness(0, 0, 0, 15);
        }
    }
    public class AddToCartButton : Button
    {

    }
}

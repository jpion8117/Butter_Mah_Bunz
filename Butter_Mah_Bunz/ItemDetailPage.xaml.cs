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
    /// Interaction logic for ItemDetailPage.xaml
    /// </summary>
    public partial class ItemDetailPage : Page
    {
        private Backend.Item _baseItem;
        private Backend.EnhancedItem _enhancedItem;
        public ItemDetailPage(string baseItemName)
        {
            InitializeComponent();
            //Updates cart count
            CartCount.Text = CoreComponents.CartCount.ToString();

            //lookup base item
            Backend.Item? item = CoreComponents.locateMenuItem(baseItemName);
            _baseItem = new Backend.Item("", "", 0);
            if (item != null)
                _baseItem = new Backend.Item(item);

            _enhancedItem = new Backend.EnhancedItem(_baseItem);

            //add item image and details to ItemInfo

            System.Windows.Controls.Label label = new System.Windows.Controls.Label();
            label.Content = _baseItem.Name;
            label.Width = 300;
            label.FontSize = 30;
            label.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            //create and format item image
            System.Windows.Controls.Image img = new System.Windows.Controls.Image();
            img.Width = 300;
            try //tries to load the image file
            {
                string url = "pack://application:,,,/Butter_Mah_Bunz;component/" + _baseItem.ImageURL;
                Uri uri = new Uri(url);
                img.Source = new BitmapImage(uri);
            }
            catch //if the file is not found this will catch the exception and load a defualt image
            {
                img.Source = new BitmapImage(new Uri("pack://application:,,,/Butter_Mah_Bunz;component/Media/bmb.png"));
            }

            //create and format description TextBlock
            System.Windows.Controls.TextBlock description = new System.Windows.Controls.TextBlock();
            description.Text = _baseItem.Description;
            description.TextWrapping = TextWrapping.Wrap;
            description.Width = 350;
            description.VerticalAlignment = VerticalAlignment.Center;
            description.FontSize = 14;

            //create and format price TextBlock
            System.Windows.Controls.TextBlock price = new System.Windows.Controls.TextBlock();
            price.Text = _baseItem.Price.ToString("C");
            price.FontSize = 24;
            price.HorizontalAlignment = HorizontalAlignment.Center;
            

            ItemInfo.Children.Add(label);
            ItemInfo.Children.Add(img);
            ItemInfo.Children.Add(price);
            ItemInfo.Children.Add(description);

            //add available enhancments Enhancments

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

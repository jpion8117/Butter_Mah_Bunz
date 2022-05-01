using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Butter_Mah_Bunz
{
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
            this.Margin = new System.Windows.Thickness(0, 15, 0, 0);
        }
    }
    class AddToCartButton : Button
    {
        private Menu _pageRef;
        private Backend.Item _item;
        public Backend.Item Item
        {
            get { return _item; }
        }
        public AddToCartButton(Backend.Item item, Menu pageRef)
        {
            _item = item;
            _pageRef = pageRef;

            this.Click += addItemToCart;
        }

        public void addItemToCart(object sender, RoutedEventArgs e)
        {
            CoreComponents.addToCart(_item);
            _pageRef.CartCount.Text = CoreComponents.CartCount.ToString();
        }
    }
}

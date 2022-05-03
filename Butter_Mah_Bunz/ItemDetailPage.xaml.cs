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
        private Backend.EnhancedItem _item;
        public Backend.EnhancedItem Item
        {
            get { return _item; }
            set { _item = value; }
        }
        public ItemDetailPage(string baseItemName)
        {
            InitializeComponent();
            //Updates cart count
            CartCount.Text = CoreComponents.CartCount.ToString();

            //lookup base item
            Backend.Item? item = CoreComponents.locateMenuItem(baseItemName);
            _item = new Backend.EnhancedItem("", "", 0);
            if (item != null)
                _item = new Backend.EnhancedItem(item);

            //add item image and details to ItemInfo

            //Creates a label element to display the item name.
            System.Windows.Controls.Label label = new System.Windows.Controls.Label();
            label.Content = _item.Name;
            label.Width = 300;
            label.FontSize = 30;
            label.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            MainStack.Height += label.ActualHeight;

            //create and format item image
            System.Windows.Controls.Image img = new System.Windows.Controls.Image();
            img.Width = 300;
            try //tries to load the image file
            {
                string url = "pack://application:,,,/Butter_Mah_Bunz;component/" + _item.ImageURL;
                Uri uri = new Uri(url);
                img.Source = new BitmapImage(uri);
            }
            catch //if the file is not found this will catch the exception and load a defualt image
            {
                img.Source = new BitmapImage(new Uri("pack://application:,,,/Butter_Mah_Bunz;component/Media/bmb.png"));
            }
            MainStack.Height += img.ActualHeight;

            //create and format description TextBlock
            System.Windows.Controls.TextBlock description = new System.Windows.Controls.TextBlock();
            description.Text = _item.Description;
            description.TextWrapping = TextWrapping.Wrap;
            description.Width = 350;
            description.VerticalAlignment = VerticalAlignment.Center;
            description.FontSize = 14;
            MainStack.Height += description.ActualHeight;

            //create and format price TextBlock
            System.Windows.Controls.TextBlock price = new System.Windows.Controls.TextBlock();
            price.Text = _item.Price.ToString("C");
            price.FontSize = 24;
            price.HorizontalAlignment = HorizontalAlignment.Center;
            price.Name = "ItemPrice";
            MainStack.Height += price.ActualHeight;

            //Adds the information generated above into the MainStack element with formatting
            ItemInfo.Children.Add(label);
            ItemInfo.Children.Add(img);
            ItemInfo.Children.Add(price);
            ItemInfo.Children.Add(description);

            //add available enhancments Enhancments
            Backend.Enhancment[] enhancments = CoreComponents.Enhancments;
            foreach (Backend.Enhancment enhancment in enhancments)
            {
                EnhancmentButton enhancmentButton = new EnhancmentButton(enhancment, this, price);
                Enhancments.Children.Add(enhancmentButton);
            }

        }
        private void GoBack(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void ToCart(object sender, RoutedEventArgs e)
        {

            if (CoreComponents.OpenForBusiness)
            {
                //add current item to the cart
                CoreComponents.addToCart(_item);

                if (!CoreComponents.CartEmpty)
                    this.NavigationService.Navigate(new Cart());
                else
                    MessageBox.Show("Thy buns remain barren (Cart is empty).", "Cart Empty");
            }
            else
            {
                MessageBox.Show("You can look, but you can't touch!", "Online orders are currently closed.");
            }
        }

        private void AddToCart(object sender, RoutedEventArgs e)
        {

            if (CoreComponents.OpenForBusiness)
            {
                CoreComponents.addToCart(_item);
                this.NavigationService.GoBack();
            }
            else
            {
                MessageBox.Show("You can look, but you can't touch!", "Online orders are currently closed.");
            }
        }
    }
}

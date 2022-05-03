using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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

            Content = superStacker9000;
            Margin = new System.Windows.Thickness(0, 15, 0, 0);
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
            if (CoreComponents.OpenForBusiness)
            {
                CoreComponents.addToCart(_item);
                _pageRef.CartCount.Text = CoreComponents.CartCount.ToString();
            }
            else
            {
                MessageBox.Show("You can look, but you can't touch!", "Online orders are currently closed.");
            }
        }
    }

    class EnhancmentButton : Button
    {
        private Backend.Enhancment _enhancment;
        private ItemDetailPage _callPage;
        private TextBlock _priceBlock;

        public EnhancmentButton(Backend.Enhancment enhancment, ItemDetailPage callPage, TextBlock priceBlock)
        {
            _enhancment = enhancment;
            _callPage = callPage;
            _priceBlock = priceBlock;
   
            Background = new SolidColorBrush(CoreComponents.Beef);
            Content = enhancment.Name + " " + enhancment.Price.ToString("C");
            HorizontalAlignment = HorizontalAlignment.Right;
            HorizontalContentAlignment = HorizontalAlignment.Right;
            Margin = new Thickness(0, 3, 15, 0);
            Width = 300;

            Click += ClickClack;
        }
        private void ClickClack(object sender, RoutedEventArgs e)
        {
            if(_callPage.Item.removeEnhancment(_enhancment))
            {
                Background = new SolidColorBrush(CoreComponents.Beef);
                    Content = _enhancment.Name  + " " + _enhancment.Price.ToString("C");
            }
            else
            {
                _callPage.Item.addEnhancment(_enhancment);
                Content = _enhancment.Name.Replace("+", "-") + " " + _enhancment.Price.ToString("C");
                Background = new SolidColorBrush(CoreComponents.Ketchup);
            }

            _priceBlock.Text = _callPage.Item.Price.ToString("C");
        }
    }

    class CartButton : Button
    {
        private string _itemUID;

        public CartButton(string[] itemInfo)
        {
            //constants for code clairity
            const int UID = 0;
            const int ITEM_NAME = 1;
            const int ITEM_PRICE = 4;
            const int ITEM_ENHANCMENT_START = 5;

            //save the uID so button can identify object in click event
            _itemUID = itemInfo[UID];

            StackPanel itemStack = new StackPanel();
            itemStack.Width = 414;

            //Name left aligned, title larger. [i][1] name, 2 desc, 3 img url, 4 total string
            Label itemName = new Label();
            itemName.Content = itemInfo[ITEM_NAME];
            //Formatting
            itemName.Width = 300;
            itemName.HorizontalAlignment = HorizontalAlignment.Left;
            itemName.FontSize = 18;
            //Adds formatted content to panel
            itemStack.Children.Add(itemName);

            for (int j = ITEM_ENHANCMENT_START; j < itemInfo.Length; j++)
            {
                //Smaller, right aligned - Enhancements loop
                Label enhName = new Label();
                enhName.Content = itemInfo[j];
                //Formatting
                enhName.Width = 150;
                enhName.HorizontalAlignment = HorizontalAlignment.Left;
                enhName.Margin = new Thickness(35, 3, 0, 3);
                enhName.FontSize = 10;
                //Adds formatted content to panel
                itemStack.Children.Add(enhName);
            }
            //Price right-aligned, calculate periods between name and price. Larger than enhancements, smaller than name. [i][4]
            Label price = new Label();
            price.Content = itemInfo[ITEM_PRICE];
            //Formatting
            price.Width = 75;
            price.HorizontalAlignment = HorizontalAlignment.Right;
            price.FontSize = 14;
            //Adds formatted content to panel
            itemStack.Children.Add(price);

            //place the itemStack Content into this button
            Content = itemStack;

            //event listener
            Click += deleteItem;
        }

        private void deleteItem(object sender, RoutedEventArgs e)
        {
            CoreComponents.removeFromCart(_itemUID);
            if(CoreComponents.TomTom != null)
                CoreComponents.TomTom.Refresh();
        }
    }
}

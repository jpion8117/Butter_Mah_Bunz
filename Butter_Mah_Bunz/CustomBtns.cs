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
    abstract class BMB_Button : Button
    {
        private SolidColorBrush _primaryBackground;
        private SolidColorBrush _secondaryBackground;

        public BMB_Button(SolidColorBrush? primaryColor = null, SolidColorBrush? secondaryColor = null)
        {
            if(primaryColor == null)
                _primaryBackground = new SolidColorBrush(CoreComponents.Ketchup);
            else
                _primaryBackground = primaryColor;

            if (secondaryColor == null)
                _secondaryBackground = new SolidColorBrush(CoreComponents.Bun);
            else
                _secondaryBackground = secondaryColor;

            //MouseEnter += toggleBackground;
            //MouseLeave += toggleBackground;
        }

        public SolidColorBrush PrimaryColor
        {
            get { return _primaryBackground; }
            set { _primaryBackground = value; }
        }
        public SolidColorBrush SecondaryColor
        {
            get { return _secondaryBackground; }
            set { _secondaryBackground = value; } 
        }

        public virtual void toggleBackground(object sender, RoutedEventArgs e)
        {
            if(Background == _primaryBackground)
                Background = _secondaryBackground;
            else
                Background = _primaryBackground;
        }
    }
    class ItemDetailsButton : BMB_Button
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
    class AddToCartButton : BMB_Button
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

    class EnhancmentButton : BMB_Button
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
            Content = enhancment.Name + " " + enhancment.Price.ToString();

            Click += ClickClack;
        }
        private void ClickClack(object sender, RoutedEventArgs e)
        {
            if(_callPage.Item.removeEnhancment(_enhancment))
            {
                Background = new SolidColorBrush(CoreComponents.Beef);
            }
            else
            {
                _callPage.Item.addEnhancment(_enhancment);
                Background = new SolidColorBrush(CoreComponents.Ketchup);
            }

            _priceBlock.Text = _callPage.Item.Price.ToString("C");
        }
    }
}

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
    /**************************************************************************************
    
                            PLEASE READ templatePageReadMe.txt

     *************************************************************************************/

    /// <summary>
    /// Interaction logic for PageTemplate.xaml
    /// </summary>
    public partial class PayConfirm : Page
    {
        public PayConfirm(TimeOnly pickupTime)
        {
            InitializeComponent();

            string[][] order = CoreComponents.CartDetails;
            StackPanel orderDetailsCtnt = new StackPanel();

            for (int i = 0; i < order.Length; i++)
            {
                StackPanel itemDetails = new StackPanel();
                //itemDetails.Margin = new Thickness(0, 5, 0, 5);
                if (i % 2 == 0) 
                {
                    itemDetails.Background = new SolidColorBrush(CoreComponents.Ketchup);
                }
                else
                {
                    itemDetails.Background = new SolidColorBrush(CoreComponents.Beef);
                }

                TextBlock itmName = new TextBlock();
                itmName.Text = order[i][1];
                itmName.FontSize += 7;
                itmName.FontWeight = FontWeights.ExtraBold;
                itemDetails.Children.Add(itmName);

                for (int j = 5; j < order[i].Length; j++)
                {
                    TextBlock itmEnhancment = new TextBlock();
                    itmEnhancment.Text = order[i][j];
                    itmEnhancment.FontSize -= 1;
                    itmEnhancment.Padding = new Thickness(15, 0, 0, 0);
                    itemDetails.Children.Add(itmEnhancment);
                }

                TextBlock itmAmount = new TextBlock();
                itmAmount.Text = order[i][4];
                itmAmount.HorizontalAlignment = HorizontalAlignment.Right;
                itemDetails.Children.Add(itmAmount);
                orderDetailsCtnt.Children.Add(itemDetails);
            }
            TextBlock subtotal = new TextBlock(), tax = new TextBlock(), total = new TextBlock();

            subtotal.Text = CoreComponents.CartSubtotal;
            subtotal.HorizontalAlignment = HorizontalAlignment.Right;
            subtotal.Padding = new Thickness(0, 10, 0, 0);
            subtotal.FontSize += 8;
            subtotal.FontWeight = FontWeights.ExtraBold;

            tax.Text = CoreComponents.CartTax;
            tax.HorizontalAlignment = HorizontalAlignment.Right;
            tax.FontSize += 8;
            tax.FontWeight = FontWeights.ExtraBold;

            total.Text = CoreComponents.CartTotal;
            total.HorizontalAlignment = HorizontalAlignment.Right;
            total.FontSize += 8;
            total.FontWeight = FontWeights.ExtraBold;
            total.Margin = new Thickness(0, 0, 0, 10);

            orderDetailsCtnt.Children.Add(subtotal);
            orderDetailsCtnt.Children.Add(tax);
            orderDetailsCtnt.Children.Add(total);

            orderDetailsExpander.Content = orderDetailsCtnt;

            orderReadyTime.Text = "See you at " + pickupTime.ToShortTimeString();
        }

        private void homeButton_Click(object sender, RoutedEventArgs e)
        {
            CoreComponents.destroyCart();
            this.NavigationService.Navigate(new HomePage());
        }
    }
}

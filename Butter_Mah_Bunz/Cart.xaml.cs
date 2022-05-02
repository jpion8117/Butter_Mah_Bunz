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
    /// Interaction logic for Cart.xaml
    /// </summary>
    public partial class Cart : Page
    {
        public Cart()
        {
            InitializeComponent();
        }
        private void GoBack(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void CancelOrder(object sender, RoutedEventArgs e)
        {
            MessageBoxResult cartEmptyYN = MessageBox.Show("Would you like to empty thy buns?", "Empty Cart", MessageBoxButton.YesNo);
            if (cartEmptyYN == MessageBoxResult.Yes)
            {
                CoreComponents.destroyCart();
                this.NavigationService.Navigate(new HomePage());
            }

        }

        private void ToPayments(object sender, RoutedEventArgs e)
        {
            //temporary bypass for testing
            MessageBox.Show("I temporarily bypassed the payment page to go straight to the " +
                "confirmation screen. This code needs to be deleted as soon as payment page logic is " +
                "done...");
            this.NavigationService.Navigate(new PayConfirm());

            //this.NavigationService.Navigate(new PaymentPage());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //This line is a bit dark... ⊙﹏⊙∥
            OrderDetails.Children.Clear();

            if (!CoreComponents.CartEmpty)
            {
                string[][] cartItems = CoreComponents.CartDetails;

                for (int i = 0; i < cartItems.Length; i++)
                {
                    CartButton button = new CartButton(cartItems[i]);

                    OrderDetails.Height += button.ActualHeight;
                    MainPanel.Height += button.ActualHeight;

                    OrderDetails.Children.Add(button);
                }


                //Generates TextBlocks to display totals.
                TextBlock orderTotalDisplay = new TextBlock();
                TextBlock orderSubDisplay = new TextBlock();
                TextBlock orderTaxDisplay = new TextBlock();

                string orderTotal, orderSub, orderTax = "";

                orderTotal = CoreComponents.CartTotal;
                orderSub = CoreComponents.CartSubtotal;
                orderTax = CoreComponents.CartTax;

                orderTotalDisplay.Text = orderTotal;
                orderSubDisplay.Text = orderSub;
                orderTaxDisplay.Text = orderTax;

                orderTotalDisplay.FontSize = 24;
                orderTotalDisplay.HorizontalAlignment = HorizontalAlignment.Center;
                orderTotalDisplay.Margin = new Thickness(0, 10, 0, 0);

                orderSubDisplay.FontSize = 16;
                orderSubDisplay.HorizontalAlignment = HorizontalAlignment.Center;
                orderSubDisplay.Margin = new Thickness(0, 10, 0, 0);

                orderTaxDisplay.FontSize = 12;
                orderTaxDisplay.HorizontalAlignment = HorizontalAlignment.Center;
                orderTaxDisplay.Margin = new Thickness(0, 10, 0, 0);

                OrderDetails.Children.Add(orderSubDisplay);
                OrderDetails.Children.Add(orderTaxDisplay);
                OrderDetails.Children.Add(orderTotalDisplay);
            }
            else
            {
                this.NavigationService.GoBack();
            }
        }
    }
}
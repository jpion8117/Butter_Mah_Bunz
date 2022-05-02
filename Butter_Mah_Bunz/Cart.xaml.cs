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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            OrderDetails.Children.Clear(); //Bit extreme if you ask me...
            
            if (!CoreComponents.CartEmpty)
            {
                foreach (string[] item in CoreComponents.CartDetails)
                {
                    CartButton button = new CartButton(item);
                    
                    OrderDetails.Children.Add(button);
                    OrderDetails.Height += button.ActualHeight;
                }
            }
            else
            {
                this.NavigationService.GoBack();
            }
        }

        private void ToPayments(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PaymentPage());
        }
    }
}

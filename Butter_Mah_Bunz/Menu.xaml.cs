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

                    //create a quickAddButton
                    AddToCartButton quickAdd = new AddToCartButton(menuItems[index], this);
                    quickAdd.Content = "Quick Add";

                    //add newly created button to the menuBox stack panel
                    MenuBox.Height += fItButton.ActualHeight;
                    MenuBox.Children.Add(fItButton);
                    MenuBox.Children.Add(quickAdd);
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
                System.Windows.MessageBox.Show("Thy buns remain barren.", "Cart Empty");
        }
    }
}

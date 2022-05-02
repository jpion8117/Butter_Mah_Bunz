﻿using System;
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

            if(!CoreComponents.CartEmpty)
            {
                string[][] cartItems = CoreComponents.CartDetails;

                for (int i = 0; i < cartItems.Length; i++)
                { 
                    Button button = new Button();
                    StackPanel itemStack = new StackPanel();
                    itemStack.Width = 414;
                    
                    //Name left aligned, title larger. [i][1] name, 2 desc, 3 img url, 4 total string
                    Label itemName = new Label();
                    itemName.Content = cartItems[i][1];
                    //Formatting
                    itemName.Width = 300;
                    itemName.HorizontalAlignment = HorizontalAlignment.Left;
                    itemName.FontSize = 18;
                    //Adds formatted content to panel
                    itemStack.Children.Add(itemName);

                    for (int j = 5; j < cartItems[i].Length; j++)
                    {
                        //Smaller, right aligned - Enhancements loop
                        Label enhName = new Label();
                        enhName.Content = cartItems[i][j];
                        //Formatting
                        enhName.Width = 150;
                        enhName.HorizontalAlignment = HorizontalAlignment.Left;
                        enhName.Margin = new Thickness(35, 5, 0, 3);
                        enhName.FontSize = 8;
                        //Adds formatted content to panel
                        itemStack.Children.Add(enhName);
                    } 
                    //Price right-aligned, calculate periods between name and price. Larger than enhancements, smaller than name. [i][4]
                    Label price = new Label();
                    price.Content = cartItems[i][4];
                    //Formatting
                    price.Width = 75;
                    price.HorizontalAlignment = HorizontalAlignment.Right;
                    price.FontSize = 14;
                    //Adds formatted content to panel
                    itemStack.Children.Add(price);

                    //Add content to button and add button to screen. Yup.
                    button.Content = itemStack;
                    OrderDetails.Height += button.ActualHeight;
                    MainPanel.Height += button.ActualHeight;
                    OrderDetails.Children.Add(button);
                }
            }
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
    }
}

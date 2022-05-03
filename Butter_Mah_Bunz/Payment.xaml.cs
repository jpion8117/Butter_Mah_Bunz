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
using System.IO;

namespace Butter_Mah_Bunz
{
    /// <summary>
    /// Interaction logic for Payment Page
    ///</summary>
    public partial class PaymentPage : Page
    {
        public PaymentPage()
        {
            InitializeComponent();

        }
        //Have no clue BUT IT DOES A THING
        private bool thisIsHereToCheckIfSomethingWasClicked = false;
        private bool InputtedSomething = false;
        private int counter = 0;
        private string orderNum = DateTime.Now.ToString();
        private string pickUpLater = "";
        private bool pickLater = false;

        //it should be checking these things for at lease one item first
        private void FinishBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CoreComponents.CartCount > 0)
            {
                //if they got here with a zero cart amount I have no clue how.
                if (thisIsHereToCheckIfSomethingWasClicked || InputtedSomething)
                {
                    //Add a check system in here later to write a new order every time
                    orderNum=orderNum.Replace("/", "");
                    orderNum = orderNum.Replace(":", "");
                    orderNum = orderNum.Replace(" ", "");
                    string path = orderNum+"_order.txt";
                    List<string> fillWithOrder= new List<string>();
                    string menuTitle = ("Order: " + orderNum);
                    string payThere = "Payment Received.";
                    // Delete the file if it exists.
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                    fillWithOrder.Add(menuTitle);
                    if (thisIsHereToCheckIfSomethingWasClicked == true)
                    {
                        payThere = "Pay at pickup.";
                    }
                    if (pickLater == true)
                    {
                       int tempHoldMin = 0;
                       int maxHold=0;
                       if(maxHold == 1)
                       {
                            tempHoldMin =15;
                       }
                        if(maxHold == 2)
                       {
                            tempHoldMin =30;
                       }
                        if(maxHold == 3)
                        {
                            tempHoldMin =45;
                        }
                        if (maxHold == 4)
                        {
                            tempHoldMin=60;
                        }
                        pickUpLater = DateTime.Now.AddMinutes(tempHoldMin).ToString();
                    }
                    int counter = 1;
                    foreach (string[] item in CoreComponents.CartDetails) {

                        string fullTest = (counter +": "+ item[1] + "--Priced: " + item[4]).ToString(); 
                        fillWithOrder.Add(fullTest);
                        //Unleash when enhancements are available
                        if (item[5] != null)
                        {
                          string enchancmentIsHere = "--Enhancements:";
                           fillWithOrder.Add(enchancmentIsHere);
                          int lengthHolder = item.Length;
                          for (int i= 5;i<lengthHolder ; i++)
                          {
                              string itemHolder = item[i];
                              fillWithOrder.Add(itemHolder);
                         }
                        }
                        counter++;
                    }
                    fillWithOrder.Add(payThere);
                    if(pickLater == true)
                    {
                        string announcement ="Your scheduled pick up time will be.";
                        fillWithOrder.Add(announcement);
                        fillWithOrder.Add(pickUpLater);
                    }
                    File.WriteAllLines(path, fillWithOrder);
                }
                else
                {
                    //you really shouldnt be here....like seriously how are you here.
                    if (counter == 2)
                    {
                        MessageBox.Show("You really like pushing buttons dont you?");
                        counter = 0;
                    }
                    if (counter == 1)
                    {
                        MessageBox.Show("Stop it! You need to enter an option!");
                        counter++;
                    }
                    if (counter == 0)
                    {
                        MessageBox.Show("You need to select an option.");
                        counter++;
                    }
                }

            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender != null)
            {
                InputtedSomething = true;
            }
        }
        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
                MessageBoxResult cartEmptyYN = MessageBox.Show("Would you like to empty thy buns?", "Empty Cart", MessageBoxButton.YesNo);
                if (cartEmptyYN == MessageBoxResult.Yes)
                {
                    CoreComponents.destroyCart();
                    this.NavigationService.Navigate(new HomePage());
                }

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if(sender!=null)
            {
                thisIsHereToCheckIfSomethingWasClicked = true;
            }

        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}

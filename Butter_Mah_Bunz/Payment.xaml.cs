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

        private TimeOnly _pickupTime = new TimeOnly();
        public PaymentPage()
        {
            InitializeComponent();

            DateTime PickUpWindow = getFirstTimeWindow();

            PickupTime.Items.Clear();
            PickupTime.Items.Add("Butta Me Up! ("+PickUpWindow.ToString("t")+")");
            PickUpWindow = PickUpWindow.AddMinutes(15);
            while (TimeOnly.FromDateTime(PickUpWindow).IsBetween(CoreComponents.Schedule[0].StartTime, 
                CoreComponents.Schedule[0].EndTime.AddMinutes(-30)))
            {
                PickupTime.Items.Add(PickUpWindow.ToString("t"));
                PickUpWindow = PickUpWindow.AddMinutes(15);
            }
            PickupTime.SelectedItem = PickupTime.Items[0];
            OrderTotal.Content = CoreComponents.CartTotal.Remove(0, 7);

        }
        //Have no clue BUT IT DOES A THING
        private bool thisIsHereToCheckIfSomethingWasClicked = false;
        private bool InputtedSomething = false;
        private int counter = 0;
        private string orderNum = DateTime.Now.ToString();

        //it should be checking these things for at lease one item first
        private void FinishBtn_Click(object sender, RoutedEventArgs e)
        {
            thisIsHereToCheckIfSomethingWasClicked = PayLater.IsChecked ?? false;
            InputtedSomething = NameOnCard.Text.Length > 0 &&
                                CardNum.Text.Length > 0 &&
                                Zip.Text.Length > 0 &&
                                CCV.Text.Length > 0 &&
                                EXP.Text.Length > 0;
            if (!CoreComponents.CartEmpty)
            {
                //if they got here with a zero cart amount I have no clue how. -Sam
                //still good to check though... -Josh
                if (thisIsHereToCheckIfSomethingWasClicked || InputtedSomething)
                {
                    //Add a check system in here later to write a new order every time
                    orderNum=orderNum.Replace("/", "");
                    orderNum = orderNum.Replace(":", "");
                    orderNum = orderNum.Replace(" ", "");
                    if(!Directory.Exists("Orders"))
                    {
                        Directory.CreateDirectory("Orders/Completed_Orders");
                    }
                    string path = "Orders/" + orderNum + "_order.txt";
                    List<string> fillWithOrder= new List<string>();
                    string menuTitle = ("Order: " + orderNum);
                    string payThere = "Payment Received.";
                    // Delete the file if it exists.
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                    fillWithOrder.Add(menuTitle);
                    fillWithOrder.Add("");
                    fillWithOrder.Add("===========================================================================");
                    fillWithOrder.Add("");
                    if (thisIsHereToCheckIfSomethingWasClicked == true)
                    {
                        payThere = "Pay at pickup.";
                    }
                    //if (pickLater == true)
                    //{
                    //   int tempHoldMin = 0;
                    //   int maxHold=0;
                    //   if(maxHold == 1)
                    //   {
                    //        tempHoldMin =15;
                    //   }
                    //    if(maxHold == 2)
                    //   {
                    //        tempHoldMin =30;
                    //   }
                    //    if(maxHold == 3)
                    //    {
                    //        tempHoldMin =45;
                    //    }
                    //    if (maxHold == 4)
                    //    {
                    //        tempHoldMin=60;
                    //    }
                    //    pickUpLater = DateTime.Now.AddMinutes(tempHoldMin).ToString();
                    //}
                    string put = PickupTime.SelectedValue.ToString() ?? "";
                    if(put.Contains("Butta"))
                    {
                        string tmp = put.Replace("Butta Me Up! (", "");
                        tmp = tmp.Replace(")", "");
                        _pickupTime = TimeOnly.Parse(tmp);
                    }
                    else
                        _pickupTime= TimeOnly.Parse(put);

                    int counter = 1;
                    foreach (string[] item in CoreComponents.CartDetails) {

                        string fullTest = (counter +": "+ item[1] + "--Priced: " + item[4]).ToString(); 
                        fillWithOrder.Add(fullTest);
                        //Unleash when enhancements are available
                        if (item.Length > 5)
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
                    fillWithOrder.Add("");
                    fillWithOrder.Add("===========================================================================");
                    fillWithOrder.Add("");
                    fillWithOrder.Add(payThere);
                    fillWithOrder.Add("");
                    fillWithOrder.Add("Order Due: " + _pickupTime.ToString("t"));
                 
                    File.WriteAllLines(path, fillWithOrder);

                    this.NavigationService.Navigate(new PayConfirm(_pickupTime));
                }
                else
                {
                    //you really shouldnt be here....like seriously how are you here.
                    if (counter >= 2)
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
                        MessageBox.Show("You need to enter payment details or select pay on pickup.");
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

        private DateTime getFirstTimeWindow()
        {
            DateTime nextPUWindow;
            DateTime currentTime = DateTime.Now;
            TimeSpan roundTime = TimeSpan.FromMinutes(15);

            nextPUWindow = new DateTime((currentTime.Ticks + roundTime.Ticks - 1) / roundTime.Ticks * roundTime.Ticks, currentTime.Kind);

            //min pickup time is 8 minutes from current time
            TimeSpan checkPUWindow = nextPUWindow - currentTime;
            while (checkPUWindow.TotalMinutes < 8)
            {
                nextPUWindow = nextPUWindow.AddMinutes(15);
                checkPUWindow = nextPUWindow - currentTime;
            }

            return nextPUWindow;
        }
    }
}

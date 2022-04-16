using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    internal class Order
    {
        private List<Item> _orderItems = new List<Item>();
        private double _subTotal;
        private double _tax;
        private double _total;

        public Order()
        {
            _subTotal = 0;
            _tax = 0;
            _total = 0;
        }

        public double Tax
        {
            get { return _tax; }
        }

        public double Total
        {
            get { return _total; }
        }

        /// <summary>
        /// Get: Returns _subTotal
        /// Set: (private) sets _subTotal, calculates _tax and _total
        /// </summary>
        public double SubTotal
        {
            get { return _subTotal; }
            
            private set 
            { 
                _subTotal = value;
                _tax = _subTotal * 0.06;
                _total = _subTotal + _tax;
            }
        }

        /// <summary>
        /// Add an item to the current order and update the totals 
        /// </summary>
        /// <param name="newItem">Item to add, can be a normal Item or an EnhancedItem</param>
        public void addToOrder(Item newItem)
        {
            //add item to the orderItems Array
            _orderItems.Add(newItem);

            //add price to the sub total (tax and total are figured out automatically)
            SubTotal += newItem.Price;
        }

        /// <summary>
        /// retrieves the order details in a string array that can be processed and presented to the user on the frontend
        /// each item's details start at the next index after Item.ITEM_START and end at the index before Item.ITEM_END
        /// this allows more flexability in the size of order detail arrays. All return arrays will follow the same format
        /// with 0 being the index immediatly following Item.ITEM_START
        ///     0: represents item name
        ///     1: represents item description
        ///     2: represents URL to the item's image
        ///     3: item's total in currency formated string
        ///     4+: every index after 3 represents one enhacment added to this item until you reach Item.ITEM_END
        /// </summary>
        /// <returns>An array of strings contianing the details of an order ready to be formated and presented to the user</returns>
        public string[] getOrderDetails()
        {
            //create a container to hold the string array
            List<string> details = new List<string>();

            //itterate through all items in the cart
            for (int i = 0; i < _orderItems.Count; i++)
            {
                //get the item info from item at index i then extract each output line
                for (int j = 0; j < _orderItems[i].getItemInfo().Length; j++)
                {
                    //add each output line to the container
                    details.Add(_orderItems[i].getItemInfo()[j]);
                }
            }

            //return the container as a string
            return details.ToArray();
        }
    }
}

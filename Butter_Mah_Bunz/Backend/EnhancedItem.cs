using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    internal class EnhancedItem : Item
    {
        public EnhancedItem(string name, string description, double price, string imageURL = "") : base(name, description, price, imageURL) {}
        public EnhancedItem(Item baseItem) : base(baseItem) {}

        private List<Enhancment> _enhancments = new List<Enhancment>();

        public void addEnhancment(Enhancment itemEnhancment)
        {
            _enhancments.Add(itemEnhancment);
        }
        override public string[] getItemInfo()
        {
            //create list to hold return array
            List<string> items = new List<string>();

            items.Add(ITEM_START);

            items.Add(_name);
            items.Add(_description);
            items.Add(_imageURL);
            items.Add(_price.ToString("C"));

            for (int index = 0; index < _enhancments.Count; index++)
            {
                string addonStr = _enhancments[index].Name + " " + _enhancments[index].Price.ToString("C");
                items.Add(addonStr);
            }

            items.Add(ITEM_END);

            //return array
            return items.ToArray();
        }
    }
}

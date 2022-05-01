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
        public void removeEnhancment(Enhancment itemEnhancment)
        {
            _enhancments.Remove(itemEnhancment);
        }
        override public string[] getItemInfo(bool closeItem = true)
        {
            List<string> items = base.getItemInfo(false).ToList();
            if (items[items.Count - 1] != Item.ITEM_END)
                throw new FormatException("Premature end of item detected");


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

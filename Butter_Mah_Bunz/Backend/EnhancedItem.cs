using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    public class EnhancedItem : Item
    {
        public EnhancedItem(string name, string description, double price, string imageURL = "") : base(name, description, price, imageURL) { }
        public EnhancedItem(Item baseItem) : base(baseItem) { }

        private List<Enhancment> _enhancments = new List<Enhancment>();

        public void addEnhancment(Enhancment itemEnhancment)
        {
            _enhancments.Add(itemEnhancment);
        }
        public bool removeEnhancment(Enhancment itemEnhancment)
        {
            return _enhancments.Remove(itemEnhancment);
        }
        public override bool IsEnhanced
        {
            get { return true; }
        }
        public override double Price
        {
            get
            {
                double addonPrice = 0;

                for (int i = 0; i < _enhancments.Count; i++)
                {
                    addonPrice += _enhancments[i].Price;
                }

                return _price + addonPrice;
            }
        }
        override public string[] getItemInfo(bool closeItem = true)
        {
            List<string> items = base.getItemInfo(false).ToList();
            if (items[items.Count - 1] == Item.ITEM_END)
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    public class Item
    {
        private static string? _itemStartIndicator;
        private static string? _itemEndIndicator;
        private static int nextID = 0;

        private static int IDAssigner
        {
            get
            {
                nextID++; //increment everytime a new item is added
                return nextID; //gives item objects a uniqueID number
            }
        }
        private static string getRandomNoise()
        {
            const int NOISE_AMNT = 1024;

            Random random = new Random();
            string randomNoise = "";

            for (int i = 0; i < NOISE_AMNT; i++)
            {
                randomNoise += (char)random.Next(32, 126);
            }

            return randomNoise;
        }

        public static string ITEM_START
        {
            get 
            { 
                if(_itemStartIndicator == null)
                {
                    _itemStartIndicator = "_____ITEM_START_____" + getRandomNoise();
                }
                return _itemStartIndicator;
            }
        }
        public static string ITEM_END
        {
            get
            {
                if (_itemEndIndicator == null)
                {
                    _itemEndIndicator = "_____ITEM_END_____" + getRandomNoise();
                }
                return _itemEndIndicator;
            }
        }
        public Item(string name, string description, double price, string imageURL = "")
        {
            _name = name;
            _description = description;
            _imageURL = imageURL;
            _price = price;
            _uniqueID = "ItemUID_" + Item.IDAssigner.ToString();
        }
        public Item(Item cItem)
        {
            _name = cItem._name;
            _description = cItem._description;
            _price = cItem._price;
            _imageURL=cItem._imageURL;
            _uniqueID = "ItemUID_" + Item.IDAssigner.ToString();
        }

        protected string _name;
        protected string _description;
        protected string _imageURL;
        protected double _price;
        protected string _uniqueID;
        public string Name
        {
            get { return _name; }
        }
        public string Description
        {
            get { return _description; }
        }
        public virtual double Price
        {
            get { return _price; }
        }
        public string ImageURL
        {
            get { return _imageURL; }
        }
        public string UniqueID
        {
            get { return _uniqueID; }
        }
        virtual public string[] getItemInfo(bool closeItem = true)
        {
            //create list to hold return array
            List<string> items = new List<string>();

            items.Add(ITEM_START);

            items.Add(_uniqueID);
            items.Add(_name);
            items.Add(_description);
            items.Add(_imageURL);
            items.Add(_price.ToString("C"));

            if (closeItem) items.Add(ITEM_END);

            //return array
            return items.ToArray();
        }
    }
}
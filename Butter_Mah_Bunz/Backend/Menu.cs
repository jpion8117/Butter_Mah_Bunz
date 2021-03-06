using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Backend
{
    
    public class Menu
    {
        //settings filepath
        const string MENU_FILE_PATH = "Backend/Menu/menu.xml";
        
        private List<Item> _menuItems = new List<Item>();
        private List<Enhancment> _enhancments = new List<Enhancment>();
        private bool _menuReady = false;

        public List<Item> MenuItems
        {
            get { return _menuItems; }
        }
        public List<Enhancment> Enhancments
        {
            get { return _enhancments; }
        }
        
        public bool MenuReady
        {
            get { return _menuReady; }
        }

        public void LoadMenu()
        {
            bool menuFound = false;
            bool enhancmentsFound = false;
            
            XmlDocument doc = new XmlDocument();
            doc.Load(MENU_FILE_PATH);

            //parse menu item data from the XML menu doc.
            foreach (XmlNode node in doc.GetElementsByTagName("item"))
            {
                //note that menu was found
                menuFound = true;

                //variables for holding the item info
                string name = "";
                string description = "";
                string imageURL = "";
                double price = 0;

                //parse each item's data from the xml file and save to local vars
                foreach (XmlNode childNode in node.ChildNodes)
                {
                    if(childNode.Name == "name")
                        name = childNode.InnerText;
                    else if(childNode.Name == "description")
                        description = childNode.InnerText;
                    else if (childNode.Name == "imageURL")
                        imageURL = childNode.InnerText;
                    else if (childNode.Name == "price")
                        price = double.Parse(childNode.InnerText);
                }

                //add the item info to the menu items
                _menuItems.Add(new Item(name, description, price, imageURL));
            }

            //locate all nodes in the enhancment section of the xml menu file
            foreach (XmlNode node in doc.GetElementsByTagName("enhancment"))
            {
                //note that enhancment menu was found
                enhancmentsFound = true;

                string name = "";
                double price = 0;

                //parse the data from each enhancment into local vars
                foreach (XmlNode childNode in node.ChildNodes)
                {
                    if (childNode.Name == "name")
                        name = childNode.InnerText;
                    else if (childNode.Name == "price")
                        price = double.Parse(childNode.InnerText);
                }

                //add the enhancment to the enhancments list
                _enhancments.Add(new Enhancment(name, price));
            }

            //set menu ready
            _menuReady = menuFound && enhancmentsFound;
        }
    }
}

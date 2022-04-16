using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    internal class Enhancment
    {
        public Enhancment(string name, double price)
        {
            _name = name;
            _price = price;
        }

        private string _name;
        private double _price;

        public string Name
        {
            get { return _name; }
        }

        public double Price
        {
            get { return _price; } 
        }

    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp
{
    public class ProductClass
    {
        public string name { get; private set; }
        public string description { get; private set; }
        public int availableNum { get; private set; }
        public string category { get; private set; }
        public float price { get; private set; }

        public ProductClass(string name, string description, int availableNum, string category, float price)
        {
            this.name = name;
            this.description = description;
            this.availableNum = availableNum;
            this.category = category;
            this.price = price;
        }
    }
}

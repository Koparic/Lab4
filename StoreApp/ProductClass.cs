using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp
{
    public class ProductClass
    {
        public string name { get; private set; }
        public int availableNum { get; private set; }
        public string category { get; private set; }
        public int price { get; private set; }

        public ProductClass(string name, int avNum, string category, int price)
        {
            this.name = name;
            this.availableNum = avNum;
            this.category = category;
            this.price = price;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp
{
    public class OrderClass
    {
        public int id { get; private set; }
        public List<(ProductClass pr, int n)> products { get; private set; } = new List<(ProductClass pr, int n)>();
        public string adress { get; private set; }
        public string comment { get; private set; }
        public bool isClosed { get; private set; }

        public OrderClass(int id, List<(ProductClass pr, int n)> products, string comment, string adress, bool isClosed = false)
        {
            this.id = id;
            this.products = products;
            this.comment = comment;
            this.adress = adress;
            this.isClosed = isClosed;
        }

        public void UpdateOrderAddress(string adress, string comment)
        {
            if (comment == null || comment.Length == 0)
            {
                comment = "Комментарий отсутствует";
            }
            this.adress = adress;
            this.comment = comment;
        }


        public void UpdateProduct(ProductClass product, int num)
        {
            for (int i = 0; i < products.Count; i++)
                if (products[i].pr == product)
                {
                    if (num == 0)
                    {
                        products.RemoveAt(i);
                        return;
                    }
                    products[i] = (product, num);
                    return;
                }
            if (num != 0)
                products.Add((product, num));
        }
    }
}

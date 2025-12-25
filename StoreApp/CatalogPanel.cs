using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StoreApp
{
    public class CatalogPanel : TableLayoutPanel
    {
        List<ProductClass> products = new List<ProductClass>();
        
        public CatalogPanel()
        {
            ColumnCount = 3;
            RowStyles.Clear();
            RowStyles.Add(new RowStyle(SizeType.AutoSize, 100F));
            Dock = DockStyle.Fill;
            AutoScroll = true;
            products.Add(new ProductClass("1", 8, "a", 100));
            products.Add(new ProductClass("2", 8, "a", 200));
            products.Add(new ProductClass("3", 8, "a", 400));
            products.Add(new ProductClass("4", 8, "a", 110));
            products.Add(new ProductClass("5", 8, "a", 400));
            products.Add(new ProductClass("6", 8, "a", 100000));
            CatalogListUpdate(products);
        }

        public void CatalogListUpdate(List<ProductClass> products)
        {
            int rowIndex = 0;
            int colIndex = 0;
            this.Controls.Clear();
            foreach (var product in products)
            {
                var card = new ProductCard(product.name, product.price.ToString(), Properties.Resources.Flower);
                this.Controls.Add(card, colIndex++, rowIndex);

                if (colIndex >= this.ColumnCount)
                {
                    colIndex = 0;
                    rowIndex++;
                }
            }
        }

    }
}

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
        public CatalogPanel()
        {
            ColumnCount = 3;
            RowStyles.Clear();
            RowStyles.Add(new RowStyle(SizeType.AutoSize, 100F));
            Dock = DockStyle.Fill;
            AutoScroll = true;

            //CatalogListUpdate(products);
        }

        public void CatalogListUpdate(ShoppingCart shoppingCart, ProductManager productManager)
        {
            int rowIndex = 0;
            int colIndex = 0;
            this.Controls.Clear();
            foreach (var product in productManager.ProductsBase)
            {
                var card = new ProductCard(product, productManager.GetImage(product.name), shoppingCart);
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

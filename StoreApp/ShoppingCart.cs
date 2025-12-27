using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StoreApp
{
    public class ShoppingCart : UserControl
    {
        private DataGridView cartGrid;
        private OrderClass order;

        public ShoppingCart()
        {
            order = new OrderClass(0, new List<(ProductClass, int)>(), "Коментарий к заказу", "Ваш Адресс");
            cartGrid = new DataGridView();
            cartGrid.Dock = DockStyle.Fill;
            cartGrid.ReadOnly = true;
            cartGrid.AllowUserToAddRows = false;
            cartGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            cartGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            cartGrid.Columns.Add("Name", "Название");
            cartGrid.Columns.Add("Quantity", "Количество");
            cartGrid.Columns.Add("Total", "Сумма");

            Controls.Add(cartGrid);
        }
        public ShoppingCart(OrderClass order)
        {
            this.order = order;
            cartGrid = new DataGridView();
            cartGrid.Dock = DockStyle.Fill;
            cartGrid.ReadOnly = true;
            cartGrid.AllowUserToAddRows = false;
            cartGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            cartGrid.Columns.Add("Name", "Название");
            cartGrid.Columns.Add("Quantity", "Количество");
            cartGrid.Columns.Add("Total", "Сумма");


            cartGrid.Rows.Clear();
            float sum = 0;
            int num = 0;
            foreach ((ProductClass pr, int n) p in order.products)
            {
                sum += p.pr.price * p.n;
                num += p.n;
                cartGrid.Rows.Add(p.pr.name, p.n.ToString() + " шт.", (p.pr.price * p.n).ToString()+ " руб.");
            }
            cartGrid.Rows.Add("Итого:", num.ToString()+" шт.", sum.ToString()+" руб.");

            Controls.Add(cartGrid);
        }

        public void AddItem(ProductClass item, int quantity)
        {
            order.UpdateProduct(item, quantity);
            cartGrid.Rows.Clear();
            float sum = 0;
            int num = 0;
            foreach ((ProductClass pr, int n) p in order.products)
            {
                sum += p.pr.price * p.n;
                num += p.n;
                cartGrid.Rows.Add(p.pr.name, p.n.ToString() + " шт.", (p.pr.price * p.n).ToString() + " руб.");
            }
            cartGrid.Rows.Add("Итого:", num.ToString() + " шт.", sum.ToString() + " руб.");

        }

        public void ClearItems()
        {
            order = new OrderClass(0, new List<(ProductClass, int)>(), "Коментарий к заказу", "Ваш Адресс");
            cartGrid.Rows.Clear();
        }

        public OrderClass GetOrder()
        {
            return order;
        }
    }
}

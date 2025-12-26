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
        private OrderManager orderManager;
        private OrderClass order = new OrderClass(0, new List<(ProductClass, int)>(), "");

        public ShoppingCart(OrderManager orderM)
        {
            orderManager = orderM;
            cartGrid = new DataGridView();
            cartGrid.Dock = DockStyle.Fill;
            cartGrid.ReadOnly = true;
            cartGrid.AllowUserToAddRows = false;
            cartGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Колонки для отображения товаров
            cartGrid.Columns.Add("Name", "Название");
            cartGrid.Columns.Add("Quantity", "Количество");
            cartGrid.Columns.Add("Total", "Сумма");

            Controls.Add(cartGrid);
        }

        public void AddItem(ProductClass item, int quantity)
        {
            order.UpdateProduct(item, quantity);
            cartGrid.Rows.Clear();
            foreach ((ProductClass pr, int n) p in order.products)
                cartGrid.Rows.Add(p.pr.name, p.n, p.pr.price * p.n);
        }

        public void ClearItems()
        {
            order = new OrderClass(0, new List<(ProductClass, int)>(), "");
            cartGrid.Rows.Clear();
        }

        public void AddOrderToManager()
        {
            if (order.products.Count > 0)
            {
                orderManager.AddOrder(order);
                order = new OrderClass(0, new List<(ProductClass, int)>(), "");
            }
        }
    }
}

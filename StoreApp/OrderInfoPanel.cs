using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace StoreApp
{
    class OrderInfoPanel : Panel
    {
        private Label addressLabel;
        private Label commentLabel;
        private ShoppingCart shoppingCart;
        private Button doneBttn;
        private OrderClass order;

        private Form1 form;

        public OrderInfoPanel(OrderClass order, Form1 form)
        {
            if (order != null)
            {
                this.form = form;
                this.order = order;
                this.Dock = DockStyle.Fill;
                commentLabel = new Label
                {
                    Text = "Коментарий: " + order.comment,
                    Dock = DockStyle.Top,
                    AutoSize = true
                };
                this.Controls.Add(commentLabel);
                addressLabel = new Label
                {
                    Text = "Адресс: " + order.adress,
                    Dock = DockStyle.Top,
                    AutoSize = true
                };
                this.Controls.Add(addressLabel);

                shoppingCart = new ShoppingCart(order)
                {
                    Dock = DockStyle.Top
                };
                this.Controls.Add(shoppingCart);

                if (!order.isClosed) {
                    doneBttn = new Button
                    {
                        Text = "Выполнено",
                        AutoSize = true,
                        Padding = new Padding(5),
                        Margin = new Padding(5),
                        Dock = DockStyle.Bottom
                    };
                    doneBttn.Click += CloseOrder;
                    this.Controls.Add(doneBttn);
                }
            }
            else
            {
                commentLabel = new Label
                {
                    Text = "Выберете заказ",
                    Dock = DockStyle.Top,
                    AutoSize = true
                };
                this.Controls.Add(commentLabel);
            }
        }

        private void CloseOrder(object sender, EventArgs e)
        {
            form.orderManager.UpdateOrder(new OrderClass(order.id, order.products, order.comment, order.adress, true));
            form.UpdateOrderLists();
        }
    }
}

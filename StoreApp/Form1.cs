using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StoreApp
{
    public partial class Form1 : Form
    {
        private TabControl tabControl;
        private CatalogPanel catalogPanel;
        private ShoppingCart shoppingCart;

        ListBox openOrdersList = new ListBox();
        ListBox closedOrdersList = new ListBox();
        private OrderInfoPanel orderInfoPanel = new OrderInfoPanel(null, null);
        SplitContainer splitContainer = new SplitContainer();

        public OrderManager orderManager { get; private set; } = new OrderManager();

        
        public Form1()
        {
            InitializeComponent();

            tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;
            tabControl.TabPages.Add("Ассортимент");
            tabControl.TabPages.Add("Менеджмент заказов");
            Controls.Add(tabControl);

            //orderManager.LoadOrders();

            ConfigureAssortmentPage(tabControl.TabPages[0]);
            ConfigureOrdersManagementPage(tabControl.TabPages[1]);
        }


        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            //orderManager.SaveOrders();
        }


        private void ConfigureAssortmentPage(TabPage page)
        {
            SplitContainer splitContainer = new SplitContainer();
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.Orientation = Orientation.Vertical;

            Panel actionPanel = new Panel();
            Button buttonSubmit;
            Button buttonClear;

            actionPanel.BackColor = Color.FromArgb(240, 240, 240);
            actionPanel.Dock = DockStyle.Bottom;
            actionPanel.Padding = new Padding(10);

            // Кнопка "Оформить"
            buttonSubmit = new Button
            {
                Text = "Оформить",
                Dock = DockStyle.Right,
                AutoSize = true
            };
            buttonSubmit.Click += OnSubmitClick;

            // Кнопка "Очистить"
            buttonClear = new Button
            {
                Text = "Очистить",
                Dock = DockStyle.Right,
                AutoSize = true
            };
            buttonClear.Click += OnClearClick;

            actionPanel.Controls.Add(buttonSubmit);
            actionPanel.Controls.Add(buttonClear);

            shoppingCart = new ShoppingCart();
            shoppingCart.Dock = DockStyle.Fill;
            splitContainer.Panel2.Controls.Add(shoppingCart);
            splitContainer.Panel2.Controls.Add(actionPanel);

            catalogPanel = new CatalogPanel();
            catalogPanel.Dock = DockStyle.Fill;
            catalogPanel.CatalogListUpdate(shoppingCart);
            splitContainer.Panel1.Controls.Add(catalogPanel);


            splitContainer.SplitterDistance = page.Width - 110;
            page.Controls.Add(splitContainer);
        }

        private void ConfigureOrdersManagementPage(TabPage page)
        {
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.Orientation = Orientation.Vertical;
            splitContainer.SplitterDistance = 60;

            openOrdersList.Dock = DockStyle.Fill;

            openOrdersList.SelectedIndexChanged += OnOpenedListSelect;

            closedOrdersList.Dock = DockStyle.Fill;
            
            closedOrdersList.SelectedIndexChanged += OnClosedListSelect;

            SplitContainer ordersSplit = new SplitContainer();
            ordersSplit.Dock = DockStyle.Fill;
            ordersSplit.Orientation = Orientation.Horizontal;
            ordersSplit.Panel1.Controls.Add(openOrdersList);
            ordersSplit.Panel2.Controls.Add(closedOrdersList);

            splitContainer.Panel1.Controls.Add(ordersSplit);

            splitContainer.Panel2.Controls.Add(orderInfoPanel);
            page.Controls.Add(splitContainer);
        }

        public void UpdateOrderLists()
        {
            openOrdersList.Items.Clear();
            closedOrdersList.Items.Clear();
            foreach (OrderManager.orderInfo orderInfo in orderManager.CreateOpenedOrdersList())
            {
                openOrdersList.Items.Add(orderInfo);
            }
            foreach (OrderManager.orderInfo orderInfo in orderManager.CreateClosedOrdersList())
            {
                closedOrdersList.Items.Add(orderInfo);
            }
        }

        private void OnSubmitClick(object sender, EventArgs e)
        {
            OrderClass order = shoppingCart.GetOrder();
            if (order.products.Count > 0)
            {
                using (OrderMakingForm form = new OrderMakingForm(order))
                {
                    DialogResult result = form.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        order.UpdateOrderAddress(form.addressTextBox.Text, form.commentTextBox.Text);
                        orderManager.AddOrder(order);
                        UpdateOrderLists();
                        shoppingCart.ClearItems();
                        catalogPanel.CatalogListUpdate(shoppingCart);
                        MessageBox.Show("Заказ подтвержден!");
                    }
                    else if (result == DialogResult.Cancel)
                    {
                        MessageBox.Show("Заказ отменен.");
                    }
                }
            }
        }

        private void OnClearClick(object sender, EventArgs e)
        {
            shoppingCart.ClearItems();
            catalogPanel.CatalogListUpdate(shoppingCart);
        }

        private void OnClosedListSelect(object sender, EventArgs e)
        {
            if (closedOrdersList.SelectedIndex != -1)
            {
                OrderManager.orderInfo orderInfo = (OrderManager.orderInfo)closedOrdersList.SelectedItem;
                OrderClass order = orderManager.FindOrderByID(orderInfo.id);
                if (order == null)
                    MessageBox.Show("Ошибка!\nЗаказ не найден.");
                else
                {
                    orderInfoPanel = new OrderInfoPanel(order, this);
                    splitContainer.Panel2.Controls.Clear();
                    splitContainer.Panel2.Controls.Add(orderInfoPanel);
                }
            }
        }

        private void OnOpenedListSelect(object sender, EventArgs e)
        {
            if (openOrdersList.SelectedIndex != -1)
            {
                OrderManager.orderInfo orderInfo = (OrderManager.orderInfo)openOrdersList.SelectedItem;
                OrderClass order = orderManager.FindOrderByID(orderInfo.id);
                if (order == null)
                    MessageBox.Show("Ошибка!\nЗаказ не найден.");
                else
                {

                    orderInfoPanel = new OrderInfoPanel(order, this);
                    splitContainer.Panel2.Controls.Clear();
                    splitContainer.Panel2.Controls.Add(orderInfoPanel);
                }
            }
        }
    }
}

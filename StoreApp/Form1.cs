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


        private OrderManager orderManager = new OrderManager();

        public Form1()
        {
            InitializeComponent();

            tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;
            tabControl.TabPages.Add("Ассортимент");
            tabControl.TabPages.Add("Менеджмент заказов");
            Controls.Add(tabControl);

            ConfigureAssortmentPage(tabControl.TabPages[0]);
            ConfigureOrdersManagementPage(tabControl.TabPages[1]);
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

            shoppingCart = new ShoppingCart(orderManager);
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
            SplitContainer splitContainer = new SplitContainer();
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.Orientation = Orientation.Vertical;
            splitContainer.SplitterDistance = 60;

            ListBox openOrdersList = new ListBox();
            openOrdersList.Dock = DockStyle.Fill;
            //openOrdersList.Items.AddRange(orderManager.CreateOpenedOrdersList().ToArray());
            //closedOrdersList.SelectedIndexChanged += ;

            ListBox closedOrdersList = new ListBox();
            closedOrdersList.Dock = DockStyle.Fill;
            //closedOrdersList.Items.AddRange(orderManager.CreateClosedOrdersList().ToArray());
            //closedOrdersList.SelectedIndexChanged += ;

            SplitContainer ordersSplit = new SplitContainer();
            ordersSplit.Dock = DockStyle.Fill;
            ordersSplit.Orientation = Orientation.Horizontal;
            ordersSplit.Panel1.Controls.Add(openOrdersList);
            ordersSplit.Panel2.Controls.Add(closedOrdersList);

            splitContainer.Panel1.Controls.Add(ordersSplit);

            page.Controls.Add(splitContainer);
        }

        private void OnSubmitClick(object sender, EventArgs e)
        {
            shoppingCart.AddOrderToManager();
        }

        private void OnClearClick(object sender, EventArgs e)
        {
            shoppingCart.ClearItems();
        }
    }
}

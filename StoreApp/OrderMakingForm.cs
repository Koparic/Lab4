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
    public partial class OrderMakingForm : Form
    {
        private ShoppingCart shoppingCart;
        public TextBox addressTextBox;
        public TextBox commentTextBox;

        public OrderMakingForm(OrderClass order)
        {
            InitializeComponent();

            this.Text = "Оформление заказа";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            shoppingCart = new ShoppingCart(order);
            shoppingCart.Dock = DockStyle.Top;
            this.Controls.Add(shoppingCart);

            // Поле для ввода адреса
            addressTextBox = new TextBox
            {
                Multiline = true,
                Dock = DockStyle.Bottom,
                Height = 50,
                Text = "Адрес доставки...",
                ForeColor = Color.Gray
            };
            addressTextBox.Enter += addressTextBox_Enter;
            addressTextBox.Leave += addressTextBox_Leave;
            this.Controls.Add(addressTextBox);

            // Поле для комментариев
            commentTextBox = new TextBox
            {
                Multiline = true,
                Dock = DockStyle.Bottom,
                Height = 50,
                Text = "Комментарий к заказу...",
                ForeColor = Color.Gray
            };
            commentTextBox.Enter += commentTextBox_Enter;
            commentTextBox.Leave += commentTextBox_Leave;
            this.Controls.Add(commentTextBox);

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
            buttonSubmit.Click += ConfirmButton_Click;

            // Кнопка "Очистить"
            buttonClear = new Button
            {
                Text = "Отмена",
                Dock = DockStyle.Right,
                AutoSize = true
            };
            buttonClear.Click += CancelButton_Click;

            actionPanel.Controls.Add(buttonSubmit);
            actionPanel.Controls.Add(buttonClear);

            this.Controls.Add(actionPanel);
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            if (addressTextBox.TextLength > 5 && addressTextBox.Text != "Адрес доставки...")
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Требуется ввести адресс!");
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void addressTextBox_Enter(object sender, EventArgs e)
        {
            if (addressTextBox.Text == "Адрес доставки...")
            {
                addressTextBox.Text = null;
                addressTextBox.ForeColor = Color.Black;
            }
        }
        private void addressTextBox_Leave(object sender, EventArgs e)
        {
            if (addressTextBox.TextLength == 0)
            {
                addressTextBox.Text = "Адрес доставки...";
                addressTextBox.ForeColor = Color.Gray;
            }
        }
        private void commentTextBox_Enter(object sender, EventArgs e)
        {
            if (commentTextBox.Text == "Комментарий к заказу...")
            {
                commentTextBox.Text = null;
                commentTextBox.ForeColor = Color.Black;
            }
        }
        private void commentTextBox_Leave(object sender, EventArgs e)
        {
            if (commentTextBox.TextLength == 0)
            {
                commentTextBox.Text = "Комментарий к заказу...";
                commentTextBox.ForeColor = Color.Gray;
            }
        }
    }
}

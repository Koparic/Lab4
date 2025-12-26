using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StoreApp
{
    public class ProductCard : Panel
    {
        private Button btnIncrease;
        private Button btnDecrease;
        private Button btnAdd;
        private Label lblQuantity;
        private int quantity = 0;

        ProductClass product;
        private ShoppingCart shoppingCart;
        public ProductCard(ProductClass product, ShoppingCart shoppingCart)
        {
            this.product = product;
            this.shoppingCart = shoppingCart;

            AutoSize = true;
            Padding = new Padding(5);
            Margin = new Padding(5);
            BackColor = Color.WhiteSmoke;
            BorderStyle = BorderStyle.FixedSingle;

            PictureBox pictureBox = new PictureBox
            {
                Image = product.image,
                SizeMode = PictureBoxSizeMode.Zoom,
                Width = 120,
                Height = 120,
                Location = new Point(5, 5)
            };

            Label labelName = new Label
            {
                Text = product.name,
                Font = new Font(FontFamily.GenericSansSerif, 14f),
                Location = new Point(5, pictureBox.Bottom + 5),
                Width = pictureBox.Width
            };

            Label labelPrice = new Label
            {
                Text = $"{product.price} руб.",
                Font = new Font(FontFamily.GenericSansSerif, 12f),
                ForeColor = Color.DarkRed,
                Location = new Point(5, labelName.Bottom + 5),
                Width = pictureBox.Width
            };

            btnAdd = new Button
            {
                Text = "Добавить",
                Width = pictureBox.Width,
                Height = 30,
                Location = new Point(5, labelPrice.Bottom + 5),
            };
            btnAdd.Click += BtnIncrease_Click;

            btnDecrease = new Button
            {
                Text = "<",
                Width = 30,
                Height = 30,
                Enabled = false,
                Location = new Point(5, labelPrice.Bottom + 5),
            };
            btnDecrease.Click += BtnDecrease_Click;

            lblQuantity = new Label
            {
                Text = "",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font(FontFamily.GenericSansSerif, 12f),
                Width = pictureBox.Width - btnDecrease.Width * 2 - 10,
                Height = 30,
                Location = new Point(btnDecrease.Right + 5, labelPrice.Bottom + 5),
            };

            btnIncrease = new Button
            {
                Text = ">",
                Width = 30,
                Height = 30,
                Enabled = false,
                Location = new Point(lblQuantity.Right + 5, labelPrice.Bottom + 5),
            };
            btnIncrease.Click += BtnIncrease_Click;


            Controls.Add(pictureBox);
            Controls.Add(labelName);
            Controls.Add(labelPrice);
            Controls.Add(btnAdd);
            Controls.Add(lblQuantity);
            Controls.Add(btnIncrease);
            Controls.Add(btnDecrease);
            UpdateUI();
        }

        private void BtnIncrease_Click(object sender, EventArgs e)
        {
            quantity++;
            UpdateUI();
            shoppingCart.AddItem(product, quantity);
        }

        private void BtnDecrease_Click(object sender, EventArgs e)
        {
            if (quantity > 0)
            {
                quantity--;
                UpdateUI();
            }
            shoppingCart.AddItem(product, quantity);
        }

        private void UpdateUI()
        {
            btnIncrease.Visible = btnDecrease.Visible = false;
            lblQuantity.Visible = false;
            btnAdd.Visible = false;
            btnIncrease.Enabled = btnDecrease.Enabled = false;
            lblQuantity.Enabled = false;
            btnAdd.Enabled = false;

            if (quantity <= 0)
            {
                btnAdd.Visible = true;
                btnAdd.Enabled = true;
            }
            else
            {
                btnIncrease.Visible = true;
                btnDecrease.Visible = true;
                btnIncrease.Enabled = true;
                btnDecrease.Enabled = true;
                lblQuantity.Text = quantity.ToString();
                lblQuantity.Visible = true;
                lblQuantity.Enabled = true;
            }
        }
    }
}

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
        public ProductCard(string name, string price, Image image)
        {
            AutoSize = true;
            Padding = new Padding(5);
            Margin = new Padding(5);
            BackColor = Color.WhiteSmoke;
            BorderStyle = BorderStyle.FixedSingle;

            PictureBox pictureBox = new PictureBox
            {
                Image = image,
                SizeMode = PictureBoxSizeMode.Zoom,
                Width = 120,
                Height = 120,
                Location = new Point(5, 5)
            };

            Label labelName = new Label
            {
                Text = name,
                Font = new Font(FontFamily.GenericSansSerif, 14f),
                Location = new Point(5, pictureBox.Bottom + 5),
                Width = pictureBox.Width
            };

            // Цена товара
            Label labelPrice = new Label
            {
                Text = $"{price} руб.",
                Font = new Font(FontFamily.GenericSansSerif, 12f),
                ForeColor = Color.DarkRed,
                Location = new Point(5, labelName.Bottom + 5),
                Width = pictureBox.Width
            };

            Controls.Add(pictureBox);
            Controls.Add(labelName);
            Controls.Add(labelPrice);
        }
    }
}

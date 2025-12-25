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
        private CatalogPanel catalogPanel;

        public Form1()
        {
            InitializeComponent();
            catalogPanel = new CatalogPanel();
            //catalogPanel.Location = new Point(10, 10);
            Controls.Add(catalogPanel); 
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

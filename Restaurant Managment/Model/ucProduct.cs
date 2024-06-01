using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant_Managment.Model
{
    
    public partial class ucProduct : UserControl
    {
        public ucProduct()
        {
            InitializeComponent();
        }

        public event EventHandler onSelect = null;

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            onSelect?.Invoke(this, e);
        }

        public int id {  get; set; }
        public string productPrice {  get; set; }

        public string productCategory { get; set; }

        public string productName
        {
            get { return lblName.Text; }
            set { lblName.Text = value; }
        }

        public Image productImage
        {
            get { return picImage.Image; }
            set { picImage.Image = value; }
        }
    }
}

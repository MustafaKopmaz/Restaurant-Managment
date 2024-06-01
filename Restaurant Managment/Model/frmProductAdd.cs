using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant_Managment.Model
{
    public partial class frmProductAdd : SampleAdd
    {
        public frmProductAdd()
        {
            InitializeComponent();
        }

        public int id = 0;
        public int cID = 0;

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image(.jpg, .png)|* .png; *.jpg";
            if (ofd.ShowDialog()==DialogResult.OK)
            {
                filePath = ofd.FileName;
                txtImage.Image=new Bitmap(filePath);
            }
        }

        string filePath;
        Byte[] imageByteArray;
        private void frmProductAdd_Load(object sender, EventArgs e)
        {
            string qry = "Select catID 'id' , catName 'name' from category";
            MainClass.CBFill(qry, cbCat);

            if(cID>0)
            {
                cbCat.SelectedValue = cID;
            }

            if (id>0)
            {
                ForUpdateLoadData();
            }
        }
        public override void btnSave_Click(object sender, EventArgs e)
        {
            string qry = "";

            if (id == 0)
            {
                qry = "Insert into products Values(@Name ,@price ,@cat ,@img)";
            }
            else
            {
                qry = "Update products Set productName = @Name , productPrice = @price , CategoryID = @cat , productImage = @img where productID = @id ";
            }

            Image temp = new Bitmap(txtImage.Image);
            MemoryStream ms = new MemoryStream();
            temp.Save(ms,System.Drawing.Imaging.ImageFormat.Png);
            imageByteArray = ms.ToArray();


            Hashtable ht = new Hashtable();
            ht.Add("@id", id);
            ht.Add("@Name", txtName.Text);
            ht.Add("@price", txtPrice.Text);
            ht.Add("@cat", Convert.ToInt32(cbCat.SelectedValue));
            ht.Add("@img", imageByteArray);

            if (MainClass.SQ1(qry, ht) > 0)
            {
                MessageBox.Show("Kaydedildi");
                id = 0;
                cID = 0;
                txtName.Text = "";
                txtPrice.Text = "";
                cbCat.SelectedIndex = 0;
                cbCat.SelectedIndex = -1;
                txtImage.Image = Restaurant_Managment.Properties.Resources.shopping_bag;
                txtName.Focus();
            }
        }

        private void ForUpdateLoadData()
        {
            string qry = @"Select * from products where productID = @id";
            SqlCommand cmd = new SqlCommand(qry, MainClass.con);
            cmd.Parameters.AddWithValue("@id", id); // assuming id is defined somewhere
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                txtName.Text = dt.Rows[0]["productName"].ToString();
                txtPrice.Text = dt.Rows[0]["productPrice"].ToString();

                if (dt.Rows[0]["productImage"] != DBNull.Value)
                {
                    byte[] imageArray = (byte[])(dt.Rows[0]["productImage"]);
                    txtImage.Image = Image.FromStream(new MemoryStream(imageArray));
                }
                else
                {
                    // Handle null image case if necessary
                }
            }
        }

    }
}

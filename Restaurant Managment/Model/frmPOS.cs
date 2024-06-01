using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Restaurant_Managment.Model
{
    public partial class frmPOS : Form
    {
        public frmPOS()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPOS_Load(object sender, EventArgs e)
        {
            
            //AddCategory();

            ProductPanel.Controls.Clear();
            LoadProducts();
        }

        private void AddItems(string id, string name,string cat, string price, System.Drawing.Image pimage)
        {
            ucProduct ucp = new ucProduct()
            {
                productName = name,
                productPrice = price,
                productCategory = cat,
                productImage = pimage,
                id = Convert.ToInt32(id),

            };

            ProductPanel.Controls.Add(ucp);

            ucp.onSelect += (ss, ee) =>
            {
                var wdg = (ucProduct)ss;

                foreach (DataGridViewRow item in guna2DataGridView1.Rows)
                {
                    //Ürün Kontrol Etme ve Fiyat Güncelleme
                    if (Convert.ToInt32(item.Cells["dgvid"].Value) == wdg.id)
                    {
                        item.Cells["dgvQty"].Value = int.Parse(item.Cells["dgvQty"].Value.ToString()) + 1;
                        item.Cells["dgvBudget"].Value = int.Parse(item.Cells["dgvQty"].Value.ToString()) * double.Parse(item.Cells["dgvPrice"].Value.ToString());

                        return;
                    }
                }

                //Yeni Ürün Ekleme
                guna2DataGridView1.Rows.Add(new object[] { 0, wdg.id, wdg.productName, 1, wdg.productPrice, wdg.productPrice });
                GetTotal();  //Her yeni ürün eklenince fiyat hesaplar
            };
        }

        //Ürünleri DataBaseden Alma Kodu

        private void LoadProducts()
        {
            string qry = "Select * from products inner join category on catID = CategoryID";

            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(qry, MainClass.con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach(DataRow item in dt.Rows)
            {
                Byte[] imageArray = (byte[])item["productImage"];
                byte[] imageByteArray = imageArray;

                AddItems(item["productID"].ToString(), item["productName"].ToString(), item["catName"].ToString(), item["productPrice"].ToString(), System.Drawing.Image.FromStream(new MemoryStream(imageArray)));
            }
        }

        private void GetTotal()
        {
            double tot = 0;
            lblTotal.Text = "";
            foreach (DataGridViewRow item in guna2DataGridView1.Rows)
            {
                tot += double.Parse(item.Cells["dgvBudget"].Value.ToString());
            }

            lblTotal.Text = tot.ToString("N2");
        }

        private void btnDin_Click(object sender, EventArgs e)
        {
            frmTableSelect frm = new frmTableSelect();
            MainClass.BlurBackground(frm);
            if(frm.TableName != "")
            {
                lblTable.Text = "Masa : " + frm.TableName;
                lblTable.Visible = true;

            }
            else
            {
                lblTable.Text = "";
                lblTable.Visible = false;

            }

            frmWaiterSelect frmWaiter = new frmWaiterSelect();
            MainClass.BlurBackground(frmWaiter);
            if(frmWaiter.Waitername != "")
            {
                lblWaiter.Text = "Garson : " + frmWaiter.Waitername;
                lblWaiter.Visible = true;

            }
            else
            {
                lblWaiter.Text = "";
                lblWaiter.Visible = false;

            }

        }
    }
}

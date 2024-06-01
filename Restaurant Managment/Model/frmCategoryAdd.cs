using System;
using System.Collections;
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
    public partial class frmCategoryAdd : SampleAdd
    {
        public frmCategoryAdd()
        {
            InitializeComponent();
        }

        public int id = 0;
        public override void btnSave_Click(object sender, EventArgs e)
        {
            string qry = "";

            if(id==0)
            {
                qry = "Insert into category Values(@Name)";
            }
            else
            {
                qry = "Update category Set catName = @Name where catID = @id ";
            }

            Hashtable ht = new Hashtable();
            ht.Add("@id", id);
            ht.Add("@Name", txtName.Text);
            
            if(MainClass.SQ1(qry, ht) > 0)
            {
                MessageBox.Show("Kaydedildi");
                id = 0;
                txtName.Text = "";
                txtName.Focus();
            }
        }

        private void frmCategoryAdd_Load(object sender, EventArgs e)
        {

        }
    }
}

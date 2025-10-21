using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_BusinnesLayer;

namespace DVLD_Project
{
    public partial class frmEditTestType : Form
    {
        private clsTestTypes _Test_Info = null;
        public frmEditTestType(clsTestTypes.enTestType TestTypeID)
        {
            InitializeComponent();
            _Test_Info = clsTestTypes.Find(TestTypeID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void frmEditTestType_Load(object sender, EventArgs e)
        {
            if (_Test_Info != null)
            {
                lblTestTypeID.Text =((int)_Test_Info.ID).ToString();
                txtTitle.Text = _Test_Info.Title;
                txtDescription.Text = _Test_Info.Description;
                txtFees.Text = _Test_Info.Fess.ToString();
            }

            else
            {
                MessageBox.Show("Application Test Type Is Not Found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            _Test_Info.Title = txtTitle.Text.Trim();
            _Test_Info.Description = txtDescription.Text.Trim();
            _Test_Info.Fess = Convert.ToSingle(txtFees.Text);

            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_Test_Info.Save())
            {
                MessageBox.Show("Application Test Type Updated Successfully", "Update Massage", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            else
                MessageBox.Show("Update Is Not Successfully", "Update Massage", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void txtName_Validating(object sender, CancelEventArgs e)
        {
            TextBox txt = sender as TextBox;

            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                errorProvider1.SetError(txt, "This field is required.");
                e.Cancel = true; // يمنع الخروج من التكست بوكس
            }
            else
            {
                errorProvider1.SetError(txt, ""); // إخفاء الخطأ إن وجد
                e.Cancel = false;
            }
        }

        private void txtPrice_Validating(object sender, CancelEventArgs e)
        {
            TextBox txt = sender as TextBox;

            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                errorProvider1.SetError(txt, "Please enter the price.");
                e.Cancel = true;
                return;
            }

            // محاولة التحويل إلى رقم عشري
            if (!decimal.TryParse(txt.Text, out decimal price) || price < 0)
            {
                errorProvider1.SetError(txt, "Please enter a valid price (positive number).");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txt, "");
                e.Cancel = false;
            }
        }


    }
}

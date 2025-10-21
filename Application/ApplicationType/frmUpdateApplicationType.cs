using DVLD_BusinnesLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class frmUpdateApplicationType : Form
    {
        private int _ApplicationTypeID = 0;

        private clsApplicationType _ApplicationType;
        public frmUpdateApplicationType(int ApplicationTypeID)
        {
            InitializeComponent();
            this._ApplicationTypeID = ApplicationTypeID; 
        }

        private void frmUpdateApplicationType_Load(object sender, EventArgs e)
        {
            _ApplicationType = clsApplicationType.Find(this._ApplicationTypeID);

            if (_ApplicationType != null)

            {
                lblApplicationTypeID.Text = _ApplicationType.ApplicationTypeID.ToString();
                txtApplicationTitle.Text = _ApplicationType.ApplicationTypeTitle;
                txtApplicationTypeFees.Text = _ApplicationType.ApplicationTypeFees.ToString();
            }

            else
            {
                MessageBox.Show("Application Type Is Not Found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _ApplicationType.ApplicationTypeTitle = txtApplicationTitle.Text;
            _ApplicationType.ApplicationTypeFees =  float.Parse(txtApplicationTypeFees.Text); 

            if (_ApplicationType.Save())
                MessageBox.Show("Updated Successfully.", "Successeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
       
            else
                MessageBox.Show("Updated Is Fild.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }
}

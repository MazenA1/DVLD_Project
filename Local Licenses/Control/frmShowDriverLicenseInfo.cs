using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Local_Licenses.Control
{
    public partial class frmShowDriverLicenseInfo : Form
    {
        private int _LicenseID = -1;
        public frmShowDriverLicenseInfo(int LicenseID)
        {
            InitializeComponent();
            this._LicenseID = LicenseID; 
        }

        private void frmShowDriverLicenseInfo_Load(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfo1.LoadDrivirLicenseData(this._LicenseID);
        }

        private void siticoneButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

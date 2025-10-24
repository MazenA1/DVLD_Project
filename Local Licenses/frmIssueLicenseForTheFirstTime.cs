using DVLD_BusinnesLayer;
using DVLD_Project.Glople_Classes;
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
    public partial class frmIssueLicenseForTheFirstTime : Form
    {
        private int _LocalDrivingLicenseApplicationID = -1;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseInfo;
        public frmIssueLicenseForTheFirstTime(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            this._LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
        }

        private void frmIssueLicenseForTheFirstTime_Load(object sender, EventArgs e)
        {
            this._LocalDrivingLicenseInfo = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(this._LocalDrivingLicenseApplicationID);

            if (this._LocalDrivingLicenseInfo == null)
            {
                MessageBox.Show("No Applicaiton with ID=" + _LocalDrivingLicenseApplicationID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            if (!this._LocalDrivingLicenseInfo.PassedAllTests())
            {

                MessageBox.Show("Person Should Pass All Tests First.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            int LicenseID = this._LocalDrivingLicenseInfo.GetActiveLicenseID();
            if (LicenseID != -1)
            {
                MessageBox.Show("Person already has License before with License ID=" + LicenseID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            ctrlLocalDrivingLicenseApplication1.LoadApplicationInfoByLocalDrivingApplicationID(this._LocalDrivingLicenseApplicationID);
        }

        private void siticoneButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            int LicenseID = this._LocalDrivingLicenseInfo.IssueLicenseForTheFirstTime(txtNotes.Text.Trim(), clsGlobal.CurrentUser.UserID);

            if (LicenseID != -1)
            {
                MessageBox.Show("License Issued Successfully with License ID = " + LicenseID.ToString(),
                    "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            else
            {
                MessageBox.Show("License Was not Issued ! ",
                 "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

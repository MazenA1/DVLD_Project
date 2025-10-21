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

namespace DVLD_Project.Application.Local_Driving_Licens
{
    public partial class frmLocalDrivingLicenseApplicationList : Form
    {
        private DataTable _dtAllDrivingLicenseApplication = null; 
        public frmLocalDrivingLicenseApplicationList()
        {
            InitializeComponent();
        }

        private void frmLocalDrivingLicenseApplicationList_Load(object sender, EventArgs e)
        {

            _dtAllDrivingLicenseApplication = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();

            dgvLocalApplicationList.DataSource = _dtAllDrivingLicenseApplication;

            lblRecordsCount.Text = dgvLocalApplicationList.RowCount.ToString();

            
            if (dgvLocalApplicationList.Rows.Count > 0)
            {

                dgvLocalApplicationList.Columns[0].HeaderText = "L.D.L.AppID";
                dgvLocalApplicationList.Columns[0].Width = 120;

                dgvLocalApplicationList.Columns[1].HeaderText = "Driving Class";
                dgvLocalApplicationList.Columns[1].Width = 300;

                dgvLocalApplicationList.Columns[2].HeaderText = "National No.";
                dgvLocalApplicationList.Columns[2].Width = 150;

                dgvLocalApplicationList.Columns[3].HeaderText = "Full Name";
                dgvLocalApplicationList.Columns[3].Width = 350;

                dgvLocalApplicationList.Columns[4].HeaderText = "Application Date";
                dgvLocalApplicationList.Columns[4].Width = 170;

                dgvLocalApplicationList.Columns[5].HeaderText = "Passed Tests";
                dgvLocalApplicationList.Columns[5].Width = 150;

                dgvLocalApplicationList.Columns[6].HeaderText = "Status";
                dgvLocalApplicationList.Columns[6].Width = 140;
            }
        }

        private void siticoneButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilter.Text != "None");

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Clear();
                txtFilterValue.Focus();
            }

            _dtAllDrivingLicenseApplication.DefaultView.RowFilter = "";
            lblRecordsCount.Text = dgvLocalApplicationList.RowCount.ToString();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = string.Empty;

            switch (cbFilter.Text)
            {
                case "L.D.L AppID":
                    FilterColumn = "LocalDrivingLicenseApplicationID";
                    break;

                case "National No":
                    FilterColumn = "NationalNo";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "Status":
                    FilterColumn = "Status";
                    break;


                default:
                    FilterColumn = "None";
                    break;
            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllDrivingLicenseApplication.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvLocalApplicationList.Rows.Count.ToString();
                return;
            }


            if (FilterColumn == "LocalDrivingLicenseApplicationID")
                //in this case we deal with integer not string.
                _dtAllDrivingLicenseApplication.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtAllDrivingLicenseApplication.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecordsCount.Text = dgvLocalApplicationList.Rows.Count.ToString();
        }

        private void editApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdateApplication((int)dgvLocalApplicationList.CurrentRow.Cells[0].Value); 
            frm.ShowDialog();

            frmLocalDrivingLicenseApplicationList_Load(null, null);
        }

        private void showApplicationDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmLocalDrivingLicenseApplicationInfo((int)dgvLocalApplicationList.CurrentRow.Cells[0].Value);
            frm.ShowDialog(); 

            frmLocalDrivingLicenseApplicationList_Load(null, null);
        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do want to delete this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int LocalDrivingLicenseApplicationID = (int)dgvLocalApplicationList.CurrentRow.Cells[0].Value;

            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplicationInfo = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);

            if (LocalDrivingLicenseApplicationInfo != null)
            {
                if (LocalDrivingLicenseApplicationInfo.Delete())
                {
                    MessageBox.Show("Application Deleted Successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //refresh the form again.
                    frmLocalDrivingLicenseApplicationList_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Could not delete applicatoin, other data depends on it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do want to cancel this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int LocalDrivingLicenseApplicationID = (int)dgvLocalApplicationList.CurrentRow.Cells[0].Value;

            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplicationInfo = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);
            
            if (LocalDrivingLicenseApplicationInfo != null)
            {
                if (LocalDrivingLicenseApplicationInfo.Cancel())
                {
                    MessageBox.Show("Application Cancelled Successfully.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    frmLocalDrivingLicenseApplicationList_Load(null, null);

                }
                else
                {
                    MessageBox.Show("Could not cancel applicatoin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void siticoneContextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

            int LocalDrivingLicenseApplicationID = (int)dgvLocalApplicationList.CurrentRow.Cells[0].Value;

            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplicationInfo = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);
            int TotalPassedTests = (int)dgvLocalApplicationList.CurrentRow.Cells[5].Value;

            bool LicenseExists = LocalDrivingLicenseApplicationInfo.IsLicenseIssued();

            //Enabled only if person passed all tests and Does not have license. 
            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = ((TotalPassedTests == 3) && !LicenseExists);
            showLicenseToolStripMenuItem.Enabled = LicenseExists;
            editApplicationToolStripMenuItem.Enabled = !LicenseExists && (LocalDrivingLicenseApplicationInfo.ApplicationStatus == clsApplication.enApplicationStatus.New);


            //Enable/Disable Cancel Menue Item
            //We only canel the applications with status=new.
            cancelApplicationToolStripMenuItem.Enabled = (LocalDrivingLicenseApplicationInfo.ApplicationStatus == clsApplication.enApplicationStatus.New);


            //Enable/Disable Delete Menue Item
            //We only allow delete incase the application status is new not complete or Cancelled.
            deleteApplicationToolStripMenuItem.Enabled = (LocalDrivingLicenseApplicationInfo.ApplicationStatus == clsApplication.enApplicationStatus.New);

            //Enable Disable Schedule menue and it's sub menue
            bool PassedVitionTest = LocalDrivingLicenseApplicationInfo.DoesPassTestType(clsTestTypes.enTestType.VisionTest);
            bool PassedWrittenTest = LocalDrivingLicenseApplicationInfo.DoesPassTestType(clsTestTypes.enTestType.WrittenTest);
            bool PassedStreetTest = LocalDrivingLicenseApplicationInfo.DoesPassTestType(clsTestTypes.enTestType.StreetTest);

            sechToolStripMenuItem.Enabled = (!PassedVitionTest || !PassedWrittenTest || !PassedStreetTest) && (LocalDrivingLicenseApplicationInfo.ApplicationStatus == clsApplication.enApplicationStatus.New);

            if (sechToolStripMenuItem.Enabled)
            {
                //To Allow Schdule vision test, Person must not passed the same test before.
                scheduleVisionTestToolStripMenuItem.Enabled = !PassedVitionTest;
                //To Allow Schdule written test, Person must pass the vision test and must not passed the same test before.
                scheduleWrittenTestToolStripMenuItem.Enabled = !PassedWrittenTest && PassedVitionTest;
                //To Allow Schdule steet test, Person must pass the vision * written tests, and must not passed the same test before.
                scheduleStreetTestToolStripMenuItem.Enabled = !PassedStreetTest && PassedWrittenTest;
            }
        }

        private void _ScheduleTest(clsTestTypes.enTestType TestTypeID)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalApplicationList.CurrentRow.Cells[0].Value;

            frmListTestAppointments frm = new frmListTestAppointments(LocalDrivingLicenseApplicationID, TestTypeID);
            frm.ShowDialog();

            // Refresh.
            frmLocalDrivingLicenseApplicationList_Load(null, null);
        }
        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestTypes.enTestType.VisionTest);
        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestTypes.enTestType.WrittenTest);
        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestTypes.enTestType.StreetTest);
        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalApplicationList.CurrentRow.Cells[0].Value;

            Form frm = new frmIssueLicenseForTheFirstTime(LocalDrivingLicenseApplicationID);
            frm.ShowDialog();

            // Refresh.
            frmLocalDrivingLicenseApplicationList_Load(null, null);
        }
    }
}

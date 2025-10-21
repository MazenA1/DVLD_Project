using DVLD_BusinnesLayer;
using DVLD_Project.Glople_Classes;
using DVLD_Project.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class frmTakeTest : Form
    {
        private int _AppointmentID;
        private clsTestTypes.enTestType _TestTypeID = clsTestTypes.enTestType.VisionTest;

        private int _TestID = -1;
        private clsTests TestInfo = null;
        public frmTakeTest(int AppointmentID, clsTestTypes.enTestType TestTypeID)
        {
            InitializeComponent();
            this._AppointmentID = AppointmentID;
            this._TestTypeID = TestTypeID;
        }

        private void _LoadData(int testAppointments)
        {
            ctrlSecheduledTest11.LoadData(testAppointments);
        }
        private void frmTest_Load(object sender, EventArgs e)
        {
            ctrlSecheduledTest11.TestTypeID = this._TestTypeID;

            clsTests TestInfo = clsTests.FindByTestAppointmentID(this._AppointmentID);

            if (!(TestInfo == null))
            {
                label8.Visible = true;

                if (TestInfo.TestResult)
                {
                    rbPass.Checked = true;
                    rbFile.Enabled = false;
                }
                else
                {
                    rbFile.Checked = true;
                    rbPass.Enabled = false;
                }


            }

            if (!clsTestAppointments.IsTestAppointmentExists(this._AppointmentID))
            {
                MessageBox.Show(
                "This Test Appointment is not found.",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
                );

                btnSave.Enabled = false;

                return;
            }


            clsTestAppointments testAppointments = clsTestAppointments.Find(this._AppointmentID);

            if (testAppointments != null /*testAppointments.LocalDrivingLicenseApplicationInfo.IsThereAnActiveScheduledTest(this._TestTypeID)*/)
                _LoadData(testAppointments.TestAppointmentID);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to save? After that you cannot change the Pass/Fail results after you save?.",
                        "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }

            if (!ctrlSecheduledTest11.testAppointments.LocalDrivingLicenseApplicationInfo.IsThereAnActiveScheduledTest(this._TestTypeID))
            {
                MessageBox.Show(
                "This test result has already been recorded.\nYou cannot modify the exam result once it has been saved.",
                "Access Denied",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
                );
                return;
            }

            this.TestInfo = new clsTests();

            this.TestInfo.TestAppointmentID = this._AppointmentID;
            this.TestInfo.TestResult = rbPass.Checked ? true : false;
            this.TestInfo.Notes = txtNotes.Text;
            this.TestInfo.CreatedByUserID = clsGlobal.CurrentUser.UserID;


            if (this.TestInfo.Save())
            {
                this._TestID = this.TestInfo.TestID;
                MessageBox.Show("Data Saved Successfully Whith Test [",
                    "Saved",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                    );
            }
            else
                MessageBox.Show(
                "Data Saved Is Not Successfully",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
                );
        }
        private void rbPass_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void rbFile_CheckedChanged(object sender, EventArgs e)
        {
            
        }
    }
}

using DVLD_BusinnesLayer;
using DVLD_Project.Glople_Classes;
using DVLD_Project.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class ctrlScheduleTest : UserControl
    {
        private enum enMode { AddNew = 0, Update = 1 }
        enMode _Mode = enMode.AddNew;

        private enum enCreationMode { FirstTimeSchedule = 0, RetakeTestSchedule = 1 }
        enCreationMode _CreationMode = enCreationMode.FirstTimeSchedule;

        private clsTestTypes.enTestType _TestTypeID = clsTestTypes.enTestType.VisionTest;

        private clsLocalDrivingLicenseApplication _LocalDrivindLicenseApplicationInfo = null;
        private int _LocalDrivingLicinseApplicationID = -1;

        private clsTestAppointments _TestAppointmentsInfo = null;
        private int _TestAppointmentID = -1;

        [Browsable(true)]
        [Category("Misc")]
        [Description("نوع الاختبار، يظهر في قسم Misc في نافذة الخصائص.")]
        [DefaultValue(typeof(clsTestTypes.enTestType), "VisionTest")]
        public clsTestTypes.enTestType TestTypeID
        {
            get => _TestTypeID;
            set
            {
                if (_TestTypeID == value) return;
                _TestTypeID = value;

                _UpdateUIForTestType();
            }
        }
        private void _UpdateUIForTestType()
        {
            switch (_TestTypeID)
            {
                case clsTestTypes.enTestType.VisionTest:
                    lblUserMassge.Text = "Vision Test";
                    PbTestType.Image = Resources.Vision_512;
                    break;

                case clsTestTypes.enTestType.WrittenTest:
                    lblUserMassge.Text = "Written Test";
                    PbTestType.Image = Resources.Written_Test_512;
                    break;

                case clsTestTypes.enTestType.StreetTest:
                    lblUserMassge.Text = "Street Test";
                    PbTestType.Image = Resources.driving_test_512;
                    break;
            }
        }
        public ctrlScheduleTest()
        {
            InitializeComponent();
        }
        private void _InitializeLicenseAndTestData(int LocalDrivingLicenseApplicationID, int TestAppointmentID)
        {
            _LocalDrivingLicinseApplicationID = LocalDrivingLicenseApplicationID;
            _LocalDrivindLicenseApplicationInfo = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(this._LocalDrivingLicinseApplicationID);
            _TestAppointmentID = TestAppointmentID;
           // _TestAppointmentsInfo = clsTestAppointments.Find(_TestAppointmentID);
        }
        public void LoadInfo(int LocalDrivingLicenseApplicationID, int TestAppointmentID = -1)  
        {
            if (TestAppointmentID == -1)
                this._Mode = enMode.AddNew; // chicken
            else
                this._Mode = enMode.Update;

            _InitializeLicenseAndTestData(LocalDrivingLicenseApplicationID, TestAppointmentID);

            if (this._LocalDrivindLicenseApplicationInfo == null)
            {
                MessageBox.Show("Error: No Local Driving License Application with ID = " + _LocalDrivingLicinseApplicationID.ToString(),
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                btnSave.Enabled = false;
                return;
            }

            //decide if the createion mode is retake test or not based if the person attended this test before
            if (_LocalDrivindLicenseApplicationInfo.DoesAttendTestType(this._TestTypeID)) 
                this._CreationMode = enCreationMode.RetakeTestSchedule;
            else
                this._CreationMode = enCreationMode.FirstTimeSchedule;


            if (this._CreationMode == enCreationMode.RetakeTestSchedule)
            {
                lbl_R_App_Fees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.RetakeTest).ApplicationTypeFees.ToString();
                gpRetakeTestInfo.Enabled = true;
                lblTitle.Text = "Schedule Retake Test";
                lblR_Test_App_ID.Text = "0";
            }
            else
            {
                gpRetakeTestInfo.Enabled = false;
                lblTitle.Text = "Schedule Test";
                lbl_R_App_Fees.Text = "0";
                lblR_Test_App_ID.Text = "N/A";
            }

            lblDrivingApplicationID.Text = (_LocalDrivindLicenseApplicationInfo.LocalDrivingLicenseApplicationID).ToString();
            lblD_Class.Text = _LocalDrivindLicenseApplicationInfo.LicenseClassInfo.ClassName.ToString();
            lblName.Text = _LocalDrivindLicenseApplicationInfo.PersonFullName.ToString();
            lblTrial.Text = _LocalDrivindLicenseApplicationInfo.TotalTrialsPerTest(_TestTypeID).ToString();

            if (this._Mode == enMode.AddNew)
            {
                lblFees.Text = clsTestTypes.Find(_TestTypeID).Fess.ToString();
                dtpTestDate.MinDate = DateTime.Now;
                lblR_Test_App_ID.Text = "N/A";
                //lbl_R_App_Fees.Text = "0";

                _TestAppointmentsInfo = new clsTestAppointments();
            }

            else 
            {
                if (!_LoadTestAppointmentData())
                    return;
            }

            lblTotal_Fees.Text = (Convert.ToSingle(lblFees.Text) + Convert.ToSingle(lbl_R_App_Fees.Text)).ToString();

            if (!_HandleActiveTestAppointmentConstraint())
                return;

            if (!_HandleAppointmentLockedConstraint())
                return;

            if (!_HandlePrviousTestConstraint())
                return;
        } 
        private bool _LoadTestAppointmentData()
        {
            _TestAppointmentsInfo = clsTestAppointments.Find(_TestAppointmentID);

            if (_TestAppointmentsInfo == null)
            {
                MessageBox.Show("Error: No Appointment with ID = " + _TestAppointmentID.ToString(),
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return false;
            }

            lblFees.Text = _TestAppointmentsInfo.PaidFees.ToString();

            DateTime minAllowed = (DateTime.Now < _TestAppointmentsInfo.AppointmentDate)
            ? DateTime.Now
            : _TestAppointmentsInfo.AppointmentDate;

            dtpTestDate.MinDate = minAllowed;
            dtpTestDate.Value = _TestAppointmentsInfo.AppointmentDate;

            if (_TestAppointmentsInfo.RetakeTestApplicationID == -1 && _TestAppointmentsInfo.RetakeTestApplicationID == null)
            {
                lblR_Test_App_ID.Text = "N/A";
                lbl_R_App_Fees.Text = "0";
            }
            else
            {
                //lbl_R_App_Fees.Text = _TestAppointmentsInfo.RetakeTestAppInfo.PaidFees.ToString();
                lbl_R_App_Fees.Text = _TestAppointmentsInfo.RetakeTestAppInfo?.PaidFees.ToString() ?? "0";
                gpRetakeTestInfo.Enabled = true;
                lblTitle.Text = "Schedule Retake Test";
                lblR_Test_App_ID.Text = _TestAppointmentsInfo.RetakeTestApplicationID.ToString();
            }

            return true;
        }
        private bool _HandleActiveTestAppointmentConstraint()
        {
            if (this._Mode == enMode.AddNew && clsLocalDrivingLicenseApplication.IsThereAnActiveScheduledTest(this._LocalDrivingLicinseApplicationID, this.TestTypeID))
            {
                lblUserMassge.Text = "Person Already have an active appointment for this test";
                btnSave.Enabled = false;
                dtpTestDate.Enabled = false;
                return false;
            }

            return true;
        }
        private bool _HandleAppointmentLockedConstraint()
        {
            if (this._TestAppointmentsInfo.IsLocked)
            {
                //if appointment is locked that means the person already sat for this test
                //we cannot update locked appointment
                lblUserMassge.Visible = true;
                lblUserMassge.Text = "Person already sat for the test, appointment loacked.";
                dtpTestDate.Enabled = false;
                btnSave.Enabled = false;
                return false;
            }
            else
            {
                lblUserMassge.Visible = false;
            }

            return true;
        }
        private bool _HandlePrviousTestConstraint()
        {
            //we need to make sure that this person passed the prvious required test before apply to the new test.
            //person cannno apply for written test unless s/he passes the vision test.
            //person cannot apply for street test unless s/he passes the written test.

            switch (_TestTypeID)
            {
                case clsTestTypes.enTestType.VisionTest:
                    //in this case no required prvious test to pass.
                    lblUserMassge.Enabled = false;

                    return true;

                case clsTestTypes.enTestType.WrittenTest:
                    //Written Test, you cannot sechdule it before person passes the vision test.
                    //we check if pass visiontest 1.

                    if (!_LocalDrivindLicenseApplicationInfo.DoesPassTestType(clsTestTypes.enTestType.VisionTest))
                    {
                        lblUserMassge.Text = "Cannot Sechule, Vision Test should be passed first";
                        lblUserMassge.Visible = true;
                        btnSave.Enabled = false;
                        dtpTestDate.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblUserMassge.Visible = false;
                        btnSave.Enabled = true;
                        dtpTestDate.Enabled = true;
                    }
                    return true;

                case clsTestTypes.enTestType.StreetTest:
                    //Street Test, you cannot sechdule it before person passes the written test.
                    //we check if pass Written 2.
                    if (!_LocalDrivindLicenseApplicationInfo.DoesPassTestType(clsTestTypes.enTestType.WrittenTest))
                    {
                        lblUserMassge.Text = "Cannot Sechule, Written Test should be passed first";
                        lblUserMassge.Visible = true;
                        btnSave.Enabled = false;
                        dtpTestDate.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblUserMassge.Visible = false;
                        btnSave.Enabled = true;
                        dtpTestDate.Enabled = true;
                    }
                    return true;
            }
            return false;
        }
        private bool _HandleRetakeApplication()
        {
            if (this._CreationMode == enCreationMode.RetakeTestSchedule && this._Mode == enMode.AddNew)
            {
                clsApplication Application = new clsApplication();

                Application.ApplicantPersonID = _LocalDrivindLicenseApplicationInfo.ApplicantPersonID;
                Application.ApplicationDate = DateTime.Now;
                Application.ApplicationTypeID = (int)clsApplication.enApplicationType.RetakeTest;
                Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
                Application.LastStatusDate = DateTime.Now;
                Application.PaidFees = clsApplicationType.Find((byte)clsApplication.enApplicationType.RetakeTest).ApplicationTypeFees;
                Application.CreatedByUserID = clsGlobal.CurrentUser.UserID;

                if (!Application.Save())
                {
                    _TestAppointmentsInfo.RetakeTestApplicationID = -1;
                    MessageBox.Show("Faild to Create application", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                _TestAppointmentsInfo.RetakeTestApplicationID = Application.ApplicationID;
            }

            return true;
        }
        private void btnSave_Click(object sender, EventArgs e) 
        {
            if (!_HandleRetakeApplication())
                return;

            _TestAppointmentsInfo.TestTypeID = this._TestTypeID;
            _TestAppointmentsInfo.LocalDrivingLicenseApplicationID = _LocalDrivindLicenseApplicationInfo.LocalDrivingLicenseApplicationID;
            _TestAppointmentsInfo.AppointmentDate = dtpTestDate.Value;
            _TestAppointmentsInfo.PaidFees = Convert.ToSingle(lblFees.Text);
            _TestAppointmentsInfo.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (_TestAppointmentsInfo.Save())
            {
                this._Mode = enMode.Update;
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
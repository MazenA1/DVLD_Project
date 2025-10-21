using DVLD_BusinnesLayer;
using DVLD_Project.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DVLD_Project
{
    public partial class ctrlSecheduledTest1 : UserControl
    {
        private clsTestTypes.enTestType _TestTypeID;
        public clsTestTypes.enTestType TestTypeID
        { 
            get { return _TestTypeID; } 

            set
            { 
                _TestTypeID = value; 
            }
        }

        public clsTestAppointments testAppointments = null;
        public ctrlSecheduledTest1()
        {
            InitializeComponent();
        }

        private void _UpdateUIForTestType()
        {
            switch (this._TestTypeID)
            {
                case clsTestTypes.enTestType.VisionTest:
                    lblTitle.Text = "Vision Test";
                    PbTestType.Image = Resources.Vision_512;
                    break;

                case clsTestTypes.enTestType.WrittenTest:
                    lblTitle.Text = "Written Test";
                    PbTestType.Image = Resources.Written_Test_512;
                    break;

                case clsTestTypes.enTestType.StreetTest:
                    lblTitle.Text = "Street Test";
                    PbTestType.Image = Resources.driving_test_512;
                    break;
            }
        }
        private void _LoadData(int testAppointmentID)
        {
            testAppointments = clsTestAppointments.Find(testAppointmentID);

            lblDrivingApplicationID.Text = testAppointments.LocalDrivingLicenseApplicationID.ToString();
            lblD_Class.Text = clsLicenseClass.Find(testAppointments.LocalDrivingLicenseApplicationInfo.LicenseClassID).ClassName;
            lblName.Text = testAppointments.LocalDrivingLicenseApplicationInfo.PersonFullName;
            lblTrial.Text = clsLocalDrivingLicenseApplication.TotalTrialsPerL_D_ApplicationIDAndTest(testAppointments.LocalDrivingLicenseApplicationID, this._TestTypeID).ToString();
            lblFees.Text = testAppointments.PaidFees.ToString();
            lbltestID.Text = ((short)testAppointments.TestTypeID == -1) ? "Not Taken Yet" : ((short)testAppointments.TestTypeID).ToString();
        }
        public void LoadData(int TestAppointmentID)
        {
            _LoadData(TestAppointmentID); 
        }

        private void ctrlSecheduledTest1_Load(object sender, EventArgs e)
        {
            _UpdateUIForTestType();
        }
    }
}

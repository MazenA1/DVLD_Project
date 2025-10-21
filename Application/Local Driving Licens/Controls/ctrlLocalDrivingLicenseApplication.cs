using DVLD_BusinnesLayer;
using DVLD_Project.Application.Local_Driving_Licens.View_Models;
using DVLD_Project.View_Models;
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
    public partial class ctrlLocalDrivingLicenseApplication : UserControl
    {
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplicationInfo;
        private int _LocalDrivingLicenseApplicationID = -1;

        private DrivingLicenseApplicationInfoViewModel _ViewModel = null;

        private int _PersonID = -1;

        private int _LicenseID;
        public int LocalDrivingLicenseApplicationID
        {
            get
            {
                return this._LocalDrivingLicenseApplicationID;
            }
        }
        public ctrlLocalDrivingLicenseApplication()
        {
            InitializeComponent();

        }
        public void LoadApplicationInfoByLocalDrivingApplicationID(int LocalDrivingLicenseApplicationID)
        {
            _LocalDrivingLicenseApplicationInfo = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID); 

            if (_LocalDrivingLicenseApplicationInfo == null)
            {


                MessageBox.Show("No Application with ApplicationID = " + LocalDrivingLicenseApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FiilLocalDrivingLicenseApplication();
        }
        public void LoadApplicationInfoByApplicationID(int ApplicationID)
        {
            _LocalDrivingLicenseApplicationInfo = clsLocalDrivingLicenseApplication.FindByApplicationID(ApplicationID);

            if (_LocalDrivingLicenseApplicationInfo == null)
            {
                _ResetLocalDrivingLicenseApplicationInfo();

                MessageBox.Show("No Application with ApplicationID = " + ApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FiilLocalDrivingLicenseApplication(); 
        }
        private void _ResetLocalDrivingLicenseApplicationInfo()
        {
            lblDrivingApplicationID.Text = "[????]";
            lblAppliedForLicense.Text = "[????]";
            lblPassedTests.Text = "[??]";
        }
        private void _FiilLocalDrivingLicenseApplication()
        {
            _LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplicationInfo.LocalDrivingLicenseApplicationID;

            _ViewModel = new DrivingLicenseApplicationInfoViewModel
            {
                LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplicationInfo.LocalDrivingLicenseApplicationID,
                AppliedForLicense = clsLicenseClass.Find(_LocalDrivingLicenseApplicationInfo.LicenseClassID).ClassName,
                PassedTest = _LocalDrivingLicenseApplicationInfo.GetPassedCount()
            };

            var bindings = new Dictionary<Label, string>
            {
                { lblDrivingApplicationID, "LocalDrivingLicenseApplicationID" },
                { lblAppliedForLicense, "AppliedForLicense"},
                { lblPassedTests, "PassedTest"}
            };

            foreach (var pair in bindings)
            {
                pair.Key.DataBindings.Clear();
                pair.Key.DataBindings.Add("Text", _ViewModel, pair.Value);
            }

            ctrlApplicationBasicInfo1.LoadApplicationBasicInfoByApplicationID(_LocalDrivingLicenseApplicationInfo.ApplicationID);
        }
    }
}

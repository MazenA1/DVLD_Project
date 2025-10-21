using DVLD_BusinnesLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class ctrlApplicationBasicInfo : UserControl
    {
        private ApplicationBasicInfoViewModel _ViewModel;

        private clsApplication _ApplicationInfo = null;

        private int _ApplicantID = -1;
        public int ApplicantID
        {
            get
            {
                return _ApplicantID;
            }
        }
        public ctrlApplicationBasicInfo()
        {
            InitializeComponent();
        }
        public void LoadApplicationBasicInfoByApplicationID(int ApplicationID)
        {
            _ApplicationInfo = clsApplication.FindBaseApplication(ApplicationID);

            if (_ApplicationInfo == null)
            {
                _ResetApplicationBasicInfo();

                MessageBox.Show($"No application was found with ID {ApplicationID}. Please check the number and try again.",
                                "Application Not Found",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

            }

            _FillApplicationBasicInfo();
        }
        private void _ResetApplicationBasicInfo()
        {
            lblApplicationID.Text = "[????]";
            lblApplicationStatus.Text = "[????]";
            lblApplicationFees.Text = "[????]";
            lblApplicationType.Text = "[????]";
            lblApplicationApplicant.Text = "[????]";
            lblApplicationDate.Text = "[??/??/????]";
            lblApplicationStatusDate.Text = "[??/??/????]";
            lblApplicationCreatedUsername.Text = "[?????]";

        }
        private void _FillApplicationBasicInfo()
        {
            _ApplicantID = _ApplicationInfo.ApplicantPersonID;

            _ViewModel = new ApplicationBasicInfoViewModel
            {
                ApplicationID = _ApplicationInfo.ApplicationID,
                Status = _ApplicationInfo.StatusText,
                Fees = _ApplicationInfo.PaidFees,
                ApplicationType = _ApplicationInfo.ApplicationTypeInfo.ApplicationTypeTitle,
                ApplicantName = _ApplicationInfo.ApplicantPersonInfo.FullName,
                ApplicationDate = _ApplicationInfo.ApplicationDate,
                ApplicationStatusDate = _ApplicationInfo.LastStatusDate,
                CreatedUserName = _ApplicationInfo.CreatedByUserInfo.UserName
            };

            var bindings = new Dictionary<Label, string>
            {
                { lblApplicationID, "ApplicationID" },
                { lblApplicationStatus, "Status" },
                { lblApplicationFees, "Fees" },
                { lblApplicationType, "ApplicationType" },
                { lblApplicationApplicant, "ApplicantName" },
                { lblApplicationDate, "ApplicationDate" },
                { lblApplicationStatusDate, "ApplicationStatusDate" },
                { lblApplicationCreatedUsername, "CreatedUserName" }
            };

            foreach (var pair in bindings)
            {
                pair.Key.DataBindings.Clear();
                pair.Key.DataBindings.Add("Text", _ViewModel, pair.Value);
            }

        }

        private void Link_lblViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmShowPersonInfo(_ApplicationInfo.ApplicantPersonID);
            frm.ShowDialog();
        }
    }
}
 
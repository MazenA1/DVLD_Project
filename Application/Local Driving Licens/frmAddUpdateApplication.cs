using DVLD_BusinnesLayer;
using DVLD_Project.Glople_Classes;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVLD_Project.Application.Local_Driving_Licens
{
    public partial class frmAddUpdateApplication : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;

        private int _SelectedPersonID = -1;
        private int _LocalDrivingLicenseAplicationID = -1;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;

        private LocalDrivingLicenseApplicationViewModel _viewModel;  // ViewModel كحقل للفورم
        private DataTable _dtLicenseClasses;  // جدول بيانات الرخص

        public frmAddUpdateApplication()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }
        public frmAddUpdateApplication(int LocalDrivingLicenseAplicationID)
        {
            InitializeComponent();
            _LocalDrivingLicenseAplicationID = LocalDrivingLicenseAplicationID;
            _Mode = enMode.Update;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void _FillLicenseClassesInComoboBox()
        {
            _dtLicenseClasses = clsLicenseClass.GetAllLicenseClassData();

            cbLicenseClasses.DataSource = _dtLicenseClasses;
            cbLicenseClasses.DisplayMember = "ClassName";
            cbLicenseClasses.ValueMember = "LicenseClassID";
        }
        private void _ResetDefualtValues()
        {
            _FillLicenseClassesInComoboBox();

            if (_Mode == enMode.AddNew)
            {
                lblfrmCaption.Text = "New Local Driving License Application";
                this.Text = "New Local Driving License Application";
                _LocalDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();
                tabApplicationImfo.Enabled = false;

                // تعيين قيمة افتراضية في الكمبو بوكس حسب الـ LicenseClassID
                if (_dtLicenseClasses != null && _dtLicenseClasses.Rows.Count > 2)
                    cbLicenseClasses.SelectedValue = _dtLicenseClasses.Rows[2]["LicenseClassID"];

                lblApplicationFees.Text = clsApplicationType.Find((byte)clsApplication.enApplicationType.NewDrivingLicense).ApplicationTypeFees.ToString();
                LblApplicationDate.Text = DateTime.Now.ToShortDateString();
                LblUserName.Text = clsGlobal.CurrentUser.UserName;
            }
            else
            {
                lblfrmCaption.Text = "Update Local Driving License Application";
                this.Text = "Update Local Driving License Application";

                tabApplicationImfo.Enabled = true;
            }
        }
        private void _Load()
        {
            ctrlPersonCardWithFilter1.FilterEnabled = false;
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseAplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("No Application with ID = " + _LocalDrivingLicenseAplicationID, "Application Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            ctrlPersonCardWithFilter1.LoadPersonInfo(_LocalDrivingLicenseApplication.ApplicantPersonID);

            _viewModel = new LocalDrivingLicenseApplicationViewModel
            {
                ApplicationID = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID,
                ApplicationDate = clsFormat.DateToShort(_LocalDrivingLicenseApplication.ApplicationDate),
                LicenseClassID = _LocalDrivingLicenseApplication.LicenseClassID,
                LicenseClassName = clsLicenseClass.Find(_LocalDrivingLicenseApplication.LicenseClassID)?.ClassName ?? "",
                PaidFees = _LocalDrivingLicenseApplication.PaidFees,
                CreatedByUserName = clsUser.FindByUserID(_LocalDrivingLicenseApplication.CreatedByUserID)?.UserName ?? ""
            };

            D_L_ApplicationID.DataBindings.Clear();
            D_L_ApplicationID.DataBindings.Add("Text", _viewModel, "ApplicationID");

            LblApplicationDate.DataBindings.Clear();
            LblApplicationDate.DataBindings.Add("Text", _viewModel, "ApplicationDate");

            lblApplicationFees.DataBindings.Clear();
            lblApplicationFees.DataBindings.Add("Text", _viewModel, "PaidFees");

            LblUserName.DataBindings.Clear();
            LblUserName.DataBindings.Add("Text", _viewModel, "CreatedByUserName");

            // ربط ComboBox بقيمة LicenseClassID في ViewModel
            cbLicenseClasses.DataBindings.Clear();
            cbLicenseClasses.DataBindings.Add("SelectedValue", _viewModel, "LicenseClassID", true, DataSourceUpdateMode.OnPropertyChanged);
        }
        private void frmAddUpdateApplication_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();

            if (_Mode == enMode.Update)
                _Load();
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                tcApplicationInfo.SelectedTab = tcApplicationInfo.TabPages["tabApplicationImfo"];
                return;
            }

            if (ctrlPersonCardWithFilter1.PersonID != -1)
            {
                tabApplicationImfo.Enabled = true;
                tcApplicationInfo.SelectedTab = tcApplicationInfo.TabPages["tabApplicationImfo"];
            }
        }
        private void btnCansel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            int selectedLicenseClassID = clsLicenseClass.Find(cbLicenseClasses.Text).LicenseClassID;

            int ActiveApplicationID = clsApplication.GetActiveApplicationIDForLicenseClass(_SelectedPersonID, clsApplication.enApplicationType.NewDrivingLicense, selectedLicenseClassID);

            if (ActiveApplicationID != -1)
            {
                MessageBox.Show("Choose another License Class, the selected Person Already have an active application for the selected class with id=" + ActiveApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbLicenseClasses.Focus();
                return;
            }

            if (clsLicense.IsLicenseExistByPersonID(ctrlPersonCardWithFilter1.PersonID, selectedLicenseClassID))
            {
                MessageBox.Show("Person already have a license with the same applied driving class, Choose different driving class", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _LocalDrivingLicenseApplication.ApplicantPersonID = ctrlPersonCardWithFilter1.PersonID;
            _LocalDrivingLicenseApplication.ApplicationDate = DateTime.Now;
            _LocalDrivingLicenseApplication.ApplicationTypeID = (byte)clsApplication.enApplicationType.NewDrivingLicense;
            _LocalDrivingLicenseApplication.ApplicationStatus = clsApplication.enApplicationStatus.New;
            _LocalDrivingLicenseApplication.LastStatusDate = DateTime.Now;
            _LocalDrivingLicenseApplication.PaidFees = clsApplicationType.Find(_LocalDrivingLicenseApplication.ApplicationTypeID).ApplicationTypeFees;
            _LocalDrivingLicenseApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _LocalDrivingLicenseApplication.LicenseClassID = selectedLicenseClassID;

            if (_LocalDrivingLicenseApplication.Save())
            {
                D_L_ApplicationID.Text = _LocalDrivingLicenseApplication.ApplicationID.ToString();
                _Mode = enMode.Update;

                lblfrmCaption.Text = "Update Local Driving License Application";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ctrlPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            _SelectedPersonID = obj;
        }

        private void frmAddUpdateApplication_Activated(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter1.FilterFocus();
        }
    }
}

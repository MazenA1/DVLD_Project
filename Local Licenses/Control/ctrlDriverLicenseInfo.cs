using DVLD_BusinnesLayer;
using DVLD_Project.Glople_Classes;
using DVLD_Project.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class ctrlDriverLicenseInfo : UserControl
    {

        private int _LicenseID = -1;
        private clsLicense _LicenseInfo;

        public int LicenseID
        {
            get { return _LicenseID; }
        }
        public clsLicense LicenseInfo
        { 
            get { return _LicenseInfo; }
        }

        public ctrlDriverLicenseInfo()
        {
            InitializeComponent();
        }
        private void _HandlePersonImage() 
        {
            if (this._LicenseInfo.DriverInfo.PersonInfo.ImagePath == "")
            {
                PbPersonImage.Image = (this._LicenseInfo.DriverInfo.PersonInfo.Gendor == 0) ? Resources.Male_512 : Resources.Female_512;
            }
            else
            {
                string ImagePath = _LicenseInfo.DriverInfo.PersonInfo.ImagePath;

                if (ImagePath != null)

                    if(File.Exists(ImagePath))
                        PbPersonImage.Load(ImagePath);
                    else
                        MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void _FillData()
        {

            lblClass.Text = this._LicenseInfo.LicenseInfo.ClassName;
            lblName.Text = this._LicenseInfo.DriverInfo.PersonInfo.FullName;
            lblLicenseID.Text = this._LicenseID.ToString();
            lblNationalNO.Text = this._LicenseInfo.DriverInfo.PersonInfo.NationalNo;
            lblGendor.Text = (this._LicenseInfo.DriverInfo.PersonInfo.Gendor == 0) ? "Mail" : "Female";
            lblIssueDate.Text = clsFormat.DateToShort(this._LicenseInfo.IssueDate);
            lblIssueReason.Text = ((clsLicense.enIssueReason)this._LicenseInfo.IssueReason).ToString();
            lblNotes.Text = this._LicenseInfo.Notes.ToString();
            lblIsActive.Text = (this._LicenseInfo.IsActive == true) ? "Yes" : "No";
            lblDateOfBirth.Text = clsFormat.DateToShort( this._LicenseInfo.DriverInfo.PersonInfo.DateOfBirth);
            lblDriverID.Text = this._LicenseInfo.DriverID.ToString();
            lblExpirationDate.Text = clsFormat.DateToShort( this._LicenseInfo.ExpirationDate);

            _HandlePersonImage();
        }
        public void LoadDrivirLicenseData(int LicenseID)
        {

            this._LicenseID = LicenseID;
            this._LicenseInfo = clsLicense.FindLicenseByID(this._LicenseID);

            if (this._LicenseInfo == null)
            {
                MessageBox.Show("License ID is not Found Whith ID" +
                    " [" + this._LicenseID.ToString() + "]", "Error", MessageBoxButtons.YesNo
                    , MessageBoxIcon.Error);
           
                return;
            }

            _FillData();
        }
    }
}

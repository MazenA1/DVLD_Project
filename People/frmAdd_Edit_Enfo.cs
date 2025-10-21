using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_BusinnesLayer;
using DVLD_Project.Glople_Classes;
using DVLD_Project.Properties;
namespace DVLD_Project.Peopel
{
    public partial class frmAdd_Edit_Enfo : Form
    {
        // Declare a delegate
        public delegate void DataBackEventHandler(object sender, int PersonID);

        // Declare an event using the delegate
        public event DataBackEventHandler DataBack;

        public enum enMode { AddNew = 0, Update = 1 };
        public enum enGendor { Male = 0, Female = 1 };

        private enMode _Mode;
        private int _PersonID;
        clsPeople _Person; 
        public frmAdd_Edit_Enfo()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }
        public frmAdd_Edit_Enfo(int PersonID)
        {
            InitializeComponent();

            this._Mode = enMode.Update;
            this._PersonID = PersonID; 
        }
        private void _ResetDefualtValues()
        {
            _FillCountryiesComboBox();

            if (_Mode == enMode.AddNew)
            {
                lblModeTitle.Text = "Add New Person";
                _Person = new clsPeople();
            }

            else
                lblModeTitle.Text = "Update Person";

            if (rbMail.Checked)
                PbPersonImage.Image = Resources.Male_512;
            else
                PbPersonImage.Image = Resources.Female_512;

            LiRemoveImage.Visible = (PbPersonImage.ImageLocation != null);

            dtpDate.MaxDate = DateTime.Now.AddYears(-18);
            dtpDate.Value = dtpDate.MaxDate;

            dtpDate.MinDate = DateTime.Now.AddYears(-100);

            cbCountry.SelectedIndex = cbCountry.FindString("Turkey");

            txtFirstName.Text = "";
            txtSecondName.Text = "";
            txtThirdName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
            rbMail.Checked = true;
            txtNationalNo.Text = "";
        }
        private void _FillCountryiesComboBox()
        {
            DataTable CountryiesData = clsCountry.ListCountryies();

            foreach (DataRow Country in CountryiesData.Rows)
            {
                cbCountry.Items.Add(Country["CountryName"]);
            }
        }
        private void _LoadData()
        {
            _Person = clsPeople.Find(_PersonID);

            if (_Person == null)
            {
                MessageBox.Show("No Person with ID = " + _PersonID, "Person Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            lblPesonID.Text = _Person.PersonID.ToString();
            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName;
            txtLastName.Text = _Person.LastName;
            txtNationalNo.Text = _Person.NationalNo;

            if (_Person.Gendor == 1)
                rbFemail.Checked = true;
            else
            {
                rbMail.Checked = true;
            }

            txtEmail.Text = _Person.Email;
            txtPhone.Text = _Person.Phone;
            dtpDate.Value = _Person.DateOfBirth;
            cbCountry.SelectedIndex = cbCountry.FindString( _Person.CountyInfo.CountryName);
            txtAddress.Text = _Person.Address;

            if (_Person.ImagePath != "")
                PbPersonImage.ImageLocation = _Person.ImagePath;

            LiRemoveImage.Visible = (_Person.ImagePath != "");

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close(); 
        }
        private void frmAdd_Edit_Enfo_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();

            if (_Mode == enMode.Update)
                _LoadData();
        }
        private void rbMail_CheckedChanged(object sender, EventArgs e)
        {
            if (PbPersonImage.ImageLocation == null)
                 PbPersonImage.Image = Resources.Male_512;
        }
        private void rbFemail_CheckedChanged(object sender, EventArgs e)
        {
            if (PbPersonImage.ImageLocation == null)
                PbPersonImage.Image = Resources.Female_512;
        } 
        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            TextBox Temp = ((TextBox)sender);

            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "This field is required!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(Temp, null);
            }
        }
        private void txtEmail_Validating(object sender, CancelEventArgs e) 
        {
            if (txtEmail.Text.Trim() == "")
                return;

            if (!clsValidation.ValidateEmail(txtEmail.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "Envallid Email Or Address Format!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtEmail, null);
            }
        }
        private void txtNationalNO_Validating(object sender, CancelEventArgs e) 
        {
            if (string.IsNullOrEmpty(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "This field is required!");
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtNationalNo, null);
            }

            if (txtNationalNo.Text.Trim() != _Person.NationalNo && clsPeople.IsPersonExist(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "National Number is used for another person!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtNationalNo, null); 
            }
        }
        private bool _HandlePersonImage()
        {
            if (_Person.ImagePath != PbPersonImage.ImageLocation)
            {
                if (_Person.ImagePath != "")
                {
                    try
                    {
                        File.Delete(_Person.ImagePath); 
                    }
                    catch (IOException)
                    {

                    }
                }
            }

            if (PbPersonImage.ImageLocation != null)
            {
                string SourceImageFile = PbPersonImage.ImageLocation.ToString();  

                if (clsUtil.CopyeImageToProjectImageFolder(ref SourceImageFile))
                {
                    PbPersonImage.ImageLocation = SourceImageFile;
                    return true; 
                }
                else
                {
                    MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return true; 
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }


            if (!_HandlePersonImage())
                return; 

           
             int NationalCountryID = clsCountry.Find(cbCountry.Text).CountryID;

            _Person.FirstName = txtFirstName.Text.Trim();
            _Person.SecondName = txtSecondName.Text.Trim();
            _Person.ThirdName = txtThirdName.Text.Trim();
            _Person.LastName = txtLastName.Text.Trim();
            _Person.NationalNo = txtNationalNo.Text.Trim();
            _Person.Email = txtEmail.Text.Trim();
            _Person.Phone = txtPhone.Text.Trim();
            _Person.Address = txtAddress.Text.Trim();
            _Person.DateOfBirth = dtpDate.Value;
            _Person.NationalityCountryID = NationalCountryID;

            if (rbMail.Checked)
                _Person.Gendor = (short)enGendor.Male;
            else
                _Person.Gendor = (short)enGendor.Female;

            if (PbPersonImage.ImageLocation != null)
                _Person.ImagePath = PbPersonImage.ImageLocation;
            else
                _Person.ImagePath = "";

            if (_Person.Save())
            {
                lblPesonID.Text = _Person.PersonID.ToString();
                _Mode = enMode.Update;

                lblModeTitle.Text = "Update Person";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);


                // Trigger the event to send data back to the caller form.
                DataBack?.Invoke(this, _Person.PersonID);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }
        private void LlSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Process the selected file
                string selectedFilePath = openFileDialog1.FileName;
                PbPersonImage.Load(selectedFilePath);
                LiRemoveImage.Visible = true;
                // ...
            }
        }
        private void LiRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PbPersonImage.ImageLocation = null;

            if (rbMail.Checked)
                PbPersonImage.Image = Resources.Male_512;
            else
                PbPersonImage.Image = Resources.Female_512;

            PbPersonImage.Visible = false;
        }
    }
} 

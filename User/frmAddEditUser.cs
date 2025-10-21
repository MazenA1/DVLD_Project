﻿using DVLD_BusinnesLayer;
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
    public partial class frmAddEditUser : Form
    {
        public enum enMode {AddNewMode = 0, UpdateMode = 1};
        private enMode _Mode;

        private int _UserID = -1;
        clsUser _User;
        public frmAddEditUser()
        {
            InitializeComponent();

            this._Mode = enMode.AddNewMode;
        }
        public frmAddEditUser(int UserID)
        {
            InitializeComponent(); 
            this._UserID = UserID;

            this._Mode = enMode.UpdateMode;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
        private void _ResetDefualtValue()
        {
            if (this._Mode == enMode.AddNewMode)
            {
                lblModeTitle.Text = "Add New User";
                this.Text = "Add New User";
                _User = new clsUser();

                tabLoginInfo.Enabled = false;

                ctrlPersonCardWithFilter1.FilterFocus(); 
            }

            else
            {
                lblModeTitle.Text = "Update User";
                this.Text = "Update User";

                tabLoginInfo.Enabled = true;
                btnSave.Enabled = true;
            }

            txtUserName.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = ""; 
            cbIsActive.Checked = true; 
        }
        private void _LoadData()
        {
            _User = clsUser.FindByUserID(this._UserID);
            ctrlPersonCardWithFilter1.FilterEnabled = false; 

            if (_User == null)
            {
                MessageBox.Show("No User with ID = " + _User, "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();

                return;
            }

            lblUserID.Text = _User.UserID.ToString();
            txtUserName.Text = _User.UserName;
            txtPassword.Text = _User.Password;
            txtConfirmPassword.Text = _User.Password;
            cbIsActive.Checked = _User.IsActive;
            ctrlPersonCardWithFilter1.LoadPersonInfo(_User.PersonID); 

        }
        private void frmAddEditUser_Load(object sender, EventArgs e)
        {
            _ResetDefualtValue();

            if (this._Mode == enMode.UpdateMode)
                _LoadData();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            _User.PersonID = ctrlPersonCardWithFilter1.PersonID;
            _User.Password = txtPassword.Text;
            _User.UserName = txtUserName.Text;
            _User.IsActive = cbIsActive.Checked;

            if (_User.Save())
            {
                lblUserID.Text = _User.UserID.ToString();

                this._Mode = enMode.UpdateMode;

                lblModeTitle.Text = "Update User";
                this.Text = "Update User";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (this._Mode == enMode.UpdateMode)
            {
                tabLoginInfo.Enabled = true;
                btnSave.Enabled = true;
                tcUserInfo.SelectedTab = tcUserInfo.TabPages["tabLoginInfo"];
                return;
            }

            if (ctrlPersonCardWithFilter1.PersonID != -1)
            {
                if (clsUser.IsUserExistByPersonID(ctrlPersonCardWithFilter1.PersonID)) 
                {
                    MessageBox.Show("Selected Person already has a user, choose another one.", "Select another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ctrlPersonCardWithFilter1.FilterFocus();
                }

                else
                {
                    tabLoginInfo.Enabled = true;
                    btnSave.Enabled = true;
                    tcUserInfo.SelectedTab = tcUserInfo.TabPages["tabLoginInfo"];
                }
            }

            else
            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.FilterFocus();
            }

        }
        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "Username cannot be blank");
                return;
            }
            else
            {
                errorProvider1.SetError(txtUserName, null);
            };


            if (_Mode == enMode.AddNewMode)
            {

                if (clsUser.IsUserExist(txtUserName.Text.Trim()))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtUserName, "username is used by another user");
                }
                else
                {
                    errorProvider1.SetError(txtUserName, null);
                };
            }
            else
            {
                //incase update make sure not to use anothers user name
                if (_User.UserName != txtUserName.Text.Trim())
                {
                    if (clsUser.IsUserExist(txtUserName.Text.Trim()))
                    {
                        e.Cancel = true;
                        errorProvider1.SetError(txtUserName, "username is used by another user");
                        return;
                    }
                    else
                    {
                        errorProvider1.SetError(txtUserName, null);
                    };
                }
            }
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

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtPassword.Text.Trim() != txtConfirmPassword.Text.Trim()) 
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Password Confirmation does not match Password!");
            }

            else
                errorProvider1.SetError(txtConfirmPassword, null);

        }
    }
}

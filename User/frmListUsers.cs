using DVLD_BusinnesLayer;
using DVLD_Project.Peopel;
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
    public partial class frmListUsers : Form
    {
        private DataTable _dtUsers = null;
        public frmListUsers()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditUser();
            frm.ShowDialog();
            frmListUsers_Load(null, null); // Proplem
        }

        private void frmListUsers_Load(object sender, EventArgs e)
        {
            _dtUsers = clsUser.GetAllUsers();
            dgvUsersList.DataSource = _dtUsers;
            lbldgvRecords.Text = dgvUsersList.Rows.Count.ToString();
            cbFilterBy.SelectedIndex = 0;

            if (dgvUsersList.Rows.Count > 0)
            {
                dgvUsersList.Columns[0].HeaderText = "User ID";
                dgvUsersList.Columns[0].Width = 100;

                dgvUsersList.Columns[1].HeaderText = "Person ID";
                dgvUsersList.Columns[1].Width = 100;

                dgvUsersList.Columns[2].HeaderText = "Full Name";
                dgvUsersList.Columns[2].Width = 250;

                dgvUsersList.Columns[3].HeaderText = "User Name";
                dgvUsersList.Columns[3].Width = 150;

                dgvUsersList.Columns[4].HeaderText = "Is Active";
                dgvUsersList.Columns[4].Width = 100;

            }


        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColmun = "";

            switch (cbFilterBy.Text)
            {
                case "User ID":
                    FilterColmun = "UserID";
                    break;

                case "UserName":
                    FilterColmun = "UserName";
                    break;

                case "Person ID":
                    FilterColmun = "PersonID";
                    break;
                     
                case "Full Name":
                    FilterColmun = "FullName";
                    break;

                case "Is Active":
                    FilterColmun = "IsActive";
                    break;

                default:
                    FilterColmun = "None";
                    break;
            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColmun == "None")
            {
                _dtUsers.DefaultView.RowFilter = "";
                lbldgvRecords.Text = dgvUsersList.Rows.Count.ToString(); 
                return;
            }

            if (FilterColmun != "FullName" && FilterColmun != "UserName")
                //in this case we deal with integer not string.

                _dtUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColmun, txtFilterValue.Text.Trim());
            else
                _dtUsers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColmun, txtFilterValue.Text.Trim()); 

            lbldgvRecords.Text = _dtUsers.Rows.Count.ToString();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbFilterBy.Text == "Is Active")
            {
                cbIsActive.Visible = true;
                txtFilterValue.Visible = false;
                cbIsActive.Focus();
                cbIsActive.SelectedIndex = 0; 
            }

            else
            {
                txtFilterValue.Visible = (cbFilterBy.Text != "None");
                cbIsActive.Visible = false;

                if (txtFilterValue.Visible)
                {
                    txtFilterValue.Text = "";
                    txtFilterValue.Focus();
                }

            }
        }

        private void cbFilterBy_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "None")
            {
                frmListUsers_Load(null, null);
            }
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColmn = "IsActive";
            string FilterValue = cbIsActive.Text;

            switch (FilterValue)
            {
                case "All":
                    break;

                case "Yes":
                    FilterValue = "1";
                    break;

                case "No":
                    FilterValue = "0";
                    break;

            }
            if (FilterValue == "All")
                _dtUsers.DefaultView.RowFilter = "";
            else
                //in this case we deal with numbers not string.
                _dtUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColmn, FilterValue);

            lbldgvRecords.Text = _dtUsers.Rows.Count.ToString(); 
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Person ID" || cbFilterBy.Text == "User ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditUser((int)dgvUsersList.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListUsers_Load(null, null); 
        }

        private void changeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmChangePassword((int)dgvUsersList.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListUsers_Load(null, null);
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditUser();
            frm.ShowDialog();
            frmListUsers_Load(null, null);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (clsUser.IsUserExist((int)dgvUsersList.CurrentRow.Cells[0].Value))
            {
                if (clsUser.DeleteUser((int)dgvUsersList.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Deleted Successfully.", "Delete Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    frmListUsers_Load(null, null);
                }

                else
                    MessageBox.Show("Deleted Not Successfully.", "Delete Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
                MessageBox.Show("Deleted Is Not Founf.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void showDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmUserInfo((int)dgvUsersList.CurrentRow.Cells[0].Value);
            frm.ShowDialog(); 
        }

        private void siticoneButton1_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
    }
}

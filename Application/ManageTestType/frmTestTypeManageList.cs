using DVLD_BusinnesLayer;
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
    public partial class frmTestTypeManageList : Form
    {
        private DataTable _dtAllTestType = null;
        public frmTestTypeManageList()
        {
            InitializeComponent();
        }
        private void frmTestTypeManageList_Load(object sender, EventArgs e)
        {
            _dtAllTestType = clsTestTypes.GetAllTestType();

            dgvManageTestTypeList.DataSource = _dtAllTestType;

            lblTestTypeRecord.Text = _dtAllTestType.Rows.Count.ToString();

            if (dgvManageTestTypeList.Rows.Count > 0)
            {
                dgvManageTestTypeList.Columns[0].HeaderText = "ID";
                dgvManageTestTypeList.Columns[0].Width = 110;

                dgvManageTestTypeList.Columns[1].HeaderText = "Title";
                dgvManageTestTypeList.Columns[1].Width = 180;

                dgvManageTestTypeList.Columns[2].HeaderText = "Description";
                dgvManageTestTypeList.Columns[2].Width = 300;

                dgvManageTestTypeList.Columns[3].HeaderText = "Fees";
                dgvManageTestTypeList.Columns[3].Width = 110;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int SelectedRowID = (int)dgvManageTestTypeList.CurrentRow.Cells[0].Value;

            Form frm = new frmEditTestType((clsTestTypes.enTestType)SelectedRowID);
            frm.ShowDialog();

            frmTestTypeManageList_Load(null, null);
        }
    }
}

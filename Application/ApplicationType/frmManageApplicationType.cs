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
    public partial class frmManageApplicationType : Form
    {
        private DataTable AllApplicationType = null;
        public frmManageApplicationType()
        {
            InitializeComponent();
        }

        private void frmManageApplicationType_Load(object sender, EventArgs e)
        {
            AllApplicationType = clsApplicationType.GetAllApplicationType();
            dgvApplicationType.DataSource = AllApplicationType;

            if (dgvApplicationType.Rows.Count > 0)
            {
                dgvApplicationType.Columns[0].HeaderText = "ID";
                dgvApplicationType.Columns[0].Width = 110;

                dgvApplicationType.Columns[1].HeaderText = "Title";
                dgvApplicationType.Columns[1].Width = 315;

                dgvApplicationType.Columns[2].HeaderText = "Fees";
                dgvApplicationType.Columns[2].Width = 110;
            }

            lbldgvRecords.Text = dgvApplicationType.RowCount.ToString(); 
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void updateApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ApplicationTypeID = (int)dgvApplicationType.CurrentRow.Cells[0].Value;

            frmUpdateApplicationType frm = new frmUpdateApplicationType(ApplicationTypeID);
            frm.ShowDialog();

            frmManageApplicationType_Load(null, null); 
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_BusinnesLayer; 
namespace DVLD_Project
{
    public partial class ctrlUserCard : UserControl
    {
        public clsUser UserInfo; 

        private int _UserID;
        public int UserID
        {
            get { return _UserID; }
        }
        public ctrlUserCard()
        {
            InitializeComponent();
        }
        private void _ResetPersonInfo()
        {
            ctrlPersonCard1.ResetPersonInfo();  
            lblUserID.Text = "[???]";
            lblUserName.Text = "[???]";
            lblIsActive.Text = "[???]";
        }
        private void _FillUserInfo()
        {
            ctrlPersonCard1.LoadPersonInfo(UserInfo.PersonID);

            lblUserID.Text = UserInfo.UserID.ToString();
            lblUserName.Text = UserInfo.UserName;

            if (UserInfo.IsActive)
                lblIsActive.Text = "Yes";
            else
                lblIsActive.Text = "No";

        }
        public void LoadUserCardInfo(int UserID)    
        {
            _UserID = UserID; 

            UserInfo = clsUser.FindByUserID(UserID); 

            if (UserInfo == null)
            {
                _ResetPersonInfo(); 

                MessageBox.Show("No User With User ID = " + UserID,"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillUserInfo(); 
        }
        
    }
}

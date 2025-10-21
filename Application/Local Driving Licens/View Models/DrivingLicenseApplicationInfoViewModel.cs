using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Application.Local_Driving_Licens.View_Models
{
    public class DrivingLicenseApplicationInfoViewModel
    {
        public int LocalDrivingLicenseApplicationID { get; set; } = -1;
        public string AppliedForLicense { set; get; } = string.Empty;
        public byte PassedTest { set; get; } = 0;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Project.View_Models
{
    public class LocalDrivingLicenseApplicationViewModel 
    {
        public int ApplicationID { get; set; }
        public string ApplicationDate { get; set; }
        public int LicenseClassID { get; set; }
        public string LicenseClassName { get; set; }
        public float PaidFees { get; set; }
        public string CreatedByUserName { get; set; }
    }
}

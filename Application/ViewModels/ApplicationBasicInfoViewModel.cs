using DVLD_BusinnesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Project
{
    public class ApplicationBasicInfoViewModel
    {
        public int ApplicationID { set; get; }
        public string Status { set; get; }
        public float Fees { set; get; }
        public string ApplicationType { set; get; }
        public string ApplicantName { set; get; }
        public DateTime ApplicationDate { set; get; }
        public DateTime ApplicationStatusDate { set; get; }
        public string CreatedUserName { set; get; }
    }
}

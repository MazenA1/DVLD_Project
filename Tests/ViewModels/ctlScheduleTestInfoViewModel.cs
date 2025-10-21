using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Project.Application.ViewModels
{
    public class ctlScheduleTestInfoViewModel
    {
        public int LocalDrivingLicenseApplicationID { set; get; }
        public string DrivingLicenseName { set; get; }
        public string ApplicantName { set; get; }
        public byte Trial { set; get; }
        public DateTime ScheduledTestDateTime { set; get; }
        public float TestFees { set; get; }
        public float RetakeTestFees { set; get; }
        public int RetakeApplicationTestsID { set; get; }
        public float TotalFees { set; get; }
    }
}

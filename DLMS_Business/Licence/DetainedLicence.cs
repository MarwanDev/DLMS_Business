using DLMS_DataAccess.Licence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLMS_Business.Licence
{
    public class DetainedLicence
    {
        public int ID { get; set; }
        public int LicenceID { get; set; }
        public DateTime DetainDate { get; set; }
        public decimal FineFees { get; set; }
        public int CreatedByUserId { get; set; }
        public bool IsReleased { get; set; }
        public int ReleasedByUserId { get; set; }
        public int ReleaseApplicationId { get; set; }

        public DetainedLicence()
        {
            this.ID = -1;
            this.LicenceID = -1;
            this.CreatedByUserId = -1;
            this.ReleasedByUserId = -1;
            this.ReleaseApplicationId = -1;
            this.DetainDate = new DateTime();
            this.FineFees = 0;
            this.IsReleased = false;
        }

        public static bool IsLicenceDetained(int licenceId)
        {
            return DetainedLicenceData.IsLicenceDetained(licenceId);
        }
    }
}

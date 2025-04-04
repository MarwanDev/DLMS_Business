using DLMS_DataAccess.Licence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DLMS_Business.Licence
{
    public class DetainedLicence
    {
        public int ID { get; set; }
        public int LicenceID { get; set; }
        public DateTime DetainDate { get; set; }
        public DateTime ReleaseDate { get; set; }
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

        private DetainedLicence(int id, int licenceId, DateTime detainDate, decimal fineFees,
            int createdByUserId, bool isReleased, DateTime releaseDate, int releasedByUserId, int releaseApplicationId)
        {
            this.ID = id;
            this.LicenceID = licenceId;
            this.CreatedByUserId = createdByUserId;
            this.DetainDate = detainDate;
            this.FineFees = fineFees;
            this.IsReleased = isReleased;
            this.ReleaseDate = releaseDate;
            this.ReleaseApplicationId = releaseApplicationId;
            this.ReleasedByUserId = releasedByUserId;
        }

        public static bool IsLicenceDetained(int licenceId)
        {
            return DetainedLicenceData.IsLicenceDetained(licenceId);
        }

        public static DetainedLicence Find(int id)
        {
            int licenceId = -1, createdByUserId = -1, releaseApplicationId = -1, releasedByUserId = -1;
            DateTime detainDate = new DateTime(), releaseDate = new DateTime();
            decimal fineFees = 0;
            bool isReleased = false;
            if (DetainedLicenceData.GetDetainedLicenceDatById(id, ref licenceId, ref detainDate, ref fineFees,
                ref createdByUserId, ref isReleased, ref releaseDate, ref releasedByUserId, ref releaseApplicationId))
                return new DetainedLicence(id, licenceId, detainDate, fineFees,
                createdByUserId, isReleased, releaseDate, releasedByUserId, releaseApplicationId);
            else return null;
        }

        public static DetainedLicence FindbyLicenceId(int licenceId)
        {
            int id = -1, createdByUserId = -1, releaseApplicationId = -1, releasedByUserId = -1;
            DateTime detainDate = new DateTime(), releaseDate = new DateTime();
            decimal fineFees = 0;
            bool isReleased = false;
            if (DetainedLicenceData.GetDetainedLicenceDatByLicenceId(ref id, licenceId, ref detainDate, ref fineFees,
                ref createdByUserId, ref isReleased, ref releaseDate, ref releasedByUserId, ref releaseApplicationId))
                return new DetainedLicence(id, licenceId, detainDate, fineFees,
                createdByUserId, isReleased, releaseDate, releasedByUserId, releaseApplicationId);
            else return null;
        }

        public bool ReleaseDetainedLicence()
        {
            return DetainedLicenceData.ReleaseDetainedLicence(ID, ReleaseDate, ReleasedByUserId, ReleaseApplicationId);
        }
    }
}

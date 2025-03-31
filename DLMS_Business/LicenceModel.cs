using DLMS_DataAccess.Licence;
using System;

namespace DLMS_Business
{
    public class LicenceModel
    {
        public int ID { get; set; }
        public int ApplicationId { get; set; }
        public int DriverId { get; set; }
        public int LicenceClassId { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }
        public string Notes { get; set; }
        public decimal PaidFees { get; set; }
        public byte IssueReason { get; set; }

        public LicenceModel()
        {
            this.ID = -1;
            this.ApplicationId = -1;
            this.LicenceClassId = -1;
            this.CreatedByUserId = -1;
            this.DriverId = -1;
            this.IssueDate = new DateTime();
            this.ExpirationDate = new DateTime();
            this.IsActive = false;
            this.Notes = "";
            this.PaidFees = 0;
            this.IssueReason = 0;
        }

        public bool AddNewLicence()
        {
            this.ID = LicenceData.AddNewLicence(ApplicationId, DriverId, LicenceClassId, IssueDate, IsActive,
                Notes, ExpirationDate, PaidFees, CreatedByUserId, IssueReason);
            return this.ID != -1;
        }
    }
}

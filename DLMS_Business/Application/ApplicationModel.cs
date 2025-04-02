using DLMS_DataAccess;
using System;
using System.Runtime.CompilerServices;

namespace DLMS_Business.Application
{
    public class ApplicationModel
    {
        public static bool DeleteApplication(int id)
        {
            return ApplicationData.DeleteApplication(id);
        }

        public int ID { get; set; }
        public int ApplicantPersonId { get; set; }
        public int ApplicationTypeId { get; set; }
        public DateTime ApplicationDate { get; set; }
        public DateTime LastStatusDate { get; set; }
        public int ApplicationStatus { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserId { get; set; }


        public ApplicationModel()
        {
            this.ID = -1;
            this.ApplicantPersonId = -1;
            this.ApplicationTypeId = -1;
            this.ApplicationDate = new DateTime();
            this.LastStatusDate = new DateTime();
            this.ApplicationStatus = -1;
            this.PaidFees = 0;
            this.CreatedByUserId = -1;
        }

        public bool AddNewApplication(ref int applicationId)
        {
            this.ID = ApplicationData.AddNewApplication(ApplicantPersonId, ApplicationDate, ApplicationTypeId,
                ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserId);
            applicationId = this.ID;
            return this.ID != -1;
        }
    }
}

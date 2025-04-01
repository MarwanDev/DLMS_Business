using DLMS_DataAccess.Licence;
using System;
using System.Data;

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
        public DateTime DateOfBirth { get; set; }
        public bool IsActive { get; set; }
        public string Notes { get; set; }
        public string FullName { get; set; }
        public string ClassName { get; set; }
        public string NationalNo { get; set; }
        public string Gender { get; set; }
        public string ImagePath { get; set; }
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

        private LicenceModel(int id, int driverId, DateTime issueDate, DateTime expirationDate,
                string notes, decimal paidFees, bool isActive, byte issueReason, string fullname,
                string className, string nationalNo, string gender, DateTime dateOfBirth, string imagePath)
        {
            this.ID = id;
            this.DriverId = driverId;
            this.IssueDate = issueDate;
            this.ExpirationDate = expirationDate;
            this.Notes = notes;
            this.PaidFees = paidFees;
            this.IsActive = isActive;
            this.IssueReason = issueReason;
            this.FullName = fullname;
            this.ClassName = className;
            this.NationalNo = nationalNo;
            this.Gender = gender;
            this.DateOfBirth = dateOfBirth;
            this.ImagePath = imagePath;
        }

        public bool AddNewLicence()
        {
            this.ID = LicenceData.AddNewLicence(ApplicationId, DriverId, LicenceClassId, IssueDate, IsActive,
                Notes, ExpirationDate, PaidFees, CreatedByUserId, IssueReason);
            return this.ID != -1;
        }

        public static LicenceModel Find(int id)
        {
            int driverId = -1;
            DateTime issueDate = new DateTime(), expirationDate = new DateTime(), dateOfBirth = new DateTime();
            bool isActive = false;
            string notes = "", fullname = "", gender = "", className = "", nationalNo = "", imagePath = "";
            decimal paidFees = 0;
            byte issueReason = 0;
            if (LicenceData.GetLicenceDataById(id, ref driverId, ref issueDate, ref expirationDate,
                ref notes, ref paidFees, ref isActive, ref issueReason, ref fullname,
                ref className, ref nationalNo, ref gender, ref dateOfBirth, ref imagePath))
                return new LicenceModel(id, driverId, issueDate, expirationDate, notes, paidFees,
                    isActive, issueReason, fullname, className, nationalNo, gender, dateOfBirth, imagePath);
            else
                return null;
        }

        public static LicenceModel FindByLocalDLApplication(int id)
        {
            int driverId = -1;
            DateTime issueDate = new DateTime(), expirationDate = new DateTime(), dateOfBirth = new DateTime();
            bool isActive = false;
            string notes = "", fullname = "", gender = "", className = "", nationalNo = "", imagePath = "";
            decimal paidFees = 0;
            byte issueReason = 0;
            if (LicenceData.GetLicenceDataByLocalDLApplicationId(id, ref driverId, ref issueDate, ref expirationDate,
                ref notes, ref paidFees, ref isActive, ref issueReason, ref fullname,
                ref className, ref nationalNo, ref gender, ref dateOfBirth, ref imagePath))
                return new LicenceModel(id, driverId, issueDate, expirationDate, notes, paidFees,
                    isActive, issueReason, fullname, className, nationalNo, gender, dateOfBirth, imagePath);
            else
                return null;
        }

        public static DataTable GetAllLocalLicencesPerPerson(int personId)
        {
            return LicenceData.GetAllLocalLicencesPerPerson(personId);
        }

        public static int GetAllLocalLicencePerPersonCount(int personId)
        {
            return LicenceData.GetAllLocalLicencePerPersonCount(personId);
        }

        public static DataTable GetAllInternationalLicencesPerPerson(int personId)
        {
            return LicenceData.GetAllInternationalLicencesPerPerson(personId);
        }

        public static int GetAllInternationalLicencePerPersonCount(int personId)
        {
            return LicenceData.GetAllInternationalLicencePerPersonCount(personId);
        }

        public static bool DoesInternationalLicenceExistWithLocalLicenceId(int licenceId)
        {
            return LicenceData.DoesInternationalLicenceExistWithLocalLicenceId(licenceId);
        }

        public static int GetLicencClassId(int licenceId)
        {
            return LicenceData.GetLicencClassId(licenceId);
        }
    }
}

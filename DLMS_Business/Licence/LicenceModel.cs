using DLMS_DataAccess;
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
        public int DetainedByUserId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DetainDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsReleased { get; set; }
        public string Notes { get; set; }
        public string FullName { get; set; }
        public string ClassName { get; set; }
        public string NationalNo { get; set; }
        public string Gender { get; set; }
        public string ImagePath { get; set; }
        public decimal PaidFees { get; set; }
        public decimal FineFees { get; set; }
        public byte IssueReason { get; set; }
        public int LocalLicenceId { get; set; }
        public int DetainId { get; set; }

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
            this.LocalLicenceId = -1;
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

        private LicenceModel(int id, int localLicenceId, string nationalNo,
            string gender, DateTime issueDate, int applicationId, bool isActive,
            DateTime dateOfBirth, int driverId, DateTime expirationDate, string imagePath, string fullName)
        {
            this.ID = id;
            this.DriverId = driverId;
            this.LocalLicenceId = localLicenceId;
            this.ApplicationId = applicationId;
            this.IssueDate = issueDate;
            this.ExpirationDate = expirationDate;
            this.IsActive = isActive;
            this.NationalNo = nationalNo;
            this.Gender = gender;
            this.DateOfBirth = dateOfBirth;
            this.ImagePath = imagePath;
            this.FullName = fullName;
        }

        public bool AddNewLicence()
        {
            this.ID = LicenceData.AddNewLicence(ApplicationId, DriverId, LicenceClassId, IssueDate, IsActive,
                Notes, ExpirationDate, PaidFees, CreatedByUserId, IssueReason);
            return this.ID != -1;
        }

        public bool AddNewInternationalLicence()
        {
            this.ID = LicenceData.AddNewInternationalLicence(ApplicationId, DriverId, LocalLicenceId, IssueDate, IsActive,
                ExpirationDate, CreatedByUserId);
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

        public int RenewLicence(int createdByUserId, int applicationId, string notes)
        {
            int oldId = ID;
            IssueDate = DateTime.Now;
            ExpirationDate = DateTime.Now.AddYears(10);
            IssueReason = 2;
            CreatedByUserId = createdByUserId;
            ApplicationId = applicationId;
            Notes = notes;
            LicenceClassId = GetLicenceClassId(ID);
            IsActive = true;
            if (AddNewLicence())
            {
                DeactivateLicence(oldId);
                return ID;
            }
            return -1;
        }

        public int ReplaceLicence(int createdByUserId, int applicationId, byte issueReason)
        {
            int oldId = ID;
            CreatedByUserId = createdByUserId;
            ApplicationId = applicationId;
            IssueReason = issueReason;
            LicenceClassId = GetLicenceClassId(ID);
            IsActive = true;
            if (AddNewLicence())
            {
                DeactivateLicence(oldId);
                return ID;
            }
            return -1;
        }

        public static bool DeactivateLicence(int id)
        {
            return LicenceData.DeactivateLicence(id);
        }

        public static LicenceModel FindInternationalLicence(int id)
        {
            int driverId = -1, localLicenceId = -1, applicationId = -1;
            DateTime issueDate = new DateTime(), expirationDate = new DateTime(), dateOfBirth = new DateTime();
            bool isActive = false;
            string fullName = "", gender = "", nationalNo = "", imagePath = "";
            if (LicenceData.GetInternationalLicenceDataById(id, ref localLicenceId, ref nationalNo, ref gender,
                ref issueDate, ref applicationId, ref isActive, ref dateOfBirth, ref driverId,
                ref expirationDate, ref imagePath, ref fullName))
                return new LicenceModel(id, localLicenceId, nationalNo, gender,
                issueDate, applicationId, isActive, dateOfBirth, driverId, expirationDate, imagePath, fullName);
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

        public static int GetAllInternationalLicencesPerPersonCount(int personId)
        {
            return LicenceData.GetAllInternationalLicencePerPersonCount(personId);
        }

        public static bool DoesInternationalLicenceExistWithLocalLicenceId(int licenceId)
        {
            return LicenceData.DoesInternationalLicenceExistWithLocalLicenceId(licenceId);
        }

        public static int GetLicenceClassId(int licenceId)
        {
            return LicenceData.GetLicenceClassId(licenceId);
        }

        public static DataTable GetAllInternationalLicences()
        {
            return LicenceData.GetAllInternationalLicences();
        }

        public static int GetAllInternationalLicenceCount()
        {
            return LicenceData.GetAllInternationalLicenceCount();
        }

        public static DataTable FilterInternationalLicences(string filterMode, string filterkeyWord)
        {
            LicenceData.CurrentFilter = filterMode;
            return LicenceData.GetFilteredInternationalLicences(filterkeyWord);
        }

        public static int GetFilteredInternationalLicencesCount(string filterkeyWord)
        {
            return LicenceData.GetFilteredInternationalLicenceCount(filterkeyWord);
        }

        public bool DetainLicecne()
        {
            this.DetainId = LicenceData.DetainLicence(ID, DetainDate, FineFees, DetainedByUserId, IsReleased);
            return this.DetainId != -1;
        }
    }
}

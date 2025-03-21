using System;
using System.Data;
using DLMS_DataAccess;

namespace DLMS_Business
{
    public class LocalDLApplication
    {
        public enum Mode { AddNew = 0, Update = 1 };
        public Mode CurrentMode = Mode.AddNew;
        public int ID { set; get; }
        public int ApplicantPersonID { set; get; }
        public DateTime ApplicationDate { set; get; }
        public int ApplicationTypeID { set; get; }
        public int ApplicationStatus { set; get; }
        public DateTime LastStatusDate { set; get; }
        public decimal PaidFees { set; get; }
        public int CreatedByUserID { set; get; }
        public int LicenceClassID { set; get; }

        public LocalDLApplication()
        {
            this.ID = -1;
            this.ApplicantPersonID = -1;
            this.ApplicationDate = new DateTime();
            this.ApplicationTypeID = -1;
            this.ApplicationStatus = -1;
            this.LastStatusDate = new DateTime();
            this.PaidFees = 0;
            this.CreatedByUserID = -1;
            this.LicenceClassID = -1;

            CurrentMode = Mode.AddNew;
        }

        private LocalDLApplication(int id, int applicantPersonId, DateTime applicationDate, int applicationTypeId, int applicationStatus,
            DateTime lastStatusDate, decimal paidFees, int createdByUserId, int licenceClassId)
        {
            ID = id;
            ApplicantPersonID = applicantPersonId;
            ApplicationDate = applicationDate;
            ApplicationTypeID = applicationTypeId;
            ApplicationStatus = applicationStatus;
            LastStatusDate = lastStatusDate;
            PaidFees = paidFees;
            CreatedByUserID = createdByUserId;
            LicenceClassID = licenceClassId;
            CurrentMode = Mode.Update;
        }

        private bool AddNewLocalDLApplication()
        {
            this.ID = LocalDLApplicationData.AddNewLocalDLApplication(ApplicantPersonID, ApplicationDate, ApplicationTypeID,
                ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID, LicenceClassID);
            return (this.ID != -1);
        }

        private bool UpdateLocalDLApplication()
        {
            return true;
        }

        public bool Save()
        {
            switch (CurrentMode)
            {
                case Mode.AddNew:
                    if (AddNewLocalDLApplication())
                    {
                        CurrentMode = Mode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case Mode.Update:
                    return UpdateLocalDLApplication();
            }
            return false;
        }

        public static int GetApplicationIdForSamePersonAndLicenceClass(int licenceClassId, int applicantPersonId)
        {
            return LocalDLApplicationData.GetApplicationIdForSamePersonAndLicenceClass(licenceClassId, applicantPersonId);
        }

        public static DataTable GetAllLocalDLApplications()
        {
            return LocalDLApplicationData.GetAllLocalDLApplications();
        }

        public static DataTable FilterLocalDLApplications(string filterMode, string filterkeyWord)
        {
            LocalDLApplicationData.CurrentFilter = filterMode;
            return LocalDLApplicationData.GetFilteredLocalDLApplications(filterkeyWord);
        }

        public static int GetAllLocalDLApplicationsCount()
        {
            return LocalDLApplicationData.GetAllLocalDLApplicationsCount();
        }

        public static int GetFilteredLocalDLApplicationsCount(string filterkeyWord)
        {
            return LocalDLApplicationData.GetFilteredLocalDLApplicationsCount(filterkeyWord);
        }

        public static bool CancelLocalDLApplication(int localDLApplicationId)
        {
            return LocalDLApplicationData.CancelLocalDLApplication(localDLApplicationId);
        }
    }
}

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
        public string PersonFullName { set; get; }
        public string ApplicationTypeTitle { set; get; }
        public string CreatedByUserName { set; get; }
        public string LicenceClassName { set; get; }
        public string StatusText { set; get; }
        public int PassedTests { set; get; }
        public int ApplicationID { set; get; }

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
            this.PersonFullName = "";
            this.ApplicationTypeTitle = "";
            this.CreatedByUserName = "";
            this.LicenceClassName = "";

            CurrentMode = Mode.AddNew;
        }

        private LocalDLApplication(int id, int applicantPersonId, DateTime applicationDate, int applicationTypeId, int applicationStatus,
            DateTime lastStatusDate, decimal paidFees, int createdByUserId, int licenceClassId, string createdByUserName)
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
            CreatedByUserName = createdByUserName;

            CurrentMode = Mode.Update;
        }

        private LocalDLApplication(int id, int applicantPersonId, DateTime applicationDate, int applicationStatus,
            decimal paidFees, int licenceClassId, string createdByUserName, string licenceClassName)
        {
            ID = id;
            ApplicantPersonID = applicantPersonId;
            ApplicationDate = applicationDate;
            ApplicationStatus = applicationStatus;
            PaidFees = paidFees;
            LicenceClassID = licenceClassId;
            CreatedByUserName = createdByUserName;
            LicenceClassName = licenceClassName;

            CurrentMode = Mode.Update;
        }

        private LocalDLApplication(int id, string className, string fullName,
            DateTime applicationDate, int passedTests, string statusText, int applicationId,
            decimal paidFees, string applicationTypeTitle, DateTime lastStatusDate, string userName)
        {
            ID = id;
            LicenceClassName = className;
            PersonFullName = fullName;
            ApplicationDate = applicationDate;
            ApplicationDate = applicationDate;
            PassedTests = passedTests;
            PaidFees = paidFees;
            StatusText = statusText;
            ApplicationID = applicationId;
            ApplicationTypeTitle = applicationTypeTitle;
            LastStatusDate = lastStatusDate;
            CreatedByUserName = userName;

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
            return LocalDLApplicationData.UpdateLocalDLApplication(ID, LicenceClassID);
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

        public static LocalDLApplication Find(int id)
        {
            int applicantPersonId = -1;
            byte applicationStatus = 0;
            DateTime applicationDate = new DateTime();
            decimal paidFees = 0;
            string createdByUserName = "";
            string licenceClassName = "";
            int licenceClassID = 0;
            if (LocalDLApplicationData.GetLocalDLApplicationInfoById(id, ref applicantPersonId, ref applicationDate, ref applicationStatus,
                ref paidFees, ref createdByUserName, ref licenceClassID, ref licenceClassName))
                return new LocalDLApplication(id, applicantPersonId, applicationDate, applicationStatus, paidFees,
                    licenceClassID, createdByUserName, licenceClassName);
            else
                return null;
        }

        public static LocalDLApplication FindInDetails(int id)
        {
            string className = "";
            string fullName = "";
            DateTime applicationDate = new DateTime();
            int passedTests = -1;
            string statusText = "";
            int applicationId = -1;
            decimal paidFees = -1;
            string applicationTypeTitle = "";
            DateTime lastStatusDate = new DateTime();
            string userName = "";
            if (LocalDLApplicationData.GetLocalDLApplicationMoreDetailsById(id, ref className, ref fullName,
                ref applicationDate, ref passedTests, ref statusText, ref applicationId,
                ref paidFees, ref applicationTypeTitle, ref lastStatusDate, ref userName))
                return new LocalDLApplication(id, className, fullName,
                    applicationDate, passedTests, statusText, applicationId,
                    paidFees, applicationTypeTitle, lastStatusDate, userName);
            else
                return null;
        }

        public static int GetPassedTestsCount(int id)
        {
            return LocalDLApplicationData.GetPassedTestsCountById(id);
        }

        public static byte GetApplicationStatusById(int id)
        {
            return LocalDLApplicationData.GetApplicationStatusById(id);
        }

        public static int GetApplicationId(int id)
        {
            return LocalDLApplicationData.GetApplicationId(id);
        }

        public static bool DeleteLocalDLApplication(int id)
        {
            return LocalDLApplicationData.DeleteLocalDLApplication(id);
        }

        public static int GetPersonApplicantId(int id)
        {
            return LocalDLApplicationData.GetPersonApplicantId(id);
        }

        public static bool ChangeApplicationStatus(int id, byte status)
        {
            return LocalDLApplicationData.ChangeApplicationStatus(id, status);
        }

        public static int GetLicenceClassId(int id)
        {
            return LocalDLApplicationData.GetLicenceClassId(id);
        }
    }
}

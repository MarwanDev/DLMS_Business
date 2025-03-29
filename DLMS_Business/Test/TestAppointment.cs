using DLMS_DataAccess;
using System;
using System.Data;

namespace DLMS_Business
{
    public class TestAppointment : Business
    {
        public enum Mode { AddNew = 0, Update = 1 };
        public Mode CurrentMode = Mode.AddNew;

        public int ID { get; set; }
        public int TestTypeID { get; set; }
        public int LocalDLApplicationID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsLocked { get; set; }

        public TestAppointment()
        {
            this.ID = -1;
            this.CreatedByUserID = -1;
            this.IsLocked = true;
            this.TestTypeID = 0;
            this.LocalDLApplicationID = 0;
            this.AppointmentDate = new DateTime();
            this.PaidFees = 0;

            CurrentMode = Mode.AddNew;
        }

        private TestAppointment(int iD, int testTypeID, int localDLApplicationID, DateTime appointmentDate, decimal paidFees, int createdByUserID, bool isLocked)
        {
            ID = iD;
            TestTypeID = testTypeID;
            LocalDLApplicationID = localDLApplicationID;
            AppointmentDate = appointmentDate;
            PaidFees = paidFees;
            CreatedByUserID = createdByUserID;
            IsLocked = isLocked;

            CurrentMode = Mode.Update;
        }

        private bool AddNewTestAppointment()
        {
            this.ID = TestAppointmentData.AddNewTestAppointment(TestTypeID, LocalDLApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked);
            return (this.ID != -1);
        }

        private bool UpdateTestAppointment()
        {
            return TestAppointmentData.UpdateTestAppointment(ID, AppointmentDate);
        }

        public bool Save()
        {
            switch (CurrentMode)
            {
                case Mode.AddNew:
                    if (AddNewTestAppointment())
                    {
                        CurrentMode = Mode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case Mode.Update:
                    return UpdateTestAppointment();
            }
            return false;
        }

        public static DataTable GetAllTestAppointmentsForLocalDLApplication(int id)
        {
            return TestAppointmentData.GetAllTestAppointmentsForLocalDLApplication(id);
        }

        public static int GetAllTestAppointmentsCountForLocalDLApplication(int id)
        {
            return TestAppointmentData.GetAllTestAppointmentsCountForLocalDLApplication(id);
        }

        public static bool DoesActiveTestAppointmentExist(int id)
        {
            return TestAppointmentData.DoesActiveTestAppointmentExist(id);
        }

        public static bool IsTestPassed(int testTypeId, int localDLApplicationid)
        {
            return TestAppointmentData.IsTestPassed(testTypeId, localDLApplicationid);
        }

        public static int GetNumberOfTrials(int testTypeId, int localDLApplicationid)
        {
            return TestAppointmentData.GetNumberOfTrials(testTypeId, localDLApplicationid);
        }

        public static TestAppointment Find(int id)
        {
            int testTypeId = -1, localDLApplicationId = -1, createdByUserId = -1;
            DateTime appointmentDate = new DateTime();
            decimal paidFees = 0;
            bool isLocked = false;
            if (TestAppointmentData.GetTestAppointmentById(id, ref testTypeId, ref localDLApplicationId, ref appointmentDate,
                ref paidFees, ref createdByUserId, ref isLocked))
            {
                return new TestAppointment(id, testTypeId, localDLApplicationId, appointmentDate,
                    paidFees, createdByUserId, isLocked);
            }
            else
                return null;
        }
    }
}

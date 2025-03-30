using DLMS_DataAccess.Test;

namespace DLMS_Business.Test
{
    public class TestModel
    {
        public int ID { get; set; }
        public int TestAppointmentID { get; set; }
        public bool TestResult { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserID { get; set; }

        public TestModel()
        {
            this.ID = -1;
            this.TestAppointmentID = -1;
            this.TestResult = false;
            this.Notes = "";
            this.CreatedByUserID = -1;
        }

        public bool TakeTest(int testAppointmentId, bool testResult, string notes, int createdByUserId)
        {
            this.ID = TestData.TakeTest(testAppointmentId, testResult, notes, createdByUserId);
            return this.ID != -1;
        }

        public bool Save()
        {
            return TakeTest(TestAppointmentID, TestResult, Notes, CreatedByUserID);
        }
    }
}

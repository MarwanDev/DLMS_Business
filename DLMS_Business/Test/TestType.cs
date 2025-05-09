﻿using DLMS_DataAccess;
using System.Data;

namespace DLMS_Business
{
    public class TestType : Business
    {
        public int ID { set; get; }
        public decimal Fees { set; get; }
        public string Title { set; get; }
        public string Description { set; get; }

        public TestType()
        {
            this.ID = -1;
            this.Fees = -1;
            this.Title = "";
            this.Description = "";
        }

        private TestType(int id, string title, decimal fees, string description)
        {
            ID = id;
            Title = title;
            Fees = fees;
            Description = description;
        }

        public bool Save()
        {
            return UpdateTestType();
        }

        private bool UpdateTestType()
        {
            return TestTypeData.UpdateTestType(ID, Title, Fees, Description);
        }

        public static TestType Find(int id)
        {
            string title = "", description = "";
            decimal fees = -1;
            if (TestTypeData.GetTestTypeDataById(id, ref title, ref fees, ref description))
                return new TestType(id, title, fees, description);
            else
                return null;
        }

        public static int GetAllTestTypesCount()
        {
            return TestTypeData.GetAllTestTypesCount();
        }

        public static DataTable GetAllTestTypes()
        {
            return TestTypeData.GetAllTestTypes();
        }
    }
}

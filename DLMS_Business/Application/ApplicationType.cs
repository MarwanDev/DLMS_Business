using DLMS_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLMS_Business
{
    public class ApplicationType : Business
    {
        public int ID { set; get; }
        public decimal Fees { set; get; }
        public string Title { set; get; }

        public ApplicationType()
        {
            this.ID = -1;
            this.Fees = -1;
            this.Title = "";
        }

        private ApplicationType(int id, string title, decimal fees)
        {
            ID = id;
            Title = title;
            Fees = fees;
        }

        public bool Save()
        {
            return UpdateApplicationType();
        }

        private bool UpdateApplicationType()
        {
            return ApplicationTypeData.UpdateApplicationType(ID, Title, Fees);
        }

        public static ApplicationType Find(int id)
        {
            string title = "";
            decimal fees = -1;
            if (ApplicationTypeData.GetApplicationTypeDataById(id, ref title, ref fees))
                return new ApplicationType(id, title, fees);
            else
                return null;
        }

        public static int GetAllApplicationTypesCount()
        {
            return ApplicationTypeData.GetAllApplicationTypesCount();
        }

        public static DataTable GetAllApplicationTypes()
        {
            return ApplicationTypeData.GetAllApplicationTypes();
        }
    }
}

using DLMS_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLMS_Business
{
    public class ApplicationType
    {
        public int ID { set; get; }
        public double Fees { set; get; }
        public string Title { set; get; }

        public ApplicationType()
        {
            this.ID = -1;
            this.Fees = -1;
            this.Title = "";
        }

        private ApplicationType(int id, string title, double fees)
        {
            ID = id;
            Title = title;
            Fees = fees;
        }

        public bool UpdateApplicationType()
        {
            return ApplicationTypeData.UpdateApplicationType(ID, Title, Fees);
        }

        public static ApplicationType Find(int id)
        {
            string title = "";
            double fees = 0;
            if (ApplicationTypeData.GetApplicationTypeById(id, ref title, ref fees))
                return new ApplicationType(id, title, fees);
            else
                return null;
        }

        public static int GetAllApplicationTypesCount()
        {
            return ApplicationTypeData.GetAllApplicationTypesCount();
        }

        public static void ApplySorting(string sortingParameter)
        {
            ApplicationTypeData.IsSortingUsed = true;
            ApplicationTypeData.SortingText = sortingParameter;
            ApplicationTypeData.ApplySorting();
        }

        public static DataTable GetAllApplicationTypes()
        {
            return ApplicationTypeData.GetAllApplicationTypes();
        }
    }
}

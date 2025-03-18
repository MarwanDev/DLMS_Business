using DLMS_DataAccess;
using System.Data;

namespace DLMS_Business
{
    public class LicenceClass
    {
        public static DataTable GetAllLicenceClassesForDropDown()
        {
            return LicenceClassData.GetAllLicenceClassesForDropDown();
        }
    }
}

using System.Data;
using DLMS_DataAccess;

namespace DLMS_Business
{
    public class Country
    {
        public static DataTable GetAllCountries()
        {
            return CountryData.GetAllCountries();
        }
    }
}

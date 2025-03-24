using DLMS_DataAccess;

namespace DLMS_Business.Application
{
    public class ApplicationModel
    {
        public static bool DeleteApplication(int id)
        {
            return ApplicationData.DeleteApplication(id);
        }
    }
}

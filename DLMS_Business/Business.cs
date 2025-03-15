using DLMS_DataAccess;

namespace DLMS_Business
{
    public class Business
    {
        public static void DisableSorting()
        {
            Data.IsSortingUsed = false;
            Data.ApplySorting();
        }

        public static void ApplySorting(string sortingParameter)
        {
            Data.IsSortingUsed = true;
            Data.SortingText = sortingParameter;
            Data.ApplySorting();
        }
    }
}

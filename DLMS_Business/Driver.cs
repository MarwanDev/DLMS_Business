using DLMS_DataAccess;
using DLMS_DataAccess.Person;
using System;
using System.Data;

namespace DLMS_Business
{
    public class Driver
    {
        public int ID { get; set; }
        public int PersonId { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }

        public Driver()
        {
            this.ID = -1;
            this.PersonId = -1;
            this.CreatedByUserId = -1;
            this.CreatedDate = new DateTime();
        }

        private Driver(int id, int personId, int createdByUserId, DateTime createdDate)
        {
            this.ID = id;
            this.PersonId = personId;
            this.CreatedByUserId = createdByUserId;
            this.CreatedDate = createdDate;
        }

        public static DataTable FilterDrivers(string filterMode, string filterkeyWord)
        {
            DriverData.CurrentFilter = filterMode;
            return DriverData.GetFilteredDrivers(filterkeyWord);
        }

        public bool AddNewDriver()
        {
            this.ID = DriverData.AddNewDriver(PersonId, CreatedByUserId, CreatedDate);
            return this.ID != -1;
        }

        public static DataTable GetAllDrivers()
        {
            return DriverData.GetAllDrivers();
        }

        public static int GetFilteredDriversCount(string filterkeyWord)
        {
            return DriverData.GetFilteredDriversCount(filterkeyWord);
        }

        public static int GetAllDriversCount()
        {
            return DriverData.GetAllDriversCount();
        }

        public static bool DoesDriverExistWithSamePersonId(int personId)
        {
            return DriverData.DoesDriverExistWithSamePersonId(personId);
        }

        public static int GetDriversIdByPersonId(int personId)
        {
            return DriverData.GetDriversIdByPersonId(personId);
        }

        public static Driver Find(int id)
        {
            int personId = -1, createdByUserId = -1;
            DateTime createdDate = new DateTime();
            if (DriverData.GetDriverDataById(id, ref personId, ref createdByUserId, ref createdDate))
                return new Driver(id, personId, createdByUserId, createdDate);
            else return null;
        }
    }
}

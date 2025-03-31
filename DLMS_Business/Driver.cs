using DLMS_DataAccess.Person;
using System;

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

        public bool AddNewDriver()
        {
            this.ID = DriverData.AddNewDriver(PersonId, CreatedByUserId, CreatedDate);
            return this.ID != -1;
        }
    }
}

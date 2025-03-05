using System;
using System.Data;
using DLMS_DataAccess;

namespace DLMS_Business
{
    public class Person
    {
        public enum Mode { AddNew = 0, Update = 1 };
        public Mode CurrentMode = Mode.AddNew;
        public int ID { set; get; }
        public string FirstName { set; get; }
        public string SecondName { set; get; }
        public string ThirdName { set; get; }
        public string LastName { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public string Address { set; get; }
        public DateTime DateOfBirth { set; get; }
        public byte Gender { set; get; }
        public int NationalityCountryID { set; get; }
        public string NationalNo { set; get; }
        public string ImagePath { set; get; }
        public string Country { set; get; }

        public Person()
        {
            this.ID = -1;
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.NationalNo = "";
            this.Email = "";
            this.Phone = "";
            this.Address = "";
            this.DateOfBirth = DateTime.Now;
            this.NationalityCountryID = -1;
            this.Gender = 2;
            this.ImagePath = "";
            CurrentMode = Mode.AddNew;
        }

        private Person(int id, string firstName,
            string secondName, string thirdName, string lastName, string nationalNo, string email,
            string phone, string address, DateTime dateOfBirth, byte gender,
            int nationalityCountryID, string imagePath, string country = "")
        {
            ID = id;
            FirstName = firstName;
            SecondName = secondName;
            ThirdName = thirdName;
            LastName = lastName;
            NationalNo = nationalNo;
            Email = email;
            Phone = phone;
            Address = address;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            NationalityCountryID = nationalityCountryID;
            ImagePath = imagePath;
            Country = country;

            CurrentMode = Mode.Update;
        }

        public static DataTable FilterPeople(string filterMode, string filterkeyWord)
        {
            PersonData.CurrentFilterMode = (PersonData.FilterMode)Enum.Parse(typeof(PersonData.FilterMode), filterMode);
            return PersonData.GetFilteredPeople(filterkeyWord);
        }

        private bool AddNewPerson()
        {
            this.ID = PersonData.AddNewPerson(FirstName, SecondName, ThirdName, LastName, NationalNo,
                DateOfBirth, Gender, Email, Phone, Address,
                NationalityCountryID, ImagePath);
            return (this.ID != -1);
        }

        private bool UpdatePerson()
        {
            return PersonData.UpdatePerson(ID, FirstName, SecondName, ThirdName, LastName, NationalNo,
                DateOfBirth, Gender, Email, Phone, Address,
                NationalityCountryID, ImagePath);
        }

        public static Person Find(int id)
        {
            string firstName = "", secondName = "", thirdName = "", lastName = "", email = "", phone = "", address = "", imagePath = "", nationalNo = "", country = "";
            DateTime dateOfBirth = DateTime.Now;
            byte gender = 0;
            int nationalityCountryID = -1;
            if (PersonData.GetPersonInfoById(id, ref firstName, ref secondName, ref thirdName, ref lastName, ref nationalNo,
                ref dateOfBirth, ref gender, ref email, ref phone, ref address, ref nationalityCountryID, ref imagePath, ref country))
            {
                return new Person(id, firstName, secondName, thirdName, lastName, nationalNo,
                    email, phone, address, dateOfBirth, gender,
                    nationalityCountryID, imagePath, country);
            }
            else
                return null;
        }

        public bool Save()
        {
            switch (CurrentMode)
            {
                case Mode.AddNew:
                    if (AddNewPerson())
                    {
                        CurrentMode = Mode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case Mode.Update:
                    return UpdatePerson();
            }
            return false;
        }

        public static bool DeletePerson(int id)
        {
            return PersonData.DeletePerson(id);
        }

        public static DataTable GetAllPeople()
        {
            return PersonData.GetAllPeople();
        }

        public static bool DoesPersonExist(int id)
        {
            return PersonData.DoesPersonExist(id);
        }

        public static void ApplySorting(string sortingParameter)
        {
            PersonData.IsSortingUsed = true;
            PersonData.SortingText = sortingParameter;
            PersonData.ApplySorting();
        }

        public static void DisableSorting()
        {
            PersonData.IsSortingUsed = false;
        }

        public static int GetAllPeopleCount()
        {
            return PersonData.GetAllPeopleCount();
        }

        public static int GetFilteredPeopleCount(string filterkeyWord)
        {
            return PersonData.GetFilteredPeopleCount(filterkeyWord);
        }

        public static bool DoesPersonExist(string filterParam, string searchKeyWord)
        {
            return PersonData.DoesPersonExist(filterParam, searchKeyWord);
        }
    }
}

using System;
using System.Data;
using DLMS_DataAccess;

namespace DLMS_Business
{
    public class User
    {
        public enum Mode { AddNew = 0, Update = 1 };
        public Mode CurrentMode = Mode.AddNew;
        public int ID { set; get; }
        public int PersonID { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        public bool IsActive { set; get; }

        public User()
        {
            this.ID = -1;
            this.PersonID = -1;
            this.UserName = "";
            this.Password = "";
            this.IsActive = false;

            CurrentMode = Mode.AddNew;
        }

        private User(int id, int personID, string userName, string password, bool isActive)
        {
            ID = id;
            PersonID = personID;
            UserName = userName;
            Password = password;
            IsActive = isActive;

            CurrentMode = Mode.Update;
        }

        private bool AddNewUser()
        {
            this.ID = UserData.AddNewUser(PersonID, UserName, Password, IsActive);
            return this.ID != -1;
        }

        private bool UpdateUser()
        {
            return UserData.UpdateUser(ID, PersonID, UserName, Password, IsActive);
        }

        public static User Find(int id)
        {
            int personID = -1;
            string userName = "", password = "";
            bool isActive = false;
            if (UserData.GetUserDataById(id, ref userName, ref password, ref isActive, ref personID))
                return new User(id, personID, userName, password, isActive);
            else
                return null;
        }

        public bool Save()
        {
            switch (CurrentMode)
            {
                case Mode.AddNew:
                    if (AddNewUser())
                    {
                        CurrentMode = Mode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case Mode.Update:
                    return UpdateUser();
            }
            return false;
        }

        public static bool DeleteUser(int id)
        {
            return UserData.DeleteUser(id);
        }

        public static DataTable GetAllUsers()
        {
            return UserData.GetAllUsers();
        }

        public static bool DoesUserExist(int id)
        {
            return UserData.DoesUserExist(id);
        }

        public static DataTable FilterUsers(string filterMode, string filterKeyWord)
        {
            UserData.CurrentFilterMode = (UserData.FilterMode)Enum.Parse(typeof(UserData.FilterMode), filterMode);
            return UserData.GetFilteredUsers(filterKeyWord);
        }

        public static int GetAllUsersCount()
        {
            return UserData.GetAllUsersCount();
        }

        public static int GetFilteredUsersCount(string filterkeyWord)
        {
            return UserData.GetFilteredUsersCount(filterkeyWord);
        }

        public static void ApplySorting(string sortingParameter)
        {
            UserData.IsSortingUsed = true;
            UserData.SortingText = sortingParameter;
            UserData.ApplySorting();
        }

        public static void DisableSorting()
        {
            UserData.IsSortingUsed = false;
        }
    }
}

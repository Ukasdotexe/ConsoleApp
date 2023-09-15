using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_System.Data_Access_Layer;


namespace DVLD_System.Business_Layer
{
    class clsUser
    {
        enum enMode { AddNew,Update}
        enMode _Mode;

        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public byte IsActive { get; set; }

        public clsUser()
        {
            _Mode = enMode.AddNew;
            UserID = -1;
            PersonID = -1;
            Username = "";
            Password = "";
            IsActive = 0;
        }

        private clsUser(int userID, int personID, string username, string password, byte isActive)
        {
            _Mode = enMode.Update;
            UserID   = userID;
            PersonID = personID;
            Username = username;
            Password = password;
            IsActive = isActive;
        }

        public static clsUser Find(int UserID)
        {
            int PersonID = -1;
            string Username = "", Password = "";
            byte IsActive = 0;

            if(UserDataLayer.GetUserInfoByUserID(UserID,ref PersonID, ref Username, ref Password,
                ref IsActive))
            {
                return new clsUser(UserID, PersonID, Username, Password, IsActive);
            }
            return null;
        }

        public static clsUser Find(string username)
        {
            int PersonID = -1, UserID = -1;
            string Password = "";
            byte IsActive = 0;

            if (UserDataLayer.GetUserInfoByUsername(ref UserID, ref PersonID, username,
                ref Password,ref IsActive))
            {
                return new clsUser(UserID, PersonID, username, Password, IsActive);
            }
            return null;
        }

        private bool _UpdateUser()
        {
            return UserDataLayer.UpdateUser(UserID, PersonID, Username, Password, IsActive);
        }

        private bool _AddNewUser()
        {
            return UserDataLayer.AddNewUser(PersonID, Username, Password, IsActive);
        }

        public static bool DeleteUser(int userID)
        {
            return UserDataLayer.DeleteUser(userID);
        }

        public static DataTable GetUsersList()
        {
            return UserDataLayer.GetUsersList();
        }

        public static bool IsExist(int userID)
        {
            return UserDataLayer.IsExist(userID);
        }

        public static bool IsExist(string username)
        {
            return UserDataLayer.IsExist(username);
        }

        public static bool IsExistByPersonID(int personID)
        {
            return UserDataLayer.IsExistByPersonID(personID);
        }

        public static bool IsValidPassword(int userID, string password)
        {
            return UserDataLayer.IsValidPassword(userID, password);
        }

        public static bool UpdatePassword(int userID, string NewPassword)
        {
            return UserDataLayer.UpdatePassword(userID, NewPassword);
        }
        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewUser())
                        {
                            _Mode = enMode.AddNew;
                            return true;
                        }
                        return false;

                    }
                case enMode.Update:
                    {
                        return _UpdateUser();
                    }
            }
            return false;
        }

    }
}

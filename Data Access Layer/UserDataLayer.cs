using clsdbClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_System.Data_Access_Layer
{
    class UserDataLayer
    {
        public static bool GetUserInfoByUserID(int userID, ref int personID,ref string username,
            ref string password,ref byte isActive)
        {
            bool isFound = false;

            string query = @"SELECT * FROM [Users] WHERE UserID=@UserID";

            clsdb.command = new SqlCommand(query, clsdb.connection);
            clsdb.AddWithValue("@UserID", userID);


            try
            {
                clsdb.OpenConnection();
                clsdb.reader = clsdb.command.ExecuteReader();
                if (clsdb.reader.Read())
                {
                    isFound = true;
                    personID = clsdb.DataBindings<int>("PersonID");
                    username = clsdb.DataBindings<string>("UserName");
                    password = clsdb.DataBindings<string>("Password");
                    isActive = Convert.ToByte(clsdb.reader["IsActive"]);
                }

                clsdb.ResetDataReader();
            }
            catch { }
            finally { clsdb.CloseConnection();clsdb.ResetSqlCommand(); }

            return isFound;
        }
        public static bool GetUserInfoByUsername(ref int userID, ref int personID,
            string username,ref string password, ref byte isActive)
        {
            bool isFound = false;

            string query = @"SELECT * FROM [Users] WHERE UserName=@UserName";

            clsdb.command = new SqlCommand(query, clsdb.connection);
            clsdb.AddWithValue("@UserName", username);


            try
            {
                clsdb.OpenConnection();
                clsdb.reader = clsdb.command.ExecuteReader();
                if (clsdb.reader.Read())
                {
                    isFound  = true;
                    userID   = clsdb.DataBindings<int>("UserID");
                    personID = clsdb.DataBindings<int>("PersonID");
                    password = clsdb.DataBindings<string>("Password");
                    isActive = Convert.ToByte(clsdb.reader["IsActive"]);
                }

                clsdb.ResetDataReader();
            }
            catch { }
            finally { clsdb.CloseConnection(); clsdb.ResetSqlCommand(); }

            return isFound;
        }
        public static bool AddNewUser( int personID, string username, string password, byte isActive)
        {
            byte rowsAffected = 0;

            string query = @"
                          INSERT INTO [dbo].[USERS]
                                     ([UserID]
                                     ,[PersonID]
                                     ,[UserName]
                                     ,[Password]
                                     ,[IsActive])
                               VALUES
                                     (@UserID
                                     ,@PersonID
                                     ,@UserName
                                     ,@Password
                                     ,@IsActive)
                          ";

            clsdb.command = new SqlCommand(query, clsdb.connection);

            clsdb.AddWithValue("@UserID",clsdb.AutoID("Users"));
            clsdb.AddWithValue("@PersonID",personID);
            clsdb.AddWithValue("@UserName",username);
            clsdb.AddWithValue("@Password",password);
            clsdb.AddWithValue("@IsActive",isActive);

            try
            {
                clsdb.OpenConnection();
                rowsAffected = Convert.ToByte(clsdb.command.ExecuteNonQuery());

            }
            catch { }
            finally { clsdb.ResetSqlCommand();clsdb.ResetSqlCommand(); }

            return rowsAffected > 0;
        }
        public static bool UpdateUser(int userID,int personID,string username,string password,
            byte isActive)
        {
            byte rowsAffected = 0;

            string query = @"
                             UPDATE [dbo].[USERS]
                                SET [PersonID]=@PersonID
                                   ,[UserName]=@UserName
                                   ,[Password]=@Password
                                   ,[IsActive]=@IsActive
                              WHERE UserID=@UserID
                             ";

            clsdb.command = new SqlCommand(query, clsdb.connection);

            clsdb.AddWithValue("@UserID"  ,userID);
            clsdb.AddWithValue("@PersonID",personID);
            clsdb.AddWithValue("@UserName",username);
            clsdb.AddWithValue("@Password",password);
            clsdb.AddWithValue("@IsActive",isActive);

            try
            {
                clsdb.OpenConnection();
                rowsAffected = Convert.ToByte(clsdb.command.ExecuteNonQuery());


            }
            catch { }
            finally { clsdb.CloseConnection();clsdb.ResetSqlCommand(); }
            return rowsAffected > 0;
        }

        public static bool DeleteUser(int userID)
        {
            int rowsAffected = -1;

            string query = @"DELETE FROM [dbo].[USERS] WHERE UserID=@UserID";

            clsdb.command = new SqlCommand(query, clsdb.connection);

            clsdb.AddWithValue("@UserID", userID);

            try
            {
                clsdb.OpenConnection();
                rowsAffected = clsdb.command.ExecuteNonQuery();
            }
            catch { }
            finally { clsdb.CloseConnection(); clsdb.ResetSqlCommand(); }
            return rowsAffected > 0;
        }
        public static DataTable GetUsersList()
        {
            return clsdb.GetRecordsList("UsersDetails");
        }
        public static bool IsExist(int userID)
        {
            return clsdb.IsExist("Users", "UserID", userID);
        }
        public static bool IsExist(string username)
        {
            return clsdb.IsExist("Users", "UserName", username);
        }

        public static bool IsExistByPersonID(int personID)
        {
            return clsdb.IsExist<int>("Users", "PersonID", personID);
        }
        public static bool IsValidPassword(int userID,string password)
        {
            bool isValid = false;
            string query = "SELECT Password FROM [Users] WHERE UserID=@UserID";

            clsdb.command = new SqlCommand(query, clsdb.connection);

            clsdb.AddWithValue("@UserID", userID);

            try
            {
                clsdb.OpenConnection();
                clsdb.reader = clsdb.command.ExecuteReader();
                if(clsdb.reader.Read())
                {
                    isValid = (clsdb.DataBindings<string>("Password") == password) ? true : false;
                }
                clsdb.ResetDataReader();
            }
            catch { }
            finally { clsdb.CloseConnection();clsdb.ResetSqlCommand(); }

            return isValid;
        }

        public static bool UpdatePassword(int userID,string NewPassword)
        {
            int rowsAffected = -1;
            string query = @"UPDATE [USERS] SET Password=@Password WHERE UserID=@UserID";

            clsdb.command = new SqlCommand(query, clsdb.connection);

            clsdb.AddWithValue("@Password", NewPassword);
            clsdb.AddWithValue("@UserID", userID);

            try
            {
                clsdb.OpenConnection();
                rowsAffected = clsdb.command.ExecuteNonQuery();
            }
            catch { }
            finally { clsdb.CloseConnection();clsdb.ResetSqlCommand(); }

            return rowsAffected > 0;
        }

       

    }
}

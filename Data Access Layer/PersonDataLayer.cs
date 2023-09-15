using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using clsdbClassLibrary;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace DVLD_System.Data_Access_Layer
{
    class PersonDataLayer
    {
        public static bool GetPersonInfoByID(int PersonID, ref string NationalNo,
            ref string FirstName,ref string SecondName,ref string ThirdName,ref string LastName,
            ref byte Gender, ref DateTime DateOfBirth,ref string Address,ref string Phone,
            ref string Email, ref int NationalityID,ref string ImagePath)
        {
            bool isFound = false;

            string query = "SELECT * FROM [People] WHERE PersonID=@PersonID";

            clsdb.command = new SqlCommand(query, clsdb.connection);

            clsdb.AddWithValue("@PersonID", PersonID);

            try
            {
                clsdb.OpenConnection();
                clsdb.reader = clsdb.command.ExecuteReader();

                if (clsdb.reader.Read())
                {
                    isFound = true;

                    NationalNo    = clsdb.DataBindings<string>("NationalNo");
                    FirstName     = clsdb.DataBindings<string>("FirstName");
                    SecondName    = clsdb.DataBindings<string>("SecondName");
                    LastName      = clsdb.DataBindings<string>("LastName");
                    Address       = clsdb.DataBindings<string>("Address");
                    Phone         = clsdb.DataBindings<string>("Phone");
                    DateOfBirth   = clsdb.DataBindings<DateTime>("DateOfBirth");
                    NationalityID = clsdb.DataBindings<int>("NationalityID");
                    Gender        = clsdb.DataBindings<byte>("Gender");

                    //Fields that Allow Null
                    clsdb.DataBindings(clsdb.reader["Email"], ref Email);
                    clsdb.DataBindings(clsdb.reader["ImagePath"], ref ImagePath);
                    clsdb.DataBindings(clsdb.reader["ThirdName"], ref ThirdName);

                }

                clsdb.ResetDataReader();
            }
            catch { }
            finally
            {
                clsdb.CloseConnection();
                clsdb.ResetSqlCommand();
            }

            return isFound;
        }

        public static bool GetPersonInfoByNationalNo(ref int PersonID, string NationalNo,
            ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName,
            ref byte Gender, ref DateTime DateOfBirth, ref string Address, ref string Phone,
            ref string Email, ref int NationalityID, ref string ImagePath)
        {
            bool isFound = false;

            string query = "SELECT * FROM [People] WHERE NationalNo=@NationalNo";

            clsdb.command = new SqlCommand(query, clsdb.connection);

            clsdb.AddWithValue("@NationalNo", NationalNo);

            try
            {
                clsdb.OpenConnection();
                clsdb.reader = clsdb.command.ExecuteReader();

                if (clsdb.reader.Read())
                {
                    isFound = true;

                    PersonID      = clsdb.DataBindings<int>("PersonID");
                    FirstName     = clsdb.DataBindings<string>("FirstName");
                    SecondName    = clsdb.DataBindings<string>("SecondName");
                    LastName      = clsdb.DataBindings<string>("LastName");
                    Address       = clsdb.DataBindings<string>("Address");
                    Phone         = clsdb.DataBindings<string>("Phone");
                    DateOfBirth   = clsdb.DataBindings<DateTime>("DateOfBirth");
                    NationalityID = clsdb.DataBindings<int>("NationalityID");
                    Gender        = clsdb.DataBindings<byte>("Gender");
                    

                    //Fields that Allow Null
                    clsdb.DataBindings(clsdb.reader["Email"], ref Email);
                    clsdb.DataBindings(clsdb.reader["ImagePath"], ref ImagePath);
                    clsdb.DataBindings(clsdb.reader["ThirdName"], ref ThirdName);

                }

                clsdb.ResetDataReader();
            }
            catch { }
            finally
            {
                clsdb.CloseConnection();
                clsdb.ResetSqlCommand();
            }

            return isFound;
        }
        public static bool AddNewPeson( string NationalNo, string FirstName, string SecondName,
            string ThirdName, string LastName, byte Gender, DateTime DateOfBirth, string Address,
            string Phone, string Email, int NationalityID, string ImagePath)
        {

            int rowsAffected = 0;
            string query = @"INSERT INTO [dbo].[People]
           ([PersonID]
           ,[NationalNo]
           ,[FirstName]
           ,[SecondName]
           ,[ThirdName]
           ,[LastName]
           ,[Gender]
           ,[DateOfBirth]
           ,[Address]
           ,[Phone]
           ,[Email]
           ,[NationalityID]
           ,[ImagePath])
     VALUES
           (@PersonID
           ,@NationalNo
           ,@FirstName
           ,@SecondName
           ,@ThirdName
           ,@LastName
           ,@Gender
           ,@DateOfBirth
           ,@Address
           ,@Phone
           ,@Email
           ,@NationalityID
           ,@ImagePath);
            SELECT SCOPE_IDENTITY()";


            clsdb.command = new SqlCommand(query, clsdb.connection);

            clsdb.AddWithValue("@PersonID", clsdb.AutoID("People"));
            clsdb.AddWithValue("@NationalNo", NationalNo);
            clsdb.AddWithValue("@FirstName", FirstName);
            clsdb.AddWithValue("@SecondName", SecondName);
            clsdb.AddWithValue("@ThirdName", ThirdName,true);
            clsdb.AddWithValue("@LastName", LastName);
            clsdb.AddWithValue("@Gender", Gender);
            clsdb.AddWithValue("@DateOfBirth", DateOfBirth);
            clsdb.AddWithValue("@Address", Address);
            clsdb.AddWithValue("@Phone", Phone);
            clsdb.AddWithValue("@Email", Email,true);
            clsdb.AddWithValue("@NationalityID", NationalityID);
            clsdb.AddWithValue("@ImagePath", ImagePath,true);

            try
            {
                clsdb.OpenConnection();
                rowsAffected = clsdb.command.ExecuteNonQuery();

            }
            catch { }
            finally
            {
                clsdb.CloseConnection();
                clsdb.ResetSqlCommand();
            }


            return rowsAffected > 0;
        }

        public static bool DeletePerson(int personID)
        {
            int rowsAffected = -1;

            string query = @"DELETE FROM [People] WHERE PersonID=@PersonID";

            clsdb.command = new SqlCommand(query, clsdb.connection);

            clsdb.AddWithValue("@PersonID", personID);

            try
            {
                clsdb.OpenConnection();
                rowsAffected = clsdb.command.ExecuteNonQuery();
            }
            catch { }
            finally { clsdb.CloseConnection();clsdb.ResetSqlCommand(); }

            return rowsAffected > 0;

        }

        public static bool UpdatePerson(int PersonID, string NationalNo, string FirstName, string SecondName,
            string ThirdName, string LastName, byte Gender, DateTime DateOfBirth, string Address,
            string Phone, string Email, int NationalityID, string ImagePath)
        {
            int Affectedrows = -1;

            string query = @"UPDATE [dbo].[People]
                      SET      [NationalNo]    = @NationalNo
                              ,[FirstName]     = @FirstName
                              ,[SecondName]    = @SecondName
                              ,[ThirdName]     = @ThirdName
                              ,[LastName]      = @LastName
                              ,[Gender]        = @Gender
                              ,[DateOfBirth]   = @DateOfBirth
                              ,[Address]       = @Address
                              ,[Phone]         = @Phone
                              ,[Email]         = @Email
                              ,[NationalityID] = @NationalityID
                              ,[ImagePath]     = @ImagePath
                         WHERE [PersonID]      = @PersonID";

            clsdb.command = new SqlCommand(query, clsdb.connection);

            clsdb.AddWithValue("@PersonID",PersonID);
            clsdb.AddWithValue("@NationalNo",NationalNo);
            clsdb.AddWithValue("@FirstName",FirstName);
            clsdb.AddWithValue("@SecondName",SecondName);
            clsdb.AddWithValue("@ThirdName",ThirdName,true);
            clsdb.AddWithValue("@LastName",LastName);
            clsdb.AddWithValue("@Gender",Gender);
            clsdb.AddWithValue("@DateOfBirth",DateOfBirth);
            clsdb.AddWithValue("@Address",Address);
            clsdb.AddWithValue("@Phone",Phone);
            clsdb.AddWithValue("@Email",Email,true);
            clsdb.AddWithValue("@NationalityID",NationalityID);
            clsdb.AddWithValue("@ImagePath",ImagePath,true);

            try
            {
                clsdb.OpenConnection();
                Affectedrows = clsdb.command.ExecuteNonQuery();
            }
            catch { }
            finally
            {
                clsdb.CloseConnection();
                clsdb.ResetSqlCommand();
            }


            return Affectedrows>0;
        }
        
        public static bool IsExist(string NationalNo)
        {
            return clsdb.IsExist("People", "NationalNo", NationalNo);
        }
        public static bool IsExist(int personID)
        {
            return clsdb.IsExist("People", "PersonID", personID);
        }
        public static DataTable GetPeople()
        {
            return clsdb.GetRecordsList("PeopleDetails");
        }

        public static void PopulateList(ref ComboBox cmbPersons)
        {
            clsdb.PopulateList(cmbPersons, "ComboPeople", "FullName", "PersonID");
        }



    }
}

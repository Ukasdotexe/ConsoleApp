using clsdbClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_System.Data_Access_Layer
{
    class CountryDataLayer
    {
        public static bool GetCountryInfoByName(ref int countryID, string name)
        {
            bool isFound = false;

            string query = "SELECT * FROM [Countries] WHERE Name=@Name";

            clsdb.command = new SqlCommand(query, clsdb.connection);
            clsdb.AddWithValue("@Name", name);

            try
            {
                clsdb.OpenConnection();
                clsdb.reader = clsdb.command.ExecuteReader();
                if (clsdb.reader.Read())
                {
                    isFound = true;
                    countryID = clsdb.DataBindings<int>("CountryID");
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

        public static DataTable GetListCountries()
        {
            return clsdb.GetRecordsList("Countries");
        }

        public static bool GetCountryInfoByID(int countryID, ref string name)
        {
            bool isFound = false;

            string query = "SELECT * FROM [Countries] WHERE CountryID=@CountryID";

            clsdb.command = new SqlCommand(query, clsdb.connection);
            clsdb.AddWithValue("@CountryID", countryID);

            try
            {
                clsdb.OpenConnection();
                clsdb.reader = clsdb.command.ExecuteReader();
                if (clsdb.reader.Read())
                {
                    isFound = true;
                    name = clsdb.DataBindings<string>("Name");
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

        public static void PopulateList(ref ComboBox cmbContries)
        {
            clsdb.PopulateList(cmbContries, "Countries", "Name", "CountryID");
        }
    }
}

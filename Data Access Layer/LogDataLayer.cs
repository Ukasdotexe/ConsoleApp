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
    class LogDataLayer
    {

        public static int AddNewLog(DateTime dateTime, int userID, string ipAdress)
        {
            int LogID = -1;

            string query = @"
                           INSERT INTO [dbo].[AuditTrails]
                                      ([LogID]
                                      ,[DateTime]
                                      ,[UserID]
                                      ,[IPAdress])
                                VALUES
                                      (@LogID
                                      ,@DateTime
                                      ,@UserID
                                      ,@IPAddress);
                           SELECT SCOPE_IDENTITY();
                           ";

            clsdb.command = new SqlCommand(query, clsdb.connection);
            clsdb.AddWithValue("@LogID", clsdb.AutoID("AuditTrails"));
            clsdb.AddWithValue("@DateTime", dateTime);
            clsdb.AddWithValue("@UserID", userID);
            clsdb.AddWithValue("@IPAddress", ipAdress);

            try
            {
                clsdb.OpenConnection();
                string res = clsdb.command.ExecuteScalar().ToString();

                if (res != null)
                    int.TryParse(res, out LogID);

            }
            catch { }
            finally
            {
                clsdb.CloseConnection();
                clsdb.ResetSqlCommand();
            }
            return LogID;
        }

        public static DataTable GetLogsList()
        {
           return clsdb.GetRecordsList("AuditTrailsDetails");
        }



    }
}

using DVLD_System.Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_System.Business_Layer
{
    class clsLog
    {
        public  int LogID { get; private set; }
        public DateTime DateTime { get; set; }
        public int UserID { get; set; }
        public string IpAdress { get; set; }

        public clsLog()
        {
            LogID = -1;
            UserID = -1;
            DateTime = DateTime.Now;
            IpAdress = "";
        }

        public bool AddNewLog()
        {
            this.LogID = LogDataLayer.AddNewLog(DateTime, UserID, IpAdress);

            return (LogID != -1);
        }

        public static DataTable GetLogsList()
        {
            return LogDataLayer.GetLogsList();
        }
    }
}

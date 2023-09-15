using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows.Forms;
using clsdbClassLibrary;
using DVLD_System.AuditTrails;
using DVLD_System.Business_Layer;
using DVLD_System.Users;

namespace DVLD_System
{
    public partial class MainForm : Form
    {
        [Obsolete]
        public MainForm()
        {
            InitializeComponent();
            Load += _AddNewLog;
        }
        clsLog _NewLog = new clsLog();

        [Obsolete]
        private void _AddNewLog(object sender, EventArgs e)
        {
            _NewLog.DateTime = DateTime.Now;
            _NewLog.UserID = clsGlobal.User.UserID;
            _NewLog.IpAdress = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();

            if (!_NewLog.AddNewLog()) MessageBox.Show("Log Error Occured!!");
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsdb.ShowForm(new FrmPeople(), this);
        }




        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsdb.ShowForm(new FrmUsers(), this);
        }

        private void auditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsdb.ShowForm(new FrmAuditTrails(), this);
        }
    }
}

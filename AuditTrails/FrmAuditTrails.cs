using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using clsdbClassLibrary;
using DVLD_System.Business_Layer;

namespace DVLD_System.AuditTrails
{
    public partial class FrmAuditTrails : Form
    {
        public FrmAuditTrails()
        {
            InitializeComponent();
            Load += _LoadLogsList;
            tbFilter.TextChanged += _IsEmpty;
            btnSearch.Click += _SearchLog;
           
        }

        DataView _dataView;
        private void _LoadLogsList(object sender,EventArgs e)
        {
            _dataView = clsLog.GetLogsList().DefaultView;
            dgvLogs.DataSource = _dataView;
            dgvLogs.ClearSelection();
        }

        private void _IsEmpty(object sender,EventArgs e)
        {
            if (tbFilter.Text == string.Empty)
                _LoadLogsList(sender, e);

        }

        private void _SearchLog(object sender,EventArgs e)
        {
            _dataView = clsLog.GetLogsList().DefaultView;
            switch (comboBox1.Text)
            {
                case "UserName":
                    {
                        _dataView.RowFilter = clsdb.Filter("User", tbFilter);
                        break;
                    }
                case "IP":
                    {
                            _dataView.RowFilter = clsdb.Filter("IP", tbFilter);
                        break;
                    }
            }
            dgvLogs.DataSource = _dataView;

        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_System.Users
{
    public partial class frmUserInfo : Form
    {
        public frmUserInfo(int UserID,int PersonID)
        {
            InitializeComponent();

            if (PersonID>0)
            {
                ctrlPersonInfo1.LoadPersonInfo(PersonID);
                ctrlUserInfo1.LoadUserInfo(UserID);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

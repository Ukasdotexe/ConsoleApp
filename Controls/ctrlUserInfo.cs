using DVLD_System.Business_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_System
{
    public partial class ctrlUserInfo : UserControl
    {
        public ctrlUserInfo()
        {
            InitializeComponent();
        }

        public  void  LoadUserInfo(int UserID)
        {
            if (clsUser.IsExist(UserID))
            {
                clsUser User = clsUser.Find(UserID);
                _FillUserInfo(User);
            }
        }

        private void _FillUserInfo(clsUser User)
        {
            lblUserID.Text   = User.UserID.ToString();
            lblUsername.Text = User.Username;
            lblIsActive.Text = (User.IsActive == 1) ? "Yes" : "NO";

        }
    }
}

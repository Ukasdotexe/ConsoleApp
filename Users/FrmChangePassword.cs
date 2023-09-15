using DVLD_System.Business_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_System.Users
{
    public partial class FrmChangePassword : Form
    {

        private int _UserID;
        private int _PersonID;
        public FrmChangePassword(int UserID,int PersonID)
        {
            InitializeComponent();
            if (UserID > 0)
            {
                _UserID   = UserID;
                _PersonID = PersonID;
                ctrlPersonInfo1.LoadPersonInfo(_PersonID);
                ctrlUserInfo1.LoadUserInfo(_UserID);
            }
        }

        private string _Message;
        Stopwatch sp = new Stopwatch();

        private void _IsActive(bool Status)
        {
            lblError.Visible = !Status;
            lblSuccess.Visible = Status;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
             if(string.IsNullOrWhiteSpace(tbCurrentPassword.Text)||
                string.IsNullOrWhiteSpace(tbConfirmPassword.Text)||
                string.IsNullOrWhiteSpace(tbNewPassword.Text))
            {
                _Message = "One Of the fields is empty!!";
                   if (!backgroundWorker1.IsBusy)
                    backgroundWorker1.RunWorkerAsync();
                return;
            }

            if (!clsUser.IsValidPassword(_UserID, tbCurrentPassword.Text))
            {
                _Message = "This account's password does not match the current password!!!";
                if (!backgroundWorker1.IsBusy)
                    backgroundWorker1.RunWorkerAsync();
                return;
            }

            if (tbNewPassword.Text != tbConfirmPassword.Text)
            {
                _Message = "Password Confirmation is not correct !!!";
                if (!backgroundWorker1.IsBusy)
                    backgroundWorker1.RunWorkerAsync();
                return;
            }

            if (clsUser.UpdatePassword(_UserID, tbNewPassword.Text))
            {
                _IsActive(true);
                lblSuccess.Text = "Password Changed with success :)";
            }
            else
            {
                _IsActive(true);
                lblError.Text = "Error Occured while Updating the password!!";
            }



        }

        delegate void MyDelegate(Label lbl, string Message);
        private void _ChangeLabelText(Label lbl,string Message)
        {
            if (lbl.Text == "")
                lbl.Text = Message;
            else
                lbl.Text = "";
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            MyDelegate delegateInstance = new MyDelegate(_ChangeLabelText);

            Invoke(delegateInstance,lblError, _Message);

            sp.Start();

            while (sp.Elapsed.TotalSeconds < 3) { }
            
            sp.Stop();
            sp.Reset();

            Invoke(delegateInstance,lblError, _Message);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

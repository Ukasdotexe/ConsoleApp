using DVLD_System.Business_Layer;
using DVLD_System.People;
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
    public partial class FrmAddNewUser : Form
    {
        public FrmAddNewUser()
        {
            InitializeComponent();
            _Mode=enMode.AddNew;
        }

        public FrmAddNewUser(int userID, int personID=-1)
        {
            InitializeComponent();
            if (userID != -1)
            {
                _Mode = enMode.Update;
                _userID = userID;
                _personID = personID;
            }
        }


        enum enMode { AddNew,Update}
        enMode _Mode;

        private string _Message;
        private int _personID = -1;
        private clsUser _NewUser;
        private int _userID;

        delegate void ErrorMessage(Label label,string Message);

        private void _GetUserInfo()
        {
            _NewUser.PersonID = _personID;
            _NewUser.Username = txtUsername.Text;
            _NewUser.Password = txtPassword.Text;
            _NewUser.IsActive = Convert.ToByte((cbIsActive.Checked) ? 1 : 0);
        }

        private bool _IsValidUsername()
        {
            if (clsUser.IsExist(txtUsername.Text))
            {
                if (_Mode == enMode.Update)
                {
                    if (txtUsername.Text == _NewUser.Username)
                        return true;
                    else
                        return false;
                }
                else
                    return false;

            }
            return true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if( string.IsNullOrWhiteSpace(txtUsername.Text)||
                string.IsNullOrWhiteSpace(txtPassword.Text)||
                string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {
                _Message = "One of the fields is empty!!";
                if (!backgroundWorker1.IsBusy)
                    backgroundWorker1.RunWorkerAsync();
                return;
            }

            if (!_IsValidUsername())
            {
                _Message = "Username already exist in database!!";
                if (!backgroundWorker1.IsBusy)
                    backgroundWorker1.RunWorkerAsync();
                return;
            }
            if (txtPassword.Text!=txtConfirmPassword.Text)
            {
                _Message = "Incorrect password confirmation!!";
                if (!backgroundWorker1.IsBusy)
                    backgroundWorker1.RunWorkerAsync();
                return;
            }

            if(_personID==-1)
            {
                _Message = "Please select a person !!";
                if(!backgroundWorker1.IsBusy)
                    backgroundWorker1.RunWorkerAsync();
                return;
            }

            _GetUserInfo();

            if (_NewUser.Save())
            {
                _Mode = enMode.Update;
                lblError.Text = string.Empty;
                lblSuccess.Text = "Record Saved Succesfuly!";
            }
            else
            {
                lblSuccess.Text = string.Empty;
                lblError.Text = "Error Occured While Saving the record!!!";
            }
        }
        private void _ChangeFormTitle()
        {
            lblTitle.Text = (_Mode == enMode.AddNew) ? "Add New User" : "Update User";
        }
        private void _DisplayErrorMessage(Label label,string Message)
        {
            label.Text = (label.Text == string.Empty) ? Message : string.Empty;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            tabcontrol1.SelectedTab = tpLoginInfo;
        }
        private void _FillPersonInfo(int PersonID)
        {
            _personID = PersonID;
        }
        private void btnSelectPerson_Click(object sender, EventArgs e)
        {
            FrmFindPerson frm = new FrmFindPerson();
            frm.DataBack += _FillPersonInfo;
            frm.ShowDialog();

            ctrlPersonInfo1.LoadPersonInfo(_personID);
            
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            ErrorMessage Message = new ErrorMessage(_DisplayErrorMessage);

            Stopwatch sw = new Stopwatch();
           
            Invoke(Message, lblError, _Message);

            sw.Start();
            while (sw.Elapsed.TotalSeconds < 3) { }
            sw.Stop();

            Invoke(Message, lblError, _Message);



        }

        private void _DisplayUserInfo(clsUser User)
        {
            ctrlPersonInfo1.LoadPersonInfo(User.PersonID);

            lblUserID.Text     = User.UserID.ToString();
            txtUsername.Text   = User.Username;
            txtPassword.Text   = User.Password;
            cbIsActive.Checked = (User.IsActive == 1) ? true : false;
        }

        private void FrmAddNewUser_Load(object sender, EventArgs e)
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    {
                        _NewUser = new clsUser();
                        break;
                    }
                case enMode.Update:
                    {
                        _NewUser = clsUser.Find(_userID);
                        _DisplayUserInfo(_NewUser);
                        break;
                    }
            }
            _ChangeFormTitle();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            tabcontrol1.SelectedTab = tpPersonalInfo;
        }
    }
}

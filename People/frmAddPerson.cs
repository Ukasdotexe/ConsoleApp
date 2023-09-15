using System;
using System.Windows.Forms;
using clsdbClassLibrary;
using ClearClassLibrary;
using DVLD_System.Business_Layer;

namespace DVLD_System.People
{
    public partial class frmAddPerson : Form
    {
        public enum enMode { AddNew, Update }
        private enMode _Mode;
        int _PersonID;
        clsPerson _Person;

        private void _SetDefaultImage()
        {
            pictureBox1.ImageLocation = (rbMale.Checked) ? clsGlobal.Male : clsGlobal.Female;
        }

        public frmAddPerson(int PersonID)
        {
            InitializeComponent();
            if (PersonID != -1)
            {
                _Mode = enMode.Update;
                _PersonID = PersonID;
            }
            else
                _Mode = enMode.AddNew;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();

            if (OFD.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = OFD.FileName;
            }
        }

        private void _GetPersonInfo()
        {
            _Person.NationalNo = tbNationalityNo.Text;
            _Person.FirstName = tbFirstName.Text;
            _Person.SecondName = tbSecondName.Text;
            _Person.ThirdName = tbThirdName.Text;
            _Person.LastName = tbLastName.Text;
            _Person.DateOfBirth = dtpBirthDate.Value;
            _Person.Gender = Convert.ToByte((rbMale.Checked) ? 0 : 1);
            _Person.Address = tbAddress.Text;
            _Person.Email = tbEmail.Text;
            _Person.Phone = tbPhone.Text;
            _Person.NationalityID = (int)cmbCountries.SelectedValue;

            if (!string.IsNullOrWhiteSpace(pictureBox1.ImageLocation))
                _Person.ImagePath = pictureBox1.ImageLocation;
            else
                _Person.ImagePath = "";
        }

        private bool _IsValidNationalNo()
        {
            if (clsPerson.IsExist(tbNationalityNo.Text))
            {
                if (_Mode == enMode.Update)
                {
                    if (tbNationalityNo.Text == _Person.NationalNo)
                        return true;
                }
                return false;
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_IsValidNationalNo())
            {
                lblAddSuccessfuly.Text = string.Empty;
                lblError.Text = "National No Already exist in database";
                return;
            }

            _GetPersonInfo();

            if (_Person.Save())
            {
                _Mode = enMode.Update;
                lblError.Text = "";
                lblAddSuccessfuly.Text = "Saved";
            }
            else
            {
                lblAddSuccessfuly.Text = "";
                lblError.Text = "Error Occured While Inserting Date";
            }
                

           

        }
        private void _IsActive(bool Status)
        {
            lblAddSuccessfuly.Visible = Status;
            lblError.Visible = !Status;
            
        }
        private void btnClear_Click(object sender, EventArgs e)
        {   
            pictureBox1.ImageLocation = clsGlobal.Male;

            Clear.ResetTextBox(lblAddSuccessfuly);
            Clear.ResetTextBox(lblError);
            Clear.ResetTextBox(lblPersonID);
            Clear.ResetTextBox(tbAddress);
            Clear.ResetTextBox(tbEmail);
            Clear.ResetTextBox(tbFirstName);
            Clear.ResetTextBox(tbLastName);
            Clear.ResetTextBox(tbNationalityNo);
            Clear.ResetTextBox(tbPhone);
            Clear.ResetTextBox(tbSecondName);
            Clear.ResetTextBox(tbThirdName);
            Clear.ResetComboBox(cmbCountries);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void _ChangeFormTitle()
        {
            if (_Mode == enMode.Update)
                lblPersonID.Text = _Person.PersonID.ToString();
            lblTitle.Text = (_Mode == enMode.AddNew) ? "Add New Person" : "Update Person";
        }
        private void _DisplayPersonInfo()
        {
            btnClear.Enabled = false;
            lblPersonID.Text           = _Person.PersonID.ToString();
            tbAddress.Text             = _Person.Address;
            tbEmail.Text               = _Person.Email;
            tbFirstName.Text           = _Person.FirstName;
            tbLastName.Text            = _Person.LastName;
            tbNationalityNo.Text       = _Person.NationalNo;
            tbPhone.Text               = _Person.Phone;
            tbSecondName.Text          = _Person.SecondName;
            tbThirdName.Text           = _Person.ThirdName;
            dtpBirthDate.Value         = _Person.DateOfBirth;
            cmbCountries.SelectedValue = _Person.NationalityID;

            if (_Person.Gender == 0) rbMale.Checked = true; else rbFemale.Checked = false;

            if(!string.IsNullOrWhiteSpace(_Person.ImagePath))
                pictureBox1.ImageLocation = _Person.ImagePath;
            
               
        }
        private void frmAddPerson_Load(object sender, EventArgs e)
        {
            clsCountry.PopulateList(ref cmbCountries);

            switch (_Mode)
            {
                case enMode.AddNew:
                    {
                        _SetDefaultImage();
                        _Person = new clsPerson();
                        break;
                    }
                case enMode.Update:
                    {
                        _Person = clsPerson.Find(_PersonID);
                        _DisplayPersonInfo();
                        break;
                    }

                    
            }
            _ChangeFormTitle();
        }
        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            _SetDefaultImage();
        }
        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            _SetDefaultImage();
        }
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pictureBox1.ImageLocation = "";
        }
        private void pictureBox1_LoadCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(pictureBox1.ImageLocation))
            {
                lblRemoveImage.Visible = false;
            }
            else
                lblRemoveImage.Visible = true;
        }
    }
}

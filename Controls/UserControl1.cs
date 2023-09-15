using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_System.Business_Layer;

namespace DVLD_System
{
    public partial class ctrlPersonInfo : UserControl
    {
        public ctrlPersonInfo()
        {
            InitializeComponent();
        }

        public void LoadPersonInfo(int PersonID)
        {
            if (clsPerson.IsExist(PersonID))
            {
                clsPerson Person = clsPerson.Find(PersonID);
                _FillPersonInfo(Person);
            }
            else
                Reset();
        }

        
        public  void LoadPersonInfo(string NationalNo)
        {
            if (clsPerson.IsExist(NationalNo))
            {
                clsPerson Person = clsPerson.Find(NationalNo);
                _FillPersonInfo(Person);
            }
            else
                Reset();
        }


        private void Reset()
        {
            lblAddress.Text = "[****]";
            lblCountry.Text = "[****]";
            lblDateOfBirth.Text = "[****]";
            lblEmail.Text = "[****]";
            lblGender.Text = "[****]";
            lblName.Text = "[****]";
            lblNationalNo.Text = "[****]";
            lblPersonID.Text = "[****]";
            lblPhone.Text = "[****]";
        }
        private void _FillPersonInfo(clsPerson Person)
        {
            lblPersonID.Text = Person.PersonID.ToString();
            lblNationalNo.Text = Person.NationalNo;
            lblName.Text = Person.FullName();
            lblGender.Text = (Person.Gender == 0) ? "Male" : "Female";
            lblDateOfBirth.Text = Person.DateOfBirth.ToString("dd-MMMM-yyyy");
            lblAddress.Text = Person.Address;
            lblPhone.Text = Person.Phone;
            lblEmail.Text = Person.Email;
            lblCountry.Text = clsCountry.Find(Person.NationalityID).Name;
            if (Person.ImagePath != "")
                pbProfilePicture.ImageLocation = Person.ImagePath;
        }
    }
}

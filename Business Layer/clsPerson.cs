using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_System.Data_Access_Layer;

namespace DVLD_System.Business_Layer
{
    public class clsPerson
    {

        public enum enMode { AddNew,Update}
        public enMode Mode { get; set; }
        public int PersonID { get; private set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string FullName()
        {
            return FirstName + " " + SecondName + " " + ThirdName + " " + LastName;
        }
        public byte Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int NationalityID { get; set; }
        public string ImagePath { get; set; }

        private clsPerson(int PersonID,string NationalNo,string FirstName,string SecondName,
            string ThirdName,string LastName,byte Gender,DateTime DateOfBirth,string Address,
            string Phone,string Email,int NationalityID,string ImagePath)
        {
            Mode = enMode.Update;
            this.PersonID = PersonID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.Gender = Gender;
            this.DateOfBirth = DateOfBirth;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalityID = NationalityID;
            this.ImagePath = ImagePath;
        }

        public clsPerson()
        {
            Mode = enMode.AddNew;

            this.NationalNo = "";
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.ImagePath = "";
            this.Address = "";
            this.Phone = "";
            this.Email = "";
            this.PersonID = -1;
            this.NationalityID = -1;
            this.Gender = 0;
            this.DateOfBirth = DateTime.Now;
        }
        public static clsPerson Find(int PersonID)
        {
            string NationalNo = "", FirstName = "", SecondName = "", ThirdName = "", LastName = "",
                ImagePath = "", Address = "", Phone = "", Email = "";
            DateTime DateOfBirth = DateTime.Now;
            int NationalityID = -1;
            byte Gender = 0;

            if (PersonDataLayer.GetPersonInfoByID(PersonID,ref NationalNo,ref FirstName,
                ref SecondName,ref ThirdName,ref LastName,ref Gender,ref DateOfBirth,
                ref Address,ref Phone,ref Email,ref NationalityID,ref ImagePath))
            {
                return new clsPerson(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName,
                    Gender, DateOfBirth, Address, Phone, Email, NationalityID, ImagePath);
            }
            return null;
        }

        public static clsPerson Find(string NationalNo)
        {
            string  FirstName = "", SecondName = "", ThirdName = "", LastName = "",
                ImagePath = "", Address = "", Phone = "", Email = "";
            DateTime DateOfBirth = DateTime.Now;
            int PersonID=-1,NationalityID = -1;
            byte Gender = 0;

            if (PersonDataLayer.GetPersonInfoByNationalNo(ref PersonID, NationalNo, ref FirstName,
                ref SecondName, ref ThirdName, ref LastName, ref Gender, ref DateOfBirth,
                ref Address, ref Phone, ref Email, ref NationalityID, ref ImagePath))
            {
                return new clsPerson(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName,
                    Gender, DateOfBirth, Address, Phone, Email, NationalityID, ImagePath);
            }
            return null;
        }

        public static DataTable GetPeople()
        {
            return PersonDataLayer.GetPeople();
        }

        public static bool IsExist(int Value)
        {
            return PersonDataLayer.IsExist(Value);
        }
        public static bool IsExist(string Value)
        {
            return PersonDataLayer.IsExist(Value);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                {
                    if (_AddNewPerson())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;
                }
                case enMode.Update:
                    {
                        return _UpdatePerson();
                    }
            }
            return false;
        }

        private bool _UpdatePerson()
        {
           return PersonDataLayer.UpdatePerson(PersonID, NationalNo, FirstName, SecondName,
               ThirdName,LastName, Gender, DateOfBirth, Address, Phone, Email,
               NationalityID, ImagePath);
        }

        private bool _AddNewPerson()
        {
            return PersonDataLayer.AddNewPeson(NationalNo, FirstName, SecondName, ThirdName,
                LastName, Gender, DateOfBirth, Address, Phone, Email, NationalityID, ImagePath);
        }

        public static bool DeletePerson(int PersonID)
        {
            return PersonDataLayer.DeletePerson(PersonID);
        }

        public static void PopulateList(ref ComboBox cmbPersons)
        {
             PersonDataLayer.PopulateList(ref cmbPersons);
        }
    }
}

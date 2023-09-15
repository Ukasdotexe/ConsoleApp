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
    class clsCountry
    {
        public int CountryID { get; set; }
        public string Name { get; set; }

        private clsCountry(int CountryID,string Name)
        {
            this.CountryID = CountryID;
            this.Name = Name;

        }

        public static clsCountry Find(int CountryID)
        {
            string Name = "";
            if(CountryDataLayer.GetCountryInfoByID(CountryID,ref Name))
            {
                return new clsCountry(CountryID, Name);
            }
            return null;
        }
        public static clsCountry Find(string Name)
        {
            int CountryID = -1;
            if (CountryDataLayer.GetCountryInfoByName(ref CountryID, Name))
            {
                return new clsCountry(CountryID, Name);
            }
            return null;
        }

        public static DataTable GetCountriesList()
        {
            return CountryDataLayer.GetListCountries();
        }
        public static void PopulateList(ref ComboBox cmbCountries)
        {
            CountryDataLayer.PopulateList(ref cmbCountries);
        }
    }
}

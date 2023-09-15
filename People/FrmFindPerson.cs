using DVLD_System.Business_Layer;
using DVLD_System.Users;
using System;
using System.Windows.Forms;

namespace DVLD_System.People
{
    public partial class FrmFindPerson : Form
    {
        public FrmFindPerson()
        {
            InitializeComponent();
            btnSearch.Click += _SearchPerson;
            btnClose.Click  += _Close;
            Load += _Loadings;

        }

        public event Action<int> DataBack;
        private void _Loadings(object sender,EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }
        private void _SearchPerson(object sender,EventArgs e)
        {
            switch (comboBox1.Text)
            {

                case "Person ID":
                    {
                        if (int.TryParse(txtFilter.Text, out int PersonID))
                            ctrlPersonInfo1.LoadPersonInfo(PersonID);
                        break;
                    }
                case "National No":
                    {
                        ctrlPersonInfo1.LoadPersonInfo(txtFilter.Text);
                        break;
                    }
            }
        }
        private void _Close(object sender,EventArgs e)
        {
            Close();
            if (string.IsNullOrWhiteSpace(txtFilter.Text)) return;

            int PersonID = _GetPersonID();

            FrmAddNewUser frm = new FrmAddNewUser();
            frm.Show();

            if (!clsUser.IsExistByPersonID(PersonID))
                Invoke(DataBack, PersonID);
            else
                MessageBox.Show("There is already a user account linked with this person",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private int _GetPersonID()
        {
            if (comboBox1.Text == "Person ID")
                return int.Parse(txtFilter.Text);
            else 
                return clsPerson.Find(txtFilter.Text).PersonID;
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_System.Business_Layer;
using DVLD_System.People;
using clsdbClassLibrary;
using System.Windows.Forms;

namespace DVLD_System
{
    public partial class FrmPeople : Form
    {
        public FrmPeople()
        {
            InitializeComponent();
        }

        DataView _dataView;

        private void _Refrech()
        {
            _dataView = clsPerson.GetPeople().DefaultView;
            dgvPeopleList.DataSource = _dataView;
            dgvPeopleList.ClearSelection();
        }
        private void FrmPeople_Load(object sender, EventArgs e)
        {
            _Refrech();
        }
        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            new frmAddPerson(-1).ShowDialog();

        }
        private void btnReload_Click(object sender, EventArgs e)
        {
            _Refrech();
        }

        private void cmbPeople_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPeople.Text == "None")
                txtFilter.Visible = false;
            else
                txtFilter.Visible = true;
        }
        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if (txtFilter.Text == string.Empty)
            {
                _Refrech();return;
            }

            _SearchPerson();
            
        }
        private void _SearchPerson()
        {
            DataView dv = clsPerson.GetPeople().DefaultView;

            switch (cmbPeople.Text)
            {
                case "Person ID":
                    {
                        if (int.TryParse(txtFilter.Text, out int ID))
                            dv.RowFilter = clsdb.Filter("Person ID", ID);
                        break;
                    }
                case "National No":
                    {
                        dv.RowFilter = clsdb.Filter("National No", txtFilter);
                        break;
                    }
                case "First Name":
                    {
                        dv.RowFilter = clsdb.Filter("First Name", txtFilter);
                        break;
                    }
                case "Second Name":
                    {
                        dv.RowFilter = clsdb.Filter("Second Name", txtFilter);
                        break;
                    }
                case "Third Name":
                    {
                        dv.RowFilter = clsdb.Filter("Third Name", txtFilter);
                        break;
                    }
                case "Last Name":
                    {
                        dv.RowFilter = clsdb.Filter("Last Name", txtFilter);
                        break;
                    }
                case "Country":
                    {
                        dv.RowFilter = clsdb.Filter("Country", txtFilter);
                        break;
                    }
                case "Gender":
                    {
                        dv.RowFilter = clsdb.Filter("Gender", txtFilter);
                        break;
                    }
                case "Phone":
                    {
                        dv.RowFilter = clsdb.Filter("Phone", txtFilter);
                        break;
                    }

                case "Email":
                    {
                        dv.RowFilter = clsdb.Filter("Email", txtFilter);
                        break;
                    }
            }
            dgvPeopleList.DataSource = dv;

        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnAddNewPerson.PerformClick();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmAddPerson(clsdb.GetCellValue<int>(dgvPeopleList, 0)).ShowDialog();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmPersonDetails(clsdb.GetCellValue<int>(dgvPeopleList,0)).ShowDialog();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to delete this Record??","Delete",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                if (clsPerson.DeletePerson(clsdb.GetCellValue<int>(dgvPeopleList, 0)))
                {
                    MessageBox.Show("Record Delete Succesfuly!!");

                }
                else
                    MessageBox.Show("Error deleting this Record");
            }
        }
    }
}

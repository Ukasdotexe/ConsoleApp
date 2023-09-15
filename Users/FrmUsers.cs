using clsdbClassLibrary;
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

namespace DVLD_System.Users
{
    public partial class FrmUsers : Form
    {
        public FrmUsers()
        {
            InitializeComponent();
        }

        DataView _dataView;

        private void _Referech()
        {
            _dataView = clsUser.GetUsersList().DefaultView;
            dgvUsersList.DataSource = _dataView;
            
        }
        private void FrmUsers_Load(object sender, EventArgs e)
        {
            _Referech();
        }

        private void cmbPeople_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbUsers.Text == "None")
                txtFilter.Visible = false;
            else
                txtFilter.Visible = true;
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtFilter.Text))
            {
                _Referech();return;
            }

            _SearchUser();

        }

        private void _SearchUser()
        {
            _dataView = clsUser.GetUsersList().DefaultView;

            switch (cmbUsers.Text)
            {
                case "User ID":
                    {
                        if (int.TryParse(txtFilter.Text, out int UserID))
                            _dataView.RowFilter = clsdb.Filter("User ID", UserID);
                        break;
                    }
                case "Full Name":
                    {
                        _dataView.RowFilter = clsdb.Filter("Full Name", txtFilter);
                        break;
                    }
                case "UserName":
                    {
                        _dataView.RowFilter = clsdb.Filter("UserName", txtFilter);
                        break;
                    }
            }

            dgvUsersList.DataSource = _dataView;
        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            new FrmAddNewUser().ShowDialog();
        }

        private void showUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmUserInfo(clsdb.GetCellValue<int>(dgvUsersList, 0),
                            clsdb.GetCellValue<int>(dgvUsersList, 1)).ShowDialog();
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnAddNewUser.PerformClick();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmChangePassword(clsdb.GetCellValue<int>(dgvUsersList, 0),
                            clsdb.GetCellValue<int>(dgvUsersList, 1)).ShowDialog();
            
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
         if( MessageBox.Show("Are you sure you want to delete this user??","Delete",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
            {
              if(clsUser.DeleteUser(clsdb.GetCellValue<int>(dgvUsersList, 0)))
                {
                    MessageBox.Show("Record Deleted Succesfuly!", "Delete",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
              else
                    MessageBox.Show("Error Occured while deleting this record!", "Erro",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmAddNewUser(clsdb.GetCellValue<int>(dgvUsersList, 0),
                              clsdb.GetCellValue<int>(dgvUsersList, 1)).ShowDialog();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            _Referech();
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmSendEmail(clsdb.GetCellValue<int>(dgvUsersList,1)).ShowDialog();
        }
    }
}

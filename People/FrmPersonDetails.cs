﻿using System;
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
    public partial class frmPersonDetails : Form
    {
        public frmPersonDetails(int PersonID)
        {
            InitializeComponent();
            if (PersonID > 0)
            {
                ctrlPersonInfo1.LoadPersonInfo(PersonID);
            }
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

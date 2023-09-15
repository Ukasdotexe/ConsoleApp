using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_System
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            label1.Invoke((MethodInvoker)delegate
            {
                label1.Text = "";
            });
            int count = 0;
            while (count < 10000000)
            {
                label1.Invoke((MethodInvoker)delegate
                {
                    label1.Text = count.ToString();
                });
                count++;
            }
        }
    }
}

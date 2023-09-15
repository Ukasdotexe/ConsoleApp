using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClearClassLibrary;
using DVLD_System.Business_Layer;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace DVLD_System
{
    public partial class frmSendEmail : Form
    {
        public frmSendEmail()
        {
            InitializeComponent();
        }

        private int _PersonID = -1;
        public frmSendEmail(int personID)
        {
            InitializeComponent();
            _PersonID = personID;
        }

        private void frmSendEmail_Load(object sender, EventArgs e)
        {
            clsPerson.PopulateList(ref cmbPersons);

            if (_PersonID != -1)
            {
                cmbPersons.SelectedValue = _PersonID;
                cmbPersons_SelectionChangeCommitted(sender, e);
            }
               
           
        }

        private void cmbPersons_SelectionChangeCommitted(object sender, EventArgs e)
        {
            tbEmail.Text = clsPerson.Find((int)cmbPersons.SelectedValue).Email;
        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(cmbPersons.Text) || string.IsNullOrEmpty(tbEmail.Text)
                || string.IsNullOrEmpty(tbSubject.Text) || string.IsNullOrEmpty(tbMessage.Text))
            {
                MessageBox.Show("One of the fields is empty!!", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);return;
            }

            var SendFrom = new MailboxAddress("Ibrahim", "siqxdpo@gmail.com");
            var SendTo = MailboxAddress.Parse(tbEmail.Text);


            MimeMessage Email = new MimeMessage();
            Email.From.Add(SendFrom);
            Email.To.Add(SendTo);

            Email.Subject = tbSubject.Text;

            Email.Body = new TextPart(TextFormat.Html)
            {
                Text = tbMessage.Text
            };

            SmtpClient Client = new SmtpClient();

            try
            {
                //estalish connection with the server

                Client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                Client.Authenticate("siqxdpo@gmail.com", "wcwxzepoydsogqae");
                Client.Send(Email);

                MessageBox.Show("Email Sent!!", "Information", MessageBoxButtons.OK,
                   MessageBoxIcon.Information); return;

            }
            catch { }
            finally
            {
                Client.Disconnect(true);
                Client.Dispose();

            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }



        private void clear()
        {
            Clear.ResetComboBox(cmbPersons);
            Clear.ResetTextBox(tbEmail);
            Clear.ResetTextBox(tbSubject);
            Clear.ResetTextBox(tbMessage);
        }
        }
    
}

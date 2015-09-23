using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KremersFingerprint
{
    public partial class AdminForm : Form
    {
        private DataTable adminTable = MainForm.AdminTable;
        public static bool temp;

        public AdminForm(bool found)
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            MainForm.ReadFromAdminTable();
            string search = txtAdminPassword.Text;
            search = MainForm.EncryptString(search, MainForm.Password, MainForm.Salt, MainForm.HashAlgorithm, MainForm.PasswordIterations, MainForm.InitialVector, MainForm.KeySize);

            temp = adminTable.Rows.Contains(search);
            if (temp != false)
            {
                MessageBox.Show("Password match.");
                MainForm.Found = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Password does not match.");
                MainForm.Found = false;
                this.Close();
            }
        }
    }
}

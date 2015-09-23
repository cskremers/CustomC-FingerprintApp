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
    public partial class GatherName : Form
    {
        string userID;
        string firstName;
        string lastName;
        private DataTable dt = MainForm.Dt;
        string Password = "A10g83l";
        string Salt = "Kosher";
        string HashAlgorithm = "SHA1";
        int PasswordIterations = 3;
        string InitialVector = "OFRna73m*aze01xY";
        int KeySize = 256;

        public GatherName()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            firstName = txtFirstName.Text;
            lastName = txtLastName.Text;
            QueryDB();
            firstName = MainForm.EncryptString(firstName, Password, "Kosher", "SHA1", PasswordIterations, InitialVector, KeySize);
            lastName = MainForm.EncryptString(lastName, Password, "Kosher", "SHA1", PasswordIterations, InitialVector, KeySize);
            userID = MainForm.EncryptString(userID, Password, "Kosher", "SHA1", PasswordIterations, InitialVector, KeySize);
            this.Close();
            MainForm.SaveData(userID, firstName, lastName);
        }

        private void QueryDB()
        {
            MainForm.ReadFromDatabase();
            string tempUserID;
            bool search = false;
            tempUserID = firstName.Remove(1).ToLower() + lastName.ToLower();
            userID = tempUserID;
            tempUserID = MainForm.EncryptString(tempUserID, Password, Salt, HashAlgorithm, PasswordIterations, InitialVector, KeySize);
            search = dt.Rows.Contains(tempUserID);
            if (search == true)
            {
                GenerateUserID();
            }
        }

        private string GenerateUserID()
        {
            //create random number
            Random generator = new Random();
            int num = generator.Next(1, 900);
            userID = userID + num;
            return userID;
        }
    }
}

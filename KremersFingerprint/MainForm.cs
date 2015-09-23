using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using SecuGen.FDxSDKPro.Windows;
using System.Media;

namespace KremersFingerprint
{
    public partial class MainForm : Form
    {
        #region Declare Variables

        //public variables
        public static DataTable Dt = new DataTable();
        public static DataTable AdminTable = new DataTable();
        public string UserId;
        public string FirstName;
        public string LastName;
        public static bool askFlag;
        public static bool Found;
        public static bool EnrollQuestion = false;

        //Encryption Variables
        public static string Password = "A10g83l";
        public static string Salt = "Kosher";
        public static string HashAlgorithm = "SHA1";
        public static int PasswordIterations = 3;
        public static string InitialVector = "OFRna73m*aze01xY";
        public static int KeySize = 256;

        //Lists
        List<string> handSelection = new List<string>();
        List<string> leftHand = new List<string>();
        List<string> rightHand = new List<string>();
        List<byte[]> templatesFromDB = new List<byte[]>();
        List<string> firstNames = new List<string>();
        List<string> lastNames = new List<string>();
        List<string> userIDs = new List<string>();
        List<string> matchScores = new List<string>();
        List<string> accessLevelAuth = new List<string>();

        //Secugen Variables
        private SGFingerPrintManager FPReader = new SGFingerPrintManager();
        private SGFPMDeviceInfoParam readerInfo = new SGFPMDeviceInfoParam();
        private Int32 imageWidth;
        private Int32 imageHeight;
        private Int32 iError;

        //Database
        private static OleDbDataAdapter dataAdapter = new OleDbDataAdapter();
        private static OleDbConnection cn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source = Database/myDatabase.accdb");

        //Finger Images
        private static Byte[] imageTemp;
        private static Byte[] imageOfThumb;
        private static Byte[] imageOfIndex;
        private static Byte[] imageOfMiddle;
        private static Byte[] imageOfRing;
        private static Byte[] imageOfPinky;
        private static byte[] imageOfThumbTemplate = new byte[400];
        private static byte[] imageOfIndexTemplate = new byte[400];
        private static byte[] imageOfMiddleTemplate = new byte[400];
        private static byte[] imageOfRingTemplate = new byte[400];
        private static byte[] imageOfPinkyTemplate = new byte[400];
        private static byte[] templateToCompare = new byte[400];
        
        //Access levels
        static string accessLevel;
        static bool lowAccessLevel;
        static bool middleAccessLevel;
        static bool topAccessLevel;
        
        //Misc Bools
        bool AddedToLists = false;
        static bool namesFull = false;
        
        //for adding data to lists
        byte[] templateFromDB;
        string firstName;
        string lastName;
        string userID;
        
        //Sound


        #endregion

        #region Upon Loading

        public MainForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                StartPosition = FormStartPosition.CenterScreen;
                DatabaseConnection();
                GetDeviceInfo();
                FPReader.EnableAutoOnEvent(true, (Int32)this.Handle);
                InitializeListSet();
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        private void InitializeListSet()
        {
            //Create add fingers to lists
            leftHand.Add("Left Thumb");
            leftHand.Add("Left Index");
            leftHand.Add("Left Middle");
            leftHand.Add("Left Ring");
            leftHand.Add("Left Pinky");
            rightHand.Add("Right Thumb");
            rightHand.Add("Right Index");
            rightHand.Add("Right Middle");
            rightHand.Add("Right Ring");
            rightHand.Add("Right Pinky");
            handSelection.Add("Left Hand");
            handSelection.Add("Right Hand");
            selectHandCombo.Items.Clear();

            for (int i = 0; i < handSelection.Count(); i++)
            {
                selectHandCombo.Items.Add(handSelection[i]);
            }

            //Middle is currently the select default
            AccessLevelCombo.Items.Add("Low");
            AccessLevelCombo.Items.Add("Middle");
            AccessLevelCombo.Items.Add("Top");
            AccessLevelCombo.SelectedIndex = 1;

            //Disable Buttons
            TurnOffButtons();
        }

        public void TurnOffButtons()
        {
            btnCaptureThumb.Visible = false;
            btnCaptureIndex.Visible = false;
            btnCaptureMiddle.Visible = false;
            btnCaptureRing.Visible = false;
            btnCapturePinky.Visible = false;
        }

        private void GetDeviceInfo()
        {
            try
            {
                SGFPMDeviceName device_name = SGFPMDeviceName.DEV_AUTO;
                Int32 device_id = (Int32)(SGFPMPortAddr.USB_AUTO_DETECT);

                FPReader.Init(device_name);
                iError = FPReader.OpenDevice(device_id);
                if (iError == (Int32)(SGFPMError.ERROR_NONE))
                {
                    //load device
                    labelStatus.Text = "Welcome to the Kremers Fingerprint Application, Please select a mode to begin";
                    //create imageTemp
                    iError = FPReader.GetDeviceInfo(readerInfo);
                    imageHeight = readerInfo.ImageHeight;
                    imageWidth = readerInfo.ImageWidth;
                    imageTemp = new byte[imageWidth * imageHeight];
                }
                else
                {
                    MessageBox.Show("Device did not load properly.");
                    MessageBox.Show("Please use Admin mode to reconnect");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show(ex.StackTrace);
            }
        }

        private void DatabaseConnection()
        {
            try
            {
                cn.Open();
                cn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Connection to database failed! Please use Login under the 'File' Menu and try to reconnect before continuing.");
                throw;
            }
        }

        #endregion

        #region Database Functions

        private void GatherName()
        {
            GatherName enroll = new GatherName();
            enroll.StartPosition = FormStartPosition.CenterScreen;
            enroll.Show();
        }

        public static void SaveData(string userId, string firstName, string lastName)
        {
            try
            {
                //Open a connection to the database
                cn.Open();
                //Create insert statement
                string insert = "Insert INTO myTable(ID,firstName,lastName,thumb,[index],middle,ring,pinky,accessLevel) VALUES(@ID,@firstName,@lastName,@thumb,@index,@middle,@ring,@pinky,@accessLevel);";
                //Execute command to DB
                OleDbCommand insertCommand = new OleDbCommand(insert, cn);

                //ID
                //Already Encrypted From GatherName Form
                insertCommand.Parameters.AddWithValue("@ID", userId);
                //FirstName
                //Already Encrypted From GatherName Form
                insertCommand.Parameters.AddWithValue("@firstName", firstName);
                //LastName
                //ALready Encrypted From GatherName Form
                insertCommand.Parameters.AddWithValue("@lastName", lastName);
                //Thumb
                imageOfThumbTemplate = EncryptBytes(imageOfThumbTemplate, Password, Salt, HashAlgorithm, PasswordIterations, InitialVector, KeySize);
                insertCommand.Parameters.AddWithValue("@thumb", imageOfThumbTemplate);
                //Index
                imageOfIndexTemplate = EncryptBytes(imageOfIndexTemplate, Password, Salt, HashAlgorithm, PasswordIterations, InitialVector, KeySize);
                insertCommand.Parameters.AddWithValue("@index", imageOfIndexTemplate);
                //Middle
                imageOfMiddleTemplate = EncryptBytes(imageOfMiddleTemplate, Password, Salt, HashAlgorithm, PasswordIterations, InitialVector, KeySize);
                insertCommand.Parameters.AddWithValue("@middle", imageOfMiddleTemplate);
                //Ring
                imageOfRingTemplate = EncryptBytes(imageOfRingTemplate, Password, Salt, HashAlgorithm, PasswordIterations, InitialVector, KeySize);
                insertCommand.Parameters.AddWithValue("@ring", imageOfRingTemplate);
                //Pinky
                imageOfPinkyTemplate = EncryptBytes(imageOfPinkyTemplate, Password, Salt, HashAlgorithm, PasswordIterations, InitialVector, KeySize);
                insertCommand.Parameters.AddWithValue("@pinky", imageOfPinkyTemplate);
                //Access Level
                accessLevel = EncryptString(accessLevel, Password, Salt, HashAlgorithm, PasswordIterations, InitialVector, KeySize);
                insertCommand.Parameters.AddWithValue("@accessLevel", accessLevel);
                //execute command
                insertCommand.ExecuteNonQuery();
                //close database connection
                cn.Close();
                string temp = DecryptString(userId, Password, Salt, HashAlgorithm, PasswordIterations, InitialVector, KeySize);
                MessageBox.Show("Enrollment Complete!" + '\n' + "Your username is: " + temp.ToString());
                ReadFromDatabase();
                AskToEnrollAgain();
                namesFull = false;
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.StackTrace);
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.Data.ToString());
                MessageBox.Show(ex.ErrorCode.ToString());
                MessageBox.Show("Error enrolling user, please try again" + "\n" +ex);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while processing command");
                MessageBox.Show(ex.StackTrace);
                MessageBox.Show(ex.Message);
            }
        }

        public static void AskToEnrollAgain()
        {
            AskToEnroll ask = new AskToEnroll();
            ask.StartPosition = FormStartPosition.CenterScreen;
            ask.ShowDialog();
            //clear out images for next user
            if (askFlag == true)
            {
                imageOfThumbTemplate = null;
                imageOfIndexTemplate = null;
                imageOfMiddleTemplate = null;
                imageOfRingTemplate = null;
                imageOfPinkyTemplate = null;
                imageOfThumb = null;
                imageOfIndex = null;
                imageOfMiddle = null;
                imageOfRing = null;
                imageOfPinky = null;
            }
            else
            {
                
            }
        }

        public static void ReadFromDatabase()
        {
            try
            {
                cn.Open();
                dataAdapter = new OleDbDataAdapter("Select * From myTable", cn);
                dataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                dataAdapter.Fill(Dt);
                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to read database.");
                MessageBox.Show(ex.StackTrace);
            }
        }

        public static void ReadFromAdminTable()
        {
            try
            {
                cn.Open();
                dataAdapter = new OleDbDataAdapter("Select * From adminTable", cn);
                dataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                dataAdapter.Fill(AdminTable);
                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to read database.");
                MessageBox.Show(ex.StackTrace);
            }
        }

        public void AddToLists()
        {
            for (int row = 0; row < Dt.Rows.Count; row++)
            {
                templateFromDB = (byte[])Dt.Rows[row]["thumb"];
                templateFromDB = DecryptBytes(templateFromDB, Password, Salt, HashAlgorithm, PasswordIterations, InitialVector, KeySize);
                templatesFromDB.Add(templateFromDB);
                templateFromDB = (byte[])Dt.Rows[row]["index"];
                templateFromDB = DecryptBytes(templateFromDB, Password, Salt, HashAlgorithm, PasswordIterations, InitialVector, KeySize);
                templatesFromDB.Add(templateFromDB);
                templateFromDB = (byte[])Dt.Rows[row]["middle"];
                templateFromDB = DecryptBytes(templateFromDB, Password, Salt, HashAlgorithm, PasswordIterations, InitialVector, KeySize);
                templatesFromDB.Add(templateFromDB);
                templateFromDB = (byte[])Dt.Rows[row]["ring"];
                templateFromDB = DecryptBytes(templateFromDB, Password, Salt, HashAlgorithm, PasswordIterations, InitialVector, KeySize);
                templatesFromDB.Add(templateFromDB);
                templateFromDB = (byte[])Dt.Rows[row]["pinky"];
                templateFromDB = DecryptBytes(templateFromDB, Password, Salt, HashAlgorithm, PasswordIterations, InitialVector, KeySize);
                templatesFromDB.Add(templateFromDB);
                firstName = (string)Dt.Rows[row]["firstName"];
                firstName = DecryptString(firstName, Password, Salt, HashAlgorithm, PasswordIterations, InitialVector, KeySize);
                firstNames.Add(firstName);
                lastName = (string)Dt.Rows[row]["lastName"];
                lastName = DecryptString(lastName, Password, Salt, HashAlgorithm, PasswordIterations, InitialVector, KeySize);
                lastNames.Add(lastName);
                userID = (string)Dt.Rows[row]["ID"];
                userID = DecryptString(userID, Password, Salt, HashAlgorithm, PasswordIterations, InitialVector, KeySize);
                userIDs.Add(userID);
                accessLevel = (string)Dt.Rows[row]["accessLevel"];
                accessLevel = DecryptString(accessLevel, Password, Salt, HashAlgorithm, PasswordIterations, InitialVector, KeySize);
                accessLevelAuth.Add(accessLevel);
                AddedToLists = true;
            }
        }

        #endregion

        #region Button Captures

        private void btnCaptureThumb_Click(object sender, EventArgs e)
        {
            try
            {
                iError = FPReader.GetDeviceInfo(readerInfo);
                if (iError == (Int32)SGFPMError.ERROR_NONE)
                {
                    imageHeight = readerInfo.ImageHeight;
                    imageWidth = readerInfo.ImageWidth;
                }

                imageOfThumb = new byte[imageWidth * imageHeight];

                iError = FPReader.GetImage(imageOfThumb);
                if (iError == (Int32)SGFPMError.ERROR_NONE)
                {
                    DrawImage(imageOfThumb, ThumbPictureBox);
                    //create template
                    FPReader.CreateTemplate(imageOfThumb, imageOfThumbTemplate);
                    btnCaptureIndex.Visible = true;
                    labelStatus.Text = "Please enroll index.";
                }
            }
            catch (Exception)
            {
                labelStatus.Text = "Couldn't acquire fingerprint. Please try again.";
            }
        }

        private void btnCaptureIndex_Click(object sender, EventArgs e)
        {
            try
            {
                iError = FPReader.GetDeviceInfo(readerInfo);
                if (iError == (Int32)SGFPMError.ERROR_NONE)
                {
                    imageHeight = readerInfo.ImageHeight;
                    imageWidth = readerInfo.ImageWidth;
                }

                imageOfIndex = new byte[imageWidth * imageHeight];

                iError = FPReader.GetImage(imageOfIndex);
                if (iError == (Int32)SGFPMError.ERROR_NONE)
                {
                    DrawImage(imageOfIndex, IndexPictureBox);
                    //create template
                    FPReader.CreateTemplate(imageOfIndex, imageOfIndexTemplate);
                    btnCaptureMiddle.Visible = true;
                    labelStatus.Text = "Please enroll middle.";
                }
            }
            catch (Exception)
            {
                labelStatus.Text = "Couldn't acquire fingerprint. Please try again.";
            }
        }

        private void btnCaptureMiddle_Click(object sender, EventArgs e)
        {
            try
            {
                iError = FPReader.GetDeviceInfo(readerInfo);
                if (iError == (Int32)SGFPMError.ERROR_NONE)
                {
                    imageHeight = readerInfo.ImageHeight;
                    imageWidth = readerInfo.ImageWidth;
                }

                imageOfMiddle = new byte[imageWidth * imageHeight];

                iError = FPReader.GetImage(imageOfMiddle);
                if (iError == (Int32)SGFPMError.ERROR_NONE)
                {
                    DrawImage(imageOfMiddle, MiddlePictureBox);
                    //create template
                    FPReader.CreateTemplate(imageOfMiddle, imageOfMiddleTemplate);
                    btnCaptureRing.Visible = true;
                    labelStatus.Text = "Please enroll ring.";
                }
            }
            catch (Exception)
            {
                labelStatus.Text = "Couldn't acquire fingerprint. Please try again.";
            }
        }

        private void btnCaptureRing_Click(object sender, EventArgs e)
        {
            try
            {
                iError = FPReader.GetDeviceInfo(readerInfo);
                if (iError == (Int32)SGFPMError.ERROR_NONE)
                {
                    imageHeight = readerInfo.ImageHeight;
                    imageWidth = readerInfo.ImageWidth;
                }

                imageOfRing = new byte[imageWidth * imageHeight];

                iError = FPReader.GetImage(imageOfRing);
                if (iError == (Int32)SGFPMError.ERROR_NONE)
                {
                    DrawImage(imageOfRing, RingPictureBox);
                    //create template
                    FPReader.CreateTemplate(imageOfRing, imageOfRingTemplate);
                    btnCapturePinky.Visible = true;
                    labelStatus.Text = "Please enroll pinky.";
                }
            }
            catch (Exception)
            {
                labelStatus.Text = "Couldn't acquire fingerprint. Please try again.";
            }
        }

        private void btnCapturePinky_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.AppStarting;
                CheckAccessLevel();
                iError = FPReader.GetDeviceInfo(readerInfo);
                if (iError == (Int32)SGFPMError.ERROR_NONE)
                {
                    imageHeight = readerInfo.ImageHeight;
                    imageWidth = readerInfo.ImageWidth;
                }

                imageOfPinky = new byte[imageWidth * imageHeight];

                iError = FPReader.GetImage(imageOfPinky);
                if (iError == (Int32)SGFPMError.ERROR_NONE)
                {
                    DrawImage(imageOfPinky, PinkyPictureBox);
                    //create template
                    FPReader.CreateTemplate(imageOfPinky, imageOfPinkyTemplate);
                    if (EnrollRadioButton.Checked)
                    {
                        GatherName();
                        ThumbPictureBox.Image = null;
                        IndexPictureBox.Image = null;
                        MiddlePictureBox.Image = null;
                        RingPictureBox.Image = null;
                        PinkyPictureBox.Image = null;
                        labelStatus.Text = "";
                    }
                    else
                    {
                        labelStatus.Text = "Thank you!";
                    }
                }
                Cursor = Cursors.Default;
            }
            catch (Exception)
            {
                labelStatus.Text = "Couldn't acquire fingerprint. Please try again.";
            }
        }

        #endregion

        #region Autocheck State of Sensor

        protected override void WndProc(ref Message message)
        {
            if (message.Msg == (Int32)SGFPMMessages.DEV_AUTOONEVENT)
            {
                if (message.WParam.ToInt32() == (Int32)SGFPMAutoOnEvent.FINGER_ON)
                {
                    imageTemp = new Byte[imageWidth * imageHeight];
                    templateToCompare = new Byte[400];
                    iError = FPReader.GetImage(imageTemp);
                    DrawImage((imageTemp), TestPictureBox);
                    FPReader.CreateTemplate(imageTemp, templateToCompare);

                    if (IdentifyRadioButton.Checked)
                    {
                        Identify();
                    }
                    else if (VerifyRadioButton.Checked)
                    {
                        Verify();
                    }
                    else
                    {
                    }
                }
                else
                {
                    TestPictureBox.Image = null;
                    imageTemp = null;
                    templateToCompare = null;
                }
            }
            base.WndProc(ref message);
        }

        #endregion

        #region Misc

        private void LowRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            while (Found == false)
            {
                MessageBox.Show("Please enter the Administrator password to accept change.");
                AdminForm verifyPassword = new AdminForm(Found);
                verifyPassword.StartPosition = FormStartPosition.CenterScreen;
                verifyPassword.ShowDialog();
                if (MiddleRadioButton.Checked)
                {
                    AccessLevelCombo.SelectedIndex = 1;
                }
                else
                {
                    AccessLevelCombo.SelectedIndex = 2;
                }
                //if passwords don't match
                if (AdminForm.temp == false)
                {
                    Found = true;
                    LowRadioButton.Checked = true;
                    AccessLevelCombo.SelectedIndex = 0;
                }
            }
        }

        private void MiddleRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            while (Found == false)
            {
                MessageBox.Show("Please enter the Administrator password to accept change.");
                AdminForm verifyPassword = new AdminForm(Found);
                verifyPassword.StartPosition = FormStartPosition.CenterScreen;
                verifyPassword.ShowDialog();
                if (LowRadioButton.Checked)
                {
                    AccessLevelCombo.SelectedIndex = 0;
                }
                else
                {
                    AccessLevelCombo.SelectedIndex = 2;
                }
                //if passwords don't match
                if (AdminForm.temp == false)
                {
                    Found = true;
                    MiddleRadioButton.Checked = true;
                    AccessLevelCombo.SelectedIndex = 1;
                }
            }
        }

        private void TopRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            while (Found == false)
            {
                MessageBox.Show("Please enter the Administrator password to accept change.");
                AdminForm verifyPassword = new AdminForm(Found);
                verifyPassword.StartPosition = FormStartPosition.CenterScreen;
                verifyPassword.ShowDialog();

                if (MiddleRadioButton.Checked)
                {
                    AccessLevelCombo.SelectedIndex = 1;
                }
                else
                {
                    AccessLevelCombo.SelectedIndex = 0;
                }
                //if passwords don't match
                if (AdminForm.temp == false)
                {
                    Found = true;
                    TopRadioButton.Checked = true;
                    AccessLevelCombo.SelectedIndex = 2;
                }
            }
        }

        private void LowRadioButton_Click(object sender, EventArgs e)
        {
            Found = false;
        }

        private void MiddleRadioButton_Click(object sender, EventArgs e)
        {
            Found = false;
        }

        private void TopRadioButton_Click(object sender, EventArgs e)
        {
            Found = false;
        }

        private void DrawImage(Byte[] image, PictureBox picture)
        {
            int colorval;
            Bitmap bmp = new Bitmap(imageWidth, imageHeight);
            picture.Image = bmp;

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    colorval = image[(j * imageWidth) + i];
                    bmp.SetPixel(i, j, Color.FromArgb(colorval, colorval, colorval));
                }
            }
            picture.Refresh();
        }

        private void selectHandCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectHandCombo.SelectedIndex == 0)
            {
                labelStatus.Text = "Please enroll Left Thumb.";
                btnCaptureThumb.Enabled = true;
            }
            else if (selectHandCombo.SelectedIndex == 1)
            {
                labelStatus.Text = "Please enroll Right Thumb.";
                btnCaptureThumb.Enabled = true;
            }
            else
            {
                MessageBox.Show("Please select a hand!");
            }
        }

        public void CheckAccessLevel()
        {
            //Check Access Level
            if (LowRadioButton.Checked)
            {
                lowAccessLevel = true;
                middleAccessLevel = false;
                topAccessLevel = false;
                accessLevel = "Low";
            }
            else if (MiddleRadioButton.Checked)
            {
                lowAccessLevel = false;
                middleAccessLevel = true;
                topAccessLevel = false;
                accessLevel = "Middle";
            }
            else if (TopRadioButton.Checked)
            {
                lowAccessLevel = false;
                middleAccessLevel = false;
                topAccessLevel = true;
                accessLevel = "Top";
            }
        }

        public void ClearInfo()
        {
            ThumbPictureBox.Image = null;
            IndexPictureBox.Image = null;
            MiddlePictureBox.Image = null;
            RingPictureBox.Image = null;
            PinkyPictureBox.Image = null;
        }

        private void FillNames()
        {
            string name;
            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                string firstName = (string)Dt.Rows[i]["firstName"];
                firstName = DecryptString(firstName, Password, Salt, HashAlgorithm, PasswordIterations, InitialVector, KeySize);
                string lastName = (string)Dt.Rows[i]["lastName"];
                lastName = DecryptString(lastName, Password, Salt, HashAlgorithm, PasswordIterations, InitialVector, KeySize);
                string userID = (string)Dt.Rows[i]["ID"];
                userID = DecryptString(userID, Password, Salt, HashAlgorithm, PasswordIterations, InitialVector, KeySize);
                string access = (string)Dt.Rows[i]["accessLevel"];
                access = DecryptString(access, Password, Salt, HashAlgorithm, PasswordIterations, InitialVector, KeySize);
                name = firstName + " " + lastName + " | " + userID + " | " + access;
                AdminComboBox.Items.Add(name);
            }
            namesFull = true;
        }

        #endregion

        #region Security Encryption Methods

        public static byte[] EncryptBytes(byte[] imageBytes, string Password, string Salt = "Kosher", string HashAlgorithm = "SHA1", int PasswordIterations = 2, string InitialVector = "OFRna73m*aze01xY", int KeySize = 256)
        {
            if (imageBytes.Length == 0)
            {
                return imageBytes;
            }
            byte[] InitialVectorBytes = Encoding.ASCII.GetBytes(InitialVector);
            byte[] SaltValueBytes = Encoding.ASCII.GetBytes(Salt);
            //byte[] PlainTextBytes = Encoding.UTF8.GetBytes(PlainText);
            PasswordDeriveBytes DerivedPassword = new PasswordDeriveBytes(Password, SaltValueBytes, HashAlgorithm, PasswordIterations);
            byte[] KeyBytes = DerivedPassword.GetBytes(KeySize / 8);
            RijndaelManaged SymmetricKey = new RijndaelManaged();
            SymmetricKey.Mode = CipherMode.CBC;
            byte[] CipherTextBytes = null;
            using (ICryptoTransform Encryptor = SymmetricKey.CreateEncryptor(KeyBytes, InitialVectorBytes))
            {
                using (MemoryStream MemStream = new MemoryStream())
                {
                    using (CryptoStream CryptoStream = new CryptoStream(MemStream, Encryptor, CryptoStreamMode.Write))
                    {
                        CryptoStream.Write(imageBytes, 0, imageBytes.Length);
                        CryptoStream.FlushFinalBlock();
                        CipherTextBytes = MemStream.ToArray();
                        MemStream.Close();
                        CryptoStream.Close();
                    }
                }
            }
            SymmetricKey.Clear();
            return CipherTextBytes;
        }

        public static byte[] DecryptBytes(byte[] cipherImageBytes, string Password, string Salt = "Kosher", string HashAlgorithm = "SHA1", int PasswordIterations = 2, string InitialVector = "OFRna73m*aze01xY", int KeySize = 256)
        {
            if (cipherImageBytes.Length == 0)
            {
                return cipherImageBytes;
            }
            byte[] InitialVectorBytes = Encoding.ASCII.GetBytes(InitialVector);
            byte[] SaltValueBytes = Encoding.ASCII.GetBytes(Salt);
            //byte[] CipherTextBytes = Convert.FromBase64String(CipherText);
            PasswordDeriveBytes DerivedPassword = new PasswordDeriveBytes(Password, SaltValueBytes, HashAlgorithm, PasswordIterations);
            byte[] KeyBytes = DerivedPassword.GetBytes(KeySize / 8);
            RijndaelManaged SymmetricKey = new RijndaelManaged();
            SymmetricKey.Mode = CipherMode.CBC;
            SymmetricKey.Padding = PaddingMode.None;
            //byte[] PlainTextBytes = new byte[CipherTextBytes.Length];
            byte[] clearImageBytes = new byte[cipherImageBytes.Length];
            int ByteCount = 0;
            using (ICryptoTransform Decryptor = SymmetricKey.CreateDecryptor(KeyBytes, InitialVectorBytes))
            {
                using (MemoryStream MemStream = new MemoryStream(cipherImageBytes))
                {
                    using (CryptoStream CryptoStream = new CryptoStream(MemStream, Decryptor, CryptoStreamMode.Read))
                    {
                        ByteCount = CryptoStream.Read(clearImageBytes, 0, clearImageBytes.Length);
                        MemStream.Close();
                        CryptoStream.Close();
                    }
                }
            }
            SymmetricKey.Clear();
            return clearImageBytes;
        }

        public static string EncryptString(string PlainText, string Password, string Salt = "Kosher", string HashAlgorithm = "SHA1", int PasswordIterations = 2, string InitialVector = "OFRna73m*aze01xY", int KeySize = 256)
        {
            if (string.IsNullOrEmpty(PlainText))
            {
                return "";
            }
            byte[] InitialVectorBytes = Encoding.ASCII.GetBytes(InitialVector);
            byte[] SaltValueBytes = Encoding.ASCII.GetBytes(Salt);
            byte[] PlainTextBytes = Encoding.UTF8.GetBytes(PlainText);
            PasswordDeriveBytes DerivedPassword = new PasswordDeriveBytes(Password, SaltValueBytes, HashAlgorithm, PasswordIterations);
            byte[] KeyBytes = DerivedPassword.GetBytes(KeySize / 8);
            RijndaelManaged SymmetricKey = new RijndaelManaged();
            SymmetricKey.Mode = CipherMode.CBC;
            byte[] CipherTextBytes = null;
            using (ICryptoTransform Encryptor = SymmetricKey.CreateEncryptor(KeyBytes, InitialVectorBytes))
            {
                using (MemoryStream MemStream = new MemoryStream())
                {
                    using (CryptoStream CryptoStream = new CryptoStream(MemStream, Encryptor, CryptoStreamMode.Write))
                    {
                        CryptoStream.Write(PlainTextBytes, 0, PlainTextBytes.Length);
                        CryptoStream.FlushFinalBlock();
                        CipherTextBytes = MemStream.ToArray();
                        MemStream.Close();
                        CryptoStream.Close();
                    }
                }
            }
            SymmetricKey.Clear();
            return Convert.ToBase64String(CipherTextBytes);
        }

        public static string DecryptString(string CipherText, string Password, string Salt = "Kosher", string HashAlgorithm = "SHA1", int PasswordIterations = 2, string InitialVector = "OFRna73m*aze01xY", int KeySize = 256)
        {
            if (string.IsNullOrEmpty(CipherText))
                return "";
            byte[] InitialVectorBytes = Encoding.ASCII.GetBytes(InitialVector);
            byte[] SaltValueBytes = Encoding.ASCII.GetBytes(Salt);
            byte[] CipherTextBytes = Convert.FromBase64String(CipherText);
            PasswordDeriveBytes DerivedPassword = new PasswordDeriveBytes(Password, SaltValueBytes, HashAlgorithm, PasswordIterations);
            byte[] KeyBytes = DerivedPassword.GetBytes(KeySize / 8);
            RijndaelManaged SymmetricKey = new RijndaelManaged();
            SymmetricKey.Mode = CipherMode.CBC;
            byte[] PlainTextBytes = new byte[CipherTextBytes.Length];
            int ByteCount = 0;
            using (ICryptoTransform Decryptor = SymmetricKey.CreateDecryptor(KeyBytes, InitialVectorBytes))
            {
                using (MemoryStream MemStream = new MemoryStream(CipherTextBytes))
                {
                    using (CryptoStream CryptoStream = new CryptoStream(MemStream, Decryptor, CryptoStreamMode.Read))
                    {
                        ByteCount = CryptoStream.Read(PlainTextBytes, 0, PlainTextBytes.Length);
                        MemStream.Close();
                        CryptoStream.Close();
                    }
                }
            }
            SymmetricKey.Clear();
            return Encoding.UTF8.GetString(PlainTextBytes, 0, ByteCount);
        }

        #endregion

        #region Mode States

        private void EnrollRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (namesFull == false)
            {
                AdminComboBox.Items.Clear();
                ReadFromDatabase();
                FillNames();
            }
            if (EnrollRadioButton.Checked)
            {
                ThumbPictureBox.Visible = true;
                IndexPictureBox.Visible = true;
                MiddlePictureBox.Visible = true;
                RingPictureBox.Visible = true;
                PinkyPictureBox.Visible = true;
                btnCaptureThumb.Visible = true;
                btnCaptureIndex.Visible = false;
                btnCaptureMiddle.Visible = false;
                btnCaptureRing.Visible = false;
                btnCapturePinky.Visible = false;
                selectHandCombo.Visible = true;
                txtVerify.Visible = false;
                AdminComboBox.Visible = false;
                btnDelete.Visible = false;
                labelEnroll.Visible = true;
                labelStatus.Text = "Device Loaded.";
            }
            else if (IdentifyRadioButton.Checked)
            {
                txtVerify.Visible = false;
                ThumbPictureBox.Visible = false;
                IndexPictureBox.Visible = false;
                MiddlePictureBox.Visible = false;
                RingPictureBox.Visible = false;
                PinkyPictureBox.Visible = false;
                btnCaptureThumb.Visible = false;
                btnCaptureIndex.Visible = false;
                btnCaptureMiddle.Visible = false;
                btnCaptureRing.Visible = false;
                btnCapturePinky.Visible = false;
                selectHandCombo.Visible = false;
                AdminComboBox.Visible = false;
                btnDelete.Visible = false;
                labelStatus.Text = "Please place a finger on the sensor to identify.";
            }
            else if (VerifyRadioButton.Checked)
            {
                ThumbPictureBox.Visible = false;
                IndexPictureBox.Visible = false;
                MiddlePictureBox.Visible = false;
                RingPictureBox.Visible = false;
                PinkyPictureBox.Visible = false;
                btnCaptureThumb.Visible = false;
                btnCaptureIndex.Visible = false;
                btnCaptureMiddle.Visible = false;
                btnCaptureRing.Visible = false;
                btnCapturePinky.Visible = false;
                selectHandCombo.Visible = false;
                AdminComboBox.Visible = false;
                btnDelete.Visible = false;
                txtVerify.Visible = true;
                labelStatus.Text = "Please enter your userID to verify.";
            }
            else if (AdminRadioButton.Checked)
            {
                txtVerify.Visible = false;
                ThumbPictureBox.Visible = false;
                IndexPictureBox.Visible = false;
                MiddlePictureBox.Visible = false;
                RingPictureBox.Visible = false;
                PinkyPictureBox.Visible = false;
                btnCaptureThumb.Visible = false;
                btnCaptureIndex.Visible = false;
                btnCaptureMiddle.Visible = false;
                btnCaptureRing.Visible = false;
                btnCapturePinky.Visible = false;
                selectHandCombo.Visible = false;
                AdminComboBox.Visible = true;
                btnDelete.Visible = true;
                txtVerify.Visible = false;
            }
        }

        private void VerifyRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (namesFull == false)
            {
                AdminComboBox.Items.Clear();
                ReadFromDatabase();
                FillNames();
            }
            if (EnrollRadioButton.Checked)
            {
                ThumbPictureBox.Visible = true;
                IndexPictureBox.Visible = true;
                MiddlePictureBox.Visible = true;
                RingPictureBox.Visible = true;
                PinkyPictureBox.Visible = true;
                btnCaptureThumb.Visible = true;
                btnCaptureIndex.Visible = true;
                btnCaptureMiddle.Visible = true;
                btnCaptureRing.Visible = true;
                btnCapturePinky.Visible = true;
                selectHandCombo.Visible = true;
                txtVerify.Visible = false;
                btnDelete.Visible = false;
                AdminComboBox.Visible = false;
                labelStatus.Text = "Device Loaded.";
            }
            else if (IdentifyRadioButton.Checked)
            {
                txtVerify.Visible = false;
                ThumbPictureBox.Visible = false;
                IndexPictureBox.Visible = false;
                MiddlePictureBox.Visible = false;
                RingPictureBox.Visible = false;
                PinkyPictureBox.Visible = false;
                btnCaptureThumb.Visible = false;
                btnCaptureIndex.Visible = false;
                btnCaptureMiddle.Visible = false;
                btnCaptureRing.Visible = false;
                btnCapturePinky.Visible = false;
                selectHandCombo.Visible = false;
                AdminComboBox.Visible = false;
                btnDelete.Visible = false;
                labelStatus.Text = "Please place a finger on the sensor to identify.";
            }
            else if (VerifyRadioButton.Checked)
            {
                ThumbPictureBox.Visible = false;
                IndexPictureBox.Visible = false;
                MiddlePictureBox.Visible = false;
                RingPictureBox.Visible = false;
                PinkyPictureBox.Visible = false;
                btnCaptureThumb.Visible = false;
                btnCaptureIndex.Visible = false;
                btnCaptureMiddle.Visible = false;
                btnCaptureRing.Visible = false;
                btnCapturePinky.Visible = false;
                selectHandCombo.Visible = false;
                AdminComboBox.Visible = false;
                btnDelete.Visible = false;
                txtVerify.Visible = true;
                labelStatus.Text = "Please enter your userID to verify.";
            }
            else if (AdminRadioButton.Checked)
            {
                txtVerify.Visible = false;
                ThumbPictureBox.Visible = false;
                IndexPictureBox.Visible = false;
                MiddlePictureBox.Visible = false;
                RingPictureBox.Visible = false;
                PinkyPictureBox.Visible = false;
                btnCaptureThumb.Visible = false;
                btnCaptureIndex.Visible = false;
                btnCaptureMiddle.Visible = false;
                btnCaptureRing.Visible = false;
                btnCapturePinky.Visible = false;
                selectHandCombo.Visible = false;
                AdminComboBox.Visible = true;
                btnDelete.Visible = true;
                txtVerify.Visible = false;
            }
        }

        private void IdentifyRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (namesFull == false)
            {
                AdminComboBox.Items.Clear();
                ReadFromDatabase();
                FillNames();
            }
            if (EnrollRadioButton.Checked)
            {
                ThumbPictureBox.Visible = true;
                IndexPictureBox.Visible = true;
                MiddlePictureBox.Visible = true;
                RingPictureBox.Visible = true;
                PinkyPictureBox.Visible = true;
                btnCaptureThumb.Visible = true;
                btnCaptureIndex.Visible = true;
                btnCaptureMiddle.Visible = true;
                btnCaptureRing.Visible = true;
                btnCapturePinky.Visible = true;
                selectHandCombo.Visible = true;
                txtVerify.Visible = false;
                AdminComboBox.Visible = false;
                btnDelete.Visible = false;
                labelStatus.Text = "Device Loaded.";
            }
            else if (IdentifyRadioButton.Checked)
            {
                txtVerify.Visible = false;
                ThumbPictureBox.Visible = false;
                IndexPictureBox.Visible = false;
                MiddlePictureBox.Visible = false;
                RingPictureBox.Visible = false;
                PinkyPictureBox.Visible = false;
                btnCaptureThumb.Visible = false;
                btnCaptureIndex.Visible = false;
                btnCaptureMiddle.Visible = false;
                btnCaptureRing.Visible = false;
                btnCapturePinky.Visible = false;
                selectHandCombo.Visible = false;
                AdminComboBox.Visible = false;
                btnDelete.Visible = false;
                labelStatus.Text = "Please place a finger on the sensor to identify.";
            }
            else if (VerifyRadioButton.Checked)
            {
                ThumbPictureBox.Visible = false;
                IndexPictureBox.Visible = false;
                MiddlePictureBox.Visible = false;
                RingPictureBox.Visible = false;
                PinkyPictureBox.Visible = false;
                btnCaptureThumb.Visible = false;
                btnCaptureIndex.Visible = false;
                btnCaptureMiddle.Visible = false;
                btnCaptureRing.Visible = false;
                btnCapturePinky.Visible = false;
                selectHandCombo.Visible = false;
                AdminComboBox.Visible = false;
                btnDelete.Visible = false;
                txtVerify.Visible = true;
                labelStatus.Text = "Please enter your userID to verify.";
            }
            else if (AdminRadioButton.Checked)
            {
                txtVerify.Visible = false;
                ThumbPictureBox.Visible = false;
                IndexPictureBox.Visible = false;
                MiddlePictureBox.Visible = false;
                RingPictureBox.Visible = false;
                PinkyPictureBox.Visible = false;
                btnCaptureThumb.Visible = false;
                btnCaptureIndex.Visible = false;
                btnCaptureMiddle.Visible = false;
                btnCaptureRing.Visible = false;
                btnCapturePinky.Visible = false;
                selectHandCombo.Visible = false;
                AdminComboBox.Visible = true;
                btnDelete.Visible = true;
                txtVerify.Visible = false;
                labelStatus.Text = "You have free reign.....";
            }
        }

        private void AdminRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (namesFull == false)
            {
                AdminComboBox.Items.Clear();
                ReadFromDatabase();
                FillNames();
            }
            if (EnrollRadioButton.Checked)
            {
                ThumbPictureBox.Visible = true;
                IndexPictureBox.Visible = true;
                MiddlePictureBox.Visible = true;
                RingPictureBox.Visible = true;
                PinkyPictureBox.Visible = true;
                btnCaptureThumb.Visible = true;
                btnCaptureIndex.Visible = true;
                btnCaptureMiddle.Visible = true;
                btnCaptureRing.Visible = true;
                btnCapturePinky.Visible = true;
                selectHandCombo.Visible = true;
                txtVerify.Visible = false;
                btnDelete.Visible = false;
                AdminComboBox.Visible = false;
                labelStatus.Text = "Device Loaded.";
            }
            else if (IdentifyRadioButton.Checked)
            {
                txtVerify.Visible = false;
                ThumbPictureBox.Visible = false;
                IndexPictureBox.Visible = false;
                MiddlePictureBox.Visible = false;
                RingPictureBox.Visible = false;
                PinkyPictureBox.Visible = false;
                btnCaptureThumb.Visible = false;
                btnCaptureIndex.Visible = false;
                btnCaptureMiddle.Visible = false;
                btnCaptureRing.Visible = false;
                btnCapturePinky.Visible = false;
                selectHandCombo.Visible = false;
                AdminComboBox.Visible = false;
                btnDelete.Visible = false;
                labelStatus.Text = "Please place a finger on the sensor to identify.";
            }
            else if (VerifyRadioButton.Checked)
            {
                ThumbPictureBox.Visible = false;
                IndexPictureBox.Visible = false;
                MiddlePictureBox.Visible = false;
                RingPictureBox.Visible = false;
                PinkyPictureBox.Visible = false;
                btnCaptureThumb.Visible = false;
                btnCaptureIndex.Visible = false;
                btnCaptureMiddle.Visible = false;
                btnCaptureRing.Visible = false;
                btnCapturePinky.Visible = false;
                selectHandCombo.Visible = false;
                AdminComboBox.Visible = false;
                btnDelete.Visible = false;
                txtVerify.Visible = true;
                labelStatus.Text = "Please enter your userID to verify.";
            }
            else if (AdminRadioButton.Checked)
            {
                txtVerify.Visible = false;
                ThumbPictureBox.Visible = false;
                IndexPictureBox.Visible = false;
                MiddlePictureBox.Visible = false;
                RingPictureBox.Visible = false;
                PinkyPictureBox.Visible = false;
                btnCaptureThumb.Visible = false;
                btnCaptureIndex.Visible = false;
                btnCaptureMiddle.Visible = false;
                btnCaptureRing.Visible = false;
                btnCapturePinky.Visible = false;
                selectHandCombo.Visible = false;
                AdminComboBox.Visible = true;
                btnDelete.Visible = true;
                txtVerify.Visible = false;
            }
        }

        private void radioButtonLeftHand_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonLeftHand.Checked == true)
            {
                selectHandCombo.SelectedIndex = 0;
            }
            else
            {
                selectHandCombo.SelectedIndex = 1;
            }
            if (selectHandCombo.SelectedIndex == 0)
            {
                labelStatus.Text = "Please enroll Left Thumb.";
                btnCaptureThumb.Enabled = true;
            }
            else if (selectHandCombo.SelectedIndex == 1)
            {
                labelStatus.Text = "Please enroll Right Thumb.";
                btnCaptureThumb.Enabled = true;
            }
        }

        private void Identify()
        {
            ReadFromDatabase();

            if (AddedToLists == false)
                AddToLists();
            //sort through List and compare fingerprints to generate score. 
            Int32 iError;
            bool matched = false;
            Int32 match_score = 0;
            SGFPMSecurityLevel security_level;

            security_level = (SGFPMSecurityLevel)AccessLevelCombo.SelectedIndex;
            matchScores.Clear();

            for (int i = 0; i < templatesFromDB.Count; i++)
            {
                iError = FPReader.MatchTemplate(templateToCompare, templatesFromDB[i], security_level, ref matched);
                iError = FPReader.GetMatchingScore(templateToCompare, templatesFromDB[i], ref match_score);

                if (iError == (Int32)SGFPMError.ERROR_NONE)
                {
                    if (matched)
                    {
                        if (i == 0 || i == 5 || i == 10 || i == 15 || i == 20 || i == 25 || i == 30)
                        {
                            string finger = "Thumb";
                            string temp = firstNames[i / 5].ToString() + " " + lastNames[i / 5].ToString() + " " + finger + " " + match_score;
                            matchScores.Add(temp);
                        }
                        else if (i == 1 || i == 6 || i == 11 || i == 16 || i == 21 || i == 26 || i == 31)
                        {
                            string finger = "Index";
                            string temp = firstNames[i / 5].ToString() + " " + lastNames[i / 5].ToString() + " " + finger + " " + match_score;
                            matchScores.Add(temp);
                        }
                        else if (i == 2 || i == 7 || i == 12 || i == 17 || i == 22 || i == 27 || i == 32)
                        {
                            string finger = "Middle";
                            string temp = firstNames[i / 5].ToString() + " " + lastNames[i / 5].ToString() + " " + finger + " " + match_score;
                            matchScores.Add(temp);
                        }
                        else if (i == 3 || i == 8 || i == 13 || i == 18 || i == 23 || i == 28 || i == 33)
                        {
                            string finger = "Ring";
                            string temp = firstNames[i / 5].ToString() + " " + lastNames[i / 5].ToString() + " " + finger + " " + match_score;
                            matchScores.Add(temp);
                        }
                        else if (i == 4 || i == 9 || i == 14 || i == 19 || i == 24 || i == 29 || i == 34)
                        {
                            string finger = "Pinky";
                            string temp = firstNames[i / 5].ToString() + " " + lastNames[i / 5].ToString() + " " + finger + " " + match_score;
                            matchScores.Add(temp);
                        }
                    }
                    else
                    {
                        if (i == 0 || i == 5 || i == 10 || i == 15 || i == 20 || i == 25 || i == 30)
                        {
                            string finger = "Thumb";
                            string temp = firstNames[i / 5].ToString() + " " + lastNames[i / 5].ToString() + " " + finger + " " + match_score;
                            matchScores.Add(temp);
                        }
                        else if (i == 1 || i == 6 || i == 11 || i == 16 || i == 21 || i == 26 || i == 31)
                        {
                            string finger = "Index";
                            string temp = firstNames[i / 5].ToString() + " " + lastNames[i / 5].ToString() + " " + finger + " " + match_score;
                            matchScores.Add(temp);
                        }
                        else if (i == 2 || i == 7 || i == 12 || i == 17 || i == 22 || i == 27 || i == 32)
                        {
                            string finger = "Middle";
                            string temp = firstNames[i / 5].ToString() + " " + lastNames[i / 5].ToString() + " " + finger + " " + match_score;
                            matchScores.Add(temp);
                        }
                        else if (i == 3 || i == 8 || i == 13 || i == 18 || i == 23 || i == 28 || i == 33)
                        {
                            string finger = "Ring";
                            string temp = firstNames[i / 5].ToString() + " " + lastNames[i / 5].ToString() + " " + finger + " " + match_score;
                            matchScores.Add(temp);
                        }
                        else if (i == 4 || i == 9 || i == 14 || i == 19 || i == 24 || i == 29 || i == 34)
                        {
                            string finger = "Pinky";
                            string temp = firstNames[i / 5].ToString() + " " + lastNames[i / 5].ToString() + " " + finger + " " + match_score;
                            matchScores.Add(temp);
                        }
                    }
                }
                else if (iError == 103)
                {
                    MessageBox.Show("No Match Found!");
                    return;
                }
            }

            //sort through match scores to find highest and output
            string[] found;
            string find = "";
            int comparer = 0;
            int index = 0;
            string foundFinger = "";
            for (int i = 0; i < matchScores.Count; i++)
            {
                char[] split = { ' ' };
                find = matchScores[i].ToString();
                found = find.Split(split);
                if (Convert.ToInt32(found[3]) > comparer)
                {
                    comparer = Convert.ToInt32(found[3]);
                    index = i;
                    foundFinger = found[2].ToString();
                }
            }
            if (index >= 0 && index < 5)
            {
                index = 0;
            }
            else if (index >= 5 && index < 10)
            {
                index = 1;
            }
            else if (index >= 10 && index < 15)
            {
                index = 2;
            }
            else if (index >= 15 && index < 20)
            {
                index = 3;
            }
            else if (index >= 20 && index < 25)
            {
                index = 4;
            }
            MessageBox.Show("Welcome " + firstNames[index].ToString() + " " + lastNames[index].ToString() + ", " + foundFinger + " finger." + "You will be granted " + accessLevelAuth[index].ToString() + " access.");
        }

        private void Verify()
        {
            ReadFromDatabase();
            if (AddedToLists == false)
                AddToLists();
            //grab user id from user on form
            string userIDInput = "";
            userIDInput = txtVerify.Text;
            userIDInput = EncryptString(userIDInput, Password, Salt, HashAlgorithm, PasswordIterations, InitialVector, KeySize);
            DataRow foundRow = Dt.Rows.Find(userIDInput);
            matchScores.Clear();
            templatesFromDB.Clear();
            //search lists for that name / user id
            if (txtVerify.Text == "" || txtVerify.Text == "Please enter user ID, then place finger on sensor")
            {
                MessageBox.Show("Please enter a valid user ID and retry");
                return;
            }
            else
            {
                //if can't find userid return and exit out.
                try
                {
                    userID = (string)foundRow[0];
                    userID = DecryptString(userID, Password, Salt, HashAlgorithm, PasswordIterations, InitialVector, KeySize);
                    firstName = (string)foundRow[1];
                    firstName = DecryptString(firstName, Password, Salt, HashAlgorithm, PasswordIterations, InitialVector, KeySize);
                    lastName = (string)foundRow[2];
                    lastName = DecryptString(lastName, Password, Salt, HashAlgorithm, PasswordIterations, InitialVector, KeySize);
                    imageOfThumbTemplate = (byte[])foundRow[3];
                    imageOfThumbTemplate = DecryptBytes(imageOfThumbTemplate, Password, Salt, HashAlgorithm, PasswordIterations, InitialVector, KeySize);
                    imageOfIndexTemplate = (byte[])foundRow[4];
                    imageOfIndexTemplate = DecryptBytes(imageOfIndexTemplate, Password, Salt, HashAlgorithm, PasswordIterations, InitialVector, KeySize);
                    imageOfMiddleTemplate = (byte[])foundRow[5];
                    imageOfMiddleTemplate = DecryptBytes(imageOfMiddleTemplate, Password, Salt, HashAlgorithm, PasswordIterations, InitialVector, KeySize);
                    imageOfRingTemplate = (byte[])foundRow[6];
                    imageOfRingTemplate = DecryptBytes(imageOfRingTemplate, Password, Salt, HashAlgorithm, PasswordIterations, InitialVector, KeySize);
                    imageOfPinkyTemplate = (byte[])foundRow[7];
                    imageOfPinkyTemplate = DecryptBytes(imageOfPinkyTemplate, Password, Salt, HashAlgorithm, PasswordIterations, InitialVector, KeySize);
                    templatesFromDB.Add(imageOfThumbTemplate);
                    templatesFromDB.Add(imageOfIndexTemplate);
                    templatesFromDB.Add(imageOfMiddleTemplate);
                    templatesFromDB.Add(imageOfRingTemplate);
                    templatesFromDB.Add(imageOfPinkyTemplate);
                    accessLevel = (string)foundRow[8];
                    accessLevel = DecryptString(accessLevel, Password, Salt, HashAlgorithm, PasswordIterations, InitialVector, KeySize);
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("The User ID was not found, please try again");
                    txtVerify.Text = "Please enter user ID, then place finger on sensor";
                    txtVerify.Focus();
                    return;
                }
                catch (Exception)
                {
                    MessageBox.Show("Error has occured, please see Verify method for details");
                }
            }
            //calculate match scores\\
            for (int i = 0; i < templatesFromDB.Count(); i++)
            {
                Int32 iError;
                bool matched = false;
                Int32 match_score = 0;
                SGFPMSecurityLevel security_level;

                security_level = (SGFPMSecurityLevel)AccessLevelCombo.SelectedIndex;

                iError = FPReader.MatchTemplate(templateToCompare, (byte[])templatesFromDB[i], security_level, ref matched);
                iError = FPReader.GetMatchingScore(templateToCompare, (byte[])templatesFromDB[i], ref match_score);

                if (iError == (Int32)SGFPMError.ERROR_NONE)
                {
                    if (matched)
                    {
                        firstName = foundRow[1].ToString();
                        lastName = foundRow[2].ToString();
                        firstName = DecryptString(firstName, Password, Salt, HashAlgorithm, PasswordIterations, InitialVector, KeySize);
                        lastName = DecryptString(lastName, Password, Salt, HashAlgorithm, PasswordIterations, InitialVector, KeySize);
                        string temp = firstName + " " + lastName + " " + match_score;
                        matchScores.Add(temp);
                    }
                    else
                    {
                        firstName = foundRow[1].ToString();
                        lastName = foundRow[2].ToString();
                        firstName = DecryptString(firstName, Password, Salt, HashAlgorithm, PasswordIterations, InitialVector, KeySize);
                        lastName = DecryptString(lastName, Password, Salt, HashAlgorithm, PasswordIterations, InitialVector, KeySize);
                        string temp = firstName + " " + lastName + " " + match_score;
                        matchScores.Add(temp);
                    }
                }
                else if (iError == 103)
                {
                    MessageBox.Show("Regristration not matched");
                    return;
                }

            }
            //Output name, last name, user id, and finger.
            string[] found;
            string find = "";
            int comparer = 0;
            int index = 0;
            for (int i = 0; i < matchScores.Count; i++)
            {
                char[] split = { ' ' };
                find = matchScores[i].ToString();
                found = find.Split(split);
                if (Convert.ToInt32(found[2]) > comparer)
                {
                    comparer = Convert.ToInt32(found[2]);
                    index = i;
                }
            }
            if (index == 0)
                MessageBox.Show("Welcome " + firstName + " " + lastName + "." + " Thumb finger.");
            else if (index == 1)
                MessageBox.Show("Welcome " + firstName + " " + lastName + "." + " Index finger.");
            else if (index == 2)
                MessageBox.Show("Welcome " + firstName + " " + lastName + "." + " Middle finger.");
            else if (index == 3)
                MessageBox.Show("Welcome " + firstName + " " + lastName + "." + " Ring finger.");
            else if (index == 4)
                MessageBox.Show("Welcome " + firstName + " " + lastName + "." + " Pinky finger.");
        }

        #endregion

        #region Admin Methods

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.AppStarting;
            ReadFromDatabase();
            AddToLists();
            cn.Open();
            userID = userIDs[AdminComboBox.SelectedIndex];
            userID = EncryptString(userID, Password, Salt, HashAlgorithm, PasswordIterations, InitialVector, KeySize);
            string delete = "Delete FROM myTable where ID ='" + userID + "'";
            dataAdapter.DeleteCommand = cn.CreateCommand();
            dataAdapter.DeleteCommand.CommandText = delete;
            dataAdapter.DeleteCommand.ExecuteNonQuery();
            cn.Close();
            labelStatus.Text = "User Deleted.";
            AdminComboBox.Items.Clear();
            namesFull = false;
            Dt.Clear();
            ReadFromDatabase();
            FillNames();
            AdminComboBox.Text = "Users | UserID | Access Level:";
        }

        #endregion

        
    }
}
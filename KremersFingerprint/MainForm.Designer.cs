namespace KremersFingerprint
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.selectHandCombo = new System.Windows.Forms.ComboBox();
            this.btnCaptureThumb = new System.Windows.Forms.Button();
            this.btnCaptureIndex = new System.Windows.Forms.Button();
            this.btnCaptureMiddle = new System.Windows.Forms.Button();
            this.btnCaptureRing = new System.Windows.Forms.Button();
            this.btnCapturePinky = new System.Windows.Forms.Button();
            this.ThumbPictureBox = new System.Windows.Forms.PictureBox();
            this.IndexPictureBox = new System.Windows.Forms.PictureBox();
            this.MiddlePictureBox = new System.Windows.Forms.PictureBox();
            this.RingPictureBox = new System.Windows.Forms.PictureBox();
            this.PinkyPictureBox = new System.Windows.Forms.PictureBox();
            this.statusStrip = new System.Windows.Forms.Label();
            this.TestPictureBox = new System.Windows.Forms.PictureBox();
            this.DisplayPictureBox = new System.Windows.Forms.PictureBox();
            this.EnrollRadioButton = new System.Windows.Forms.RadioButton();
            this.VerifyRadioButton = new System.Windows.Forms.RadioButton();
            this.IdentifyRadioButton = new System.Windows.Forms.RadioButton();
            this.ModeRadioGroupBox = new System.Windows.Forms.GroupBox();
            this.AdminRadioButton = new System.Windows.Forms.RadioButton();
            this.txtVerify = new System.Windows.Forms.TextBox();
            this.AccessLevelGroupBox = new System.Windows.Forms.GroupBox();
            this.TopRadioButton = new System.Windows.Forms.RadioButton();
            this.MiddleRadioButton = new System.Windows.Forms.RadioButton();
            this.LowRadioButton = new System.Windows.Forms.RadioButton();
            this.AdminComboBox = new System.Windows.Forms.ComboBox();
            this.AccessLevelCombo = new System.Windows.Forms.ComboBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lblThumb = new System.Windows.Forms.Label();
            this.lblIndex = new System.Windows.Forms.Label();
            this.lblMiddle = new System.Windows.Forms.Label();
            this.lblRing = new System.Windows.Forms.Label();
            this.lblPinky = new System.Windows.Forms.Label();
            this.groupBoxHand = new System.Windows.Forms.GroupBox();
            this.groupBoxAdmin = new System.Windows.Forms.GroupBox();
            this.labelStatus = new System.Windows.Forms.Label();
            this.radioButtonRightHand = new System.Windows.Forms.RadioButton();
            this.radioButtonLeftHand = new System.Windows.Forms.RadioButton();
            this.groupBoxHandSelection = new System.Windows.Forms.GroupBox();
            this.labelEnroll = new System.Windows.Forms.Label();
            this.labelIdentify = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ThumbPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IndexPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MiddlePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RingPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PinkyPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TestPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DisplayPictureBox)).BeginInit();
            this.ModeRadioGroupBox.SuspendLayout();
            this.AccessLevelGroupBox.SuspendLayout();
            this.groupBoxHand.SuspendLayout();
            this.groupBoxAdmin.SuspendLayout();
            this.groupBoxHandSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // selectHandCombo
            // 
            this.selectHandCombo.Enabled = false;
            this.selectHandCombo.FormattingEnabled = true;
            this.selectHandCombo.Location = new System.Drawing.Point(17, 60);
            this.selectHandCombo.Name = "selectHandCombo";
            this.selectHandCombo.Size = new System.Drawing.Size(334, 21);
            this.selectHandCombo.TabIndex = 0;
            this.selectHandCombo.Text = "Select Hand";
            this.selectHandCombo.Visible = false;
            this.selectHandCombo.SelectedIndexChanged += new System.EventHandler(this.selectHandCombo_SelectedIndexChanged);
            // 
            // btnCaptureThumb
            // 
            this.btnCaptureThumb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(191)))));
            this.btnCaptureThumb.ForeColor = System.Drawing.Color.Green;
            this.btnCaptureThumb.Location = new System.Drawing.Point(6, 339);
            this.btnCaptureThumb.Name = "btnCaptureThumb";
            this.btnCaptureThumb.Size = new System.Drawing.Size(75, 23);
            this.btnCaptureThumb.TabIndex = 3;
            this.btnCaptureThumb.Text = "Capture";
            this.btnCaptureThumb.UseVisualStyleBackColor = false;
            this.btnCaptureThumb.Click += new System.EventHandler(this.btnCaptureThumb_Click);
            // 
            // btnCaptureIndex
            // 
            this.btnCaptureIndex.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(191)))));
            this.btnCaptureIndex.ForeColor = System.Drawing.Color.Green;
            this.btnCaptureIndex.Location = new System.Drawing.Point(91, 135);
            this.btnCaptureIndex.Name = "btnCaptureIndex";
            this.btnCaptureIndex.Size = new System.Drawing.Size(75, 23);
            this.btnCaptureIndex.TabIndex = 4;
            this.btnCaptureIndex.Text = "Capture";
            this.btnCaptureIndex.UseVisualStyleBackColor = false;
            this.btnCaptureIndex.Click += new System.EventHandler(this.btnCaptureIndex_Click);
            // 
            // btnCaptureMiddle
            // 
            this.btnCaptureMiddle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(191)))));
            this.btnCaptureMiddle.ForeColor = System.Drawing.Color.Green;
            this.btnCaptureMiddle.Location = new System.Drawing.Point(212, 98);
            this.btnCaptureMiddle.Name = "btnCaptureMiddle";
            this.btnCaptureMiddle.Size = new System.Drawing.Size(75, 23);
            this.btnCaptureMiddle.TabIndex = 5;
            this.btnCaptureMiddle.Text = "Capture";
            this.btnCaptureMiddle.UseVisualStyleBackColor = false;
            this.btnCaptureMiddle.Click += new System.EventHandler(this.btnCaptureMiddle_Click);
            // 
            // btnCaptureRing
            // 
            this.btnCaptureRing.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(191)))));
            this.btnCaptureRing.ForeColor = System.Drawing.Color.Green;
            this.btnCaptureRing.Location = new System.Drawing.Point(306, 113);
            this.btnCaptureRing.Name = "btnCaptureRing";
            this.btnCaptureRing.Size = new System.Drawing.Size(75, 23);
            this.btnCaptureRing.TabIndex = 7;
            this.btnCaptureRing.Text = "Capture";
            this.btnCaptureRing.UseVisualStyleBackColor = false;
            this.btnCaptureRing.Click += new System.EventHandler(this.btnCaptureRing_Click);
            // 
            // btnCapturePinky
            // 
            this.btnCapturePinky.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(191)))));
            this.btnCapturePinky.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnCapturePinky.ForeColor = System.Drawing.Color.Green;
            this.btnCapturePinky.Location = new System.Drawing.Point(373, 213);
            this.btnCapturePinky.Name = "btnCapturePinky";
            this.btnCapturePinky.Size = new System.Drawing.Size(75, 23);
            this.btnCapturePinky.TabIndex = 8;
            this.btnCapturePinky.Text = "Capture";
            this.btnCapturePinky.UseVisualStyleBackColor = false;
            this.btnCapturePinky.Click += new System.EventHandler(this.btnCapturePinky_Click);
            // 
            // ThumbPictureBox
            // 
            this.ThumbPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.ThumbPictureBox.Location = new System.Drawing.Point(6, 236);
            this.ThumbPictureBox.Name = "ThumbPictureBox";
            this.ThumbPictureBox.Size = new System.Drawing.Size(75, 97);
            this.ThumbPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ThumbPictureBox.TabIndex = 9;
            this.ThumbPictureBox.TabStop = false;
            // 
            // IndexPictureBox
            // 
            this.IndexPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.IndexPictureBox.Location = new System.Drawing.Point(91, 33);
            this.IndexPictureBox.Name = "IndexPictureBox";
            this.IndexPictureBox.Size = new System.Drawing.Size(75, 97);
            this.IndexPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.IndexPictureBox.TabIndex = 10;
            this.IndexPictureBox.TabStop = false;
            // 
            // MiddlePictureBox
            // 
            this.MiddlePictureBox.BackColor = System.Drawing.Color.Transparent;
            this.MiddlePictureBox.Location = new System.Drawing.Point(212, 0);
            this.MiddlePictureBox.Name = "MiddlePictureBox";
            this.MiddlePictureBox.Size = new System.Drawing.Size(75, 97);
            this.MiddlePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.MiddlePictureBox.TabIndex = 11;
            this.MiddlePictureBox.TabStop = false;
            // 
            // RingPictureBox
            // 
            this.RingPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.RingPictureBox.Location = new System.Drawing.Point(299, 12);
            this.RingPictureBox.Name = "RingPictureBox";
            this.RingPictureBox.Size = new System.Drawing.Size(75, 97);
            this.RingPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.RingPictureBox.TabIndex = 12;
            this.RingPictureBox.TabStop = false;
            // 
            // PinkyPictureBox
            // 
            this.PinkyPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.PinkyPictureBox.Location = new System.Drawing.Point(373, 110);
            this.PinkyPictureBox.Name = "PinkyPictureBox";
            this.PinkyPictureBox.Size = new System.Drawing.Size(75, 97);
            this.PinkyPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PinkyPictureBox.TabIndex = 13;
            this.PinkyPictureBox.TabStop = false;
            // 
            // statusStrip
            // 
            this.statusStrip.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.statusStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusStrip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.statusStrip.Location = new System.Drawing.Point(0, 511);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1016, 23);
            this.statusStrip.TabIndex = 14;
            // 
            // TestPictureBox
            // 
            this.TestPictureBox.Location = new System.Drawing.Point(394, 32);
            this.TestPictureBox.Name = "TestPictureBox";
            this.TestPictureBox.Size = new System.Drawing.Size(75, 97);
            this.TestPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.TestPictureBox.TabIndex = 15;
            this.TestPictureBox.TabStop = false;
            // 
            // DisplayPictureBox
            // 
            this.DisplayPictureBox.Location = new System.Drawing.Point(394, 135);
            this.DisplayPictureBox.Name = "DisplayPictureBox";
            this.DisplayPictureBox.Size = new System.Drawing.Size(75, 97);
            this.DisplayPictureBox.TabIndex = 23;
            this.DisplayPictureBox.TabStop = false;
            // 
            // EnrollRadioButton
            // 
            this.EnrollRadioButton.AutoSize = true;
            this.EnrollRadioButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(223)))));
            this.EnrollRadioButton.Location = new System.Drawing.Point(23, 19);
            this.EnrollRadioButton.Name = "EnrollRadioButton";
            this.EnrollRadioButton.Size = new System.Drawing.Size(51, 17);
            this.EnrollRadioButton.TabIndex = 19;
            this.EnrollRadioButton.Text = "Enroll";
            this.EnrollRadioButton.UseVisualStyleBackColor = true;
            this.EnrollRadioButton.CheckedChanged += new System.EventHandler(this.EnrollRadioButton_CheckedChanged);
            // 
            // VerifyRadioButton
            // 
            this.VerifyRadioButton.AutoSize = true;
            this.VerifyRadioButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(223)))));
            this.VerifyRadioButton.Location = new System.Drawing.Point(23, 42);
            this.VerifyRadioButton.Name = "VerifyRadioButton";
            this.VerifyRadioButton.Size = new System.Drawing.Size(51, 17);
            this.VerifyRadioButton.TabIndex = 20;
            this.VerifyRadioButton.Text = "Verify";
            this.VerifyRadioButton.UseVisualStyleBackColor = true;
            this.VerifyRadioButton.CheckedChanged += new System.EventHandler(this.VerifyRadioButton_CheckedChanged);
            // 
            // IdentifyRadioButton
            // 
            this.IdentifyRadioButton.AutoSize = true;
            this.IdentifyRadioButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(223)))));
            this.IdentifyRadioButton.Location = new System.Drawing.Point(23, 65);
            this.IdentifyRadioButton.Name = "IdentifyRadioButton";
            this.IdentifyRadioButton.Size = new System.Drawing.Size(59, 17);
            this.IdentifyRadioButton.TabIndex = 21;
            this.IdentifyRadioButton.Text = "Identify";
            this.IdentifyRadioButton.UseVisualStyleBackColor = true;
            this.IdentifyRadioButton.CheckedChanged += new System.EventHandler(this.IdentifyRadioButton_CheckedChanged);
            // 
            // ModeRadioGroupBox
            // 
            this.ModeRadioGroupBox.Controls.Add(this.labelIdentify);
            this.ModeRadioGroupBox.Controls.Add(this.labelEnroll);
            this.ModeRadioGroupBox.Controls.Add(this.AdminRadioButton);
            this.ModeRadioGroupBox.Controls.Add(this.EnrollRadioButton);
            this.ModeRadioGroupBox.Controls.Add(this.IdentifyRadioButton);
            this.ModeRadioGroupBox.Controls.Add(this.VerifyRadioButton);
            this.ModeRadioGroupBox.Controls.Add(this.txtVerify);
            this.ModeRadioGroupBox.ForeColor = System.Drawing.Color.White;
            this.ModeRadioGroupBox.Location = new System.Drawing.Point(12, 36);
            this.ModeRadioGroupBox.Name = "ModeRadioGroupBox";
            this.ModeRadioGroupBox.Size = new System.Drawing.Size(376, 117);
            this.ModeRadioGroupBox.TabIndex = 22;
            this.ModeRadioGroupBox.TabStop = false;
            this.ModeRadioGroupBox.Text = "Mode";
            // 
            // AdminRadioButton
            // 
            this.AdminRadioButton.AutoSize = true;
            this.AdminRadioButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(223)))));
            this.AdminRadioButton.Location = new System.Drawing.Point(23, 89);
            this.AdminRadioButton.Name = "AdminRadioButton";
            this.AdminRadioButton.Size = new System.Drawing.Size(85, 17);
            this.AdminRadioButton.TabIndex = 22;
            this.AdminRadioButton.Text = "Administrator";
            this.AdminRadioButton.UseVisualStyleBackColor = true;
            this.AdminRadioButton.CheckedChanged += new System.EventHandler(this.AdminRadioButton_CheckedChanged);
            // 
            // txtVerify
            // 
            this.txtVerify.Location = new System.Drawing.Point(122, 41);
            this.txtVerify.Name = "txtVerify";
            this.txtVerify.Size = new System.Drawing.Size(237, 20);
            this.txtVerify.TabIndex = 26;
            this.txtVerify.Text = "Please enter user ID, then place finger on sensor";
            this.txtVerify.Visible = false;
            // 
            // AccessLevelGroupBox
            // 
            this.AccessLevelGroupBox.Controls.Add(this.TopRadioButton);
            this.AccessLevelGroupBox.Controls.Add(this.MiddleRadioButton);
            this.AccessLevelGroupBox.Controls.Add(this.LowRadioButton);
            this.AccessLevelGroupBox.Controls.Add(this.AccessLevelCombo);
            this.AccessLevelGroupBox.ForeColor = System.Drawing.Color.White;
            this.AccessLevelGroupBox.Location = new System.Drawing.Point(12, 284);
            this.AccessLevelGroupBox.Name = "AccessLevelGroupBox";
            this.AccessLevelGroupBox.Size = new System.Drawing.Size(132, 137);
            this.AccessLevelGroupBox.TabIndex = 24;
            this.AccessLevelGroupBox.TabStop = false;
            this.AccessLevelGroupBox.Text = "Access Level";
            // 
            // TopRadioButton
            // 
            this.TopRadioButton.AutoSize = true;
            this.TopRadioButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(223)))));
            this.TopRadioButton.Location = new System.Drawing.Point(8, 68);
            this.TopRadioButton.Name = "TopRadioButton";
            this.TopRadioButton.Size = new System.Drawing.Size(44, 17);
            this.TopRadioButton.TabIndex = 2;
            this.TopRadioButton.Text = "Top";
            this.TopRadioButton.UseVisualStyleBackColor = true;
            this.TopRadioButton.CheckedChanged += new System.EventHandler(this.TopRadioButton_CheckedChanged);
            this.TopRadioButton.Click += new System.EventHandler(this.TopRadioButton_Click);
            // 
            // MiddleRadioButton
            // 
            this.MiddleRadioButton.AutoSize = true;
            this.MiddleRadioButton.Checked = true;
            this.MiddleRadioButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(223)))));
            this.MiddleRadioButton.Location = new System.Drawing.Point(7, 45);
            this.MiddleRadioButton.Name = "MiddleRadioButton";
            this.MiddleRadioButton.Size = new System.Drawing.Size(56, 17);
            this.MiddleRadioButton.TabIndex = 1;
            this.MiddleRadioButton.TabStop = true;
            this.MiddleRadioButton.Text = "Middle";
            this.MiddleRadioButton.UseVisualStyleBackColor = true;
            this.MiddleRadioButton.CheckedChanged += new System.EventHandler(this.MiddleRadioButton_CheckedChanged);
            this.MiddleRadioButton.Click += new System.EventHandler(this.MiddleRadioButton_Click);
            // 
            // LowRadioButton
            // 
            this.LowRadioButton.AutoSize = true;
            this.LowRadioButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(223)))));
            this.LowRadioButton.Location = new System.Drawing.Point(7, 20);
            this.LowRadioButton.Name = "LowRadioButton";
            this.LowRadioButton.Size = new System.Drawing.Size(45, 17);
            this.LowRadioButton.TabIndex = 0;
            this.LowRadioButton.Text = "Low";
            this.LowRadioButton.UseVisualStyleBackColor = true;
            this.LowRadioButton.CheckedChanged += new System.EventHandler(this.LowRadioButton_CheckedChanged);
            this.LowRadioButton.Click += new System.EventHandler(this.LowRadioButton_Click);
            // 
            // AdminComboBox
            // 
            this.AdminComboBox.FormattingEnabled = true;
            this.AdminComboBox.Location = new System.Drawing.Point(13, 24);
            this.AdminComboBox.Name = "AdminComboBox";
            this.AdminComboBox.Size = new System.Drawing.Size(258, 21);
            this.AdminComboBox.TabIndex = 25;
            this.AdminComboBox.Text = "Users | UserID | Access Level:";
            this.AdminComboBox.Visible = false;
            // 
            // AccessLevelCombo
            // 
            this.AccessLevelCombo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(132)))), ((int)(((byte)(143)))));
            this.AccessLevelCombo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(227)))), ((int)(((byte)(223)))));
            this.AccessLevelCombo.FormattingEnabled = true;
            this.AccessLevelCombo.Location = new System.Drawing.Point(6, 91);
            this.AccessLevelCombo.Name = "AccessLevelCombo";
            this.AccessLevelCombo.Size = new System.Drawing.Size(89, 21);
            this.AccessLevelCombo.TabIndex = 27;
            this.AccessLevelCombo.Visible = false;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(191)))));
            this.btnDelete.ForeColor = System.Drawing.Color.Red;
            this.btnDelete.Location = new System.Drawing.Point(276, 22);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 28;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Visible = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // lblThumb
            // 
            this.lblThumb.AutoSize = true;
            this.lblThumb.BackColor = System.Drawing.Color.Transparent;
            this.lblThumb.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(223)))));
            this.lblThumb.Location = new System.Drawing.Point(18, 365);
            this.lblThumb.Name = "lblThumb";
            this.lblThumb.Size = new System.Drawing.Size(40, 13);
            this.lblThumb.TabIndex = 29;
            this.lblThumb.Text = "Thumb";
            // 
            // lblIndex
            // 
            this.lblIndex.AutoSize = true;
            this.lblIndex.BackColor = System.Drawing.Color.Transparent;
            this.lblIndex.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(223)))));
            this.lblIndex.Location = new System.Drawing.Point(107, 161);
            this.lblIndex.Name = "lblIndex";
            this.lblIndex.Size = new System.Drawing.Size(33, 13);
            this.lblIndex.TabIndex = 30;
            this.lblIndex.Text = "Index";
            // 
            // lblMiddle
            // 
            this.lblMiddle.AutoSize = true;
            this.lblMiddle.BackColor = System.Drawing.Color.Transparent;
            this.lblMiddle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(223)))));
            this.lblMiddle.Location = new System.Drawing.Point(230, 124);
            this.lblMiddle.Name = "lblMiddle";
            this.lblMiddle.Size = new System.Drawing.Size(38, 13);
            this.lblMiddle.TabIndex = 31;
            this.lblMiddle.Text = "Middle";
            // 
            // lblRing
            // 
            this.lblRing.AutoSize = true;
            this.lblRing.BackColor = System.Drawing.Color.Transparent;
            this.lblRing.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(223)))));
            this.lblRing.Location = new System.Drawing.Point(321, 140);
            this.lblRing.Name = "lblRing";
            this.lblRing.Size = new System.Drawing.Size(29, 13);
            this.lblRing.TabIndex = 32;
            this.lblRing.Text = "Ring";
            // 
            // lblPinky
            // 
            this.lblPinky.AutoSize = true;
            this.lblPinky.BackColor = System.Drawing.Color.Transparent;
            this.lblPinky.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(223)))));
            this.lblPinky.Location = new System.Drawing.Point(394, 236);
            this.lblPinky.Name = "lblPinky";
            this.lblPinky.Size = new System.Drawing.Size(33, 13);
            this.lblPinky.TabIndex = 33;
            this.lblPinky.Text = "Pinky";
            // 
            // groupBoxHand
            // 
            this.groupBoxHand.BackgroundImage = global::KremersFingerprint.Properties.Resources.Hand;
            this.groupBoxHand.Controls.Add(this.ThumbPictureBox);
            this.groupBoxHand.Controls.Add(this.IndexPictureBox);
            this.groupBoxHand.Controls.Add(this.MiddlePictureBox);
            this.groupBoxHand.Controls.Add(this.lblPinky);
            this.groupBoxHand.Controls.Add(this.RingPictureBox);
            this.groupBoxHand.Controls.Add(this.lblRing);
            this.groupBoxHand.Controls.Add(this.PinkyPictureBox);
            this.groupBoxHand.Controls.Add(this.lblMiddle);
            this.groupBoxHand.Controls.Add(this.btnCaptureThumb);
            this.groupBoxHand.Controls.Add(this.lblIndex);
            this.groupBoxHand.Controls.Add(this.btnCaptureIndex);
            this.groupBoxHand.Controls.Add(this.lblThumb);
            this.groupBoxHand.Controls.Add(this.btnCaptureMiddle);
            this.groupBoxHand.Controls.Add(this.btnCaptureRing);
            this.groupBoxHand.Controls.Add(this.btnCapturePinky);
            this.groupBoxHand.ForeColor = System.Drawing.Color.White;
            this.groupBoxHand.Location = new System.Drawing.Point(475, 32);
            this.groupBoxHand.Name = "groupBoxHand";
            this.groupBoxHand.Size = new System.Drawing.Size(443, 470);
            this.groupBoxHand.TabIndex = 34;
            this.groupBoxHand.TabStop = false;
            this.groupBoxHand.Text = "Finger Prints";
            // 
            // groupBoxAdmin
            // 
            this.groupBoxAdmin.Controls.Add(this.AdminComboBox);
            this.groupBoxAdmin.Controls.Add(this.btnDelete);
            this.groupBoxAdmin.ForeColor = System.Drawing.Color.White;
            this.groupBoxAdmin.Location = new System.Drawing.Point(12, 220);
            this.groupBoxAdmin.Name = "groupBoxAdmin";
            this.groupBoxAdmin.Size = new System.Drawing.Size(376, 61);
            this.groupBoxAdmin.TabIndex = 35;
            this.groupBoxAdmin.TabStop = false;
            this.groupBoxAdmin.Text = "Admin";
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.labelStatus.Location = new System.Drawing.Point(8, 9);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(464, 20);
            this.labelStatus.TabIndex = 37;
            this.labelStatus.Text = "Verify that Databse and Biometric Device are connected.";
            // 
            // radioButtonRightHand
            // 
            this.radioButtonRightHand.AutoSize = true;
            this.radioButtonRightHand.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(223)))));
            this.radioButtonRightHand.Location = new System.Drawing.Point(198, 23);
            this.radioButtonRightHand.Name = "radioButtonRightHand";
            this.radioButtonRightHand.Size = new System.Drawing.Size(145, 17);
            this.radioButtonRightHand.TabIndex = 39;
            this.radioButtonRightHand.Text = "Enroll Right Hand Fingers";
            this.radioButtonRightHand.UseVisualStyleBackColor = true;
            // 
            // radioButtonLeftHand
            // 
            this.radioButtonLeftHand.AutoSize = true;
            this.radioButtonLeftHand.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(223)))));
            this.radioButtonLeftHand.Location = new System.Drawing.Point(23, 23);
            this.radioButtonLeftHand.Name = "radioButtonLeftHand";
            this.radioButtonLeftHand.Size = new System.Drawing.Size(138, 17);
            this.radioButtonLeftHand.TabIndex = 38;
            this.radioButtonLeftHand.Text = "Enroll Left Hand Fingers";
            this.radioButtonLeftHand.UseVisualStyleBackColor = true;
            this.radioButtonLeftHand.CheckedChanged += new System.EventHandler(this.radioButtonLeftHand_CheckedChanged);
            // 
            // groupBoxHandSelection
            // 
            this.groupBoxHandSelection.Controls.Add(this.radioButtonLeftHand);
            this.groupBoxHandSelection.Controls.Add(this.radioButtonRightHand);
            this.groupBoxHandSelection.Controls.Add(this.selectHandCombo);
            this.groupBoxHandSelection.ForeColor = System.Drawing.Color.White;
            this.groupBoxHandSelection.Location = new System.Drawing.Point(12, 157);
            this.groupBoxHandSelection.Name = "groupBoxHandSelection";
            this.groupBoxHandSelection.Size = new System.Drawing.Size(376, 62);
            this.groupBoxHandSelection.TabIndex = 40;
            this.groupBoxHandSelection.TabStop = false;
            this.groupBoxHandSelection.Text = "Hand Selection";
            // 
            // labelEnroll
            // 
            this.labelEnroll.AutoSize = true;
            this.labelEnroll.Location = new System.Drawing.Point(121, 23);
            this.labelEnroll.Name = "labelEnroll";
            this.labelEnroll.Size = new System.Drawing.Size(130, 13);
            this.labelEnroll.TabIndex = 27;
            this.labelEnroll.Text = "Select A Hand To Enroll...";
            this.labelEnroll.Visible = false;
            // 
            // labelIdentify
            // 
            this.labelIdentify.AutoSize = true;
            this.labelIdentify.Location = new System.Drawing.Point(121, 69);
            this.labelIdentify.Name = "labelIdentify";
            this.labelIdentify.Size = new System.Drawing.Size(171, 13);
            this.labelIdentify.TabIndex = 28;
            this.labelIdentify.Text = "Place Any Finger On The Sensor...";
            // 
            // UserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(1016, 534);
            this.Controls.Add(this.groupBoxHandSelection);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.AccessLevelGroupBox);
            this.Controls.Add(this.groupBoxAdmin);
            this.Controls.Add(this.groupBoxHand);
            this.Controls.Add(this.ModeRadioGroupBox);
            this.Controls.Add(this.DisplayPictureBox);
            this.Controls.Add(this.TestPictureBox);
            this.Controls.Add(this.statusStrip);
            this.Name = "UserInterface";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fingerprint Recognition";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ThumbPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IndexPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MiddlePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RingPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PinkyPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TestPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DisplayPictureBox)).EndInit();
            this.ModeRadioGroupBox.ResumeLayout(false);
            this.ModeRadioGroupBox.PerformLayout();
            this.AccessLevelGroupBox.ResumeLayout(false);
            this.AccessLevelGroupBox.PerformLayout();
            this.groupBoxHand.ResumeLayout(false);
            this.groupBoxHand.PerformLayout();
            this.groupBoxAdmin.ResumeLayout(false);
            this.groupBoxHandSelection.ResumeLayout(false);
            this.groupBoxHandSelection.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox selectHandCombo;
        private System.Windows.Forms.Button btnCaptureThumb;
        private System.Windows.Forms.Button btnCaptureIndex;
        private System.Windows.Forms.Button btnCaptureMiddle;
        private System.Windows.Forms.Button btnCaptureRing;
        private System.Windows.Forms.PictureBox ThumbPictureBox;
        private System.Windows.Forms.PictureBox IndexPictureBox;
        private System.Windows.Forms.PictureBox MiddlePictureBox;
        private System.Windows.Forms.PictureBox RingPictureBox;
        private System.Windows.Forms.PictureBox PinkyPictureBox;
        private System.Windows.Forms.Label statusStrip;
        private System.Windows.Forms.PictureBox TestPictureBox;
        private System.Windows.Forms.PictureBox DisplayPictureBox;
        private System.Windows.Forms.RadioButton EnrollRadioButton;
        private System.Windows.Forms.RadioButton VerifyRadioButton;
        private System.Windows.Forms.RadioButton IdentifyRadioButton;
        private System.Windows.Forms.GroupBox ModeRadioGroupBox;
        private System.Windows.Forms.RadioButton AdminRadioButton;
        private System.Windows.Forms.RadioButton TopRadioButton;
        private System.Windows.Forms.RadioButton MiddleRadioButton;
        public System.Windows.Forms.GroupBox AccessLevelGroupBox;
        public System.Windows.Forms.RadioButton LowRadioButton;
        private System.Windows.Forms.ComboBox AdminComboBox;
        private System.Windows.Forms.TextBox txtVerify;
        private System.Windows.Forms.ComboBox AccessLevelCombo;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lblThumb;
        private System.Windows.Forms.Label lblIndex;
        private System.Windows.Forms.Label lblMiddle;
        private System.Windows.Forms.Label lblRing;
        private System.Windows.Forms.Label lblPinky;
        private System.Windows.Forms.Button btnCapturePinky;
        private System.Windows.Forms.GroupBox groupBoxHand;
        public System.Windows.Forms.GroupBox groupBoxAdmin;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.RadioButton radioButtonRightHand;
        public System.Windows.Forms.RadioButton radioButtonLeftHand;
        private System.Windows.Forms.GroupBox groupBoxHandSelection;
        private System.Windows.Forms.Label labelIdentify;
        private System.Windows.Forms.Label labelEnroll;
    }
}


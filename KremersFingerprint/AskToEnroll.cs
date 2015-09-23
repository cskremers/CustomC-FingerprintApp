using System;
using System.Windows.Forms;

namespace KremersFingerprint
{
    public partial class AskToEnroll : Form
    {
        MainForm instance = new MainForm();

        public AskToEnroll()
        {
            InitializeComponent();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            MainForm.askFlag = true;
            instance.ClearInfo();
            Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            MainForm.askFlag = false;
            Close();
        }
    }
}

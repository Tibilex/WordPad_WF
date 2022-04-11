using System;
using System.Drawing;
using System.Windows.Forms;

namespace WordPad_WF
{
    public partial class AboutProgramForm : Form
    {
        public AboutProgramForm()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.HelpButton = false;
            this.Size = new Size(400, 600);
            this.Text = Properties.Resources.aboutFormName;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            button1.Text = "Ok";

            button1.Click += Ok;
        }

        private void Ok(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

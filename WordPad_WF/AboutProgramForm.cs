using System;
using System.Diagnostics;
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
            this.Size = new Size(400, 400);
            this.Text = Properties.Resources.aboutFormName;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            button1.Text = "OkEventArgs";
            pictureBox1.BackgroundImage = Properties.Resources.windows_10_120px;
            pictureBox2.BackgroundImage = Properties.Resources.code_fork_120px;
            richTextBox1.Text = Properties.Resources.about;
            richTextBox1.ReadOnly = true;
            richTextBox1.BorderStyle = BorderStyle.None;

            richTextBox1.LinkClicked += GitLinkClickedEventArgs;
            button1.Click += OkEventArgs;
        }

        private void GitLinkClickedEventArgs(object sender, LinkClickedEventArgs e)
        {
            Process.Start("https://github.com/Tibilex/WordPad_WF/tree/work");
        }

        private void OkEventArgs(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}

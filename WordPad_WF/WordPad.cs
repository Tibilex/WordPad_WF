using System;
using System.Drawing;
using System.Windows.Forms;
using WordPad_WF.Controls;

namespace WordPad_WF
{
    public partial class WordPad : Form
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        SaveFileDialog saveFileDialog = new SaveFileDialog();

        IconsToolBar iconsToolBar;
        ToolStripMenuItem fileToolBar;
        MenuStrip menuStrip = new MenuStrip();
        CustomToolBarFile file = new CustomToolBarFile();

        public WordPad()
        {
            InitializeComponent();
            this.Name = Properties.Resources.formName;
            this.StartPosition = FormStartPosition.CenterScreen;
            //this.FormBorderStyle = FormBorderStyle.None;
        }
        private void WordPad_Load(object sender, EventArgs e)
        {
            this.Controls.Add(menuStrip);
            menuStrip.Items.Add(fileToolBar = new ToolStripMenuItem("Файл") { BackColor = Color.FromArgb(0, 128, 204), ForeColor = Color.White });
            Controls.Add(file);
            file.Visible = false;
            fileToolBar.MouseDown += FileTab;

            iconsToolBar = new IconsToolBar(this);
            iconsToolBar.open.Click += Open;
            iconsToolBar.save.Click += Save;
            iconsToolBar.undo.Click += Undo;
            iconsToolBar.redo.Click += Redo;
        }

        private void FileTab(object sender, EventArgs e)
        {
            if (file.Visible == true)
            {
                file.Visible = false;
            }
            else
            {
                file.Visible = true;
            }
        }

        private void Open(object sender, EventArgs e)
        {
            openFileDialog.Filter = "Файл RTF (*.rtf)|*.rtf|" +
                "Текстовый документ (*.txt)|*.txt|" +
                "Документ Office Open XML (*.docx)|*.docx|" +
                "Документ OpenDocument (*.odt)|*.odt";
            openFileDialog.ShowDialog();
        }
        private void Create(object sender, EventArgs e)
        {

        }
        private void Save(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "Файл RTF (*.rtf)|*.rtf|" +
                "Текстовый документ (*.txt)|*.txt|" +
                "Документ Office Open XML (*.docx)|*.docx|" +
                "Документ OpenDocument (*.odt)|*.odt";
            saveFileDialog.ShowDialog();
        }
        private void SaveAs(object sender, EventArgs e)
        {

        }
        private void Undo(object sender, EventArgs e) { SendKeys.Send("^z"); }
        private void Redo(object sender, EventArgs e) { SendKeys.Send("^y"); }
 
        private void SendMail(object sender, EventArgs e)
        {

        }
        private void QuickPrint(object sender, EventArgs e)
        {

        }

    }
}

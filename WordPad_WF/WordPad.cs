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
        ToolStripMenuItem MainToolBar;
        ToolStripMenuItem ViewToolBar;
        MenuStrip menuStrip = new MenuStrip();
        CustomToolBarFile file = new CustomToolBarFile();
        CustomToolBarMain main = new CustomToolBarMain();
        CustomToolBarView view = new CustomToolBarView();

        public WordPad()
        {
            InitializeComponent();
            this.Name = Properties.Resources.formName;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(1000, 1000);
            //this.FormBorderStyle = FormBorderStyle.None;
        }
        private void WordPad_Load(object sender, EventArgs e)
        {

            this.Controls.Add(menuStrip);
            menuStrip.Items.Add(fileToolBar = new ToolStripMenuItem("Файл") { BackColor = Color.FromArgb(0, 128, 204), ForeColor = Color.White });
            menuStrip.Items.Add(MainToolBar = new ToolStripMenuItem("Главная") { Padding = new Padding(1, 1, 1, 3) });
            menuStrip.Items.Add(ViewToolBar = new ToolStripMenuItem("Вид") { Padding = new Padding(1, 1, 1, 3) });

            Controls.Add(file);
            Controls.Add(main);
            Controls.Add(view);

            file.Visible = false;
            main.Visible = true;
            view.Visible = false;

            file.buttonExit.Click += Exit;
            fileToolBar.MouseDown += FileTab;
            MainToolBar.MouseDown += MainTab;
            ViewToolBar.MouseDown += ViewTab;

            iconsToolBar = new IconsToolBar(this);
            iconsToolBar.open.Click += Open;
            iconsToolBar.save.Click += Save;
            iconsToolBar.undo.Click += Undo;
            iconsToolBar.redo.Click += Redo;
            iconsToolBar.exit.Click += Exit;

        }

        private void ViewTab(object sender, MouseEventArgs e)
        {
            main.Visible = false;
            view.Visible = true;              
        }

        private void MainTab(object sender, MouseEventArgs e)
        {
            main.Visible = true;
            view.Visible = false;
        }

        private void FileTab(object sender, EventArgs e)
        {
            if (file.Visible == true)
            {
                file.Visible = false;
            }
            else { file.Visible = true; }
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
        private void Exit(object sender, EventArgs e)
        {

            DialogResult = MessageBox.Show($"Вы хотите сохранить изменения в", "WordPad", MessageBoxButtons.YesNoCancel);
            if (DialogResult == DialogResult.Yes) 
            { 
                saveFileDialog.Filter = "Файл RTF (*.rtf)|*.rtf|" +
                    "Текстовый документ (*.txt)|*.txt|" +
                    "Документ Office Open XML (*.docx)|*.docx|" +
                    "Документ OpenDocument (*.odt)|*.odt";
                saveFileDialog.ShowDialog();
            }
            if (DialogResult == DialogResult.No) { this.Close(); }
        }

    }
}

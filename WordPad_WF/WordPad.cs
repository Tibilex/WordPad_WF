﻿using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using WordPad_WF.Controls;

namespace WordPad_WF
{
    public partial class WordPad : Form
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        PrintDialog printDialog = new PrintDialog();
        PrintDocument printDoc = new PrintDocument();

        IconsToolBar iconsToolBar;
        ToolStripMenuItem fileToolBar;
        ToolStripMenuItem MainToolBar;
        ToolStripMenuItem ViewToolBar;
        AboutProgramForm aboutProgramForm;

        MenuStrip menuStrip = new MenuStrip();
        CustomToolBarFile file = new CustomToolBarFile();
        CustomToolBarMain main = new CustomToolBarMain();
        CustomToolBarView view = new CustomToolBarView();
        CustomTetxBox CustomTetxBox = new CustomTetxBox();

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

            file.buttonOpen.Click += Open;
            file.buttonCreate.Click += Create;
            file.buttonSave.Click += Save;
            file.buttonSaveAs.Click += SaveAs;
            file.buttonPrint.Click += QuickPrint;
            file.buttonSendMail.Click += SendMail;
            file.buttonExit.Click += Exit;
            file.buttonAbout.Click += AboutProgram;

            fileToolBar.MouseDown += FileTab;
            MainToolBar.MouseDown += MainTab;
            ViewToolBar.MouseDown += ViewTab;
            

            this.Controls.Add(CustomTetxBox);
            iconsToolBar = new IconsToolBar(this);
            iconsToolBar.open.Click += Open;
            iconsToolBar.save.Click += Save;
            iconsToolBar.undo.Click += Undo;
            iconsToolBar.redo.Click += Redo;
            iconsToolBar.sendMail.Click += SendMail;
            iconsToolBar.quickPrint.Click += QuickPrint;
            iconsToolBar.exit.Click += Exit;
            iconsToolBar.minimize.Click += Minimize;
            iconsToolBar.maximize.Click += Maximize;
            iconsToolBar.reestablish.Click += Reestablish;
        }

        private void AboutProgram(object sender, EventArgs e)
        {
            aboutProgramForm = new AboutProgramForm();
            aboutProgramForm.ShowDialog();
            file.Visible = false;
        }

        private void Reestablish(object sender, EventArgs e) { this.WindowState = FormWindowState.Normal; }      
        private void Maximize(object sender, EventArgs e) { this.WindowState = FormWindowState.Maximized; }
        private void Minimize(object sender, EventArgs e) { this.WindowState = FormWindowState.Minimized; }

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
                openFileDialog.Filter = "Все документы WordPad (*.rtf,*.docx,*.odt,*.txt)|*.rtf;*.docx;*.odt;*.txt|" +
                "Файл RTF (*.rtf)|*.rtf|" +
                "Текстовый документ (*.txt)|*.txt|" +
                "Документ Office Open XML (*.docx)|*.docx|" +
                "Документ OpenDocument (*.odt)|*.odt";
                openFileDialog.ShowDialog();

            string fileNameCheck = openFileDialog.FileName;

            if (!String.IsNullOrEmpty(fileNameCheck))
            {
                iconsToolBar.name.Text = $"   {Path.GetFileName(openFileDialog.FileName)} - WordPad";
                file.fileHistory.SelectionStart = file.fileHistory.Text.Length;
                file.fileHistory.Text = $"   {Path.GetFileName(openFileDialog.FileName)}";
                CustomTetxBox.Text = File.ReadAllText(openFileDialog.FileName);
            }
            file.Visible = false;
        }
        private void Create(object sender, EventArgs e)
        {
            if (CustomTetxBox.Text != "")
            {
                DialogResult = MessageBox.Show($"Вы хотите сохранить изменения в", "WordPad", MessageBoxButtons.YesNo);
                if (DialogResult == DialogResult.Yes)
                {
                    SaveFileDialog();
                }
            }
            else { CustomTetxBox.Text = ""; }
            file.Visible = false;
        }
        private void Save(object sender, EventArgs e)
        {          
            if (CustomTetxBox.Text != "")
            {
                string path = openFileDialog.FileName;
                if (path != "")
                {
                    File.WriteAllText(path, CustomTetxBox.Text);
                    string[] array = path.Split('\\');
                    CustomTetxBox.Text = array[array.Length - 1];
                }
            }
            file.Visible = false;
        }
        private void SaveAs(object sender, EventArgs e)
        {
            SaveFileDialog();
            file.Visible = false;
        }
        private void Undo(object sender, EventArgs e) { SendKeys.Send("^z"); }
        private void Redo(object sender, EventArgs e) { SendKeys.Send("^y"); }
 
        private void SendMail(object sender, EventArgs e)
        {
            MessageBox.Show($"Команда \"отправить\" не смогла отправить сообщение", "WordPad",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            file.Visible = false;
        }

        private void QuickPrint(object sender, EventArgs e)
        {
            printDoc.DocumentName = iconsToolBar.name.Text;
            printDialog.Document = printDoc;
            printDialog.AllowSelection = true;
            printDialog.AllowSomePages = true;
            if (printDialog.ShowDialog() == DialogResult.OK) printDoc.Print();
            file.Visible = false;
        }
        private void Exit(object sender, EventArgs e)
        {
            SendKeys.SendWait("%{F4}");
            DialogResult = MessageBox.Show($"Вы хотите сохранить изменения в", "WordPad", MessageBoxButtons.YesNoCancel);
            if (DialogResult == DialogResult.Yes)
            {
                SaveFileDialog();
            }
            if (DialogResult == DialogResult.No) { this.Close(); }
        }
        private void SaveFileDialog()
        {
            if (CustomTetxBox.Text != "")
            {
                saveFileDialog.Filter = "Файл RTF (*.rtf)|*.rtf|" +
                "Текстовый документ (*.txt)|*.txt|" +
                "Документ Office Open XML (*.docx)|*.docx|" +
                "Документ OpenDocument (*.odt)|*.odt";
                saveFileDialog.ShowDialog();

                string path = saveFileDialog.FileName;
                if (path != "")
                {
                    File.WriteAllText(path, CustomTetxBox.Text);
                    string[] array = path.Split('\\');
                    CustomTetxBox.Text = array[array.Length - 1];
                }
                saveFileDialog.Reset();
            }
            file.Visible = false;
        }
    }
}

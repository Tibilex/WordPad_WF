using System;
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
        CustomTextBox CustomTextBox = new CustomTextBox();

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
            file.buttonSaveAs.MouseHover += EnterSaveAsFormat;
            file.saveFileFormatButtons.MouseLeave += LeaveSaveAsFormat;

            fileToolBar.MouseDown += FileTab;
            MainToolBar.MouseDown += MainTab;
            ViewToolBar.MouseDown += ViewTab;

            CustomTextBox.MouseUp += FontFormat;
            main.fontBold.Click += BoldFont;
            main.fontItalic.Click += ItalicFont;
            main.fontUnderline.Click += UnderlineFont;
            main.fontName.SelectedIndexChanged += FontName;
            main.fontSize.SelectedIndexChanged += FontSize;

            this.Controls.Add(CustomTextBox);
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


        #region - Font Style -
        private void FontSize(object sender, EventArgs e)
        {
            Font SelectedCurrentFont = CustomTextBox.SelectionFont;
            float newSize = (float)Convert.ToDouble(main.fontSize.SelectedItem);
            CustomTextBox.SelectionFont = new Font(SelectedCurrentFont.FontFamily, newSize, SelectedCurrentFont.Style);
        }

        private void FontName(object sender, EventArgs e)
        {
            Font SelectedCurrentFont = CustomTextBox.SelectionFont;
            CustomTextBox.SelectionFont = new Font(main.fontName.SelectedItem.ToString(), SelectedCurrentFont.Size, SelectedCurrentFont.Style);
        }

        private void UnderlineFont(object sender, EventArgs e)
        {
            if (CustomTextBox.SelectionFont != null)
            {
                Font currentFont = CustomTextBox.SelectionFont;
                FontStyle newFontStyle;

                if (CustomTextBox.SelectionFont.Underline == true)
                {
                    newFontStyle = FontStyle.Regular;
                }
                else
                {
                    newFontStyle = FontStyle.Underline;
                }
                CustomTextBox.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
            }
        }

        private void ItalicFont(object sender, EventArgs e)
        {
            if (CustomTextBox.SelectionFont != null)
            {
                Font currentFont = CustomTextBox.SelectionFont;
                FontStyle newFontStyle;

                if (CustomTextBox.SelectionFont.Italic == true)
                {
                    newFontStyle = FontStyle.Regular;
                }
                else
                {
                    newFontStyle = FontStyle.Italic;
                }
                CustomTextBox.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
            }
        }

        private void BoldFont(object sender, EventArgs e)
        {
            Font currentFont = CustomTextBox.SelectionFont;
            FontStyle newFontStyle;

            if (CustomTextBox.SelectionFont.Bold == true)
            {
                newFontStyle = FontStyle.Regular;
            }
            else
            {
                newFontStyle = FontStyle.Bold;
            }
            CustomTextBox.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
        }

        private void FontFormat(object sender, MouseEventArgs e)
        {
            Font SelectedCurrentFont = CustomTextBox.SelectionFont;
            main.fontSize.SelectedItem = SelectedCurrentFont.Size.ToString();
            main.fontName.SelectedItem = SelectedCurrentFont.Name.ToString();
            main.fontBold.Checked = false;
            main.fontItalic.Checked = false;
            main.fontUnderline.Checked = false;
            switch (SelectedCurrentFont.Style.ToString())
            {
                case "Bold":
                    main.fontBold.Checked = true;
                    break;
                case "Italic":
                    main.fontItalic.Checked = true;
                    break;
                case "Underline":
                    main.fontUnderline.Checked = true;
                    break;
                case "Strikeout":
                    break;
                case "Regular":
                    break;
            }
        }
        #endregion

        private void EnterSaveAsFormat(object sender, EventArgs e)
        {
            file.lastDocLabel.Visible = false;
            file.openDocumentList.Visible = false;
            file.saveFileFormatLabel.Visible = true;
            file.saveFileFormatButtons.Visible = true;
        }
        private void LeaveSaveAsFormat(object sender, EventArgs e)
        {
            file.lastDocLabel.Visible = true;
            file.openDocumentList.Visible = true;
            file.saveFileFormatLabel.Visible = false;
            file.saveFileFormatButtons.Visible = false;
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
                CustomTextBox.Text = File.ReadAllText(openFileDialog.FileName);
            }
            file.Visible = false;
        }
        private void Create(object sender, EventArgs e)
        {
            if (CustomTextBox.Text != "")
            {
                DialogResult = MessageBox.Show($"Вы хотите сохранить изменения в", "WordPad", MessageBoxButtons.YesNo);
                if (DialogResult == DialogResult.Yes)
                {
                    SaveFileDialog();
                }
            }
            else { CustomTextBox.Text = ""; }
            file.Visible = false;
        }
        private void Save(object sender, EventArgs e)
        {          
            if (CustomTextBox.Text != "")
            {
                string path = openFileDialog.FileName;
                if (path != "")
                {
                    File.WriteAllText(path, CustomTextBox.Text);
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
            if (CustomTextBox.Text != "")
            {
                saveFileDialog.Filter = "Файл RTF (*.rtf)|*.rtf|" +
                "Текстовый документ (*.txt)|*.txt|" +
                "Документ Office Open XML (*.docx)|*.docx|" +
                "Документ OpenDocument (*.odt)|*.odt";
                saveFileDialog.ShowDialog();

                string path = saveFileDialog.FileName;
                if (path != "")
                {
                    File.WriteAllText(path, CustomTextBox.Text);
                }
                saveFileDialog.Reset();
            }
            file.Visible = false;
        }
    }
}

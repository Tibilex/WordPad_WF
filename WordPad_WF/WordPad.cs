using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using WordPad_WF.Controls;

namespace WordPad_WF
{
    public partial class WordPad : Form
    {
        #region - Objects & Fields -
        OpenFileDialog openFileDialog = new OpenFileDialog();
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        PrintDialog printDialog = new PrintDialog();
        PrintDocument printDoc = new PrintDocument();
        ColorDialog ColorDialog = new ColorDialog();

        IconsToolBar iconsToolBar;
        ToolStripMenuItem fileToolBar;
        ToolStripMenuItem MainToolBar;
        ToolStripMenuItem ViewToolBar;
        AboutProgramForm aboutProgramForm;
        OnPaintButtons closeWindow;
        OnPaintButtons minimizeWindow;
        OnPaintButtons maximazeWindow;
        OnPaintButtons ReestablishWindow;

        MenuStrip menuStrip = new MenuStrip();
        CustomToolBarFile file = new CustomToolBarFile();
        CustomToolBarMain main = new CustomToolBarMain();
        CustomToolBarView view = new CustomToolBarView();
        CustomTextBox CustomTextBox = new CustomTextBox();

        const int cGrip = 16;
        const int cCaption = 32;
        string fileFormatFilter = "Файл RTF (*.rtf)|*.rtf|" +
                "Текстовый документ (*.txt)|*.txt|" +
                "Документ Office Open XML (*.docx)|*.docx|" +
                "Документ OpenDocument (*.odt)|*.odt";
        string imageFormatFilter = 
            $"Все файлы изображений (*.BMP,*.DIB,*.RLE,*.JPG,*.JPEG,*.JPE,*.JFIF,*.GIF,*.EMF,*.WMF,*.TIFF,*.PNG,*.ICO)|*.bmp;*.dib;*.rle;*.jpg;*.jpeg;*.jpe;*.jfif;*.gif;*.emf;*.wmf;*.tiff;*.png;*.ico|" +
            "BMP (*.BMP,*.DIB,*.RLE)|*.bmp;*.dib;*.rle|" +
            "JPEG (*.JPG,*.JPEG,*.JPE,*.JFIF)|*.jpg;*.jpeg;*.jpe;*.jfif|" +
            "GIF (*.GIF)|*.gif|" + 
            "EMF (*.EMF)|*.emf|" + 
            "WMF (*.WMF)|*.wmf|" +
            "TIFF (*.TIFF)|*.tiff|" + 
            "PNG (*.PNG)|*.png|" + 
            "ICO (*.ICO)|*.ico|" + 
            "Все файлы (*.*)|*.*";
        #endregion

        #region - Form load - 
        public WordPad()
        {
            InitializeComponent();
            this.Name = Properties.Resources.formName;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(1000, 1000);
            this.FormBorderStyle = FormBorderStyle.None;
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
        }
        private void WordPad_Load(object sender, EventArgs e)
        {
            this.MouseDown += FormDrag;
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

            // Tab "File"
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
            file.buttonRTF.Click += SaveAs;
            file.buttonTXT.Click += SaveAs;
            file.buttonXML.Click += SaveAs;
            file.buttonOpenDoc.Click += SaveAs;
            file.buttonAllFormats.Click += SaveAs;

            fileToolBar.MouseDown += FileTab;
            MainToolBar.MouseDown += MainTab;
            ViewToolBar.MouseDown += ViewTab;

            CustomTextBox.MouseUp += FontFormat;

            // Tab "Main"
            main.buttonCut.Click += Cut;
            main.buttonCopy.Click += Copy;
            main.buttonPaste.Click += Paste;
            main.fontBold.Click += BoldFont;
            main.fontItalic.Click += ItalicFont;
            main.fontUnderline.Click += UnderlineFont;
            main.strikethrow.Click += StrikeOutFont;
            main.subscript.Click += Subscrypt;
            main.superscrypt.Click += SuperScrypt;
            main.fontName.SelectedIndexChanged += FontName;
            main.fontSize.SelectedIndexChanged += FontSize;
            main.fontColor.Click += FontColor;
            main.textSelectionСolor.Click += TextSelectedColor;
            main.fontSizeUp.Click += FontSizeUp;
            main.fontSizeDown.Click += FontSizeDown;
            main.buttonPaint.Click += OpenPaint;
            main.buttonPicture.Click += OpenImage;
            main.buttonSelectAll.Click += SelectAll;
            main.buttonSearch.Click += Search;
            main.buttonRepalace.Click += Replace;
            main.alignLeft.Click += AlignLeft;
            main.alignCenter.Click += AlignCenter;
            main.alignRight.Click += AlignRight;
            main.indent.Click += Indent;
            main.outdent.Click += Outdent;

            view.buttonZoomUp.Click += ZoomUp;
            view.buttonZoomDown.Click += ZoomDown;

            //Close, Minimize, Maximaze, Restore buttons
            this.Controls.Add(closeWindow = new OnPaintButtons(new Point(this.Width - 44, -2),
                Properties.Resources.Close_24px, new Size(44, 32), 13, 8, 16, 16, 28, 0, 44, 32));
            this.Controls.Add(maximazeWindow = new OnPaintButtons(new Point(this.Width - 90, -2),
                Properties.Resources.maximize_button_24px, new Size(44, 32), 13, 8, 16, 16, 28, 0, 44, 32));
            this.Controls.Add(ReestablishWindow = new OnPaintButtons(new Point(this.Width - 90, -2),
                Properties.Resources.restore_down_24px, new Size(44, 32), 13, 8, 16, 16, 28, 0, 44, 32));
            this.Controls.Add(minimizeWindow = new OnPaintButtons(new Point(this.Width - 136, -2),
                Properties.Resources.subtract_24px, new Size(44, 32), 13, 8, 16, 16, 28, 0, 44, 32));
            closeWindow.color = Color.Red;
            closeWindow.Click += Exit;
            minimizeWindow.Click += Minimize;
            maximazeWindow.Click += ReestablishMainWondow;
            ReestablishWindow.Click += ReestablishMainWondow;

            // Icon toolbar in left corner of the screen
            iconsToolBar = new IconsToolBar(this);
            iconsToolBar.MouseDown += FormDrag;
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

            this.Controls.Add(CustomTextBox);
        }

        #endregion

        #region - Events -

        #region - Font Style events -
        private void Outdent(object sender, EventArgs e) { AlignText(5); }
        private void Indent(object sender, EventArgs e) { AlignText(4); }
        private void AlignLeft(object sender, EventArgs e) { AlignText(1); }
        private void AlignCenter(object sender, EventArgs e) { AlignText(2); }
        private void AlignRight(object sender, EventArgs e) { AlignText(3); }
        // font size up combobox event
        private void FontSizeUp(object sender, EventArgs e)
        {
            if (main.fontSize.SelectedIndex != main.fontSize.Items.Count -1) 
            {
                main.fontSize.SelectedIndex++;
            }
        }
        // font size down combobox event
        private void FontSizeDown(object sender, EventArgs e)
        {
            if (main.fontSize.SelectedIndex != 0)
            {
                main.fontSize.SelectedIndex--;
            }
        }
        // Color BackGround Text event
        private void TextSelectedColor(object sender, EventArgs e)
        {
            DialogResult = ColorDialog.ShowDialog(this);
            if (DialogResult == DialogResult.OK)
            {
                CustomTextBox.SelectionBackColor = ColorDialog.Color;
            }
        }
        // Color Text event
        private void FontColor(object sender, EventArgs e)
        {
            DialogResult = ColorDialog.ShowDialog(this);
            if (DialogResult == DialogResult.OK)
            {
                CustomTextBox.SelectionColor = ColorDialog.Color;
            }
        }
        // Font Size event
        private void FontSize(object sender, EventArgs e)
        {
            Font SelectedCurrentFont = CustomTextBox.SelectionFont;
            float newSize = (float)Convert.ToDouble(main.fontSize.SelectedItem);
            CustomTextBox.SelectionFont = new Font(SelectedCurrentFont.FontFamily, newSize, SelectedCurrentFont.Style);
        }
        // Font name event
        private void FontName(object sender, EventArgs e)
        {
            Font SelectedCurrentFont = CustomTextBox.SelectionFont;
            CustomTextBox.SelectionFont = new Font(main.fontName.SelectedItem.ToString(), SelectedCurrentFont.Size, SelectedCurrentFont.Style);
        }
        // Underline text format event
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
        // Strikeout text format event
        private void StrikeOutFont(object sender, EventArgs e)
        {
            if (CustomTextBox.SelectionFont != null)
            {
                Font currentFont = CustomTextBox.SelectionFont;
                FontStyle newFontStyle;

                if (CustomTextBox.SelectionFont.Strikeout == true)
                {
                    newFontStyle = FontStyle.Regular;
                }
                else
                {
                    newFontStyle = FontStyle.Strikeout;
                }
                CustomTextBox.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
            }
        }
        // Italic text format event
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
        // Bold text format event
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
        // Superscrypt text format event
        private void SuperScrypt(object sender, EventArgs e)
        {
            CustomTextBox.SelectionCharOffset = 8;
        }
        // Subscrypt text format event
        private void Subscrypt(object sender, EventArgs e)
        {
            CustomTextBox.SelectionCharOffset = -8;
        }
        // font format event
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

        #region - Form events -
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x84)
            {
                Point pos = new Point(m.LParam.ToInt32());
                pos = this.PointToClient(pos);
                if (pos.Y < cCaption)
                {
                    m.Result = (IntPtr)2;
                    return;
                }

                if (pos.X >= this.ClientSize.Width - cGrip && pos.Y >= this.ClientSize.Height - cGrip)
                {
                    m.Result = (IntPtr)17;
                    return;
                }
            }
            base.WndProc(ref m);
        }
        private void FormDrag(object sender, MouseEventArgs e)
        {
            base.Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }
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

        private void Reestablish(object sender, EventArgs e) { this.WindowState = FormWindowState.Normal;}
        private void ReestablishMainWondow(object sender, EventArgs e)
        {
            if (maximazeWindow.Visible == true)
            {
                maximazeWindow.Visible = false;
                this.WindowState = FormWindowState.Maximized;
            }
            else if (ReestablishWindow.Visible == true)
            {
                ReestablishWindow.Visible = false;
                maximazeWindow.Visible = true;
                this.WindowState = FormWindowState.Normal;
            }
            ReestablishWindow.Visible = true;
        }
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
        private void Exit(object sender, EventArgs e)
        {
            if (CustomTextBox.Text != "")
            {
                SendKeys.SendWait("%{F4}");
                DialogResult = MessageBox.Show($"Вы хотите сохранить изменения в", "WordPad", MessageBoxButtons.YesNoCancel);
                if (DialogResult == DialogResult.Yes)
                {
                    SaveFileDialog(fileFormatFilter);
                }
                if (DialogResult == DialogResult.No) { this.Close(); }
            }
            this.Close();
        }
        #endregion

        #region - Tools events -
        private void ZoomUp(object sender, EventArgs e)
        {

        }
        private void ZoomDown(object sender, EventArgs e)
        {

        }
        private void Search(object sender, EventArgs e)
        {
            
        }

        private void Replace(object sender, EventArgs e)
        {
            
        }

        private void SelectAll(object sender, EventArgs e) { SendKeys.Send("^a"); }
        private void OpenPaint(object sender, EventArgs e)
        {
            
        }
        private void OpenImage(object sender, EventArgs e)
        {

            openFileDialog.Filter = imageFormatFilter;
            openFileDialog.ShowDialog();
            string fileNameCheck = openFileDialog.FileName;

            if (!String.IsNullOrEmpty(fileNameCheck))
            {
                Image img = Image.FromFile(openFileDialog.FileName);
                Clipboard.Clear();
                Clipboard.SetImage(img);
                CustomTextBox.Paste();
                Clipboard.Clear();
            }
        }
        private void Cut(object sender, EventArgs e) { SendKeys.Send("^x"); }
        private void Copy(object sender, EventArgs e) { SendKeys.Send("^c"); }
        private void Paste(object sender, EventArgs e) { SendKeys.Send("^v"); }
        private void Open(object sender, EventArgs e)
        {
            openFileDialog.Filter = $"Все документы WordPad (*.rtf,*.docx,*.odt,*.txt)|*.rtf;*.docx;*.odt;*.txt|{fileFormatFilter}";
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
                    SaveFileDialog(fileFormatFilter);
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
            SaveFileDialog(fileFormatFilter);
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

        #endregion

        #endregion

        private void SaveFileDialog(string filter)
        {
            if (CustomTextBox.Text != "")
            {
                saveFileDialog.Filter = filter;
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

        private void AlignText(int num)
        {
            switch (num)
            {
                case 1:
                    CustomTextBox.SelectAll();
                    CustomTextBox.SelectionAlignment = HorizontalAlignment.Left;
                    break;
                case 2:
                    CustomTextBox.SelectAll();
                    CustomTextBox.SelectionAlignment = HorizontalAlignment.Center;
                    break;
                case 3:
                    CustomTextBox.SelectAll();
                    CustomTextBox.SelectionAlignment = HorizontalAlignment.Right;
                    break;
                case 4:
                    CustomTextBox.SelectAll();
                    CustomTextBox.SelectionIndent = 0;
                    break;
                case 5:
                    CustomTextBox.SelectAll();
                    CustomTextBox.SelectionIndent = 40;
                    break;
                default:
                    break;
            }
        }
    }
}

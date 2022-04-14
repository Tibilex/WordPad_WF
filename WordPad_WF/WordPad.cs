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
        private OpenFileDialog _openFileDialog = new OpenFileDialog();
        private SaveFileDialog _saveFileDialog = new SaveFileDialog();
        private PrintDialog _printDialog = new PrintDialog();
        private PrintDocument _printDocument = new PrintDocument();
        private ColorDialog _colorDialog = new ColorDialog();

        private IconsToolBar _iconsToolBar;
        private ToolStripMenuItem _fileToolBar;
        private ToolStripMenuItem _mainToolBar;
        private ToolStripMenuItem _viewToolBar;
        private AboutProgramForm _aboutProgramForm;
        private OnPaintButtons _closeWindow;
        private OnPaintButtons _minimizeWindow;
        private OnPaintButtons _maximazeWindow;
        private OnPaintButtons _restoreWindow;

        private MenuStrip _menuStrip = new MenuStrip();
        private CustomToolBarFile _fileTab = new CustomToolBarFile();
        private CustomToolBarMain _mainTab = new CustomToolBarMain();
        private CustomToolBarView _viewTab = new CustomToolBarView();
        private CustomTextBox _customTextBox = new CustomTextBox();

        private const int c_grip = 16;
        private const int c_caption = 32;
        private string _fileFormatFilter = "Файл RTF (*.rtf)|*.rtf|" +
                "Текстовый документ (*.txt)|*.txt|" +
                "Документ Office OpenEventArgs XML (*.docx)|*.docx|" +
                "Документ OpenDocument (*.odt)|*.odt";
        private string _imageFormatFilter = 
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
            this.MouseDown += FormDragMouseEventArgs;
            this.Controls.Add(_menuStrip);
            _menuStrip.Items.Add(_fileToolBar = new ToolStripMenuItem("Файл") { BackColor = Color.FromArgb(0, 128, 204), ForeColor = Color.White });
            _menuStrip.Items.Add(_mainToolBar = new ToolStripMenuItem("Главная") { Padding = new Padding(1, 1, 1, 3) });
            _menuStrip.Items.Add(_viewToolBar = new ToolStripMenuItem("Вид") { Padding = new Padding(1, 1, 1, 3) });

            Controls.Add(_fileTab);
            Controls.Add(_mainTab);
            Controls.Add(_viewTab);

            _fileTab.Visible = false;
            _mainTab.Visible = true;
            _viewTab.Visible = false;

            // Tab "File"
            _fileTab.buttonOpen.Click += OpenEventArgs;
            _fileTab.buttonCreate.Click += CreateEventArgs;
            _fileTab.buttonSave.Click += SaveEventArgs;
            _fileTab.buttonSaveAs.Click += SaveAsEventArgs;
            _fileTab.buttonPrint.Click += QuickPrintEventArgs;
            _fileTab.buttonSendMail.Click += SendMailEventArgs;
            _fileTab.buttonExit.Click += ExitProgrammEventArgs;
            _fileTab.buttonAbout.Click += AboutProgramEventArgs;
            _fileTab.buttonSaveAs.MouseHover += EnterSaveAsFormatEventArgs;
            _fileTab.saveFileFormatButtons.MouseLeave += LeaveSaveAsFormatEventArgs;
            _fileTab.buttonRTF.Click += SaveAsEventArgs;
            _fileTab.buttonTXT.Click += SaveAsEventArgs;
            _fileTab.buttonXML.Click += SaveAsEventArgs;
            _fileTab.buttonOpenDoc.Click += SaveAsEventArgs;
            _fileTab.buttonAllFormats.Click += SaveAsEventArgs;

            _fileToolBar.MouseDown += FileTabEventArgs;
            _mainToolBar.MouseDown += MainTabMouseEventArgs;
            _viewToolBar.MouseDown += ViewTabMouseEventArgs;

            _customTextBox.MouseUp += FontFormatMouseEventArgs;

            // Tab "Main"
            _mainTab.buttonCut.Click += CutEventArgs;
            _mainTab.buttonCopy.Click += CopyEventArgs;
            _mainTab.buttonPaste.Click += PasteEventArgs;
            _mainTab.fontBold.Click += BoldFontEventArgs;
            _mainTab.fontItalic.Click += ItalicFontEventArgs;
            _mainTab.fontUnderline.Click += UnderlineFontEventArgs;
            _mainTab.strikethrow.Click += StrikeOutFontEventArgs;
            _mainTab.subscript.Click += SubscryptEventArgs;
            _mainTab.superscrypt.Click += SuperScryptEventArgs;
            _mainTab.fontName.SelectedIndexChanged += FontNameEventArgs;
            _mainTab.fontSize.SelectedIndexChanged += FontSizeEventArgs;
            _mainTab.fontColor.Click += FontColorEventArgs;
            _mainTab.textSelectionСolor.Click += TextSelectedColorEventArgs;
            _mainTab.fontSizeUp.Click += FontSizeUpEventArgs;
            _mainTab.fontSizeDown.Click += FontSizeDownEventArgs;
            _mainTab.buttonPaint.Click += OpenPaintEventArgs;
            _mainTab.buttonPicture.Click += OpenImageEventArgs;
            _mainTab.buttonSelectAll.Click += SelectAllEventArgs;
            _mainTab.buttonSearch.Click += SearchEventArgs;
            _mainTab.buttonRepalace.Click += ReplaceEventArgs;
            _mainTab.alignLeft.Click += AlignLeftEventArgs;
            _mainTab.alignCenter.Click += AlignCenterEventArgs;
            _mainTab.alignRight.Click += AlignRightEventArgs;
            _mainTab.indent.Click += IndentEventArgs;
            _mainTab.outdent.Click += OutdentEventArgs;

            _viewTab.buttonZoomUp.Click += ZoomUpEventArgs;
            _viewTab.buttonZoomDown.Click += ZoomDownEventArgs;

            //Close, Minimize, Maximaze, Restore buttons
            this.Controls.Add(_closeWindow = new OnPaintButtons(new Point(this.Width - 44, -2),
                Properties.Resources.Close_24px, new Size(44, 32), 13, 8, 16, 16, 28, 0, 44, 32));
            this.Controls.Add(_maximazeWindow = new OnPaintButtons(new Point(this.Width - 90, -2),
                Properties.Resources.maximize_button_24px, new Size(44, 32), 13, 8, 16, 16, 28, 0, 44, 32));
            this.Controls.Add(_restoreWindow = new OnPaintButtons(new Point(this.Width - 90, -2),
                Properties.Resources.restore_down_24px, new Size(44, 32), 13, 8, 16, 16, 28, 0, 44, 32));
            this.Controls.Add(_minimizeWindow = new OnPaintButtons(new Point(this.Width - 136, -2),
                Properties.Resources.subtract_24px, new Size(44, 32), 13, 8, 16, 16, 28, 0, 44, 32));
            _closeWindow.color = Color.Red;
            _closeWindow.Click += ExitProgrammEventArgs;
            _minimizeWindow.Click += MinimizeWindowEventArgs;
            _maximazeWindow.Click += RestoreMainWondowEventArgs;
            _restoreWindow.Click += RestoreMainWondowEventArgs;

            // Icon toolbar in left corner of the screen
            _iconsToolBar = new IconsToolBar(this);
            _iconsToolBar.MouseDown += FormDragMouseEventArgs;
            _iconsToolBar.open.Click += OpenEventArgs;
            _iconsToolBar.save.Click += SaveEventArgs;
            _iconsToolBar.undo.Click += UndoEventArgs;
            _iconsToolBar.redo.Click += RedoEventArgs;
            _iconsToolBar.sendMail.Click += SendMailEventArgs;
            _iconsToolBar.quickPrint.Click += QuickPrintEventArgs;
            _iconsToolBar.exit.Click += ExitProgrammEventArgs;
            _iconsToolBar.minimize.Click += MinimizeWindowEventArgs;
            _iconsToolBar.maximize.Click += MaximizeWindowEventArgs;
            _iconsToolBar.reestablish.Click += RestoreWindowEventArgs;

            this.Controls.Add(_customTextBox);
        }

        #endregion

        #region - Events -

        #region - Font Style events -
        private void OutdentEventArgs(object sender, EventArgs e) { AlignText(5); }
        private void IndentEventArgs(object sender, EventArgs e) { AlignText(4); }
        private void AlignLeftEventArgs(object sender, EventArgs e) { AlignText(1); }
        private void AlignCenterEventArgs(object sender, EventArgs e) { AlignText(2); }
        private void AlignRightEventArgs(object sender, EventArgs e) { AlignText(3); }
        // font size up combobox event
        private void FontSizeUpEventArgs(object sender, EventArgs e)
        {
            if (_mainTab.fontSize.SelectedIndex != _mainTab.fontSize.Items.Count -1) 
            {
                _mainTab.fontSize.SelectedIndex++;
            }
        }
        // font size down combobox event
        private void FontSizeDownEventArgs(object sender, EventArgs e)
        {
            if (_mainTab.fontSize.SelectedIndex != 0)
            {
                _mainTab.fontSize.SelectedIndex--;
            }
        }
        // Color BackGround Text event
        private void TextSelectedColorEventArgs(object sender, EventArgs e)
        {
            DialogResult = _colorDialog.ShowDialog(this);
            if (DialogResult == DialogResult.OK)
            {
                _customTextBox.SelectionBackColor = _colorDialog.Color;
            }
        }
        // Color Text event
        private void FontColorEventArgs(object sender, EventArgs e)
        {
            DialogResult = _colorDialog.ShowDialog(this);
            if (DialogResult == DialogResult.OK)
            {
                _customTextBox.SelectionColor = _colorDialog.Color;
            }
        }
        // Font Size event
        private void FontSizeEventArgs(object sender, EventArgs e)
        {
            Font SelectedCurrentFont = _customTextBox.SelectionFont;
            float newSize = (float)Convert.ToDouble(_mainTab.fontSize.SelectedItem);
            _customTextBox.SelectionFont = new Font(SelectedCurrentFont.FontFamily, newSize, SelectedCurrentFont.Style);
        }
        // Font name event
        private void FontNameEventArgs(object sender, EventArgs e)
        {
            Font SelectedCurrentFont = _customTextBox.SelectionFont;
            _customTextBox.SelectionFont = new Font(_mainTab.fontName.SelectedItem.ToString(), SelectedCurrentFont.Size, SelectedCurrentFont.Style);
        }
        // Underline text format event
        private void UnderlineFontEventArgs(object sender, EventArgs e)
        {
            if (_customTextBox.SelectionFont != null)
            {
                Font currentFont = _customTextBox.SelectionFont;
                FontStyle newFontStyle;

                if (_customTextBox.SelectionFont.Underline == true)
                {
                    newFontStyle = FontStyle.Regular;
                }
                else
                {
                    newFontStyle = FontStyle.Underline;
                }
                _customTextBox.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
            }
        }
        // Strikeout text format event
        private void StrikeOutFontEventArgs(object sender, EventArgs e)
        {
            if (_customTextBox.SelectionFont != null)
            {
                Font currentFont = _customTextBox.SelectionFont;
                FontStyle newFontStyle;

                if (_customTextBox.SelectionFont.Strikeout == true)
                {
                    newFontStyle = FontStyle.Regular;
                }
                else
                {
                    newFontStyle = FontStyle.Strikeout;
                }
                _customTextBox.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
            }
        }
        // Italic text format event
        private void ItalicFontEventArgs(object sender, EventArgs e)
        {
            if (_customTextBox.SelectionFont != null)
            {
                Font currentFont = _customTextBox.SelectionFont;
                FontStyle newFontStyle;

                if (_customTextBox.SelectionFont.Italic == true)
                {
                    newFontStyle = FontStyle.Regular;
                }
                else
                {
                    newFontStyle = FontStyle.Italic;
                }
                _customTextBox.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
            }
        }
        // Bold text format event
        private void BoldFontEventArgs(object sender, EventArgs e)
        {
            Font currentFont = _customTextBox.SelectionFont;
            FontStyle newFontStyle;

            if (_customTextBox.SelectionFont.Bold == true)
            {
                newFontStyle = FontStyle.Regular;
            }
            else
            {
                newFontStyle = FontStyle.Bold;
            }
            _customTextBox.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
        }
        // Superscrypt text format event
        private void SuperScryptEventArgs(object sender, EventArgs e)
        {
            _customTextBox.SelectionCharOffset = 8;
        }
        // Subscrypt text format event
        private void SubscryptEventArgs(object sender, EventArgs e)
        {
            _customTextBox.SelectionCharOffset = -8;
        }
        // font format event
        private void FontFormatMouseEventArgs(object sender, MouseEventArgs e)
        {
            Font SelectedCurrentFont = _customTextBox.SelectionFont;
            _mainTab.fontSize.SelectedItem = SelectedCurrentFont.Size.ToString();
            _mainTab.fontName.SelectedItem = SelectedCurrentFont.Name.ToString();
            _mainTab.fontBold.Checked = false;
            _mainTab.fontItalic.Checked = false;
            _mainTab.fontUnderline.Checked = false;
            switch (SelectedCurrentFont.Style.ToString())
            {
                case "Bold":
                    _mainTab.fontBold.Checked = true;
                    break;
                case "Italic":
                    _mainTab.fontItalic.Checked = true;
                    break;
                case "Underline":
                    _mainTab.fontUnderline.Checked = true;
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
                if (pos.Y < c_caption)
                {
                    m.Result = (IntPtr)2;
                    return;
                }

                if (pos.X >= this.ClientSize.Width - c_grip && pos.Y >= this.ClientSize.Height - c_grip)
                {
                    m.Result = (IntPtr)17;
                    return;
                }
            }
            base.WndProc(ref m);
        }
        private void FormDragMouseEventArgs(object sender, MouseEventArgs e)
        {
            base.Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }
        private void EnterSaveAsFormatEventArgs(object sender, EventArgs e)
        {
            _fileTab.lastDocLabel.Visible = false;
            _fileTab.openDocumentList.Visible = false;
            _fileTab.saveFileFormatLabel.Visible = true;
            _fileTab.saveFileFormatButtons.Visible = true;
        }
        private void LeaveSaveAsFormatEventArgs(object sender, EventArgs e)
        {
            _fileTab.lastDocLabel.Visible = true;
            _fileTab.openDocumentList.Visible = true;
            _fileTab.saveFileFormatLabel.Visible = false;
            _fileTab.saveFileFormatButtons.Visible = false;
        }

        private void AboutProgramEventArgs(object sender, EventArgs e)
        {
            _aboutProgramForm = new AboutProgramForm();
            _aboutProgramForm.ShowDialog();
            _fileTab.Visible = false;
        }

        private void RestoreWindowEventArgs(object sender, EventArgs e) { this.WindowState = FormWindowState.Normal;}
        private void RestoreMainWondowEventArgs(object sender, EventArgs e)
        {
            if (_maximazeWindow.Visible == true)
            {
                _maximazeWindow.Visible = false;
                this.WindowState = FormWindowState.Maximized;
            }
            else if (_restoreWindow.Visible == true)
            {
                _restoreWindow.Visible = false;
                _maximazeWindow.Visible = true;
                this.WindowState = FormWindowState.Normal;
            }
            _restoreWindow.Visible = true;
        }
        private void MaximizeWindowEventArgs(object sender, EventArgs e) { this.WindowState = FormWindowState.Maximized; }
        private void MinimizeWindowEventArgs(object sender, EventArgs e) { this.WindowState = FormWindowState.Minimized; }

        private void ViewTabMouseEventArgs(object sender, MouseEventArgs e)
        {
            _mainTab.Visible = false;
            _viewTab.Visible = true;              
        }

        private void MainTabMouseEventArgs(object sender, MouseEventArgs e)
        {
            _mainTab.Visible = true;
            _viewTab.Visible = false;
        }

        private void FileTabEventArgs(object sender, EventArgs e)
        {
            if (_fileTab.Visible == true)
            {
                _fileTab.Visible = false;
            }
            else { _fileTab.Visible = true; }
        }
        private void ExitProgrammEventArgs(object sender, EventArgs e)
        {
            if (_customTextBox.Text != "")
            {
                SendKeys.SendWait("%{F4}");
                DialogResult = MessageBox.Show($"Вы хотите сохранить изменения в", "WordPad", MessageBoxButtons.YesNoCancel);
                if (DialogResult == DialogResult.Yes)
                {
                    SaveFileDialog(_fileFormatFilter);
                }
                if (DialogResult == DialogResult.No) { this.Close(); }
            }
            this.Close();
        }
        #endregion

        #region - Tools events -
        private void ZoomUpEventArgs(object sender, EventArgs e)
        {

        }
        private void ZoomDownEventArgs(object sender, EventArgs e)
        {

        }
        private void SearchEventArgs(object sender, EventArgs e)
        {
            
        }

        private void ReplaceEventArgs(object sender, EventArgs e)
        {
            
        }

        private void SelectAllEventArgs(object sender, EventArgs e) { SendKeys.Send("^a"); }
        private void OpenPaintEventArgs(object sender, EventArgs e)
        {
            
        }
        private void OpenImageEventArgs(object sender, EventArgs e)
        {

            _openFileDialog.Filter = _imageFormatFilter;
            _openFileDialog.ShowDialog();
            string fileNameCheck = _openFileDialog.FileName;

            if (!String.IsNullOrEmpty(fileNameCheck))
            {
                Image img = Image.FromFile(_openFileDialog.FileName);
                Clipboard.Clear();
                Clipboard.SetImage(img);
                _customTextBox.Paste();
                Clipboard.Clear();
            }
        }
        private void CutEventArgs(object sender, EventArgs e) { SendKeys.Send("^x"); }
        private void CopyEventArgs(object sender, EventArgs e) { SendKeys.Send("^c"); }
        private void PasteEventArgs(object sender, EventArgs e) { SendKeys.Send("^v"); }
        private void OpenEventArgs(object sender, EventArgs e)
        {
            _openFileDialog.Filter = $"Все документы WordPad (*.rtf,*.docx,*.odt,*.txt)|*.rtf;*.docx;*.odt;*.txt|{_fileFormatFilter}";
            _openFileDialog.ShowDialog();

            string fileNameCheck = _openFileDialog.FileName;

            if (!String.IsNullOrEmpty(fileNameCheck))
            {
                _iconsToolBar.name.Text = $"   {Path.GetFileName(_openFileDialog.FileName)} - WordPad";
                _fileTab.fileHistory.SelectionStart = _fileTab.fileHistory.Text.Length;
                _fileTab.fileHistory.Text = $"   {Path.GetFileName(_openFileDialog.FileName)}";
                _customTextBox.Text = File.ReadAllText(_openFileDialog.FileName);
            }
            _fileTab.Visible = false;
        }
        private void CreateEventArgs(object sender, EventArgs e)
        {
            if (_customTextBox.Text != "")
            {
                DialogResult = MessageBox.Show($"Вы хотите сохранить изменения в", "WordPad", MessageBoxButtons.YesNo);
                if (DialogResult == DialogResult.Yes)
                {
                    SaveFileDialog(_fileFormatFilter);
                }
            }
            else { _customTextBox.Text = ""; }
            _fileTab.Visible = false;
        }
        private void SaveEventArgs(object sender, EventArgs e)
        {          
            if (_customTextBox.Text != "")
            {
                string path = _openFileDialog.FileName;
                if (path != "")
                {
                    File.WriteAllText(path, _customTextBox.Text);
                }
            }
            _fileTab.Visible = false;
        }
        private void SaveAsEventArgs(object sender, EventArgs e)
        {
            SaveFileDialog(_fileFormatFilter);
            _fileTab.Visible = false;
        }
        private void UndoEventArgs(object sender, EventArgs e) { SendKeys.Send("^z"); }
        private void RedoEventArgs(object sender, EventArgs e) { SendKeys.Send("^y"); }
 
        private void SendMailEventArgs(object sender, EventArgs e)
        {
            MessageBox.Show($"Команда \"отправить\" не смогла отправить сообщение", "WordPad",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            _fileTab.Visible = false;
        }

        private void QuickPrintEventArgs(object sender, EventArgs e)
        {
            _printDocument.DocumentName = _iconsToolBar.name.Text;
            _printDialog.Document = _printDocument;
            _printDialog.AllowSelection = true;
            _printDialog.AllowSomePages = true;
            if (_printDialog.ShowDialog() == DialogResult.OK) _printDocument.Print();
            _fileTab.Visible = false;
        }

        #endregion

        #endregion

        private void SaveFileDialog(string filter)
        {
            if (_customTextBox.Text != "")
            {
                _saveFileDialog.Filter = filter;
                _saveFileDialog.ShowDialog();

                string path = _saveFileDialog.FileName;
                if (path != "")
                {
                    File.WriteAllText(path, _customTextBox.Text);
                }
                _saveFileDialog.Reset();
            }
            _fileTab.Visible = false;
        }

        private void AlignText(int num)
        {
            switch (num)
            {
                case 1:
                    _customTextBox.SelectAll();
                    _customTextBox.SelectionAlignment = HorizontalAlignment.Left;
                    break;
                case 2:
                    _customTextBox.SelectAll();
                    _customTextBox.SelectionAlignment = HorizontalAlignment.Center;
                    break;
                case 3:
                    _customTextBox.SelectAll();
                    _customTextBox.SelectionAlignment = HorizontalAlignment.Right;
                    break;
                case 4:
                    _customTextBox.SelectAll();
                    _customTextBox.SelectionIndent = 0;
                    break;
                case 5:
                    _customTextBox.SelectAll();
                    _customTextBox.SelectionIndent = 40;
                    break;
                default:
                    break;
            }
        }
    }
}

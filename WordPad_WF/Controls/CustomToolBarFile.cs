using System;
using System.Drawing;
using System.Windows.Forms;

namespace WordPad_WF.Controls
{
    class CustomToolBarFile : Panel
    {
        #region - Objects -

        public CustomButton buttonCreate;
        public CustomButton buttonOpen;
        public CustomButton buttonSave;
        public CustomButton buttonSaveAs;
        public CustomButton buttonPrint;
        public CustomButton buttonSendMail;
        public CustomButton buttonAbout;
        public CustomButton buttonExit;
        public CustomButton buttonRTF;
        public CustomButton buttonXML;
        public CustomButton buttonOpenDoc;
        public CustomButton buttonTXT;
        public CustomButton buttonAllFormats;
        public RichTextBox fileHistory;

        private GroupBox _mainOptions;
        private GroupBox _printMailOptions;
        private GroupBox _aboutExitOptions;

        public GroupBox lastDocLabel;
        public GroupBox openDocumentList;
        public GroupBox saveFileFormatLabel;
        public GroupBox saveFileFormatButtons;

        #endregion

        public CustomToolBarFile()
        {
            this.Location = new Point(0, 54);
            this.Size = new Size(508, 350);
            this.BackColor = SystemColors.MenuBar;

            _mainOptions = new GroupBox()
            {
                Location = new Point(4, -5),
                Size = new Size(252, 180),
            };

            _mainOptions.Controls.Add(buttonCreate = new CustomButton("Создать", new Point(1, 8), Properties.Resources.create_40px));
            _mainOptions.Controls.Add(buttonOpen = new CustomButton("Открыть", new Point(1, 50), Properties.Resources.live_folder_40px));
            _mainOptions.Controls.Add(buttonSave = new CustomButton("Сохранить", new Point(1, 92), Properties.Resources.save_40px));
            _mainOptions.Controls.Add(buttonSaveAs = new CustomButton("Сохранить как", new Point(1, 134), Properties.Resources.save_as_40px));

            _printMailOptions = new GroupBox()
            {
                Location = new Point(4, _mainOptions.Height - 13),
                Size = new Size(252, 94),
            };
            _printMailOptions.Controls.Add(buttonPrint = new CustomButton("Печать", new Point(1, 8), Properties.Resources.print_40px));
            _printMailOptions.Controls.Add(buttonSendMail = new CustomButton("Отправить по эл. почте", new Point(1, 50), Properties.Resources.send_email_40px));

            _aboutExitOptions = new GroupBox()
            {
                Location = new Point(4, _mainOptions.Height + _printMailOptions.Height - 24),
                Size = new Size(252, 100),
            };
            _aboutExitOptions.Controls.Add(buttonAbout = new CustomButton("О программе", new Point(1, 12), Properties.Resources.info_40px));
            _aboutExitOptions.Controls.Add(buttonExit = new CustomButton("Выход", new Point(1, 54), Properties.Resources.Logout_40px));

            lastDocLabel = new GroupBox()
            {
                Location = new Point(255, -5),
                Size = new Size(252, 32),
                Visible = true
            };
            lastDocLabel.Controls.Add(new Label()
            {
                Text = "Последние Документы",
                Location = new Point(5, 12),
                AutoSize = false,
                Size = new Size(150, 15),
                Font = new Font("Arial", 9F, FontStyle.Bold),
                ForeColor = Color.Gray,

            });
            openDocumentList = new GroupBox()
            {
                Location = new Point(255, 20),
                Size = new Size(252, 330),
                Visible = true
            };
            fileHistory = new RichTextBox()
            {
                Text = "   Вы пока ничего не открывали",
                Multiline = true,
                Location = new Point(2, 10),
                Size = new Size(openDocumentList.Width - 5, openDocumentList.Height - 12),
                ReadOnly = true,
                BorderStyle = BorderStyle.None
            };

            openDocumentList.Controls.Add(fileHistory);

            saveFileFormatLabel = new GroupBox()
            {
                Location = new Point(255, -5),
                Size = new Size(252, 32),
                Visible = false
            };
            saveFileFormatLabel.Controls.Add(new Label()
            {
                Text = "Сохранение копии документа",
                Location = new Point(5, 12),
                AutoSize = false,
                Size = new Size(180, 15),
                Font = new Font("Arial", 9F, FontStyle.Bold),
                ForeColor = Color.Gray,

            });
            saveFileFormatButtons = new GroupBox()
            {
                Location = new Point(255, 20),
                Size = new Size(252, 330),
                Visible = false,                
            };
            saveFileFormatButtons.Controls.Add(buttonRTF = new CustomButton("Документ в формате RTF", new Point(1, 6), Properties.Resources.document_64px));
            saveFileFormatButtons.Controls.Add(buttonXML = new CustomButton("Документ OpenEventArgs Office XML", new Point(1, 48), Properties.Resources.xml_file_64px));
            saveFileFormatButtons.Controls.Add(buttonOpenDoc = new CustomButton("Текст OpenDocument", new Point(1, 90), Properties.Resources.doc_64px));
            saveFileFormatButtons.Controls.Add(buttonTXT = new CustomButton("Обычный Текст", new Point(1, 132), Properties.Resources.txt_64px));
            saveFileFormatButtons.Controls.Add(buttonAllFormats = new CustomButton("Другие форматы", new Point(1, 174), Properties.Resources.save_as_40px));

            this.Controls.Add(_mainOptions);
            this.Controls.Add(_printMailOptions);
            this.Controls.Add(_aboutExitOptions);
            this.Controls.Add(lastDocLabel);
            this.Controls.Add(openDocumentList);
            this.Controls.Add(saveFileFormatLabel);
            this.Controls.Add(saveFileFormatButtons);

        }

        
    }
}

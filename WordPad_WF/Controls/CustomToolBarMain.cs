using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WordPad_WF.Controls
{
    class CustomToolBarMain : Panel
    {
        #region - Objects - 
        private GroupBox _bufferSector;
        private GroupBox _fontSector;
        private GroupBox _paragraphSector;
        private GroupBox _insertSector;
        private GroupBox _editingSector;

        public ComboBox fontName;
        public ComboBox fontSize;

        public CheckBox fontBold;
        public CheckBox fontItalic;
        public CheckBox fontUnderline;
        public CheckBox strikethrow;

        public RadioButton subscript;
        public RadioButton superscrypt;

        public Button fontSizeUp;
        public Button fontSizeDown;
        public Button fontColor;
        public Button textSelectionСolor;
        public Button alignLeft;
        public Button alignRight;
        public Button alignCenter;
        public Button alignJustify;
        public Button paragraph;
        public Button checkList;
        public Button indent;
        public Button outdent;
        public Button updown;

        public OnPaintButtons buttonPaste;
        public OnPaintButtons buttonCopy;
        public OnPaintButtons buttonCut;
        public OnPaintButtons buttonPicture;
        public OnPaintButtons buttonPaint;
        public OnPaintButtons buttonDate;
        public OnPaintButtons buttonObject;
        public OnPaintButtons buttonRepalace;
        public OnPaintButtons buttonSelectAll;
        public OnPaintButtons buttonSearch;

        #endregion

        public CustomToolBarMain()
        {
            this.Location = new Point(0, 54);
            this.Size = new Size(1000, 100);
            this.BackColor = SystemColors.ControlLightLight;
            this.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            #region - Buffer Sector -
            _bufferSector = new GroupBox()
            {
                Location = new Point(0, -5),
                Size = new Size(160, 106),               
            };
            _bufferSector.Controls.Add(new Label()
            {
                AutoSize = false,
                Size = new Size(100, 15),
                Text = "Буфер обмена",
                Location = new Point(40, this.Height - 11)
            });

            buttonPaste = new OnPaintButtons(new Point(8, 12), Properties.Resources.paste_64px, new Size(50, 70), 4, 4, 40, 40, 0, 48, 54, 20);
            buttonPaste.Text = "Вставить";

            buttonCut = new OnPaintButtons(new Point(64, 14), Properties.Resources.cut_40px, new Size(80, 20), 2, 1, 18, 18, 24, 0, 80, 20);
            buttonCut.Text = "Вырезать";

            buttonCopy = new OnPaintButtons(new Point(64, 38), Properties.Resources.copy_64px, new Size(88, 20), 2, 1, 18, 18, 24, 0, 88, 20);
            buttonCopy.Text = "Копировать";


            _bufferSector.Controls.Add(buttonPaste);
            _bufferSector.Controls.Add(buttonCut);
            _bufferSector.Controls.Add(buttonCopy);
            #endregion

            #region - Font Sector - 
            _fontSector = new GroupBox()
            {
                Location = new Point(158, -5),
                Size = new Size(226, 106),
            };
            _fontSector.Controls.Add(new Label()
            {
                AutoSize = false,
                Size = new Size(100, 15),
                Text = "Шрифт",
                Location = new Point(95, this.Height - 11)
            });

            fontName = new ComboBox();
            fontName.Items.AddRange(FontFamily.Families.Select(f => f.Name).ToArray());
            fontName.Location = new Point(8, 20);
            fontName.SelectedItem = "Calibri";

            fontSize = new ComboBox();
            string[] FontSize = new string[] { "8", "9", "10", "11", "12", "14", "16", "18", "20", "22", "24", "26", "28", "36", "48", "72" };
            fontSize.Items.AddRange(FontSize);
            fontSize.SelectedIndex = 3;
            fontSize.Size = new Size(40, 21);
            fontSize.Location = new Point(128, 20);

            fontSizeUp = new Button()
            {
                Location = new Point(172, 19),
                Size = new Size(22, 22),
                BackgroundImage = Properties.Resources.increase_font_20px,
                BackgroundImageLayout = ImageLayout.Stretch
            };

            fontSizeDown = new Button()
            {
                Location = new Point(196, 19),
                Size = new Size(22, 22),
                BackgroundImage = Properties.Resources.decrease_font_20px,
                BackgroundImageLayout = ImageLayout.Stretch
            };

            fontBold = new CheckBox()
            {
                Appearance = Appearance.Button,
                Location = new Point(8, 45),
                Checked = false,
                Size = new Size(24, 24),
                BackgroundImage = Properties.Resources.bold_20px,
                BackgroundImageLayout = ImageLayout.Stretch
            };

            fontItalic = new CheckBox()
            {
                Appearance = Appearance.Button,
                Location = new Point(34, 45),
                Checked = false,
                Size = new Size(24, 24),
                BackgroundImage = Properties.Resources.italic_20px,
                BackgroundImageLayout = ImageLayout.Stretch
            };

            fontUnderline = new CheckBox()
            {
                Appearance = Appearance.Button,
                Location = new Point(60, 45),
                Checked = false,
                Size = new Size(24, 24),
                BackgroundImage = Properties.Resources.underline_20px,
                BackgroundImageLayout = ImageLayout.Stretch
            };

            strikethrow = new CheckBox()
            {
                Appearance = Appearance.Button,
                Location = new Point(86, 45),
                Checked = false,
                Size = new Size(24, 24),
                BackgroundImage = Properties.Resources.strikethrough_20px,
                BackgroundImageLayout = ImageLayout.Stretch
            };

            subscript = new RadioButton()
            {
                Appearance = Appearance.Button,
                Location = new Point(112, 45),
                Checked = false,
                Size = new Size(24, 24),
                BackgroundImage = Properties.Resources.subscript_20px,
                BackgroundImageLayout = ImageLayout.Stretch
            };

            superscrypt = new RadioButton()
            {
                Appearance = Appearance.Button,
                Location = new Point(138, 45),
                Checked = false,
                Size = new Size(24, 24),
                BackgroundImage = Properties.Resources.superscript_20px,
                BackgroundImageLayout = ImageLayout.Stretch
            };

            fontColor = new Button()
            {
                Location = new Point(164, 45),
                Size = new Size(24, 24),
                BackgroundImage = Properties.Resources.text_color_20px,
                BackgroundImageLayout = ImageLayout.Stretch
            };

            textSelectionСolor = new Button()
            {
                Location = new Point(190, 45),
                Size = new Size(24, 24),
                BackgroundImage = Properties.Resources.crayon_20px,
                BackgroundImageLayout = ImageLayout.Stretch
            };

            _fontSector.Controls.Add(fontName);
            _fontSector.Controls.Add(fontSize);
            _fontSector.Controls.Add(fontSizeUp);
            _fontSector.Controls.Add(fontSizeDown);
            _fontSector.Controls.Add(fontBold);
            _fontSector.Controls.Add(fontItalic);
            _fontSector.Controls.Add(fontUnderline);
            _fontSector.Controls.Add(strikethrow);
            _fontSector.Controls.Add(subscript);
            _fontSector.Controls.Add(superscrypt);
            _fontSector.Controls.Add(fontColor);
            _fontSector.Controls.Add(textSelectionСolor);
            
            #endregion

            #region - Paragraph Sector - 
            _paragraphSector = new GroupBox()
            {
                Location = new Point(382, -5),
                Size = new Size(128, 106),
            };
            _paragraphSector.Controls.Add(new Label()
            {
                AutoSize = false,
                Size = new Size(60, 15),
                Text = "Абзац",
                Location = new Point(45, this.Height - 11)
            });

            indent = new Button()
            {
                Location = new Point(16, 20),
                Size = new Size(24, 24),
                BackgroundImage = Properties.Resources.indent_20px,
                BackgroundImageLayout = ImageLayout.Stretch
            };

            outdent = new Button()
            {
                Location = new Point(40, 20),
                Size = new Size(24, 24),
                BackgroundImage = Properties.Resources.outdent_20px,
                BackgroundImageLayout = ImageLayout.Stretch
            };

            checkList = new Button()
            {
                Location = new Point(64, 20),
                Size = new Size(24, 24),
                BackgroundImage = Properties.Resources.checklist_20px,
                BackgroundImageLayout = ImageLayout.Stretch
            };

            updown = new Button()
            {
                Location = new Point(88, 20),
                Size = new Size(24, 24),
                BackgroundImage = Properties.Resources.up_down_arrow_20px,
                BackgroundImageLayout = ImageLayout.Stretch
            };

            alignLeft = new Button()
            {
                Location = new Point(4, 45),
                Size = new Size(24, 24),
                BackgroundImage = Properties.Resources.align_left_20px,
                BackgroundImageLayout = ImageLayout.Stretch
            };

            alignCenter = new Button()
            {
                Location = new Point(28, 45),
                Size = new Size(24, 24),
                BackgroundImage = Properties.Resources.align_center_20px,
                BackgroundImageLayout = ImageLayout.Stretch
            };

            alignRight = new Button()
            {
                Location = new Point(52, 45),
                Size = new Size(24, 24),
                BackgroundImage = Properties.Resources.align_right_20px,
                BackgroundImageLayout = ImageLayout.Stretch
            };

            alignJustify = new Button()
            {
                Location = new Point(76, 45),
                Size = new Size(24, 24),
                BackgroundImage = Properties.Resources.align_justify_20px,
                BackgroundImageLayout = ImageLayout.Stretch
            };

            paragraph = new Button()
            {
                Location = new Point(100, 45),
                Size = new Size(24, 24),
                BackgroundImage = Properties.Resources.paragraph_20px,
                BackgroundImageLayout = ImageLayout.Stretch
            };

            _paragraphSector.Controls.Add(indent);
            _paragraphSector.Controls.Add(outdent);
            _paragraphSector.Controls.Add(checkList);
            _paragraphSector.Controls.Add(updown);
            _paragraphSector.Controls.Add(alignLeft);
            _paragraphSector.Controls.Add(alignCenter);
            _paragraphSector.Controls.Add(alignRight);
            _paragraphSector.Controls.Add(alignJustify);
            _paragraphSector.Controls.Add(paragraph);
            #endregion

            #region - Insert Sector - 
            _insertSector = new GroupBox()
            {
                Location = new Point(508, -5),
                Size = new Size(226, 106),
            };
            _insertSector.Controls.Add(new Label()
            {
                AutoSize = false,
                Size = new Size(100, 15),
                Text = "Вставка",
                Location = new Point(96, this.Height - 11)
            });

            buttonPicture = new OnPaintButtons(new Point(4, 12), Properties.Resources.picture_64px, new Size(76, 70), 18, 4, 40, 40, 0, 40, 78, 28);
            buttonPicture.Text = "Изображение";

            buttonPaint = new OnPaintButtons(new Point(82, 12), Properties.Resources.paint_palette_64px, new Size(46, 70), 4, 4, 36, 36, 0, 44, 50, 30);
            buttonPaint.Text = "Рисунок Paint";

            buttonDate = new OnPaintButtons(new Point(130, 12), Properties.Resources.timetable_64px, new Size(46, 70), 6, 4, 36, 36, 2, 44, 50, 30);
            buttonDate.Text = "Дата и Время";

            buttonObject = new OnPaintButtons(new Point(177, 12), Properties.Resources.objects_40px, new Size(46, 70), 6, 4, 36, 36, 2, 40, 50, 26);
            buttonObject.Text = "Объект";

            _insertSector.Controls.Add(buttonPicture);
            _insertSector.Controls.Add(buttonPaint);
            _insertSector.Controls.Add(buttonDate);
            _insertSector.Controls.Add(buttonObject);
            #endregion

            #region - Editing Sector -
            _editingSector = new GroupBox()
            {
                Location = new Point(728, -5),
                Size = new Size(126, 106),
            };
            _editingSector.Controls.Add(new Label()
            {
                AutoSize = false,
                Size = new Size(60, 15),
                Text = "Правка",
                Location = new Point(45, this.Height - 11)
            });

            buttonSearch = new OnPaintButtons(new Point(12, 10), Properties.Resources.bino_40px, new Size(80, 24), 3, 3, 18, 18, 24, 2, 80, 20);
            buttonSearch.Text = "Поиск";

            buttonRepalace = new OnPaintButtons(new Point(12, 36), Properties.Resources.replace_64px, new Size(80, 24), 3, 3, 18, 18, 24, 2, 80, 20);
            buttonRepalace.Text = "Замена";

            buttonSelectAll = new OnPaintButtons(new Point(12, 62), Properties.Resources.select_all_64px, new Size(100, 24), 3, 3, 18, 18, 24, 2, 80, 20);
            buttonSelectAll.Text = "Выделить всё";

            _editingSector.Controls.Add(buttonSearch);
            _editingSector.Controls.Add(buttonRepalace);
            _editingSector.Controls.Add(buttonSelectAll);
            #endregion

            this.Controls.Add(_bufferSector);
            this.Controls.Add(_fontSector);
            this.Controls.Add(_paragraphSector);
            this.Controls.Add(_insertSector);
            this.Controls.Add(_editingSector);
        }
    }
}

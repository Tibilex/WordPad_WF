using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WordPad_WF.Controls
{
    class CustomToolBarMain : Panel
    {
        #region - Objects - 
        GroupBox bufferSector;
        GroupBox fontSector;
        GroupBox paragraphSector;
        GroupBox insertSector;
        GroupBox editingSector;

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

        #endregion

        public CustomToolBarMain()
        {
            this.Location = new Point(0, 54);
            this.Size = new Size(1000, 100);
            this.BackColor = SystemColors.ControlLightLight;
            this.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            #region - Buffer Sector -
            bufferSector = new GroupBox()
            {
                Location = new Point(0, -5),
                Size = new Size(160, 106),               
            };
            bufferSector.Controls.Add(new Label()
            {
                AutoSize = false,
                Size = new Size(100, 15),
                Text = "Буфер обмена",
                Location = new Point(40, this.Height - 11)
            });
            #endregion

            #region - Font Sector - 
            fontSector = new GroupBox()
            {
                Location = new Point(158, -5),
                Size = new Size(226, 106),
            };
            fontSector.Controls.Add(new Label()
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

            fontSizeUp = new Button();
            fontSizeUp.Location = new Point(172, 19);
            fontSizeUp.Size = new Size(22, 22);
            fontSizeUp.BackgroundImage = Properties.Resources.increase_font_20px;
            fontSizeUp.BackgroundImageLayout = ImageLayout.Stretch;

            fontSizeDown = new Button();
            fontSizeDown.Location = new Point(196, 19);
            fontSizeDown.Size = new Size(22, 22);
            fontSizeDown.BackgroundImage = Properties.Resources.decrease_font_20px;
            fontSizeDown.BackgroundImageLayout = ImageLayout.Stretch;

            fontBold = new CheckBox();
            fontBold.Appearance = Appearance.Button;
            fontBold.Checked = true;
            fontBold.Location = new Point(8, 45);
            fontBold.Checked = false;
            fontBold.Size = new Size(24, 24);
            fontBold.BackgroundImage = Properties.Resources.bold_20px;
            fontBold.BackgroundImageLayout = ImageLayout.Stretch;

            fontItalic = new CheckBox();
            fontItalic.Appearance = Appearance.Button;
            fontItalic.Location = new Point(34, 45);
            fontItalic.Checked = false;
            fontItalic.Size = new Size(24, 24);
            fontItalic.BackgroundImage = Properties.Resources.italic_20px;
            fontItalic.BackgroundImageLayout = ImageLayout.Stretch;

            fontUnderline = new CheckBox();
            fontUnderline.Appearance = Appearance.Button;
            fontUnderline.Location = new Point(60, 45);
            fontUnderline.Checked = false;
            fontUnderline.Size = new Size(24, 24);
            fontUnderline.BackgroundImage = Properties.Resources.underline_20px;
            fontUnderline.BackgroundImageLayout = ImageLayout.Stretch;

            strikethrow = new CheckBox();
            strikethrow.Appearance = Appearance.Button;
            strikethrow.Location = new Point(86, 45);
            strikethrow.Checked = false;
            strikethrow.Size = new Size(24, 24);
            strikethrow.BackgroundImage = Properties.Resources.strikethrough_20px;
            strikethrow.BackgroundImageLayout = ImageLayout.Stretch;

            subscript = new RadioButton();
            subscript.Appearance = Appearance.Button;
            subscript.Location = new Point(112, 45);
            subscript.Checked = false;
            subscript.Size = new Size(24, 24);
            subscript.BackgroundImage = Properties.Resources.subscript_20px;
            subscript.BackgroundImageLayout = ImageLayout.Stretch;

            superscrypt = new RadioButton();
            superscrypt.Appearance = Appearance.Button;
            superscrypt.Location = new Point(138, 45);
            superscrypt.Checked = false;
            superscrypt.Size = new Size(24, 24);
            superscrypt.BackgroundImage = Properties.Resources.superscript_20px;
            superscrypt.BackgroundImageLayout = ImageLayout.Stretch;

            fontColor = new Button();
            fontColor.Location = new Point(164, 45);
            fontColor.Size = new Size(24, 24);
            fontColor.BackgroundImage = Properties.Resources.text_color_20px;
            fontColor.BackgroundImageLayout = ImageLayout.Stretch;

            textSelectionСolor = new Button();
            textSelectionСolor.Location = new Point(190, 45);
            textSelectionСolor.Size = new Size(24, 24);
            textSelectionСolor.BackgroundImage = Properties.Resources.crayon_20px;
            textSelectionСolor.BackgroundImageLayout = ImageLayout.Stretch;

            fontSector.Controls.Add(fontName);
            fontSector.Controls.Add(fontSize);
            fontSector.Controls.Add(fontSizeUp);
            fontSector.Controls.Add(fontSizeDown);
            fontSector.Controls.Add(fontBold);
            fontSector.Controls.Add(fontItalic);
            fontSector.Controls.Add(fontUnderline);
            fontSector.Controls.Add(strikethrow);
            fontSector.Controls.Add(subscript);
            fontSector.Controls.Add(superscrypt);
            fontSector.Controls.Add(fontColor);
            fontSector.Controls.Add(textSelectionСolor);
            
            #endregion

            #region - Paragraph Sector - 
            paragraphSector = new GroupBox()
            {
                Location = new Point(382, -5),
                Size = new Size(128, 106),
            };
            paragraphSector.Controls.Add(new Label()
            {
                AutoSize = false,
                Size = new Size(60, 15),
                Text = "Абзац",
                Location = new Point(45, this.Height - 11)
            });
            #endregion

            #region - Insert Sector - 
            insertSector = new GroupBox()
            {
                Location = new Point(508, -5),
                Size = new Size(226, 106),
            };
            insertSector.Controls.Add(new Label()
            {
                AutoSize = false,
                Size = new Size(100, 15),
                Text = "Вставка",
                Location = new Point(96, this.Height - 11)
            });
            #endregion

            #region - Editing Sector -
            editingSector = new GroupBox()
            {
                Location = new Point(728, -5),
                Size = new Size(126, 106),
            };
            editingSector.Controls.Add(new Label()
            {
                AutoSize = false,
                Size = new Size(60, 15),
                Text = "Правка",
                Location = new Point(45, this.Height - 11)
            });
            #endregion

            this.Controls.Add(bufferSector);
            this.Controls.Add(fontSector);
            this.Controls.Add(paragraphSector);
            this.Controls.Add(insertSector);
            this.Controls.Add(editingSector);
        }
    }
}

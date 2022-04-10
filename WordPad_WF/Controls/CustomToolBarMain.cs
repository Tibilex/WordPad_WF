using System;
using System.Drawing;
using System.Windows.Forms;

namespace WordPad_WF.Controls
{
    class CustomToolBarMain : Panel
    {
        GroupBox groupBox;
        GroupBox groupBox2;
        GroupBox groupBox3;
        GroupBox groupBox4;
        GroupBox groupBox5;

        public CustomToolBarMain()
        {
            this.Location = new Point(0, 54);
            this.Size = new Size(1000, 100);
            this.BackColor = Color.FromArgb(235, 235, 235);
            //this.BackColor = Color.Gray;
            this.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            groupBox = new GroupBox()
            {
                Location = new Point(0, -5),
                Size = new Size(160, 106),               
            };
            groupBox.Controls.Add(new Label()
            {
                AutoSize = false,
                Size = new Size(100, 15),
                Text = "Буфер обмена",
                Location = new Point(40, this.Height - 11)
            });

            groupBox2 = new GroupBox()
            {
                Location = new Point(158, -5),
                Size = new Size(226, 106),
            };
            groupBox2.Controls.Add(new Label()
            {
                AutoSize = false,
                Size = new Size(100, 15),
                Text = "Шрифт",
                Location = new Point(95, this.Height - 11)
            });

            groupBox3 = new GroupBox()
            {
                Location = new Point(382, -5),
                Size = new Size(128, 106),
            };
            groupBox3.Controls.Add(new Label()
            {
                AutoSize = false,
                Size = new Size(60, 15),
                Text = "Абзац",
                Location = new Point(45, this.Height - 11)
            });

            groupBox4 = new GroupBox()
            {
                Location = new Point(508, -5),
                Size = new Size(226, 106),
            };
            groupBox4.Controls.Add(new Label()
            {
                AutoSize = false,
                Size = new Size(100, 15),
                Text = "Вставка",
                Location = new Point(95, this.Height - 11)
            });

            groupBox5 = new GroupBox()
            {
                Location = new Point(728, -5),
                Size = new Size(126, 106),
            };
            groupBox5.Controls.Add(new Label()
            {
                AutoSize = false,
                Size = new Size(60, 15),
                Text = "Правка",
                Location = new Point(45, this.Height - 11)
            });

            this.Controls.Add(groupBox);
            this.Controls.Add(groupBox2);
            this.Controls.Add(groupBox3);
            this.Controls.Add(groupBox4);
            this.Controls.Add(groupBox5);
        }
    }
}

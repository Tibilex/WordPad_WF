using System;
using System.Drawing;
using System.Windows.Forms;

namespace WordPad_WF.Controls
{
    internal class CustomToolBarView : Panel
    {
        GroupBox groupBox;
        GroupBox groupBox2;
        GroupBox groupBox3;

        public CustomToolBarView()
        {
            this.Location = new Point(0, 54);
            this.Size = new Size(1000, 100);
            this.BackColor = Color.FromArgb(235, 235, 235);
            //this.BackColor = Color.Gray;
            this.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            groupBox = new GroupBox()
            {
                Location = new Point(0, -5),
                Size = new Size(185, 106),
            };
            groupBox.Controls.Add(new Label()
            {
                AutoSize = false,
                Size = new Size(100, 15),
                Text = "Масштаб",
                Location = new Point(70, this.Height - 11)
            });

            groupBox2 = new GroupBox()
            {
                Location = new Point(158, -5),
                Size = new Size(180, 106),
            };
            groupBox2.Controls.Add(new Label()
            {
                AutoSize = true,
                Size = new Size(120, 15),
                Text = "Показать или скрыть",
                Location = new Point(45, this.Height - 11)
            });

            groupBox3 = new GroupBox()
            {
                Location = new Point(336, -5),
                Size = new Size(158, 106),
            };
            groupBox3.Controls.Add(new Label()
            {
                AutoSize = false,
                Size = new Size(60, 15),
                Text = "Параметры",
                Location = new Point(50, this.Height - 11)
            });

            this.Controls.Add(groupBox);
            this.Controls.Add(groupBox2);
            this.Controls.Add(groupBox3);
        }
    }
}

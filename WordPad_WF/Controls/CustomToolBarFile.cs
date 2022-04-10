using System;
using System.Drawing;
using System.Windows.Forms;

namespace WordPad_WF.Controls
{
    class CustomToolBarFile : Panel
    {
        CustomButton button;
        GroupBox groupBox;
        GroupBox groupBox2;
        GroupBox groupBox3;
        GroupBox groupBox4;
        GroupBox groupBox5;
        public CustomToolBarFile()
        {
            this.Location = new Point(0, 54);
            this.Size = new Size(600, 350);

            groupBox = new GroupBox()
            {
                Location = new Point(4, -5),
                Size = new Size(252, 180),
                Text = ""                
            };

            groupBox.Controls.Add(button = new CustomButton("Создать", new Point(1,8),Properties.Resources.create_40px));
            groupBox.Controls.Add(button = new CustomButton("Открыть", new Point(1,50),Properties.Resources.live_folder_40px));
            groupBox.Controls.Add(button = new CustomButton("Сохранить", new Point(1,92),Properties.Resources.save_40px));
            groupBox.Controls.Add(button = new CustomButton("Сохранить как", new Point(1,134),Properties.Resources.save_as_40px));

            groupBox2 = new GroupBox()
            {
                Location = new Point(4, groupBox.Height - 12),
                Size = new Size(252, 94),
                Text = ""
            };
            groupBox2.Controls.Add(button = new CustomButton("Печать", new Point(1, 8), Properties.Resources.print_40px));
            groupBox2.Controls.Add(button = new CustomButton("Отправить по эл. почте", new Point(1, 50), Properties.Resources.send_email_40px));
            groupBox3 = new GroupBox()
            {
                Location = new Point(4, groupBox.Height + groupBox2.Height - 24),
                Size = new Size(252, 100),
                Text = ""
            };
            groupBox3.Controls.Add(button = new CustomButton("О программе", new Point(1, 12), Properties.Resources.info_40px));
            groupBox3.Controls.Add(button = new CustomButton("Выход", new Point(1, 54), Properties.Resources.Logout_40px));

            groupBox4 = new GroupBox()
            {
                Location = new Point(255, -5),
                Size = new Size(345, 32),
                Text = ""
            };
            groupBox4.Controls.Add(new Label() 
            { 
                Text = "Последние Документы",
                Location = new Point(5, 12),
                AutoSize = false,
                Size = new Size(150, 15),
                Font = new Font("Arial", 9F, FontStyle.Bold),
                ForeColor = Color.Gray,

            });
            groupBox5 = new GroupBox()
            {
                Location = new Point(255, 20),
                Size = new Size(345, 330),
                Text = ""
            };
            groupBox5.Controls.Add(new RichTextBox()
            {
                Text = "  ТЕТОВАЯ СТРОКА",
                Multiline = true,
                Location = new Point(2, 10),
                Size = new Size(groupBox5.Width -5, groupBox5.Height - 12),
                ReadOnly = true,
                BorderStyle = BorderStyle.None
            });
            this.Controls.Add(groupBox);
            this.Controls.Add(groupBox2);
            this.Controls.Add(groupBox3);
            this.Controls.Add(groupBox4);
            this.Controls.Add(groupBox5);
        }
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;

namespace WordPad_WF.Controls
{
    class IconsToolBar : MenuStrip
    {
        public ToolStripMenuItem window;
        public ToolStripMenuItem reestablish;
        public ToolStripMenuItem minimize;
        public ToolStripMenuItem maximize;
        public ToolStripMenuItem move;
        public ToolStripMenuItem size;
        public ToolStripMenuItem exit;
        public ToolStripMenuItem name;

        public ToolStripMenuItem toolList;
        public ToolStripMenuItem save;
        public ToolStripMenuItem save2;
        public ToolStripMenuItem undo;
        public ToolStripMenuItem undo2;
        public ToolStripMenuItem redo;
        public ToolStripMenuItem redo2;
        public ToolStripMenuItem create;
        public ToolStripMenuItem create2;
        public ToolStripMenuItem open;
        public ToolStripMenuItem open2;
        public ToolStripMenuItem quickPrint;
        public ToolStripMenuItem quickPrint2;
        public ToolStripMenuItem sendMail;
        public ToolStripMenuItem sendMail2;

        public IconsToolBar(WordPad form)
        {
            this.Font = form.Font;
            this.Dock = DockStyle.Top;
            this.ShowItemToolTips = true;
            this.Stretch = false;
            //this.AutoSize = false;
            //this.Width = 400;

            Items.AddRange(new ToolStripItem[] { window = new ToolStripMenuItem(Properties.Resources.start_menu24px) 
            { Size = new Size(24, 24), AutoSize = false } });

            window.DropDownItems.AddRange(new ToolStripItem[]
            {
                reestablish = new ToolStripMenuItem("Восстановить", Properties.Resources.maximize_window48px),
                move = new ToolStripMenuItem("Переместить"),
                size = new ToolStripMenuItem("Размер"),
                minimize = new ToolStripMenuItem("Свернуть", Properties.Resources.minimize48px),
                maximize = new ToolStripMenuItem("Развернуть", Properties.Resources.square_48px),
                exit = new ToolStripMenuItem("Закрыть", Properties.Resources.close_window48px),
            });

            Items.Add(new ToolStripSeparator());

            Items.Add(save2 = new ToolStripMenuItem(Properties.Resources.save24px) { Visible = true, Size = new Size(24,24), AutoSize = false});
            Items.Add(undo2 = new ToolStripMenuItem(Properties.Resources.undo24px) { Visible = true, Size = new Size(24, 24), AutoSize = false });
            Items.Add(redo2 = new ToolStripMenuItem(Properties.Resources.redo24px) { Visible = true, Size = new Size(24, 24), AutoSize = false });
            Items.Add(create2 = new ToolStripMenuItem(Properties.Resources.create24px) { Visible = false, Size = new Size(24, 24), AutoSize = false });
            Items.Add(open2 = new ToolStripMenuItem(Properties.Resources.open_24px) { Visible = false, Size = new Size(24, 24), AutoSize = false });
            Items.Add(quickPrint2 = new ToolStripMenuItem(Properties.Resources.print24px) { Visible = false, Size = new Size(24, 24), AutoSize = false });
            Items.Add(sendMail2 = new ToolStripMenuItem(Properties.Resources.send_24px) { Visible = false, Size = new Size(24,24), AutoSize = false });
            
            Items.AddRange(new ToolStripItem[] { toolList = new ToolStripMenuItem(Properties.Resources.pull_down48px)
            {Size = new Size(24, 24), AutoSize = false } });

            toolList.DropDownItems.AddRange(new ToolStripItem[]
            {
                create = new ToolStripMenuItem("Создать") { Checked = false, CheckOnClick = true },
                open = new ToolStripMenuItem("Открыть") { Checked = false, CheckOnClick = true },
                save = new ToolStripMenuItem("Сохранить") { Checked = true, CheckOnClick = true },
                quickPrint = new ToolStripMenuItem("Быстрая печать") { Checked = false, CheckOnClick = true },
                sendMail = new ToolStripMenuItem("Оправить по почте") { Checked = false, CheckOnClick = true },
                undo = new ToolStripMenuItem("Отменить") { Checked = true, CheckOnClick = true },
                redo = new ToolStripMenuItem("Вернуть") { Checked = true, CheckOnClick = true }
            });

            save.CheckedChanged += Save;
            undo.CheckedChanged += Undo;
            redo.CheckedChanged += Redo;
            create.CheckedChanged += Create;
            open.CheckedChanged += Open;
            quickPrint.CheckedChanged += QuickPrint;
            sendMail.CheckedChanged += SendMail;

            Items.Add(new ToolStripSeparator());
            Items.Add(name = new ToolStripMenuItem("") { Text = "" });


            form.Controls.Add(this);
        }

        private void SendMail(object sender, EventArgs e)
        {
            if (sendMail.Checked) { sendMail2.Visible = true; }
            else { sendMail2.Visible = false; }
        }

        private void QuickPrint(object sender, EventArgs e)
        {
            if (quickPrint.Checked) { quickPrint2.Visible = true; }
            else { quickPrint2.Visible = false; }
        }

        private void Open(object sender, EventArgs e)
        {
            if (open.Checked) { open2.Visible = true; }
            else { open2.Visible = false; }
        }

        private void Create(object sender, EventArgs e)
        {
            if (create.Checked) { create2.Visible = true; }
            else { create2.Visible = false; }
        }

        private void Redo(object sender, EventArgs e)
        {
            if (redo.Checked) { redo2.Visible = true; }
            else { redo2.Visible = false; }
        }

        private void Undo(object sender, EventArgs e)
        {
            if (undo.Checked) { undo2.Visible = true; }
            else { undo2.Visible = false; }
        }

        private void Save(object sender, EventArgs e)
        {
            if (save.Checked) { save2.Visible = true;}
            else { save2.Visible = false; }
        }

    }
}

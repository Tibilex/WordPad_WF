using System;
using System.Drawing;
using System.Windows.Forms;

namespace WordPad_WF.Controls
{
    class IconsToolBar : MenuStrip
    {
        ToolStripMenuItem window;
        public ToolStripMenuItem reestablish;
        public ToolStripMenuItem minimize;
        public ToolStripMenuItem maximize;
        public ToolStripMenuItem move;
        public ToolStripMenuItem size;
        public ToolStripMenuItem exit;
        public ToolStripMenuItem name;

        ToolStripMenuItem toolList;
        ToolStripMenuItem saveContext;
        ToolStripMenuItem undoContext;
        ToolStripMenuItem redoContext;
        ToolStripMenuItem createContext;
        ToolStripMenuItem openContext;
        ToolStripMenuItem quickPrintContext;
        ToolStripMenuItem sendMailContext;

        public ToolStripMenuItem save;
        public ToolStripMenuItem undo;
        public ToolStripMenuItem redo;
        public ToolStripMenuItem create;
        public ToolStripMenuItem open;
        public ToolStripMenuItem quickPrint;
        public ToolStripMenuItem sendMail;

        public IconsToolBar(WordPad form)
        {
            this.Font = form.Font;
            this.Dock = DockStyle.Top;
            this.ShowItemToolTips = true;
            this.Stretch = false;

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

            Items.Add(save = new ToolStripMenuItem(Properties.Resources.save24px) { Visible = true, Size = new Size(24,24), AutoSize = false});
            Items.Add(undo = new ToolStripMenuItem(Properties.Resources.undo24px) { Visible = true, Size = new Size(24, 24), AutoSize = false });
            Items.Add(redo = new ToolStripMenuItem(Properties.Resources.redo24px) { Visible = true, Size = new Size(24, 24), AutoSize = false });
            Items.Add(create = new ToolStripMenuItem(Properties.Resources.create24px) { Visible = false, Size = new Size(24, 24), AutoSize = false });
            Items.Add(open = new ToolStripMenuItem(Properties.Resources.open_24px) { Visible = false, Size = new Size(24, 24), AutoSize = false });
            Items.Add(quickPrint = new ToolStripMenuItem(Properties.Resources.print24px) { Visible = false, Size = new Size(24, 24), AutoSize = false });
            Items.Add(sendMail = new ToolStripMenuItem(Properties.Resources.send_24px) { Visible = false, Size = new Size(24,24), AutoSize = false });
            
            Items.AddRange(new ToolStripItem[] { toolList = new ToolStripMenuItem(Properties.Resources.pull_down48px)
            {Size = new Size(24, 24), AutoSize = false } });

            toolList.DropDownItems.AddRange(new ToolStripItem[]
            {
                name = new ToolStripMenuItem("Настройка панели быстрого доступа") {Enabled = false },
                new ToolStripSeparator(),
                createContext = new ToolStripMenuItem("Создать") { Checked = false, CheckOnClick = true },
                openContext = new ToolStripMenuItem("Открыть") { Checked = false, CheckOnClick = true },
                saveContext = new ToolStripMenuItem("Сохранить") { Checked = true, CheckOnClick = true },
                quickPrintContext = new ToolStripMenuItem("Быстрая печать") { Checked = false, CheckOnClick = true },
                sendMailContext = new ToolStripMenuItem("Оправить по почте") { Checked = false, CheckOnClick = true },
                undoContext = new ToolStripMenuItem("Отменить") { Checked = true, CheckOnClick = true },
                redoContext = new ToolStripMenuItem("Вернуть") { Checked = true, CheckOnClick = true }
            });

            saveContext.CheckedChanged += Save;
            undoContext.CheckedChanged += Undo;
            redoContext.CheckedChanged += Redo;
            createContext.CheckedChanged += Create;
            openContext.CheckedChanged += Open;
            quickPrintContext.CheckedChanged += QuickPrint;
            sendMailContext.CheckedChanged += SendMail;

            Items.Add(new ToolStripSeparator());
            Items.Add(name = new ToolStripMenuItem("") { Text = "" });

            form.Controls.Add(this);
        }

        private void SendMail(object sender, EventArgs e)
        {
            if (sendMailContext.Checked) { sendMail.Visible = true; }
            else { sendMail.Visible = false; }
        }

        private void QuickPrint(object sender, EventArgs e)
        {
            if (quickPrintContext.Checked) { quickPrint.Visible = true; }
            else { quickPrint.Visible = false; }
        }

        private void Open(object sender, EventArgs e)
        {
            if (openContext.Checked) { open.Visible = true; }
            else { open.Visible = false; }
        }

        private void Create(object sender, EventArgs e)
        {
            if (createContext.Checked) { create.Visible = true; }
            else { create.Visible = false; }
        }

        private void Redo(object sender, EventArgs e)
        {
            if (redoContext.Checked) { redo.Visible = true; }
            else { redo.Visible = false; }
        }

        private void Undo(object sender, EventArgs e)
        {
            if (undoContext.Checked) { undo.Visible = true; }
            else { undo.Visible = false; }
        }

        private void Save(object sender, EventArgs e)
        {
            if (saveContext.Checked) { save.Visible = true;}
            else { save.Visible = false; }
        }

    }
}

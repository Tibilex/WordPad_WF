using System;
using System.Drawing;
using System.Windows.Forms;

namespace WordPad_WF.Controls
{
    class IconsToolBar : MenuStrip
    {
        #region - Objects -       

        private ToolStripMenuItem _window;
        public ToolStripMenuItem reestablish;
        public ToolStripMenuItem minimize;
        public ToolStripMenuItem maximize;
        public ToolStripMenuItem move;
        public ToolStripMenuItem size;
        public ToolStripMenuItem exit;
        public ToolStripMenuItem name;

        private ToolStripMenuItem _toolList;
        private ToolStripMenuItem _saveContext;
        private ToolStripMenuItem _undoContext;
        private ToolStripMenuItem _redoContext;
        private ToolStripMenuItem _createContext;
        private ToolStripMenuItem _openContext;
        private ToolStripMenuItem _quickPrintContext;
        private ToolStripMenuItem _sendMailContext;

        public ToolStripMenuItem save;
        public ToolStripMenuItem undo;
        public ToolStripMenuItem redo;
        public ToolStripMenuItem create;
        public ToolStripMenuItem open;
        public ToolStripMenuItem quickPrint;
        public ToolStripMenuItem sendMail;

        #endregion

        #region - Constructor -
        public IconsToolBar(WordPad form)
        {
            this.Font = form.Font;
            this.Dock = DockStyle.Top;
            this.ShowItemToolTips = true;
            this.Stretch = false;

            Items.AddRange(new ToolStripItem[] { _window = new ToolStripMenuItem(Properties.Resources.start_menu_40px) 
            { Size = new Size(24, 24), AutoSize = false } });

            _window.DropDownItems.AddRange(new ToolStripItem[]
            {
                reestablish = new ToolStripMenuItem("Восстановить", Properties.Resources.restore_window_40px),
                move = new ToolStripMenuItem("Переместить", Properties.Resources.expand_40px),
                size = new ToolStripMenuItem("Размер", Properties.Resources.drag_40px),
                minimize = new ToolStripMenuItem("Свернуть", Properties.Resources.minimize_window_40px),
                maximize = new ToolStripMenuItem("Развернуть", Properties.Resources.maximize_window_40px),
                new ToolStripSeparator(),
                exit = new ToolStripMenuItem("Закрыть     alt+F4", Properties.Resources.close_window_40px),
            });

            Items.Add(new ToolStripSeparator());

            Items.Add(save = new ToolStripMenuItem(Properties.Resources.save_40px) { Visible = true, Size = new Size(24,24), AutoSize = false});
            Items.Add(undo = new ToolStripMenuItem(Properties.Resources.undo_40px) { Visible = true, Size = new Size(24, 24), AutoSize = false });
            Items.Add(redo = new ToolStripMenuItem(Properties.Resources.redo_40px) { Visible = true, Size = new Size(24, 24), AutoSize = false });
            Items.Add(create = new ToolStripMenuItem(Properties.Resources.create_40px) { Visible = false, Size = new Size(24, 24), AutoSize = false });
            Items.Add(open = new ToolStripMenuItem(Properties.Resources.live_folder_40px) { Visible = false, Size = new Size(24, 24), AutoSize = false });
            Items.Add(quickPrint = new ToolStripMenuItem(Properties.Resources.print_40px) { Visible = false, Size = new Size(24, 24), AutoSize = false });
            Items.Add(sendMail = new ToolStripMenuItem(Properties.Resources.send_email_40px) { Visible = false, Size = new Size(24,24), AutoSize = false });
            
            Items.AddRange(new ToolStripItem[] { _toolList = new ToolStripMenuItem(Properties.Resources.pull_down_40px)
            {Size = new Size(24, 24), AutoSize = false } });

            _toolList.DropDownItems.AddRange(new ToolStripItem[]
            {
                name = new ToolStripMenuItem("Настройка панели быстрого доступа") {Enabled = false },
                new ToolStripSeparator(),
                _createContext = new ToolStripMenuItem("Создать") { Checked = false, CheckOnClick = true },
                _openContext = new ToolStripMenuItem("Открыть") { Checked = false, CheckOnClick = true },
                _saveContext = new ToolStripMenuItem("Сохранить") { Checked = true, CheckOnClick = true },
                _quickPrintContext = new ToolStripMenuItem("Быстрая печать") { Checked = false, CheckOnClick = true },
                _sendMailContext = new ToolStripMenuItem("Оправить по почте") { Checked = false, CheckOnClick = true },
                _undoContext = new ToolStripMenuItem("Отменить") { Checked = true, CheckOnClick = true },
                _redoContext = new ToolStripMenuItem("Вернуть") { Checked = true, CheckOnClick = true }
            });

            _saveContext.CheckedChanged += SaveEventArgs;
            _undoContext.CheckedChanged += UndoEventArgs;
            _redoContext.CheckedChanged += RedoEventArgs;
            _createContext.CheckedChanged += CreateEventArgs;
            _openContext.CheckedChanged += OpenEventArgs;
            _quickPrintContext.CheckedChanged += QuickPrintEventArgs;
            _sendMailContext.CheckedChanged += SendMailEventArgs;

            Items.Add(new ToolStripSeparator());
            Items.Add(name = new ToolStripMenuItem("") { Text = "Документ - WordPad" });

            form.Controls.Add(this);
        }
        #endregion

        #region - Events -

        private void SendMailEventArgs(object sender, EventArgs e)
        {
            if (_sendMailContext.Checked) { sendMail.Visible = true; }
            else { sendMail.Visible = false; }
        }

        private void QuickPrintEventArgs(object sender, EventArgs e)
        {
            if (_quickPrintContext.Checked) { quickPrint.Visible = true; }
            else { quickPrint.Visible = false; }
        }

        private void OpenEventArgs(object sender, EventArgs e)
        {
            if (_openContext.Checked) { open.Visible = true; }
            else { open.Visible = false; }
        }

        private void CreateEventArgs(object sender, EventArgs e)
        {
            if (_createContext.Checked) { create.Visible = true; }
            else { create.Visible = false; }
        }

        private void RedoEventArgs(object sender, EventArgs e)
        {
            if (_redoContext.Checked) { redo.Visible = true; }
            else { redo.Visible = false; }
        }

        private void UndoEventArgs(object sender, EventArgs e)
        {
            if (_undoContext.Checked) { undo.Visible = true; }
            else { undo.Visible = false; }
        }

        private void SaveEventArgs(object sender, EventArgs e)
        {
            if (_saveContext.Checked) { save.Visible = true;}
            else { save.Visible = false; }
        }

        #endregion

    }
}

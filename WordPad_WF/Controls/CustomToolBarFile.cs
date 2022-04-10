using System;
using System.Drawing;
using System.Windows.Forms;

namespace WordPad_WF.Controls
{
    class CustomToolBarFile : Panel
    {
        Panel panel;
        public CustomToolBarFile()
        {
            this.Text = "Файл";
            this.BackColor = Color.FromArgb(0, 128, 204);
            this.ForeColor = Color.White;
            this.Size = new Size(600, 400);
        }

        private void Panel()
        {
        }
    }
}

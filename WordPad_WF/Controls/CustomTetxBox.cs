using System;
using System.Drawing;
using System.Windows.Forms;

namespace WordPad_WF.Controls
{
    class CustomTetxBox : RichTextBox
    {
        public CustomTetxBox()
        {
            this.Location = new Point(50, 156);
            this.Size = new Size(885, 785);
            this.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.Multiline = true;
        }
    }
}

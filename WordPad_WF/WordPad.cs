using System;
using System.Drawing;
using System.Windows.Forms;
using WordPad_WF.Controls;

namespace WordPad_WF
{
    public partial class WordPad : Form
    {
        IconsToolBar iconsToolBar;
        public WordPad()
        {
            InitializeComponent();
            this.Name = Properties.Resources.formName;
            this.StartPosition = FormStartPosition.CenterScreen;
            //this.FormBorderStyle = FormBorderStyle.None;
            iconsToolBar = new IconsToolBar(this);

        }

    }
}

﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace WordPad_WF.Controls
{
    internal class CustomToolBarView : Panel
    {
        #region - Objects - 
        GroupBox scaleSector;
        GroupBox showHideSector;
        GroupBox optionsSector;

        public OnPaintButtons buttonZoomUp;
        public OnPaintButtons buttonZoomDown;
        public OnPaintButtons buttonStockSize;

        MenuStrip menuStrip;
        public ToolStripMenuItem buttonTransfer;
        public ToolStripMenuItem transferItem1;
        public ToolStripMenuItem transferItem2;
        public ToolStripMenuItem transferItem3;
        MenuStrip menuStrip2;
        public ToolStripMenuItem buttonUnit;
        public ToolStripMenuItem unitItem1;
        public ToolStripMenuItem unitItem2;
        public ToolStripMenuItem unitItem3;
        public ToolStripMenuItem unitItem4;

        PictureBox imageRuler;
        PictureBox imageStatusBar;

        public CheckBox ruler;
        public CheckBox statusBar;
        #endregion

        public CustomToolBarView()
        {
            this.Location = new Point(0, 54);
            this.Size = new Size(1000, 100);
            this.BackColor = SystemColors.ControlLightLight;
            this.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            #region - Scale sector -
            scaleSector = new GroupBox()
            {
                Location = new Point(0, -5),
                Size = new Size(185, 106),
            };
            scaleSector.Controls.Add(new Label()
            {
                AutoSize = false,
                Size = new Size(100, 15),
                Text = "Масштаб",
                Location = new Point(70, this.Height - 11)
            });

            buttonZoomUp = new OnPaintButtons(new Point(6, 12), Properties.Resources.zoom_in_64px, new Size(56, 70), 8, 6, 40, 40, 0, 48, 78, 24);
            buttonZoomUp.Text = "Увеличить";

            buttonZoomDown = new OnPaintButtons(new Point(65, 12), Properties.Resources.zoom_out_64px, new Size(60, 70), 10, 6, 40, 40, 0, 48, 78, 24);
            buttonZoomDown.Text = "Уменьшить";

            buttonStockSize = new OnPaintButtons(new Point(126, 12), Properties.Resources.surface_40px, new Size(56, 70), 10, 6, 40, 40, 12, 48, 78, 24);
            buttonStockSize.Text = "100 %";

            scaleSector.Controls.Add(buttonZoomUp);
            scaleSector.Controls.Add(buttonZoomDown);
            scaleSector.Controls.Add(buttonStockSize);
            #endregion

            #region - Show/Hide sector -
            showHideSector = new GroupBox()
            {
                Location = new Point(158, -5),
                Size = new Size(180, 106),
            };
            showHideSector.Controls.Add(new Label()
            {
                AutoSize = true,
                Size = new Size(120, 15),
                Text = "Показать или скрыть",
                Location = new Point(45, this.Height - 11)
            });

            imageRuler = new PictureBox()
            {
                Location = new Point(32, 20),
                Size = new Size(20, 20),
                BackgroundImage = Properties.Resources.length_40px,
                BackgroundImageLayout = ImageLayout.Stretch
            };

            imageStatusBar = new PictureBox()
            {
                Location = new Point(32, 45),
                Size = new Size(20, 20),
                BackgroundImage = Properties.Resources.navigation_toolbar_bottom_64px,
                BackgroundImageLayout = ImageLayout.Stretch
            };

            ruler = new CheckBox()
            {
                Location = new Point(60, 23),
                Size = new Size(80, 20),
                Text = "Линейка",
                Checked = true              
            };

            statusBar = new CheckBox()
            {
                Location = new Point(60, 46),
                Size = new Size(115, 20),
                Text = "Строка состония",
                Checked = true
            };

            showHideSector.Controls.Add(imageRuler);
            showHideSector.Controls.Add(imageStatusBar);
            showHideSector.Controls.Add(ruler);
            showHideSector.Controls.Add(statusBar);
            #endregion

            #region - Options sector -
            optionsSector = new GroupBox()
            {
                Location = new Point(336, -5),
                Size = new Size(158, 106),
            };
            optionsSector.Controls.Add(new Label()
            {
                AutoSize = false,
                Size = new Size(60, 15),
                Text = "Параметры",
                Location = new Point(50, this.Height - 11)
            });

            menuStrip = new MenuStrip()
            {
                Location = new Point(50,20),
                Size = new Size(24, 24),
                BackColor = SystemColors.ControlLightLight
            };
            menuStrip.Items.AddRange(new ToolStripItem[] { buttonTransfer = new ToolStripMenuItem(Properties.Resources.transfer_64px)
            { 
                Size = new Size(140, 26),
                AutoSize = false,
                Text = "Перенос по словам",
                BackColor = SystemColors.ControlLightLight               
            } });

            buttonTransfer.DropDownItems.AddRange(new ToolStripItem[]
            {
                transferItem1 = new ToolStripMenuItem("Без переноса") {CheckOnClick = true, Checked = true},
                transferItem2 = new ToolStripMenuItem("В границах окна") {CheckOnClick = true, Checked = false},
                transferItem3 = new ToolStripMenuItem("В границах линейки") {CheckOnClick = true, Checked = false},
            });

            menuStrip2 = new MenuStrip()
            {
                Location = new Point(50, 46),
                Size = new Size(24, 24),
                BackColor = SystemColors.ControlLightLight
            };
            menuStrip2.Items.AddRange(new ToolStripItem[] { buttonUnit = new ToolStripMenuItem(Properties.Resources.lipids_40px)
            {
                Size = new Size(140, 26),
                AutoSize = false,
                Text = "Единица измерения",
                BackColor = SystemColors.ControlLightLight
            } });
            buttonUnit.DropDownItems.AddRange(new ToolStripItem[]
            {
                unitItem1 = new ToolStripMenuItem("Дюймы") {CheckOnClick = true, Checked = false},
                unitItem2 = new ToolStripMenuItem("Сантиметры") {CheckOnClick = true, Checked = true},
                unitItem3 = new ToolStripMenuItem("Точки") {CheckOnClick = true, Checked = false},
                unitItem4 = new ToolStripMenuItem("Пики") {CheckOnClick = true, Checked = false}
            });

            optionsSector.Controls.Add(menuStrip2);           
            optionsSector.Controls.Add(menuStrip);           
            #endregion

            this.Controls.Add(scaleSector);
            this.Controls.Add(showHideSector);
            this.Controls.Add(optionsSector);
        }
    }
}

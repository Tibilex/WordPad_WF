using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WordPad_WF.Controls
{
    class FormCloseMinimizeButtons : Control
    {
        StringFormat _format = new StringFormat();
        bool MouseEntered = false;
        bool MousePressed = false;
        Image image;
        Point point;
        Color color;

        public FormCloseMinimizeButtons(Point point, Image image, Color color)
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor |
                ControlStyles.UserPaint, true);
            DoubleBuffered = true;

            this.image = image;
            this.point = point;
            this.color = color;
            this.Location = point;
            this.Size = new Size(44, 32);            
            this.BackColor = SystemColors.ControlLightLight;
            this.Anchor = AnchorStyles.Top | AnchorStyles.Bottom  | AnchorStyles.Right;
            _format.Alignment = StringAlignment.Near;
            _format.LineAlignment = StringAlignment.Center;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graphic = e.Graphics;
            graphic.SmoothingMode = SmoothingMode.HighQuality;
            graphic.Clear(this.BackColor);

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            Rectangle rectText = new Rectangle(60, 0, Width - 1, Height - 1);
            Rectangle rectImage = new Rectangle(13, 8, 16, 16);

            graphic.DrawImage(image, rectImage);
            graphic.DrawRectangle(new Pen(this.BackColor), rect);
            graphic.FillRectangle(new SolidBrush(this.BackColor), rectText);
            graphic.DrawString(this.Text, this.Font, new SolidBrush(ForeColor), rectText, _format);

            if (MouseEntered)
            {
                graphic.DrawRectangle(new Pen(Color.FromArgb(50, Color.FromArgb(0, 128, 204))), rect);
                graphic.FillRectangle(new SolidBrush(Color.FromArgb(50, Color.FromArgb(140, 202, 247))), rect);
            }

            if (MousePressed)
            {
                graphic.DrawRectangle(new Pen(Color.FromArgb(60, Color.FromArgb(0, 128, 204))), rect);
                graphic.FillRectangle(new SolidBrush(Color.FromArgb(60, Color.Black)), rect);
            }
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.BackColor = color;
            MouseEntered = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.BackColor = SystemColors.ControlLightLight;
            MouseEntered = false;
            Invalidate();
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            MousePressed = true;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            MousePressed = false;
            Invalidate();
        }
    }
}

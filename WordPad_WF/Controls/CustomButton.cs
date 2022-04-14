using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WordPad_WF.Controls
{
    class CustomButton : Control
    {
        private StringFormat _format = new StringFormat();
        private bool _mouseEntered = false;
        private bool _mousePressed = false;
        private Image _image;

        public CustomButton(string text, Point point, Image image)
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor |
                ControlStyles.UserPaint, true);
            DoubleBuffered = true;

            this._image = image;
            this.Location = point;
            this.Text = text;
            this.Size = new Size(250, 42);
            this.ForeColor = Color.Black;
            this.Font = new Font("Arial", 9F, FontStyle.Regular);
            this.BackColor = Color.White;
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
            Rectangle rectimage = new Rectangle(10, 2, 36, 36);

            graphic.DrawImage(_image, rectimage);
            graphic.DrawRectangle(new Pen(this.BackColor), rect);
            graphic.FillRectangle(new SolidBrush(this.BackColor), rectText);
            graphic.DrawString(this.Text, this.Font, new SolidBrush(ForeColor), rectText, _format);

            if (_mouseEntered)
            {
                graphic.DrawRectangle(new Pen(Color.FromArgb(50, Color.FromArgb(0, 128, 204))), rect);
                graphic.FillRectangle(new SolidBrush(Color.FromArgb(50, Color.FromArgb(140, 202, 247))), rect);
            }

            if (_mousePressed)
            {
                graphic.DrawRectangle(new Pen(Color.FromArgb(60, Color.FromArgb(0, 128, 204))), rect);
                graphic.FillRectangle(new SolidBrush(Color.FromArgb(60, Color.Black)), rect);
            }

        }
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _mouseEntered = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _mouseEntered = false;
            Invalidate();
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            _mousePressed = true;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _mousePressed = false;
            Invalidate();
        }
    }
}

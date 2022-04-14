using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WordPad_WF.Controls
{
    class OnPaintButtons : Control
    {
        #region - Fields & objects -
        private StringFormat _format = new StringFormat();
        private bool _mouseEntered = false;
        private bool _mousePressed = false;
        private Image _image;
        public Color color;
        private int _X;
        private int _Y;
        private int _width;
        private int _height;
        private int _fontIndentX;
        private int _fontIndentY;
        private int _fontSizeX;
        private int _fontSizeY;
        #endregion

        #region - Constructors -
        public OnPaintButtons()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor |
                ControlStyles.UserPaint, true);
            DoubleBuffered = true;

            this.Size = new Size(44, 32);
            this.BackColor = SystemColors.ControlLightLight;
            this.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            _format.Alignment = StringAlignment.Near;
            _format.LineAlignment = StringAlignment.Center;
        }

        public OnPaintButtons(Point point, Image image, Size size, int X, int Y, int width, int height, int indentX, int indentY, int fontSizeX, int fontSizeY) : this()
        {
            this._image = image;
            this._width = width;
            this._height = height;
            this._fontIndentX = indentX;
            this._fontIndentY = indentY;
            this._fontSizeX = fontSizeX;
            this._fontSizeY = fontSizeY;
            this._X = X;
            this._Y = Y;
            this.Size = size;
            this.Location = point;
        }
        #endregion

        #region - Events & Methods -
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graphic = e.Graphics;
            graphic.SmoothingMode = SmoothingMode.HighQuality;
            graphic.Clear(this.BackColor);

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            Rectangle rectText = new Rectangle(_fontIndentX, _fontIndentY, _fontSizeX - 1, _fontSizeY - 1);
            Rectangle rectImage = new Rectangle(_X, _Y, _width, _height);

            graphic.DrawImage(_image, rectImage);
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
            if (!Enabled)
            {
                graphic.DrawRectangle(new Pen(Color.FromArgb(50, Color.White)), rect);
                graphic.FillRectangle(new SolidBrush(Color.FromArgb(50, Color.White)), rect);
                ForeColor = Color.DarkGray;
            }
            else
            {
                graphic.DrawRectangle(new Pen(Color.FromArgb(50, Color.White)), rect);
                graphic.FillRectangle(new SolidBrush(Color.FromArgb(50, Color.White)), rect);
                ForeColor = Color.Black;
            }

        }
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.BackColor = color;
            _mouseEntered = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.BackColor = SystemColors.ControlLightLight;
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
        #endregion
    }
}

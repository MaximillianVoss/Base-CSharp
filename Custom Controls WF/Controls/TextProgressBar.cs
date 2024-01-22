using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Custom_Controls_WF.Controls
{
    public enum ProgressBarDisplayMode
    {
        NoText,
        Percentage,
        CurrProgress,
        CustomText,
        TextAndPercentage,
        TextAndCurrProgress
    }

    public class TextProgressBar : ProgressBar
    {


        #region Поля

        #endregion

        #region Свойства
        [Description("Font of the text on ProgressBar"), Category("Additional Options")]
        public Font TextFont { get; set; } = new Font(FontFamily.GenericSansSerif, 11, FontStyle.Regular | FontStyle.Italic);

        private SolidBrush _textColourBrush = (SolidBrush)Brushes.Black;
        [Category("Additional Options")]
        public Color TextColor
        {
            get => this._textColourBrush.Color;
            set
            {
                this._textColourBrush.Dispose();
                this._textColourBrush = new SolidBrush(value);
            }
        }

        private SolidBrush _progressColourBrush = (SolidBrush)Brushes.LightGreen;
        [Category("Additional Options"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public Color ProgressColor
        {
            get => this._progressColourBrush.Color;
            set
            {
                this._progressColourBrush.Dispose();
                this._progressColourBrush = new SolidBrush(value);
            }
        }

        private ProgressBarDisplayMode _visualMode = ProgressBarDisplayMode.CurrProgress;
        [Category("Additional Options"), Browsable(true)]
        public ProgressBarDisplayMode VisualMode
        {
            get => this._visualMode;
            set
            {
                this._visualMode = value;
                this.Invalidate();//redraw component after change value from VS Properties section
            }
        }

        private string _text = string.Empty;

        [Description("If it's empty, % will be shown"), Category("Additional Options"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string CustomText
        {
            get => this._text;
            set
            {
                this._text = value;
                this.Invalidate();//redraw component after change value from VS Properties section
            }
        }

        private string _textToDraw
        {
            get
            {
                string text = this.CustomText;

                switch (this.VisualMode)
                {
                    case ProgressBarDisplayMode.Percentage:
                        text = this._percentageStr;
                        break;
                    case ProgressBarDisplayMode.CurrProgress:
                        text = this._currProgressStr;
                        break;
                    case ProgressBarDisplayMode.TextAndCurrProgress:
                        text = $"{this.CustomText}: {this._currProgressStr}";
                        break;
                    case ProgressBarDisplayMode.TextAndPercentage:
                        text = $"{this.CustomText}: {this._percentageStr}";
                        break;
                }

                return text;
            }
            set
            {
            }
        }

        private string _percentageStr => $"{(int)((float)this.Value - this.Minimum) / ((float)this.Maximum - this.Minimum) * 100}%";

        private string _currProgressStr => $"{this.Value}/{this.Maximum}";
        #endregion

        #region Методы
        private void FixComponentBlinking()
        {
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }
        private void DrawProgressBar(Graphics g)
        {
            Rectangle rect = this.ClientRectangle;

            ProgressBarRenderer.DrawHorizontalBar(g, rect);

            rect.Inflate(-3, -3);

            if (this.Value > 0)
            {
                var clip = new Rectangle(rect.X, rect.Y, (int)Math.Round((float)this.Value / this.Maximum * rect.Width), rect.Height);

                g.FillRectangle(this._progressColourBrush, clip);
            }
        }
        private void DrawStringIfNeeded(Graphics g)
        {
            if (this.VisualMode != ProgressBarDisplayMode.NoText)
            {

                string text = this._textToDraw;

                SizeF len = g.MeasureString(text, this.TextFont);

                var location = new Point((this.Width / 2) - ((int)len.Width / 2), (this.Height / 2) - ((int)len.Height / 2));

                g.DrawString(text, this.TextFont, this._textColourBrush, location);
            }
        }
        public new void Dispose()
        {
            this._textColourBrush.Dispose();
            this._progressColourBrush.Dispose();
            base.Dispose();
        }
        #endregion

        #region Конструкторы/Деструкторы
        public TextProgressBar()
        {
            this.Value = this.Minimum;
            this.FixComponentBlinking();
        }
        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            this.DrawProgressBar(g);

            this.DrawStringIfNeeded(g);
        }
        #endregion


    }
}

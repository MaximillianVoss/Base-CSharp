using Custom_Controls_WF.Classes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Custom_Controls_WF
{
    public partial class Plot : UserControl
    {

        #region Поля
        private double plotScale;
        private Graphics graphics;
        #endregion

        #region Свойства
        public double PlotScale
        {
            get => this.plotScale;
            set => this.plotScale = value >= 1 ? value : 1;
        }
        public int MouseX
        {
            set; get;
        }
        public int MouseY
        {
            set; get;
        }
        public Point Middle => new Point(this.pbMain.Width / 2, this.pbMain.Height / 2);
        #endregion

        #region Методы
        public void RefreshImage()
        {
            this.pbMain.Refresh();
            //this.pbMain_Click(this.pbMain, null);
        }
        public void Clear()
        {
            var g = Graphics.FromHwnd(this.pbMain.Handle);
            g.Clear(Color.Transparent);
            g.Dispose();
        }
        public void Draw(String text, PlotPoint point)
        {
            this.graphics.DrawString(text, new Font("Segoe UI", 10), point.brush, new PointF((float)point.X, (float)point.Y));
        }
        public void Draw(PlotPoint point, int size, bool isCentered = false, bool isScaled = false)
        {
            size = Math.Abs(size);
            int x = (int)point.X - (size / 2);
            int y = (int)point.Y - (size / 2);
            if (isScaled)
            {
                x *= (int)this.PlotScale;
                y *= (int)this.PlotScale;
            }
            if (isCentered)
            {
                x += this.Middle.X;
                y += this.Middle.Y;
            }
            this.graphics.FillEllipse(point.brush, new Rectangle(x, y, size, size));
        }
        public void Draw(PlotPoint from, PlotPoint to, int thikness, bool isCentered = false, bool isScaled = false)
        {
            thikness = Math.Abs(thikness);
            double x1 = from.X;
            double y1 = from.Y;
            double x2 = to.X;
            double y2 = to.Y;
            if (isScaled)
            {
                x1 *= this.PlotScale;
                y1 *= this.PlotScale;
                x2 *= this.PlotScale;
                y2 *= this.PlotScale;
            }
            if (isCentered)
            {
                x1 += this.Middle.X;
                y1 += this.Middle.Y;
                x2 += this.Middle.X;
                y2 += this.Middle.Y;
            }
            this.graphics.DrawLine(new Pen(from.brush, thikness), (int)x1, (int)y1, (int)x2, (int)y2);
        }
        public void Draw(List<PlotPoint> points, int thikness, bool isCentered = false, bool isScaled = false)
        {
            for (int i = 0; i < points.Count - 1; i++)
            {
                this.Draw(points[i], points[i + 1], thikness, isCentered, isScaled);
            }
        }
        public void DrawAxis(int stepX, int stepY, Brush brush, int thikness = 2)
        {
            if (stepX < 1 || stepY < 1)
            {
                throw new Exception("Недопустимый шаг разметки осей!");
            }

            this.Draw(new PlotPoint(0, this.Middle.Y, brush), new PlotPoint(this.pbMain.Width, this.Middle.Y, brush), thikness);
            this.Draw(new PlotPoint(this.Middle.X, 0, brush), new PlotPoint(this.Middle.X, this.pbMain.Height, brush), thikness);
            int halfMark = thikness * 2;
            #region Оси
            for (int i = this.Middle.X, counter = 0; i < this.pbMain.Width; i += stepX * (int)this.PlotScale, counter++)
            {
                this.Draw(
                    new PlotPoint(i, this.Middle.Y - halfMark, brush),
                    new PlotPoint(i, this.Middle.Y + halfMark, brush), thikness
                    );
                //this.Draw(counter.ToString(), new PlotPoint(i, this.Middle.Y + halfMark, brush));
            }
            for (int i = this.Middle.X, counter = 0; i > 0; i -= stepX * (int)this.PlotScale, counter++)
            {
                this.Draw(
                    new PlotPoint(i, this.Middle.Y - halfMark, brush),
                    new PlotPoint(i, this.Middle.Y + halfMark, brush), thikness
                    );
                //this.Draw((-counter).ToString(), new PlotPoint(i, this.Middle.Y + halfMark, brush));
            }
            for (int i = this.Middle.Y; i < this.pbMain.Height; i += stepY * (int)this.PlotScale)
            {
                this.Draw(
                    new PlotPoint(this.Middle.X - halfMark, i, brush),
                    new PlotPoint(this.Middle.X + halfMark, i, brush), thikness
                    );
            }
            for (int i = this.Middle.Y; i > 0; i -= stepY * (int)this.PlotScale)
            {
                this.Draw(
                    new PlotPoint(this.Middle.X - halfMark, i, brush),
                    new PlotPoint(this.Middle.X + halfMark, i, brush), thikness
                    );
            }

            #endregion


        }
        public void DrawAxis(int stepX, int stepY)
        {
            this.DrawAxis(stepX, stepY, new SolidBrush(Color.Black));
        }
        public void UpdateCoordinates(int x, int y)
        {
            this.MouseX = x;
            this.MouseY = y;
            this.lblCoords.Text = String.Format("X:{0} Y:{1}", x, y);
        }
        #endregion

        #region Конструкторы/Деструкторы
        public Plot()
        {
            this.InitializeComponent();
            this.PlotScale = 20;
            this.graphics = this.pbMain.CreateGraphics();
        }

        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий
        private void Plot_Load(object sender, EventArgs e)
        {
            this.pbMain_Paint(this.pbMain, null);
        }
        private void pbMain_LoadCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {

        }
        private void pbMain_MouseMove(object sender, MouseEventArgs e)
        {
            this.UpdateCoordinates(e.X, e.Y);
        }
        private void pbMain_MouseDown(object sender, MouseEventArgs e)
        {


        }
        private void pbMain_Click(object sender, EventArgs e)
        {
            this.PlotScale = 20;
            var points = new List<PlotPoint>();
            for (double i = 0; i < 100; i += 0.1)
            {
                points.Add(new PlotPoint(i, Math.Sin(i), new SolidBrush(Color.Red)));
            }
            this.Draw(points, 2, true, true);
            this.DrawAxis(1, 1);
            //List<PlotPoint> points = new List<PlotPoint>();
            //Random rand = new Random();
            //for (int i = 0; i < 100; i++)
            //    points.Add(new PlotPoint(rand.Next(0, 1000), rand.Next(0, 1000), new SolidBrush(Color.Red)));
            //this.Draw(points, 2);
        }
        private void pbMain_Paint(object sender, PaintEventArgs e)
        {

        }
        private void Plot_Resize(object sender, EventArgs e)
        {
            this.pbMain.Width = this.Width;
            this.pbMain.Height = this.Height;
        }

        #endregion


    }
}

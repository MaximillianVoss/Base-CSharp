using System.Drawing;

namespace Custom_Controls_WF.Classes
{
    /// <summary>
    /// Точка на графике
    /// </summary>
    public class PlotPoint
    {


        #region Поля
        private double x;
        private double y;
        #endregion

        #region Свойства
        public double X { get => this.x; set => this.x = value; }
        public double Y { get => this.y; set => this.y = value; }
        public Brush brush { get; set; }
        #endregion

        #region Методы

        #endregion

        #region Конструкторы/Деструкторы
        public PlotPoint(double x, double y, Brush brush)
        {
            this.x = x;
            this.y = y;
            this.brush = brush;
        }
        public PlotPoint() : this(0, 0, new SolidBrush(Color.Black))
        {

        }
        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий

        #endregion


    }
}

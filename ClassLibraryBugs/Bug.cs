using System;
using System.Drawing;
using System.Collections.Generic;

namespace ClassLibraryBugs
{
    public class Bug
    {
        /// <summary>
        /// Координаты левого верхнего угла квадрата
        /// </summary>
        private Point leftup;

        /// <summary>
        /// Квадрат, задающий жука
        /// </summary>
        private Rectangle rect;

        /// <summary>
        /// Размер квадрата
        /// </summary>
        private int size;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Bug(int x, int y)
        {
            leftup = new Point(x, y);

            size = 50;
        }
        /// <summary>
        /// Вывод фигуры в picturebox
        /// </summary>
        /// <param name="g"></param>
        /// <param name="pen"></param>
        /// <param name="brush"></param>
        public void Show(Graphics g, Pen pen, SolidBrush brush)
        {
            rect = new Rectangle(leftup.X, leftup.Y, size, size);
            g.DrawRectangle(pen, rect);
            g.FillRectangle(brush, rect);
        }

        /// <summary>
        /// Движение жука на доске
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public void Move(int a, int b)
        {
            leftup.X += a; leftup.Y += b;
        }

        // получение координат
        public int GetX() { return leftup.X; }
        public int GetY() { return leftup.Y; }
    }
}

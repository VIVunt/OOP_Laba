using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Laba1
{
    public class DrawLine : Object, IDraw
    {
        private Point Point1 = new Point();
        private Point Point2 = new Point();
        public int Width { get; set; }
        public int Height { get; set; }
        public int Thickness { get; set; }
        public Color FrontColor { get; set; }

        public void Initialization(Point point1, Point point2, int thickness, Color Front, Color Back)
        {
            this.Point1 = point1;
            this.Point2 = point2;
            this.Thickness = thickness;
            this.FrontColor = Front;
        }

        public void Draw(Form1 form)
        {
            Width = Math.Abs(Point2.X - Point1.X) + 1;
            Height = Math.Abs(Point2.Y - Point1.Y) + 1;

            Graphics graph = form.CreateGraphics();
            Pen pen = new Pen(FrontColor, Thickness);
            graph.DrawLine(pen, Point1.X, Point1.Y, Point2.X, Point2.Y);
            graph.Dispose();
            pen.Dispose();
        }

        public void MouseDown(Form1 form, Point point, MouseButtons msbut)
        {
        }

        public void MouseMove(Form1 form, Point point, MouseButtons msbut)
        {
            if (msbut == MouseButtons.Left)
            {
                //Установка конечных координат
                this.Point2.X = point.X;
                this.Point2.Y = point.Y;

                //Обновление поля
                form.FieldRefresh();
            }
        }

        public void MouseUp(Object form, Point point, MouseButtons msbut)
        {
            if (msbut != MouseButtons.Left)
            {
                if ((this.Width != 0) || (this.Height != 0))
                {
                    //Добавление фигуры 
                    if (form is Form1)
                    {
                        ((Form1)form).AddFigure();
                    }
                    else if (form is BrokenLine)
                    {
                        ((BrokenLine)form).AddFigure();
                    }
                }
                else
                {
                    //Не добавление фигуры
                    if (form is Form1)
                    {
                        ((Form1)form).NotAddFigure();
                    }
                    else if (form is BrokenLine)
                    {
                        ((BrokenLine)form).NotAddFigure();
                    }
                }
            }
        }
    }  
}

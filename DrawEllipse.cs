using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Laba1
{
    public class DrawEllipse : Object, IDraw
    {
        private Point Point1 = new Point();
        private Point Point2 = new Point();
        public int Width { get; set; }
        public int Height { get; set; }
        public int Thickness { get; set; }
        public Color FrontColor { get; set; }
        public Color BackColor { get; set; }

        public void Initialization(Point point1, Point point2, int thickness, Color Front, Color Back)
        {
            this.Point1 = point1;
            this.Point2 = point2;
            this.Thickness = thickness;
            this.FrontColor = Front;
            this.BackColor = Back;
        }

        public void Draw(Form1 form)
        {
            Width = Math.Abs(Point2.X - Point1.X) + 1;
            Height = Math.Abs(Point2.Y - Point1.Y) + 1;

            int start1, start2;

            if (Point1.X <= Point2.X) start1 = Point1.X;
            else start1 = Point2.X;

            if (Point1.Y <= Point2.Y) start2 = Point1.Y;
            else start2 = Point2.Y;

            SolidBrush brush = new SolidBrush(BackColor);
            Graphics graph = form.CreateGraphics();
            Pen pen = new Pen(FrontColor, Thickness);
            pen.Alignment = PenAlignment.Inset;
            graph.DrawEllipse(pen, start1, start2, Width, Height);
            graph.FillEllipse(brush, start1 + Thickness, start2 + Thickness, Width - Thickness * 2, Height - Thickness * 2);
            graph.Dispose();
            pen.Dispose();
            brush.Dispose();
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
                    Form1 form1 = (Form1)form; 
                    form1.AddFigure();
                }
                else
                {
                    //Не добавление фигуры
                    Form1 form1 = (Form1)form; 
                    form1.NotAddFigure();
                }
            }
        }
    }
}

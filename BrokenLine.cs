using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Laba1
{
    class BrokenLine : Object, IDraw
    {
        public delegate void DrawDelegate(Form1 form);
        public event DrawDelegate ToDraw;
        private DrawLine line = null;

        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Thickness { get; set; }
        public Color FrontColor { get; set; }

        public void Initialization(Point point1, Point point2, int thickness, Color Front, Color Back)
        {
            this.Thickness = thickness;
            this.FrontColor = Front;
        }

        public void Draw(Form1 form)
        {
            //Отрисовка линий
            if (ToDraw != null) ToDraw(form);
            if (line != null) line.Draw(form);
        }

        public void AddFigure()
        {
            ToDraw += line.Draw;
            line = null;
        }

        public void NotAddFigure()
        {
            line = null;
        }

        public void MouseDown(Form1 form, Point point, MouseButtons msbut)
        {
            if (Form.MouseButtons == MouseButtons.Left)
            {
                if (line != null)
                {
                    ToDraw += line.Draw;
                }
                //Создание линии
                line = new DrawLine(); 
                Point p2 = new Point();
                p2.X = 0;
                p2.Y = 0;
                line.Initialization(point, p2, Thickness, FrontColor, Color.Black);

                //Обработка нажатия ЛКМ
                if (line != null) line.MouseDown(form, point, msbut);
            }
            else if (Form.MouseButtons == MouseButtons.Right)
            {
                //Обработка нажатия ПКМ
                if (line != null) line.MouseUp(this, point, msbut);
                if (ToDraw != null) form.AddFigure();
                else form.NotAddFigure();
            }
        }

        public void MouseMove(Form1 form, Point point, MouseButtons msbut)
        {
            //Обработка движения мыши
            if (line != null) line.MouseMove(form, point, MouseButtons.Left);
        }

        public void MouseUp(Object form, Point point, MouseButtons msbut)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using PluginInterface;

namespace PlugRhombus
{
    [Serializable]
    [Plugin("Ромб")]
    public class DrawRhombus : IDraw
    {
        public Point Point1 = new Point();
        public Point Point2 = new Point();
        public int Width;
        public int Height;
        public int Thickness;
        public Color Front;
        public Color Back;

        public void Initialization(Point point, int thickness, Color front, Color back)
        {
            Point1 = point;
            Thickness = thickness;
            Front = front;
            Back = back;
        }

        public void Draw(Graphics graph)
        {
            int start1, start2;

            if (Point1.X <= Point2.X) start1 = Point1.X;
            else start1 = Point2.X;

            if (Point1.Y <= Point2.Y) start2 = Point1.Y;
            else start2 = Point2.Y;

            Point[] points1 = new Point[]{
                new Point(start1 + (int)Math.Round((float)(Width/2)), start2),
                new Point(start1 + Width, start2 + (int)Math.Round((float)(Height/2))),
                new Point(start1 + (int)Math.Round((float)(Width/2)), start2 + Height),
                new Point(start1, start2 + (int)Math.Round((float)(Height/2)))
            };

            Point[] points2 = new Point[]{
                new Point(start1 + (int)Math.Round((float)(Width/2)), start2 + Thickness),
                new Point(start1 + Width - Thickness, start2 + (int)Math.Round((float)(Height/2))),
                new Point(start1 + (int)Math.Round((float)(Width/2)), start2 + Height - Thickness),
                new Point(start1 + Thickness, start2 + (int)Math.Round((float)(Height/2)))
            };

            Pen pen = new Pen(Front, Thickness);
            pen.Alignment = PenAlignment.Inset;
            graph.DrawPolygon(pen, points1);
            pen.Dispose();

            SolidBrush brush = new SolidBrush(Back);
            graph.FillPolygon(brush, points2);
            brush.Dispose();
        }

        public void SetPoint(Point point)
        {
            Point2 = point;
            Width = Math.Abs(Point2.X - Point1.X) + 1;
            Height = Math.Abs(Point2.Y - Point1.Y) + 1;
        }

        public void AddPoint(Point point)
        {

        }

        public void Save()
        {

        }

        public bool IsSimpleFigure()
        {
            return true;
        }

        public int GetWidth()
        {
            return Width;
        }

        public int GetHeight()
        {
            return Height;
        }
    }
}

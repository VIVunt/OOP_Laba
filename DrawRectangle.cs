using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Laba1
{
    public class DrawRectangle : IDraw
    {
        public Point Point1 = new Point();
        public Point Point2 = new Point();
        public int Width;
        public int Height;
        public int Thickness;
        public Pen pen;
        public SolidBrush brush;   

        public void Initialization(Point point, int thickness, Color Front, Color Back)
        {
            Point1 = point;
            Thickness = thickness;
            pen = new Pen(Front, thickness);
            pen.Alignment = PenAlignment.Inset;
            brush = new SolidBrush(Back);
        }

        public void Draw(Graphics graph)
        {
            int start1, start2;

            if (Point1.X <= Point2.X) start1 = Point1.X;
            else start1 = Point2.X;

            if (Point1.Y <= Point2.Y) start2 = Point1.Y;
            else start2 = Point2.Y;

            graph.DrawRectangle(pen, start1, start2, Width, Height);
            graph.FillRectangle(brush, start1 + Thickness, start2 + Thickness, Width - Thickness * 2, Height - Thickness * 2);
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

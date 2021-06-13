using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using PluginInterface;

namespace Laba1
{
    [Serializable]
    public class DrawLine : IDraw
    {
        public Point Point1 = new Point();
        public Point Point2 = new Point();
        public int Width;
        public int Height;
        public int Thickness;
        public Color Front;

        public void Initialization(Point point, int thickness, Color front, Color back)
        {
            this.Point1 = point;
            this.Thickness = thickness;
            this.Front = front;
        }

        public void Draw(Graphics graph)
        {
            Pen pen = new Pen(Front, Thickness);
            graph.DrawLine(pen, Point1, Point2);
            pen.Dispose();
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

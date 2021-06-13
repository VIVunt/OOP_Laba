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
    class DrawBrokenLine : IDraw
    {
        public List<Point> ListPoints = new List<Point>();
        public Point Point1;
        public Color Front;
        public int Thickness;

        public void Initialization(Point point, int thickness, Color front, Color back)
        {
            ListPoints.Add(point);
            Front = front;
            Thickness = thickness;
        }

        public void Draw(Graphics graph)
        {
            Pen pen = new Pen(Front, Thickness);

            for (int i = 1; i < ListPoints.Count; i++)
			{
			    graph.DrawLine(pen, ListPoints[i-1], ListPoints[i]);
            }
            graph.DrawLine(pen, ListPoints[ListPoints.Count - 1], Point1);

            pen.Dispose();
        }

        public void SetPoint(Point point)
        {
            Point1 = point;
        }

        public void AddPoint(Point point)
        {
            ListPoints.Add(point);
        }

        public void Save()
        {

        }

        public bool IsSimpleFigure()
        {
            return false;
        }

        public int GetWidth()
        {
            return 1;
        }

        public int GetHeight()
        {
            return 1;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Laba1
{
    class Polygon : IDraw
    {
        public Pen pen;
        public List<Point> ListPoints = new List<Point>();
        public Point Point1;

        public void Initialization(Point point, int thickness, Color Front, Color Back)
        {
            ListPoints.Add(point);
            pen = new Pen(Front, thickness);
        }

        public void Draw(Graphics graph)
        {
            for (int i = 1; i < ListPoints.Count; i++)
            {
                graph.DrawLine(pen, ListPoints[i - 1], ListPoints[i]);
            }
            graph.DrawLine(pen, ListPoints[ListPoints.Count - 1], Point1);
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
            ListPoints.Add(Point1);
            ListPoints.Add(ListPoints[0]);
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

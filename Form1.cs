using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing.Drawing2D;

namespace Laba1
{
    public partial class Form1 : Form
    {
        public delegate void DrawDelegate();
        public event DrawDelegate ToDraw;
        private IDraw figure;
        private bool DrawPermission = false;
        private Graphics Graph;
        private int X1;
        private int Y1;
        private Assembly assembly = Assembly.GetExecutingAssembly();

        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            Graph = this.CreateGraphics();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            this.X1 = Form1.MousePosition.X;
            this.Y1 = Form1.MousePosition.Y;

            Type type = assembly.GetType(comboBox1.SelectedItem.ToString());
            figure = Activator.CreateInstance(type) as IDraw;
            figure.Init(X1, Y1, 0, 0, 0, this);

            DrawPermission = false;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Form1.MouseButtons == MouseButtons.Left && DrawPermission)
            {
                figure.X2 = Form1.MousePosition.X - 1;
                figure.Y2 = Form1.MousePosition.Y - 23;

                Graph.Clear(System.Drawing.SystemColors.Control);
                if (ToDraw != null) ToDraw();
                figure.Draw();

                textBox1.Text = figure.X1.ToString();
                textBox2.Text = figure.Y1.ToString();
                textBox3.Text = figure.X2.ToString();
                textBox4.Text = figure.Y2.ToString();

            } else DrawPermission = true;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if ((figure.Width != 0) || (figure.Height != 0))
            {
                ToDraw += figure.Draw;
            }
            figure = null;
            DrawPermission = false;
        }
    }

    public interface IDraw
    {
        void Draw();
        void Init(int x1, int y1, int x2, int y2, int thickness, Form1 form);
        int X1 { get; set; }
        int Y1 { get; set; }
        int X2 { get; set; }
        int Y2 { get; set; }
        int Width { get; set; }
        int Height { get; set; }
    }

    public class DrawRectangle: Object, IDraw
    {
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Thickness { get; set; }
        public Form1 form { get; set; }
        public int FrontColor { get; set; }

        public void Init(int x1, int y1, int x2, int y2, int thickness, Form1 form)
        {
            this.X1 = x1;
            this.Y1 = y1;
            this.X2 = x2;
            this.Y2 = y2;
            this.Thickness = thickness;
            this.form = form;
        }

        public void Draw()
        {
            Width = Math.Abs(X2 - X1) + 1;
            Height = Math.Abs(Y2 - Y1) + 1;

            int start1, start2;

            if (X1 <= X2) start1 = X1;
            else start1 = X2;

            if (Y1 <= Y2) start2 = Y1;
            else start2 = Y2;

            SolidBrush brush = new SolidBrush(Color.Red);
            Graphics graph = form.CreateGraphics();
            Pen pen = new Pen(Color.Blue, 10);
            pen.Alignment = PenAlignment.Inset;
            graph.DrawRectangle(pen, start1, start2, Width, Height);
            graph.FillRectangle(brush, start1 + 10, start2 + 10, Width - 20, Height - 20);
            graph.Dispose();
            pen.Dispose();
            brush.Dispose();
        }
    }

    public class DrawEllipse : Object, IDraw
    {
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Thickness { get; set; }
        public Form1 form { get; set; }
        public int FrontColor { get; set; }

        public void Init(int x1, int y1, int x2, int y2, int thickness, Form1 form)
        {
            this.X1 = x1;
            this.Y1 = y1;
            this.X2 = x2;
            this.Y2 = y2;
            this.Thickness = thickness;
            this.form = form;
        }

        public void Draw()
        {
            Width = Math.Abs(X2 - X1) + 1;
            Height = Math.Abs(Y2 - Y1) + 1;

            int start1, start2;

            if (X1 <= X2) start1 = X1;
            else start1 = X2;

            if (Y1 <= Y2) start2 = Y1;
            else start2 = Y2;

            SolidBrush brush = new SolidBrush(Color.Red);
            Graphics graph = form.CreateGraphics();
            Pen pen = new Pen(Color.Blue, 10);
            pen.Alignment = PenAlignment.Inset;
            graph.DrawEllipse(pen, start1, start2, Width, Height);
            graph.FillEllipse(brush, start1 + 10, start2 + 10, Width - 20, Height - 20);
            graph.Dispose();
            pen.Dispose();
            brush.Dispose();
        }
    }

    public class DrawLine : Object, IDraw
    {
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Thickness { get; set; }
        public Form1 form { get; set; }
        public int FrontColor { get; set; }

        public void Init(int x1, int y1, int x2, int y2, int thickness, Form1 form)
        {
            this.X1 = x1;
            this.Y1 = y1;
            this.X2 = x2;
            this.Y2 = y2;
            this.Thickness = thickness;
            this.form = form;
        }

        public void Draw()
        {
            Width = Math.Abs(X2 - X1) + 1;
            Height = Math.Abs(Y2 - Y1) + 1;

            Graphics graph = form.CreateGraphics();
            Pen pen = new Pen(Color.Blue, 10);
            graph.DrawLine(pen, X1, Y1, X2, Y2);
            graph.Dispose();
            pen.Dispose();
        }
    }  
}
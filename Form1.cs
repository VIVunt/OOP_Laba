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
        public delegate void DrawDelegate(Form1 form);
        public event DrawDelegate ToDraw;
        private IDraw figure = null;
        private Graphics Graph;
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

        private Type GetFigure(int n)
        {
            string[] arr = { "DrawLine", "DrawRectangle", "DrawEllipse", "BrokenLine", "Polygon" };
            return assembly.GetType("Laba1" + "." + arr[n]);
        }

        private int GetThickness(int n)
        {
            int[] arr = { 1, 2, 3, 4, 5 };
            return arr[n];
        }

        private Color GetColor(int n)
        {
            Color[] arr = { Color.Red, Color.Blue, Color.White, Color.Black };
            return arr[n];
        }

        public void FieldRefresh()
        {
            Graph.Clear(System.Drawing.SystemColors.Control);
            if (ToDraw != null) ToDraw(this);
            if (figure != null) figure.Draw(this);
        }

        public void AddFigure()
        {
            ToDraw += figure.Draw;
            figure = null;
        }

        public void NotAddFigure()
        {
            figure = null;
        }

        //временная отладочная процедура
        public void Debug(int X1, int Y1, int X2, int Y2, int thickness)
        {
            //отладка
            textBox1.Text = X1.ToString();
            textBox2.Text = Y1.ToString();
            textBox3.Text = X2.ToString();
            textBox4.Text = Y2.ToString();
            textBox5.Text = thickness.ToString();
            //
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (Form.MouseButtons == MouseButtons.Left)
            {
                if (figure == null)
                {
                    //Создание фигуры
                    Type type = GetFigure(comboBox1.SelectedIndex);
                    figure = Activator.CreateInstance(type) as IDraw;

                    //Инициализация значений
                    Point p= new Point();
                    p.X = Form1.MousePosition.X - Location.X - 8;
                    p.Y = Form1.MousePosition.Y - Location.Y - 32;
                    figure.Initialization(p, p, GetThickness(comboBox2.SelectedIndex), GetColor(comboBox3.SelectedIndex), GetColor(comboBox4.SelectedIndex));
                }
            }

            //Инициализация значений
            Point point = new Point();
            point.X = Form1.MousePosition.X - Location.X - 8;
            point.Y = Form1.MousePosition.Y - Location.Y - 32;


            //Обработка нажатия ЛКМ
            if (figure != null) figure.MouseDown(this, point, Form1.MouseButtons);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            //Инициализация значений
            Point point = new Point();
            point.X = Form1.MousePosition.X - Location.X - 8;
            point.Y = Form1.MousePosition.Y - Location.Y - 32; 

            //Обработка движения мыши
            if (figure != null) figure.MouseMove(this, point, Form1.MouseButtons);
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            //Инициализация значений
            Point point = new Point();
            point.X = Form1.MousePosition.X - Location.X - 8;
            point.Y = Form1.MousePosition.Y - Location.Y - 32;

            //Обработка Отжатия ЛКМ
            if (figure != null) figure.MouseUp(this, point, Form1.MouseButtons);
        }
    }

    public interface IDraw
    {
        void Draw(Form1 form);
        void MouseDown(Form1 form, Point point, MouseButtons msbut);
        void MouseMove(Form1 form, Point point, MouseButtons msbut);
        void MouseUp(Object form, Point point, MouseButtons msbut);
        void Initialization(Point point1, Point point2, int thickness, Color Front, Color Back);
    }
}
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization.Formatters.Binary;
using PluginInterface;

namespace Laba1
{
    public partial class Form1 : Form
    {
        private IDraw figure = null;
        private Image img = null;
        private Graphics Graph;
        private Assembly assembly = Assembly.GetExecutingAssembly();
        private List<IDraw> ListFigures = new List<IDraw>();
        private List<IDraw> RedoListFigures = new List<IDraw>();
        private List<Type> FiguresType = new List<Type> { typeof(DrawLine), typeof(DrawRectangle), typeof(DrawEllipse), typeof(DrawBrokenLine), typeof(DrawPolygon) };
        private bool Ctrl = false;
        private bool Z = false;
        private bool Y = false;

        public Form1()
        {
            InitializeComponent();

            Figures.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Size = Size;
            pictureBox1.Location = new Point(0, 27);
            img = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            FieldRefresh();
            pictureBox1.Refresh();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (Form.MouseButtons == MouseButtons.Left)
            {
                if (figure == null)
                {
                    //Создание фигуры
                    Type type = GetFigure(Figures.SelectedIndex);
                    figure = Activator.CreateInstance(type) as IDraw;

                    //Инициализация значений
                    Point point = new Point();
                    point.X = Form1.MousePosition.X - pictureBox1.Location.X - 1;
                    point.Y = Form1.MousePosition.Y - pictureBox1.Location.Y - 22;
                    figure.Initialization(point, GetThickness(comboBox2.SelectedIndex), GetColor(comboBox3.SelectedIndex), GetColor(comboBox4.SelectedIndex));
                }
                else
                {
                    //Обновление
                    FieldRefresh();

                    //Отрисовка
                    Point point = new Point();
                    point.X = Form1.MousePosition.X - pictureBox1.Location.X - 1;
                    point.Y = Form1.MousePosition.Y - pictureBox1.Location.Y - 22;
                    if (figure.IsSimpleFigure())
                    {
                        figure.SetPoint(point);
                    }
                    else
                    {
                        figure.AddPoint(point);
                    }
                    figure.Draw(Graph);
                    pictureBox1.Refresh();
                }
            }
            else if (Form.MouseButtons == MouseButtons.Right)
            {
                if (figure != null)
                {
                    if (!figure.IsSimpleFigure())
                    {
                        //Сохранение фигуры
                        if ((figure.GetWidth() > 0) && (figure.GetHeight() > 0))
                        {
                            figure.Save();
                            AddFigure();
                        }
                        figure = null;

                        //Обновление
                        FieldRefresh();
                        pictureBox1.Refresh();
                    }
                }
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (figure != null)
            {
                if (figure.IsSimpleFigure())
                {
                    if (Form.MouseButtons == MouseButtons.Left)
                    {
                        //Обновление
                        FieldRefresh();

                        //Отрисовка
                        Point point = new Point();
                        point.X = Form1.MousePosition.X - pictureBox1.Location.X - 1;
                        point.Y = Form1.MousePosition.Y - pictureBox1.Location.Y - 22;
                        figure.SetPoint(point);
                        figure.Draw(Graph);
                        pictureBox1.Refresh();
                    }
                }
                else
                {
                    //Обновление
                    FieldRefresh();

                    //Отрисовка
                    Point point = new Point();
                    point.X = Form1.MousePosition.X - pictureBox1.Location.X - 1;
                    point.Y = Form1.MousePosition.Y - pictureBox1.Location.Y - 22;
                    figure.SetPoint(point);
                    figure.Draw(Graph);
                    pictureBox1.Refresh();
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (figure != null)
            {
                if (figure.IsSimpleFigure())
                {
                    //Сохранение фигуры
                    if ((figure.GetWidth() > 0) && (figure.GetHeight() > 0))
                    {
                        figure.Save();
                        AddFigure();
                    }
                    figure = null;

                    //Обновление
                    FieldRefresh();
                    pictureBox1.Refresh();
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Z) Z = true;
            if (e.KeyCode == Keys.Y) Y = true;
            if (ModifierKeys == Keys.Control) Ctrl = true;
            if (Z && Ctrl) RemoveFigure();
            if (Y && Ctrl) ReturnFigure();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Z) Z = false;
            if (e.KeyCode == Keys.Y) Y = false;
            if (ModifierKeys != Keys.Control) Ctrl = false;
        }

        private Type GetFigure(int n)
        {
            return FiguresType[n];
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
            Bitmap bmp = new Bitmap(img, pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bmp;
            Graph = Graphics.FromImage(pictureBox1.Image);

            for (int i = 0; i < ListFigures.Count; i++)
            {
                ListFigures[i].Draw(Graph);
            }
        }

        public void RemoveFigure()
        {
            if (ListFigures.Count > 0)
            {
                RedoListFigures.Add(ListFigures[ListFigures.Count - 1]);
                ListFigures.RemoveAt(ListFigures.Count - 1);
                FieldRefresh();
                pictureBox1.Refresh();
            }
        }

        public void ReturnFigure()
        {
            if (RedoListFigures.Count > 0)
            {
                ListFigures.Add(RedoListFigures[RedoListFigures.Count - 1]);
                RedoListFigures.RemoveAt(RedoListFigures.Count - 1);
                FieldRefresh();
                pictureBox1.Refresh();
            }
        }

        public void AddFigure()
        {
            ListFigures.Add(figure);
            ClearRedoList();
            FieldRefresh();
        }

        public void ClearListFigures()
        {
            for (int i = ListFigures.Count - 1; i >= 0; i--)
            {
                ListFigures.RemoveAt(i);
            }
        }

        public void ClearRedoList()
        {
            for (int i = RedoListFigures.Count - 1; i >= 0; i--)
            {
                RedoListFigures.RemoveAt(i);
            }
        }

        public void Serialize()
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream("../../Saved/figures.dat", FileMode.OpenOrCreate))
            {
                try
                {
                    formatter.Serialize(fs, ListFigures);
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }

            using (FileStream fs = new FileStream("../../Saved/Redofigures.dat", FileMode.OpenOrCreate))
            {
                try
                {
                    formatter.Serialize(fs, RedoListFigures);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        public void Deserialize()
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream("../../Saved/figures.dat", FileMode.OpenOrCreate))
            {
                try
                {
                    ListFigures = (List<IDraw>)formatter.Deserialize(fs);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }

            }

            using (FileStream fs = new FileStream("../../Saved/Redofigures.dat", FileMode.OpenOrCreate))
            {
                try
                {
                    RedoListFigures = (List<IDraw>)formatter.Deserialize(fs);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }

            }
            FieldRefresh();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            Serialize();    
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            Deserialize();   
        }

        public void LoadPlugin()
        {
            OpenFileDialog open_dialog = new OpenFileDialog();
            if (open_dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Assembly asm = Assembly.LoadFrom(open_dialog.FileName);

                    Type[] types = asm.GetTypes();
                    foreach (Type t in types)
                    {
                        PluginAttribute attr = Attribute.GetCustomAttribute(t, typeof(PluginAttribute)) as PluginAttribute;
                        if (attr != null)
                        {
                            FiguresType.Add(t);
                            this.Figures.Items.Add(attr.Name);
                        }
                    }
                }
                catch
                {
                    DialogResult rezult = MessageBox.Show("Невозможно открыть выбранный файл",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ChoosePlugine_Click(object sender, EventArgs e)
        {
            LoadPlugin();
        }
    }
}
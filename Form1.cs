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
        private IDraw figure = null;
        private Image img = null;
        private Graphics Graph;
        private Assembly assembly = Assembly.GetExecutingAssembly();
        private List<IDraw> ListFigures = new List<IDraw>();
        private List<IDraw> RedoListFigures = new List<IDraw>();
        private bool Ctrl = false;
        private bool Z = false;
        private bool Y = false;

        public Form1()
        {
            InitializeComponent();

            comboBox1.SelectedIndex = 0;
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
                    Type type = GetFigure(comboBox1.SelectedIndex);
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

        private void OpenImage()
        {
            OpenFileDialog open_dialog = new OpenFileDialog(); 
            open_dialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            if (open_dialog.ShowDialog() == DialogResult.OK) 
            {
                try
                {
                    img = new Bitmap(open_dialog.FileName);

                    //Обновление
                    ClearListFigures();
                    ClearRedoList();
                    FieldRefresh();
                    pictureBox1.Refresh();
                }
                catch
                {
                    DialogResult rezult = MessageBox.Show("Невозможно открыть выбранный файл",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SaveImage()
        {
            SaveFileDialog savedialog = new SaveFileDialog();
            savedialog.Title = "Сохранить картинку как...";
            savedialog.OverwritePrompt = true;
            savedialog.CheckPathExists = true;
            savedialog.Filter = "Image Files(*.BMP)|*.BMP|Image Files(*.JPG)|*.JPG|Image Files(*.GIF)|*.GIF|Image Files(*.PNG)|*.PNG|All files (*.*)|*.*";
            savedialog.ShowHelp = true;
            if (savedialog.ShowDialog() == DialogResult.OK) 
            {
                try
                {
                    pictureBox1.Image.Save(savedialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                catch
                {
                    MessageBox.Show("Невозможно сохранить изображение", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }            
        }

        private Type GetFigure(int n)
        {
            Type[] arr = { typeof(DrawLine), typeof(DrawRectangle), typeof(DrawEllipse), typeof(BrokenLine), typeof(Polygon) };
            return arr[n];
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

        private void OpenButton_Click(object sender, EventArgs e)
        {
            OpenImage();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveImage();
        }
    }

    public interface IDraw
    {
        void Initialization(Point point, int thickness, Color Front, Color Back);
        void Draw(Graphics graph);
        void SetPoint(Point point);
        void AddPoint(Point point);
        bool IsSimpleFigure();
        void Save();
        int GetWidth();
        int GetHeight();
    }
}
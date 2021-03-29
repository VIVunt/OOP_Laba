using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Laba1
{
    public interface IDraw
    {
        void Draw();
    }

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics graph = this.CreateGraphics();
            Pen pen = new Pen(Color.Red);

            graph.DrawLine(pen, 50, 90, 170, 190);
            graph.Dispose();
        }
    }
}

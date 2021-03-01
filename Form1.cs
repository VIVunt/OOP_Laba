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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics graph = Graphics.FromImage(bmp);

            Pen pen = new Pen(Color.Green);
            graph.DrawLine(pen, 50, 90, 170, 190);

            pictureBox1.Image = bmp;
        }
    }
}

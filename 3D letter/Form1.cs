using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace _3D_letter
{
    public partial class Form1 : Form
    {
        static Graphics graph;
        static Graphics back;
        Bitmap bmp;        
        Bitmap background;
        figure myFigure;
        Pen pen;

        public Form1()
        {
            InitializeComponent();
            myFigure = new figure();
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            background = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graph = Graphics.FromImage(bmp);
            back = Graphics.FromImage(background);

            graph.CompositingMode = CompositingMode.SourceOver;
            graph.CompositingQuality = CompositingQuality.HighQuality;
            graph.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graph.SmoothingMode = SmoothingMode.HighQuality;
            back.SmoothingMode = SmoothingMode.HighQuality;
            graph.PixelOffsetMode = PixelOffsetMode.HighQuality;

            graph.TranslateTransform(pictureBox1.Width / 2, pictureBox1.Height / 2);
            back.TranslateTransform(pictureBox1.Width / 2, pictureBox1.Height / 2);

            pen = new Pen(Color.Green);
            back.DrawLine(pen, 0, 0, pictureBox1.Width, 0); //x
            back.DrawLine(pen, 0, 0, 0, -pictureBox1.Height / 2); //y
            back.DrawLine(pen, 0, 0, -pictureBox1.Width / 2, pictureBox1.Height / 2); //z
            pictureBox1.BackgroundImage = background;

            Draw();
        }
        
        public void Draw()
        {
            graph.Clear(Color.Empty);
       
            pen.Color = Color.White;
            pen.Width = 2;
            for (int i = 0; i < myFigure.points; i++)
            {
                
                if (i != myFigure.points - 1)
                {
                    graph.DrawLine(pen, myFigure.frontSide2D[i], myFigure.frontSide2D[i + 1]);
                    graph.DrawLine(pen, myFigure.backSide2D[i], myFigure.backSide2D[i + 1]);
                }
                else
                {
                    graph.DrawLine(pen, myFigure.frontSide2D[i], myFigure.frontSide2D[0]);
                    graph.DrawLine(pen, myFigure.backSide2D[i], myFigure.backSide2D[0]);
                }
                graph.DrawLine(pen, myFigure.frontSide2D[i], myFigure.backSide2D[i]);
            }
            pictureBox1.Image = bmp;
        }

        private void AngelX(object sender, EventArgs e) 
        {
            myFigure.angleX = (float)numericUpDown1.Value;
            myFigure.Transfomation();
            Draw();
        }
               
        private void AngelY(object sender, EventArgs e)
        {
            myFigure.angleY = (float)numericUpDown2.Value;
            myFigure.Transfomation();
            Draw();
        }

        private void AngelZ(object sender, EventArgs e)
        {
            myFigure.angleZ = (float)numericUpDown3.Value;
            myFigure.Transfomation();
            Draw();
        }

        private void TranslateX(object sender, EventArgs e)
        {
            myFigure.translateX = (float)numericUpDown6.Value;
            myFigure.Transfomation();
            Draw();
        }

        private void TranslateY(object sender, EventArgs e)
        {
            myFigure.translateY = (float)numericUpDown5.Value;
            myFigure.Transfomation();
            Draw();
        }

        private void TranslateZ(object sender, EventArgs e)
        {
            myFigure.translateZ = (float)numericUpDown4.Value;
            myFigure.Transfomation();
            Draw();
        }

        private void ScaleX(object sender, EventArgs e)
        {
            myFigure.scaleX = (float)numericUpDown9.Value * 0.1f + 1;
            myFigure.Transfomation();
            Draw();
        }

        private void ScaleY(object sender, EventArgs e)
        {
            myFigure.scaleY = (float)numericUpDown8.Value * 0.1f + 1;
            myFigure.Transfomation();
            Draw();
        }

        private void ScaleZ(object sender, EventArgs e)
        {
            myFigure.scaleZ = (float)numericUpDown7.Value * 0.1f + 1;
            myFigure.Transfomation();
            Draw();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            numericUpDown1.Value = numericUpDown1.Minimum;
            numericUpDown2.Value = numericUpDown2.Minimum;
            numericUpDown3.Value = numericUpDown3.Minimum;

            numericUpDown4.Value = 0;
            numericUpDown5.Value = 0;
            numericUpDown6.Value = 0;

            numericUpDown7.Value = numericUpDown7.Minimum;
            numericUpDown8.Value = numericUpDown8.Minimum;
            numericUpDown9.Value = numericUpDown9.Minimum;

            myFigure.BackUp();
            Draw();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                timer1.Start();
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
                groupBox3.Enabled = false;
                button1.Enabled = false;
            }
            else
            {
                timer1.Stop();
                groupBox1.Enabled = true;
                groupBox2.Enabled = true;
                groupBox3.Enabled = true;
                button1.Enabled = true;
            }
        }

        
        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }
    }
}

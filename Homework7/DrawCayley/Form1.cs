using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawCayley
{
    public partial class Form1 : Form
    {
        private Graphics graphics;
        double th1, th2;
        double per1, per2;
        Pen pen = Pens.Black;
        int depth, length;

        public Form1()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
        
            if (graphics != null) graphics.Clear(panel1.BackColor);
            switch (comboBox1.Text)
            {
                case "蓝色": pen = Pens.Blue; break;
                case "绿色": pen = Pens.Green; break;
                case "红色": pen = Pens.Red; break;
                case "黄色": pen = Pens.Yellow; break;
                case "黑色": pen = Pens.Black; break;
                default: pen = Pens.Black; break;
            }
            try
            {
                depth = Convert.ToInt32(textBox1.Text);
                length = Convert.ToInt32(textBox2.Text);
                per1 = Convert.ToDouble(textBox3.Text);
                per2 = Convert.ToDouble(textBox4.Text);
                th1 = Convert.ToDouble(textBox5.Text);
                th2 = Convert.ToDouble(textBox6.Text);
            }
            catch (System.FormatException)
            {
                MessageBox.Show("输入数据有误，请重新输入！");
            }

            if (graphics == null) graphics = panel1.CreateGraphics();
            DrawCayleyTree(depth, 300, 370, length, -Math.PI / 2);

        }

        void DrawCayleyTree(int n, double x0, double y0,double leng, double th)
        {
            if (n == 0) return;
            double x1 = x0 + leng * Math.Cos(th);
            double y1 = y0 + leng * Math.Sin(th);

            graphics.DrawLine(pen, (int)x0, (int)y0, (int)x1, (int)y1);
            DrawCayleyTree(n - 1, x1, y1, per1 * leng, th + th1);
            DrawCayleyTree(n - 1, x1, y1, per2 * leng, th - th2);

        }
    }
}

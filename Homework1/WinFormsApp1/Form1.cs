using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double m = 0, n = 0;
            string sign = "";
            if(comboBox1.SelectedIndex != -1)
                sign = comboBox1.SelectedItem.ToString();
            double result = 0;
            if (textBox2.Text.Trim() == string.Empty || textBox3.Text.Trim() == string.Empty)
                MessageBox.Show("请输入正确的操作数！");
            else
            {
                m = double.Parse(textBox2.Text);
                n = double.Parse(textBox3.Text);
                switch (sign)
                {
                    case "+":
                        result = m + n;
                        break;
                    case "-":
                        result = m - n;
                        break;
                    case "*":
                        result = m * n;
                        break;
                    case "/":
                        if (n == 0)
                            MessageBox.Show("请输入正确的除数！");
                        else
                            result = m / n;
                        break;
                    case "%":
                        result = m % n;
                        break;
                    default:
                        MessageBox.Show("操作符错误！");
                        break;
                }
            }
            this.textBox1.Text = $"{result}";
        }
    }
}

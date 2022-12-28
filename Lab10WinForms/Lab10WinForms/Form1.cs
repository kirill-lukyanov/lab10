using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab10WinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            chart1.Series["Низкое"].Points.Clear();
            for (int i = 1; i <= 12; i++)
            {
                chart1.Series["Низкое"].Points.AddXY(i, MN_Low(i));
            }
            chart1.Series["Среднее"].Points.Clear();
            for (int i = 1; i <= 12; i++)
            {
                chart1.Series["Среднее"].Points.AddXY(i, MN_Medium(i));
            }
            chart1.Series["Высокое"].Points.Clear();
            for (int i = 1; i <= 12; i++)
            {
                chart1.Series["Высокое"].Points.AddXY(i, MN_High(i));
            }




            chart2.Series["Низкое"].Points.Clear();
            for (int i = 1; i <= 16; i++)
            {
                chart2.Series["Низкое"].Points.AddXY(i, MP_Low(i));
            }
            chart2.Series["Среднее"].Points.Clear();
            for (int i = 1; i <= 16; i++)
            {
                chart2.Series["Среднее"].Points.AddXY(i, MP_Medium(i));
            }
            chart2.Series["Высокое"].Points.Clear();
            for (int i = 1; i <= 16; i++)
            {
                chart2.Series["Высокое"].Points.AddXY(i, MP_High(i));
            }


        }

        //Нечеткое подмножество "низкое", определенное на множестве значений количества выдач
        static float MN_Low(float n)
        {
            if (n <= 3) return 1;
            if (n > 3 && n < 6) return (6 - n) / 3;
            if (n >= 6) return 0;
            return 0;
        }
        //Нечеткое подмножество "среднее", определенное на множестве значений количества выдач
        static float MN_Medium(float n)
        {
            if (n < 3 || n > 9) return 0;
            if (n >= 3 && n < 6) return (n - 3) / 3;
            if (n >= 6 && n <= 9) return (9 - n) / 3;
            return 0;
        }
        //Нечеткое подмножество "высокое", определенное на множестве значений количества выдач
        static float MN_High(float n)
        {
            if (n < 6) return 0;
            if (n >= 6 && n < 9) return (n - 6) / 3;
            if (n >= 9) return 1;
            return 0;
        }

        static float MP_Low(float n)
        {
            if (n <= 4) return 1;
            if (n > 4 && n < 8) return (8 - n) / 4;
            if (n >= 8) return 0;
            return 0;
        }

        static float MP_Medium(float n)
        {
            if (n < 4 || n > 12) return 0;
            if (n >= 4 && n < 8) return (n - 4) / 4;
            if (n >= 8 && n <= 12) return (12 - n) / 4;
            return 0;
        }

        static float MP_High(float n)
        {
            if (n < 8) return 0;
            if (n >= 8 && n < 12) return (n - 8) / 4;
            if (n >= 12) return 1;
            return 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            float MN_Low_r = MN_Low(Convert.ToSingle(textBox1.Text));
            float MN_Medium_r = MN_Medium(Convert.ToSingle(textBox1.Text));
            float MN_High_r = MN_High(Convert.ToSingle(textBox1.Text));

            label2.Text = $"MN_Low = {MN_Low_r}";
            label3.Text = $"MN_Medium = {MN_Medium_r}";
            label4.Text = $"MN_High = {MN_High_r}";

            chart3.Series["Низкое"].Points.Clear();
            for (int i = 1; i <= 16; i++)
            {
                chart3.Series["Низкое"].Points.AddXY(i, MN_Low_r * MP_Low(i));
            }
            chart3.Series["Среднее"].Points.Clear();
            for (int i = 1; i <= 16; i++)
            {
                chart3.Series["Среднее"].Points.AddXY(i, MN_Medium_r * MP_Medium(i));
            }
            chart3.Series["Высокое"].Points.Clear();
            for (int i = 1; i <= 16; i++)
            {
                chart3.Series["Высокое"].Points.AddXY(i, MN_High_r * MP_High(i));
            }

            float[] resPlot = new float[17];

            chart4.Series["Объединение"].Points.Clear();
            for (int i = 1; i <= 16; i++)
            {
                resPlot[i] = Math.Max(MN_High_r * MP_High(i), Math.Max(MN_Medium_r * MP_Medium(i), MN_Low_r * MP_Low(i)));
                chart4.Series["Объединение"].Points.AddXY(i, resPlot[i]);
                
            }

            float sumi = 0;
            float sumq = 0;
            for (int i = 1; i <= 16; i++)
            {
                sumi += i * resPlot[i];
                sumq += resPlot[i];
            }
            label5.Text = $"Центр тяжести полученной фигуры находится в точке {sumi / sumq}";

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace test1
{
    public partial class 程式期末報告 : Form
    {
        public 程式期末報告()
        {
            InitializeComponent();
            this.Paint += Form1_Paint;
            this.DoubleBuffered = true; // 減少閃爍
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // 漸變背景
            Color startColor = Color.White;
            Color endColor = Color.LightBlue;

            // 繪製漸變背景
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            LinearGradientBrush brush = new LinearGradientBrush(rect, startColor, endColor, LinearGradientMode.Vertical);
            e.Graphics.FillRectangle(brush, rect);
        }



        private void Button1_Click(object sender, EventArgs e)
        {
            // 猜拳
            Form2 form2Instance = new Form2();
            form2Instance.Show();
            button1.BackColor = Color.LightBlue;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            // 隨機分組
            Form5 form5Instance = new Form5();
            form5Instance.Show();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            // 加法練習
            Form4 windowPage4 = new Form4();
            windowPage4.Show();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            // 轉盤
            Form3 windowPage4 = new Form3();
            windowPage4.Show();
        }

        //private void Button5_Click(object sender, EventArgs e)
        //{
        //    // 健康
        //    Form5 windowPage5 = new Form5();
        //    windowPage5.Show();
        //}

        private void button6_Click(object sender, EventArgs e)
        {
            // Tom Cat Game1
            Form6 windowPage6 = new Form6();
            windowPage6.Show();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            // Tom Cat Game2
            Form7 windowPage7 = new Form7();
            windowPage7.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // ball game
            Form9 windowPage9 = new Form9();
            windowPage9.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //snake game
            Form10 windowPage10 = new Form10();
            windowPage10.Show();
        }
    }
}
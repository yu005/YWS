using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace test1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Paint += Form1_Paint;
            this.DoubleBuffered = true; // 启用双缓冲减少闪烁
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // 定义渐变色的起始颜色和结束颜色
            Color startColor = Color.White;
            Color endColor = Color.LightBlue;

            // 绘制渐变色背景
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            LinearGradientBrush brush = new LinearGradientBrush(rect, startColor, endColor, LinearGradientMode.Vertical);
            e.Graphics.FillRectangle(brush, rect);
        }

        private void ApplyCustomButtonStyle(Button button)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.BackColor = Color.LightSkyBlue;
            button.ForeColor = Color.White;
            button.Font = new Font("Arial", 10, FontStyle.Bold);
            button.Paint += CustomButton_Paint;
        }

        private void CustomButton_Paint(object sender, PaintEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                // 设置渐变色
                Color startColor = Color.LightSkyBlue;
                Color endColor = Color.SteelBlue;

                // 绘制圆角矩形
                Rectangle rect = new Rectangle(0, 0, btn.Width, btn.Height);
                using (LinearGradientBrush brush = new LinearGradientBrush(rect, startColor, endColor, LinearGradientMode.Vertical))
                {
                    using (GraphicsPath path = new GraphicsPath())
                    {
                        int radius = 20;
                        path.AddArc(rect.Left, rect.Top, radius, radius, 180, 90);
                        path.AddArc(rect.Right - radius, rect.Top, radius, radius, 270, 90);
                        path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
                        path.AddArc(rect.Left, rect.Bottom - radius, radius, radius, 90, 90);
                        path.CloseFigure();
                        e.Graphics.FillPath(brush, path);
                    }
                }

                // 绘制按钮文本
                TextRenderer.DrawText(e.Graphics, btn.Text, btn.Font, rect, btn.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            // 创建 Form2 的实例并显示它
            Form2 form2Instance = new Form2();
            form2Instance.Show();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            // 创建 Form3 的实例并显示它
            Form5 form5Instance = new Form5();
            form5Instance.Show();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            // 数字练习
            Form4 windowPage4 = new Form4();
            windowPage4.Show();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            // Navigate to Window Page 4
            Form3 windowPage4 = new Form3();
            windowPage4.Show();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            // Navigate to Window Page 5
            Form5 windowPage5 = new Form5();
            windowPage5.Show();
        }
    }
}
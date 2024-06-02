using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace test1
{
    public partial class Form3 : Form
    {
        private string[] options; // 選項
        private Random random = new Random(); // 隨機數生成器
        private float currentAngle = 0; // 目前旋轉的角度
        private float angularVelocity = 10; // 初始角速度
        private const float decelerationRate = 0.1f; // 減速率
        private System.Windows.Forms.Timer timer; // 定時器

        public Form3()
        {
            InitializeComponent();
            this.Paint += new PaintEventHandler(this.Form2_Paint); // 綁定繪製事件
            this.DoubleBuffered = true; // 啟用雙緩衝
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "轉盤";

            // 創建用於輸入選項的文本框
            TextBox textBoxOptions = new TextBox
            {
                Location = new System.Drawing.Point(10, 10),
                Size = new System.Drawing.Size(200, 100),
                Name = "textBoxOptions",
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };
            this.Controls.Add(textBoxOptions);

            // 創建用於啟動旋轉的按鈕
            Button buttonStart = new Button
            {
                Text = "Start",
                Location = new System.Drawing.Point(220, 10),
                Size = new System.Drawing.Size(75, 23),
                Name = "buttonStart"
            };
            buttonStart.Click += new EventHandler(StartSpinning); // 綁定按鈕點擊事件
            this.Controls.Add(buttonStart);

            // 初始化計時器
            timer = new System.Windows.Forms.Timer(this.components)
            {
                Interval = 30 // 每30毫秒更新一次
            };
            timer.Tick += new EventHandler(Timer_Tick); // 綁定計時器的Tick事件
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            // 定義漸層色的起始顏色和結束顏色
            Color startColor = Color.White;
            Color endColor = Color.Wheat;

            // 繪製漸層色背景
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            using (LinearGradientBrush brush = new LinearGradientBrush(rect, startColor, endColor, LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(brush, rect);
            }
        }

        private void StartSpinning(object sender, EventArgs e)
        {
            TextBox textBoxOptions = this.Controls.Find("textBoxOptions", true)[0] as TextBox;
            options = textBoxOptions.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            if (options.Length > 1)
            {
                currentAngle = 0;
                angularVelocity = 10 + (float)(random.NextDouble() * 5); // 重置角速度并加入隨機性
                timer.Start();
            }
            else
            {
                MessageBox.Show("Please enter at least two options.");
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // 減速
            angularVelocity -= decelerationRate;

            if (angularVelocity <= 0.01f) // 如果角速度已經很小，即將停止
            {
                angularVelocity = 0; // 設置角速度為0，以確保不會有負速度
                timer.Stop(); // 停止計時器

                // 計算停止時的選項
                float finalAngle = currentAngle % 360;
                float anglePerOption = 360f / options.Length;
                int winningIndex = (options.Length - 1 - (int)(finalAngle / anglePerOption)) % options.Length;
                string winningOption = options[winningIndex];

                MessageBox.Show("最終結果: " + winningOption);
            }
            else
            {
                // 根據角速度計算角度的增量
                float angleIncrement = angularVelocity;
                currentAngle += angleIncrement; // 增加角度
                currentAngle = currentAngle % 360; // 確保角度在0到360之間
            }
            this.Invalidate(); // 重新繪製
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            if (options == null || options.Length == 0) return;

            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            float centerX = this.ClientSize.Width / 2;
            float centerY = this.ClientSize.Height / 2;
            float radius = Math.Min(centerX, centerY) - 10;
            float anglePerOption = 360f / options.Length;

            for (int i = 0; i < options.Length; i++)
            {
                float startAngle = i * anglePerOption + currentAngle + 270;
                float sweepAngle = anglePerOption;
                g.FillPie(new SolidBrush(GetColor(i)), centerX - radius, centerY - radius, radius * 2, radius * 2, startAngle, sweepAngle);

                // 繪製選項文字
                float textAngle = (startAngle + sweepAngle / 2) % 360;
                PointF textPosition = new PointF(
                    centerX + (float)(radius * 0.7 * Math.Cos(textAngle * Math.PI / 180)),
                    centerY + (float)(radius * 0.7 * Math.Sin(textAngle * Math.PI / 180))
                );
                g.TranslateTransform(textPosition.X, textPosition.Y);
                g.RotateTransform(textAngle + 90);
                g.DrawString(options[i], this.Font, Brushes.Black, new PointF(0, 0), new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                g.ResetTransform();
            }

            // 畫出指示器
            g.FillPolygon(Brushes.Black, new PointF[]
            {
                new PointF(centerX - 10, centerY - radius - 10),
                new PointF(centerX + 10, centerY - radius - 10),
                new PointF(centerX, centerY - radius + 10)
            });
        }

        // 根據索引生成顏色
        private Color GetColor(int index)
        {
            return Color.FromArgb((int)(Math.Sin(index * 0.3) * 127 + 128), (int)(Math.Sin(index * 0.3 + 2) * 127 + 128), (int)(Math.Sin(index * 0.3 + 4) * 127 + 128));
        }
    }
}


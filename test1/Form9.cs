using System;
using System.Drawing;
using System.Windows.Forms;

namespace test1
{
    public partial class Form9 : Form
    {
        // 宣告變數
        // 球的位置
        private Point ballPosition;

        // 籃框的位置
        private Point hoopPosition;

        // 球的初始速度X軸方向
        private double initialVelocityX;

        // 球的初始速度Y軸方向
        private double initialVelocityY;

        // 重力影響
        private double gravity = 0.05;

        // 計時器
        private System.Windows.Forms.Timer timer;

        // 球是否移動
        private bool ballMoving = false;

        // 得分
        private int score = 0;

        // 籃框移動速度
        private int hoopSpeed = 2;

        // 籃框是否向右移動
        private bool hoopMovingRight = true;

        // 速度係數
        private const double speedFactor = 10.0;

        public Form9()
        {
            InitializeComponent();
            InitializeGame(); // 初始化遊戲
        }

        private void InitializeGame()
        {
            // 設定視窗大小
            this.ClientSize = new Size(800, 600);

            // 初始化球的位置
            ballPosition = new Point(this.ClientSize.Width / 2, this.ClientSize.Height - 30);

            // 初始化籃框的位置
            hoopPosition = new Point(this.ClientSize.Width / 2 - 50, 50);

            // 設定滑鼠事件
            this.MouseDown += MainForm_MouseDown;

            // 設定重新開始按鈕
            Button restartButton = new Button();
            restartButton.Text = "重新開始";
            restartButton.Size = new Size(100, 50);
            restartButton.Location = new Point(this.ClientSize.Width - 150, 50);
            restartButton.Click += RestartButton_Click;
            this.Controls.Add(restartButton);

            // 啟動遊戲計時器
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 10;
            timer.Tick += Timer_Tick;
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (!ballMoving)
            {
                // 計算球的初始速度和方向
                double angle = Math.Atan2(e.Y - ballPosition.Y, e.X - ballPosition.X);
                initialVelocityX = Math.Cos(angle) * speedFactor;
                initialVelocityY = Math.Sin(angle) * speedFactor;
                ballMoving = true;
                timer.Start();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // 更新球的位置
            ballPosition.X += (int)initialVelocityX;
            ballPosition.Y += (int)initialVelocityY;
            initialVelocityY -= gravity;

            // 移動籃框
            MoveHoop();

            // 重新繪製球和籃框
            this.Invalidate();

            // 檢查是否進球
            if (ballPosition.X > hoopPosition.X && ballPosition.X < hoopPosition.X + 100 &&
                ballPosition.Y > hoopPosition.Y && ballPosition.Y < hoopPosition.Y + 20)
            {
                timer.Stop();
                ballMoving = false;
                score++;
                hoopSpeed = 2 + score; // 每得一分，籃框速度增加1
                MessageBox.Show("進球了！");
                ResetGame();
            }

            // 檢查球是否超出視窗邊界
            if (ballPosition.Y > this.ClientSize.Height || ballPosition.X > this.ClientSize.Width)
            {
                timer.Stop();
                ballMoving = false;
                MessageBox.Show("未中！");
                ResetGame();
            }
        }

        private void MoveHoop()
        {
            // 左右移動籃框
            if (hoopMovingRight)
            {
                hoopPosition.X += hoopSpeed;
                if (hoopPosition.X >= this.ClientSize.Width - 100)
                {
                    hoopPosition.X = this.ClientSize.Width - 100;
                    hoopMovingRight = false;
                }
            }
            else
            {
                hoopPosition.X -= hoopSpeed;
                if (hoopPosition.X <= 0)
                {
                    hoopPosition.X = 0;
                    hoopMovingRight = true;
                }
            }
        }

        private void ResetGame()
        {
            // 停止球的移動並重置位置
            ballPosition = new Point(this.ClientSize.Width / 2, this.ClientSize.Height - 30);
            initialVelocityX = 0;
            initialVelocityY = 0;
            ballMoving = false;

            // 重新繪製視窗
            this.Invalidate();
        }

        private void RestartButton_Click(object sender, EventArgs e)
        {
            // 重新啟動遊戲
            score = 0;
            hoopSpeed = 2; // 重置籃框速度
            ResetGame();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // 繪製球和籃框
            e.Graphics.FillEllipse(Brushes.Red, ballPosition.X, ballPosition.Y, 20, 20);
            e.Graphics.FillRectangle(Brushes.Black, hoopPosition.X, hoopPosition.Y, 100, 20);
            // 繪製得分
            e.Graphics.DrawString("得分: " + score, new Font("Arial", 16), Brushes.Black, 10, 10);
        }
    }
}

using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace test1
{
    public partial class Form8 : Form
    {
        private System.Windows.Forms.Timer gameTimer;
        private bool isJumping;
        private int jumpSpeed;
        private int gravity;
        private int obstacleSpeed;
        private int score;
        private int dinoX, dinoY, dinoWidth, dinoHeight;
        private int obstacleX, obstacleY, obstacleWidth, obstacleHeight;
        private int[] starX, starY; // 添加小星星的位置數組
        private int[] cactusX, cactusY;

        private int backgroundSpeed;
        private bool isPaused;
        private int spiderWidth = 100;
        private int spiderHeight = 100;

        private List<int> spiderXPositions;
        private int initialSpiderX = 800; // 初始位置設置為窗口外面
        private int spiderSpacing = 200; // 蜘蛛之间的間距



        public Form8()
        {
            InitializeComponent();
            InitializeGame();
            isPaused = false; // 初始化遊戲不暫停
        }

        private void InitializeGame()
        {
            // 设置窗体
            this.Width = 800;
            this.Height = 450;
            this.BackColor = Color.SkyBlue;
            this.Text = "Dino Game";
            this.DoubleBuffered = true;

            // 初始化游戏变量
            isJumping = false;
            jumpSpeed = 0;
            gravity = 15;
            obstacleSpeed = 10;
            score = 0;
            backgroundSpeed = 5;

            // 設置計時器
            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 20; // 每秒50幀
            gameTimer.Tick += GameTimer_Tick;

            // 初始化恐龍位置和大小
            dinoWidth = 50;
            dinoHeight = 50;
            dinoX = 50;
            dinoY = this.ClientSize.Height - dinoHeight - 50;

            // 初始化障礙物位置和大小
            obstacleWidth = 50;
            obstacleHeight = 50;
            obstacleX = this.ClientSize.Width;
            obstacleY = this.ClientSize.Height - obstacleHeight - 50;

            // 初始化小星星位置
            starX = new int[] { 200, 350, 500, 650 }; // 根據需要調整位置
            starY = new int[] { 100, 80, 120, 90 }; // 根據需要調整位置

            // 初始化仙人掌位置
            cactusX = new int[] { 100, 300, 500, 700 };
            cactusY = new int[] { this.ClientSize.Height - 100, this.ClientSize.Height - 120, this.ClientSize.Height - 110, this.ClientSize.Height - 130 };

            // 初始化蜘蛛位置
            spiderXPositions = new List<int>();

            int initialSpiderCount = 3; // 初始蜘蛛的數量
            spiderSpacing = 400; // 蜘蛛之間的間距

            for (int i = 0; i < initialSpiderCount; i++)
            {
                spiderXPositions.Add(initialSpiderX + (i * spiderSpacing));
            }





            // 啟動計時器
            gameTimer.Start();

            // 設置按键事件
            this.KeyDown += GameForm_KeyDown;
            this.KeyUp += GameForm_KeyUp;
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            // 更新恐龍位置
            if (isJumping)
            {
                dinoY -= jumpSpeed;
                jumpSpeed -= 1;
                if (dinoY + dinoHeight >= this.ClientSize.Height - 50)
                {
                    dinoY = this.ClientSize.Height - dinoHeight - 50;
                    isJumping = false;
                }
            }
            else
            {
                dinoY += gravity;
                if (dinoY + dinoHeight > this.ClientSize.Height - 50)
                {
                    dinoY = this.ClientSize.Height - dinoHeight - 50;
                }
            }

            // 更新蜘蛛的位置，並檢查是否被跳過
            for (int i = 0; i < spiderXPositions.Count; i++)
            {
                spiderXPositions[i] -= obstacleSpeed;

                // 如果蜘蛛移出了視窗範圍，將其移動到最後一只蜘蛛後面，以保持間距
                if (spiderXPositions[i] + spiderWidth < 0)
                {
                    int maxSpiderX = spiderXPositions.Max();
                    spiderXPositions[i] = maxSpiderX + spiderSpacing;
                }

                // 調整恐龍和蜘蛛的碰撞矩形
                Rectangle spiderRect = new Rectangle(spiderXPositions[i] + 20, this.ClientSize.Height - spiderHeight - 30, spiderWidth - 40, spiderHeight - 20);
                Rectangle dinoRectangle = new Rectangle(dinoX, dinoY, dinoWidth, dinoHeight);
           

            if (spiderRect.IntersectsWith(dinoRectangle))
                {
                    gameTimer.Stop(); // 遊戲结束
                    MessageBox.Show($"Game Over! Your score is: {score}");
                    Application.Restart(); // 重啟遊戲
                }

                // 檢查恐龍是否跳過蜘蛛
                if (spiderXPositions[i] + spiderWidth < dinoX)
                {
                    // 增加分數並移除該蜘蛛
                    score += 1;
                    // 將該蜘蛛移到最右邊
                    int maxSpiderX = spiderXPositions.Max();
                    spiderXPositions[i] = maxSpiderX + spiderSpacing;

                    // 檢查是否達到10分
                    if (score >= 10)
                    {
                        gameTimer.Stop(); // 停止遊戲
                        MessageBox.Show("恭喜通關！");
                        Application.Restart(); // 重啟遊戲或根據需要進行其他處理
                    }
                }
            }

                // 更新星星的位置
                for (int i = 0; i < starX.Length; i++)
            {
                starX[i] -= backgroundSpeed;
                if (starX[i] < -60) // 60 是星星的寬度
                {
                    starX[i] = this.ClientSize.Width;
                }
            }

            // 更新仙人掌的位置
            for (int i = 0; i < cactusX.Length; i++)
            {
                cactusX[i] -= backgroundSpeed;
                if (cactusX[i] < -60) // 60 是仙人掌的寬度
                {
                    cactusX[i] = this.ClientSize.Width;
                }
            }




            // 檢查碰撞
            Rectangle dinoRect = new Rectangle(dinoX, dinoY, dinoWidth, dinoHeight);
            Rectangle obstacleRect = new Rectangle(obstacleX, obstacleY, obstacleWidth, obstacleHeight);
            if (dinoRect.IntersectsWith(obstacleRect))
            {
                gameTimer.Stop();
                MessageBox.Show($"遊戲結束！您的分數為：{score}");
                Application.Restart();
            }

            // 重新繪製
            this.Invalidate();



        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W && !isJumping) // 當按下 'W' 鍵且恐龍沒有在跳躍時
            {
                isJumping = true;
                jumpSpeed = 15;
            }
            else if (e.KeyCode == Keys.Space) // 當按下空格建
            {
                if (isPaused)
                {
                    gameTimer.Start(); // 繼續遊戲
                    isPaused = false;
                }
                else
                {
                    gameTimer.Stop(); // 暂停遊戲
                    isPaused = true;
                }
                Invalidate(); // 重新繪製以顯示暫停文本
            }
        }

        private void GameForm_KeyUp(object sender, KeyEventArgs e)
        {
            // 當按下 'W' 鍵釋放時
            if (e.KeyCode == Keys.W)
            {
                isJumping = false;
            }
        }




        private void DrawScore(Graphics g)
        {
            string scoreText = $"Score: {score}";
            Font scoreFont = new Font(FontFamily.GenericSansSerif, 15, FontStyle.Bold); // 使用字體大小為 15
            g.DrawString(scoreText, scoreFont, Brushes.Black, new PointF(10, 10));
            scoreFont.Dispose(); // 釋放字體資源
        }



        private void DrawCat(Graphics g, int x, int y)
        {
            // 繪製貓咪的頭部
            g.FillEllipse(Brushes.Gray, x, y, 50, 50);

            // 繪製貓咪的耳朵
            Point[] leftEarPoints = {
        new Point(x + 5, y + 10),
        new Point(x + 20, y - 10),
        new Point(x + 30, y + 10),
        new Point(x + 15, y + 20)
    };
            g.FillPolygon(Brushes.Gray, leftEarPoints);

            Point[] rightEarPoints = {
        new Point(x + 20, y + 10),
        new Point(x + 35, y - 10),
        new Point(x + 40, y + 10),
        new Point(x + 25, y + 20)
    };
            g.FillPolygon(Brushes.Gray, rightEarPoints);

            // 繪製貓咪的眼睛
            g.FillEllipse(Brushes.White, x + 15, y + 15, 10, 10);
            g.FillEllipse(Brushes.White, x + 25, y + 15, 10, 10);
            g.FillEllipse(Brushes.Black, x + 18, y + 18, 5, 5);
            g.FillEllipse(Brushes.Black, x + 28, y + 18, 5, 5);

            // 繪製貓咪的鼻子
            g.FillEllipse(Brushes.Pink, x + 22, y + 30, 6, 6);

            // 繪製貓咪的嘴巴
            g.FillPie(Brushes.Black, x + 20, y + 32, 10, 10, 0, -180);

            // 繪製貓咪的胡鬚
            g.DrawLine(Pens.Black, x + 15, y + 30, x + 5, y + 25);
            g.DrawLine(Pens.Black, x + 15, y + 33, x + 5, y + 33);
            g.DrawLine(Pens.Black, x + 15, y + 36, x + 5, y + 41);
            g.DrawLine(Pens.Black, x + 35, y + 30, x + 45, y + 25);
            g.DrawLine(Pens.Black, x + 35, y + 33, x + 45, y + 33);
            g.DrawLine(Pens.Black, x + 35, y + 36, x + 45, y + 41);

            // 繪製貓咪的尾巴
            Point[] tailPoints = {
        new Point(x+5, y + 35),     // 尾巴起始點，位置在貓頭左側稍下方
        new Point(x - 30, y + 40),  // 尾巴向上彎曲一些
        new Point(x - 35, y + 35),  // 尾巴第三個點，向上彎曲一些
        new Point(x - 40, y + 30)   // 尾巴最終點，向上彎曲
    };
            g.DrawCurve(new Pen(Color.Gray, 3), tailPoints, tension: 0.5f);  // 使用曲線繪製尾巴，使其柔和且更自然
        }



        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            // 繪製背景
            DrawBackground(g);

            // 繪製貓咪
            DrawCat(g, dinoX, dinoY);

            // 繪製障礙物
            g.FillRectangle(Brushes.Red, obstacleX, obstacleY, obstacleWidth, obstacleHeight);

            // 如果遊戲暫停，繪製暫停文本
            if (isPaused)
            {
                DrawPausedText(g);
            }

            // 繪製蜘蛛
            for (int i = 0; i < spiderXPositions.Count; i++)
            {
                Color spiderColor = i switch
                {
                    0 => Color.Green,
                    1 => Color.Red,
                    2 => Color.Blue,
                    _ => Color.Black // 如果需要添加更多蜘蛛，可以使用其他顏色
                };
                DrawSpider(g, spiderXPositions[i], this.ClientSize.Height - spiderHeight - 10, spiderColor); // 將蜘蛛位置調整到地面上方
            }

            // 繪製分數
            DrawScore(g);
        }

        private void DrawPausedText(Graphics g)
        {
            string pausedText = "Game Paused";
            Font pausedFont = new Font(FontFamily.GenericSansSerif, 24, FontStyle.Bold);
            SizeF pausedTextSize = g.MeasureString(pausedText, pausedFont);
            PointF pausedTextLocation = new PointF((this.ClientSize.Width - pausedTextSize.Width) / 2, (this.ClientSize.Height - pausedTextSize.Height) / 2);
            g.DrawString(pausedText, pausedFont, Brushes.Black, pausedTextLocation);
            pausedFont.Dispose();
        }

        private void DrawSpider(Graphics g, int x, int y, Color color)
        {
            // 繪製蜘蛛的身體
            g.FillEllipse(new SolidBrush(color), x + 20, y + 20, 60, 40); // 身體部分
            g.FillEllipse(new SolidBrush(color), x + 35, y, 30, 30); // 頭部

            // 繪製蜘蛛的腿
            Pen legPen = new Pen(color, 4);
            int legLength = 40; // 較短的腿長度
                                // 左側的腿
            g.DrawLine(legPen, x + 40, y + 30, x + 40 - legLength, y + 30 - legLength);
            g.DrawLine(legPen, x + 40, y + 30, x + 40 - legLength, y + 30 + legLength);
            g.DrawLine(legPen, x + 40, y + 35, x + 40 - legLength, y + 35 - legLength / 2);
            g.DrawLine(legPen, x + 40, y + 35, x + 40 - legLength, y + 35 + legLength / 2);

            // 右側的腿
            g.DrawLine(legPen, x + 60, y + 30, x + 60 + legLength, y + 30 - legLength);
            g.DrawLine(legPen, x + 60, y + 30, x + 60 + legLength, y + 30 + legLength);
            g.DrawLine(legPen, x + 60, y + 35, x + 60 + legLength, y + 35 - legLength / 2);
            g.DrawLine(legPen, x + 60, y + 35, x + 60 + legLength, y + 35 + legLength / 2);

            // 繪製蜘蛛的眼睛
            g.FillEllipse(Brushes.White, x + 40, y + 10, 10, 10); // 左眼
            g.FillEllipse(Brushes.White, x + 50, y + 10, 10, 10); // 右眼
            g.FillEllipse(Brushes.Black, x + 43, y + 13, 5, 5);   // 左眼瞳孔
            g.FillEllipse(Brushes.Black, x + 53, y + 13, 5, 5);   // 右眼瞳孔

            // 繪製蜘蛛的嘴巴
            g.FillPie(Brushes.Black, x + 42, y + 20, 15, 10, 0, -180); // 嘴巴

            // 繪製蜘蛛的觸角
            g.DrawLine(legPen, x + 40, y + 20, x + 30, y + 10);
            g.DrawLine(legPen, x + 60, y + 20, x + 70, y + 10);
        }


        public enum SnakeType
        {
            Green,
            Red,
            Blue
        }

        private void DrawBackground(Graphics g)
        {
            // Change background color to light brown for the entire background
            g.Clear(Color.FromArgb(222, 184, 135)); // Light brown color

            // Change ground color to a slightly darker shade of brown
            g.FillRectangle(Brushes.SaddleBrown, 0, this.ClientSize.Height - 50, this.ClientSize.Width, 50);

            // Calculate the height of the deep blue section (1/4 of screen height)
            int deepBlueHeight = this.ClientSize.Height / 4;

            // 深蓝色背景
            g.FillRectangle(Brushes.DarkBlue, 0, 0, this.ClientSize.Width, deepBlueHeight);

            // 小星星
            foreach (var (x, y) in starX.Zip(starY, Tuple.Create))
            {
                // 调整小星星位置，向上偏移一些
                int adjustedY = y - 45; // 可以根据需要调整偏移量
                                        // 限制小星星在深蓝色背景范围内
                if (adjustedY >= 0 && adjustedY <= deepBlueHeight)
                {
                    DrawStar(g, x, adjustedY);
                }
            }

            // 仙人掌
            foreach (var (x, y) in cactusX.Zip(cactusY, Tuple.Create))
            {
                DrawCactus(g, x, y);
            }
        }

        private void DrawStar(Graphics g, int x, int y)
        {
            // 小星星
            Point[] starPoints = new Point[]
            {
        new Point(x, y + 15),
        new Point(x + 5, y + 5),
        new Point(x + 15, y),
        new Point(x + 5, y - 5),
        new Point(x, y - 15),
        new Point(x - 5, y - 5),
        new Point(x - 15, y),
        new Point(x - 5, y + 5),
        new Point(x, y + 15)
            };
            g.FillPolygon(Brushes.Yellow, starPoints);
        }

        private void DrawCactus(Graphics g, int x, int y)
        {
            // 仙人掌
            g.FillRectangle(Brushes.Green, x + 15, y, 30, 75); // 主幹
            g.FillRectangle(Brushes.Green, x, y + 15, 15, 30); // 左手
            g.FillRectangle(Brushes.Green, x + 45, y + 15, 15, 30); // 右手

            // 左臂
            Point[] leftArmPoints = new Point[]
            {
        new Point(x, y + 15),
        new Point(x, y - 75)
            };
            g.FillPolygon(Brushes.Green, leftArmPoints);

            // 右臂
            Point[] rightArmPoints = new Point[]
            {
        new Point(x + 60, y + 15),
        new Point(x + 60, y - 75)
            };
            g.FillPolygon(Brushes.Green, rightArmPoints);
        }
    }
}

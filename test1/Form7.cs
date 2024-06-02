using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using test1;

namespace test1
{
    public partial class Form7 : Form
    {
        private System.Windows.Forms.Timer gameTimer;
        private bool isJumping;
        private int jumpSpeed;
        private int gravity;
        private int obstacleSpeed;
        private int score;
        private int dinoX, dinoY, dinoWidth, dinoHeight;
        private int obstacleX, obstacleY, obstacleWidth, obstacleHeight;
        private int[] cloudX, cloudY;
        private int[] treeX, treeY;
        private int[] birdX, birdY;
        private int backgroundSpeed;
        private bool isPaused;
        private int snakeWidth = 80;
        private int snakeHeight = 80;

        private List<int> snakeXPositions;
        private int initialSnakeX = 800; // 初始位置設在視窗外面
        private int snakeSpacing = 200; // 蛇之间的間距




        public Form7()
        {
            InitializeComponent();
            InitializeGame();
            isPaused = false; // 初始化时游戏不暂停
        }

        private void InitializeGame()
        {
            // 設定視窗
            this.Width = 800;
            this.Height = 450;
            this.BackColor = Color.SkyBlue;
            this.Text = "恐龍遊戲";
            this.DoubleBuffered = true;

            // 初始化遊戲變數
            isJumping = false;
            jumpSpeed = 0;
            gravity = 15;
            obstacleSpeed = 10;
            score = 0;
            backgroundSpeed = 5;

            // 設定計時器
            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 20; // 50幀每秒
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

            // 初始化雲的位置
            cloudX = new int[] { 150, 300, 450, 600 };
            cloudY = new int[] { 50, 30, 70, 40 };

            // 初始化樹木的位置
            treeX = new int[] { 100, 300, 500, 700 };
            treeY = new int[] { this.ClientSize.Height - 100, this.ClientSize.Height - 120, this.ClientSize.Height - 110, this.ClientSize.Height - 130 };

            // 初始化小鳥的位置
            birdX = new int[] { 200, 400, 600 };
            birdY = new int[] { 100, 150, 120 };

            // 初始化蛇的位置
            snakeXPositions = new List<int>();

            int initialSnakeCount = 3; // 初始蛇的數量
            snakeSpacing = 400; // 蛇之間的間隔

            for (int i = 0; i < initialSnakeCount; i++)
            {
                snakeXPositions.Add(initialSnakeX + (i * snakeSpacing));
            }

            // 啟動計時器
            gameTimer.Start();

            // 設定按鍵事件
            this.KeyDown += GameForm_KeyDown;
            this.KeyUp += GameForm_KeyUp;
        }

        private void CheckNextLevel()
        {
            if (score >= 10)
            {
                // 分數達到10分，停止目前的遊戲
                gameTimer.Stop();
                MessageBox.Show("Congratulations! You've passed to the next level.");

                // 關閉目前的窗體，並顯示下一關窗體
                this.Hide(); //隱藏目前的窗體
                Form8 nextLevelForm = new Form8(); // 創建並連結下一個是視窗
                nextLevelForm.ShowDialog(); // 顯示下一關的視窗
                this.Close(); // 關閉目前的視窗
            }
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
            // 更新蛇的位置並檢查是否被跳過
            for (int i = 0; i < snakeXPositions.Count; i++)
            {
                snakeXPositions[i] -= obstacleSpeed;

                // 如果蛇移出了視窗，將其移動到最後一條蛇後面，以保持間隔
                if (snakeXPositions[i] + snakeWidth < 0)
                {
                    int maxSnakeX = snakeXPositions.Max();
                    snakeXPositions[i] = maxSnakeX + snakeSpacing;
                }
            


            // 檢查每條蛇是否與恐龍發生碰撞
            Rectangle snakeRect = new Rectangle(snakeXPositions[i], this.ClientSize.Height - snakeHeight - 50, snakeWidth, snakeHeight);
                Rectangle dinoRectangle = new Rectangle(dinoX, dinoY, dinoWidth, dinoHeight);

                if (snakeRect.IntersectsWith(dinoRectangle))
                {
                    gameTimer.Stop(); // 遊戲結束
                    MessageBox.Show($"遊戲結束！您的分數為：{score}");
                    Application.Restart(); // 重新啟動遊戲
                }

                // 檢查恐龍是否跳過蛇
                if (snakeXPositions[i] + snakeWidth < dinoX)
                {
                    // 增加分數並移除該蛇
                    score += 1;
                    // 將該蛇移至最右邊
                    int maxSnakeX = snakeXPositions.Max();
                    snakeXPositions[i] = maxSnakeX + snakeSpacing;
                }
            }


            // 更新雲的位置
            for (int i = 0; i < cloudX.Length; i++)
            {
                cloudX[i] -= backgroundSpeed;
                if (cloudX[i] < -60) // 60 為雲的寬度
                {
                    cloudX[i] = this.ClientSize.Width;
                }
            }

            // 更新樹的位置
            for (int i = 0; i < treeX.Length; i++)
            {
                treeX[i] -= backgroundSpeed;
                if (treeX[i] < -60) // 60 為樹的寬度
                {
                    treeX[i] = this.ClientSize.Width;
                }
            }

            // 更新小鳥的位置
            for (int i = 0; i < birdX.Length; i++)
            {
                birdX[i] -= backgroundSpeed + 2; // 小鳥移動速度稍快
                if (birdX[i] < -40) // 40 為小鳥的寬度
                {
                    birdX[i] = this.ClientSize.Width;
                }
            }


            // 檢查碰撞
            Rectangle dinoRect = new Rectangle(dinoX, dinoY, dinoWidth, dinoHeight);
            Rectangle obstacleRect = new Rectangle(obstacleX, obstacleY, obstacleWidth, obstacleHeight);
            if (dinoRect.IntersectsWith(obstacleRect)) // 如果恐龍矩形和障礙物矩形相交
            {
                gameTimer.Stop(); // 停止計時器
                MessageBox.Show($"遊戲結束！您的分數是：{score}"); // 顯示遊戲結束訊息和分數
                Application.Restart(); // 重新啟動應用程式
            }

            // 重新繪製
            this.Invalidate(); // 重繪視窗

            CheckNextLevel(); // 檢查是否進入下一級別

        }


            // 當按下按鍵時觸發的事件
            private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            // 如果按下的是 'W' 鍵且恐龍沒有在跳躍中
            if (e.KeyCode == Keys.W && !isJumping)
            {
                // 設置跳躍狀態為 true，跳躍速度為 15
                isJumping = true;
                jumpSpeed = 15;
            }
            // 如果按下的是空格鍵
            else if (e.KeyCode == Keys.Space)
            {
                // 如果遊戲已暫停
                if (isPaused)
                {
                    gameTimer.Start(); // 繼續遊戲
                    isPaused = false;
                }
                else
                {
                    gameTimer.Stop(); // 暫停遊戲
                    isPaused = true;
                }
                Invalidate(); // 重新繪製以顯示暫停文本
            }
        }


        // 當按鍵放開時觸發的事件
        private void GameForm_KeyUp(object sender, KeyEventArgs e)
        {
            // 如果放開的是 'W' 鍵
            if (e.KeyCode == Keys.W)
            {
                // 將跳躍狀態設置為 false
                isJumping = false;
            }
        }




        // 繪製分數
        private void DrawScore(Graphics g)
        {
            // 分數文本
            string scoreText = $"Score: {score}";
            // 分數字型
            Font scoreFont = new Font(FontFamily.GenericSansSerif, 15, FontStyle.Bold); // 使用字體大小為 15
            // 繪製分數文本到指定位置
            g.DrawString(scoreText, scoreFont, Brushes.Black, new PointF(10, 10));
            // 釋放字型資源
            scoreFont.Dispose();
        }




        private void DrawCat(Graphics g, int x, int y)
        {
            // 绘制猫咪的头
            g.FillEllipse(Brushes.Gray, x, y, 50, 50);

            // 绘制猫咪的耳朵
            Point[] leftEarPoints = {
                new Point(x + 5, y + 10),
                new Point(x + 20, y - 10),
                new Point(x + 30, y + 10),
                new Point(x + 15, y + 20)
            };
            // 繪製貓咪的左耳朵
            g.FillPolygon(Brushes.Gray, leftEarPoints);

            // 繪製貓咪的右耳朵
            Point[] rightEarPoints = {
                new Point(x + 20, y + 10),  // 右耳朵頂點
                new Point(x + 35, y - 10),  // 右耳朵上側點
                new Point(x + 40, y + 10),  // 右耳朵頂點
                new Point(x + 25, y + 20)   // 右耳朵下側點
            };
            g.FillPolygon(Brushes.Gray, rightEarPoints);


            // 猫咪的眼睛
            g.FillEllipse(Brushes.White, x + 15, y + 15, 10, 10);
            g.FillEllipse(Brushes.White, x + 25, y + 15, 10, 10);
            g.FillEllipse(Brushes.Black, x + 18, y + 18, 5, 5);
            g.FillEllipse(Brushes.Black, x + 28, y + 18, 5, 5);

            // 猫咪的鼻子
            g.FillEllipse(Brushes.Pink, x + 22, y + 30, 6, 6);

            // 貓咪的嘴巴
            g.FillPie(Brushes.Black, x + 20, y + 32, 10, 10, 0, -180);

            // 貓咪的鬍鬚
            g.DrawLine(Pens.Black, x + 15, y + 30, x + 5, y + 25);
            g.DrawLine(Pens.Black, x + 15, y + 33, x + 5, y + 33);
            g.DrawLine(Pens.Black, x + 15, y + 36, x + 5, y + 41);
            g.DrawLine(Pens.Black, x + 35, y + 30, x + 45, y + 25);
            g.DrawLine(Pens.Black, x + 35, y + 33, x + 45, y + 33);
            g.DrawLine(Pens.Black, x + 35, y + 36, x + 45, y + 41);

            // 绘制猫咪的尾巴
            Point[] tailPoints = {
            new Point(x+5, y + 35),     // 尾巴起起點，位置在猫頭左側下方
            new Point(x - 30, y + 40),  // 尾巴向上彎曲一些
            new Point(x - 35, y + 35),  // 尾巴第三个點，向上彎曲
            new Point(x - 40, y + 30)   // 尾巴最終點，向上彎曲
            };
            g.DrawCurve(new Pen(Color.Gray, 3), tailPoints, tension: 0.5f);  // 使用曲线绘制尾巴，使其柔和且更自然
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            // 繪製背景
            DrawBackground(g);

            // 繪製猫咪
            DrawCat(g, dinoX, dinoY);

            // 繪製障礙物
            g.FillRectangle(Brushes.Red, obstacleX, obstacleY, obstacleWidth, obstacleHeight);

            // 如果遊戲暫停，繪製暫停文本
            if (isPaused)
            {
                DrawPausedText(g);
            }

            // 繪製蛇
            for (int i = 0; i < snakeXPositions.Count; i++)
            {
                Color snakeColor = i switch
                {
                    0 => Color.Green,
                    1 => Color.Red,
                    2 => Color.Blue,
                    _ => Color.Black // 如果有更多蛇會有更多的顏色
                };
                DrawSnake(g, snakeXPositions[i], this.ClientSize.Height - snakeHeight - 50, snakeColor);
            }

            // 繪製分數
            DrawScore(g);
        }
        // 繪製遊戲暫停時的文字
        private void DrawPausedText(Graphics g)
        {
            // 定義暫停文字
            string pausedText = "Game Paused";
            // 創建暫停文字字體
            Font pausedFont = new Font(FontFamily.GenericSansSerif, 24, FontStyle.Bold);
            // 測量暫停文字的大小
            SizeF pausedTextSize = g.MeasureString(pausedText, pausedFont);
            // 計算暫停文字的位置，使其置中顯示
            PointF pausedTextLocation = new PointF((this.ClientSize.Width - pausedTextSize.Width) / 2, (this.ClientSize.Height - pausedTextSize.Height) / 2);
            // 在畫布上繪製暫停文字
            g.DrawString(pausedText, pausedFont, Brushes.Black, pausedTextLocation);
            // 釋放字體資源
            pausedFont.Dispose();
        }


        private void DrawSnake(Graphics g, int x, int y, Color color)
        {
            // 蛇的头部
            g.FillEllipse(new SolidBrush(color), x, y, 40, 40);

            // 蛇的身体
            g.FillEllipse(new SolidBrush(color), x + 30, y + 10, 40, 40);
            g.FillEllipse(new SolidBrush(color), x + 60, y + 20, 40, 40);
            g.FillEllipse(new SolidBrush(color), x + 90, y + 30, 40, 40);

            // 蛇的尾部
            g.FillEllipse(new SolidBrush(color), x + 120, y + 40, 40, 40);

            // 蛇的眼睛
            g.FillEllipse(Brushes.White, x + 10, y + 10, 10, 10);
            g.FillEllipse(Brushes.White, x + 10, y + 30, 10, 10);
            g.FillEllipse(Brushes.Black, x + 15, y + 15, 5, 5);
            g.FillEllipse(Brushes.Black, x + 15, y + 35, 5, 5);

            // 蛇的舌頭
            g.FillPie(Brushes.Red, x - 10, y + 20, 20, 20, 0, 180);
        }

        // 定義蛇的類型列舉
        public enum SnakeType
        {
            Green,  // 綠色蛇
            Red,    // 紅色蛇
            Blue    // 藍色蛇
        }


        private void DrawBackground(Graphics g)
        {
            // 繪製地面
            g.FillRectangle(Brushes.SandyBrown, 0, this.ClientSize.Height - 50, this.ClientSize.Width, 50);

            // 繪製雲
            foreach (var (x, y) in cloudX.Zip(cloudY, Tuple.Create))
            {
                DrawCloud(g, x, y);
            }

            // 繪製樹木
            foreach (var (x, y) in treeX.Zip(treeY, Tuple.Create))
            {
                DrawTree(g, x, y);
            }

            // 繪製小鳥
            foreach (var (x, y) in birdX.Zip(birdY, Tuple.Create))
            {
                DrawBird(g, x, y);
            }

        }

        // 繪製雲的方法
        private void DrawCloud(Graphics g, int x, int y)
        {
            // 繪製雲朵的圓形部分
            g.FillEllipse(Brushes.White, x, y, 60, 30);
            g.FillEllipse(Brushes.White, x + 20, y - 10, 60, 40);
            g.FillEllipse(Brushes.White, x + 40, y, 60, 30);
        }


        private void DrawTree(Graphics g, int x, int y)
        {
            // 樹幹
            g.FillRectangle(Brushes.SaddleBrown, x + 10, y, 20, 50);

            // 樹葉
            g.FillEllipse(Brushes.ForestGreen, x - 10, y - 30, 60, 60);
        }

        private void DrawBird(Graphics g, int x, int y)
        {
            // 小鳥的身体
            g.FillEllipse(Brushes.Gold, x, y, 40, 20);

            // 小鳥的翅膀
            g.FillPolygon(Brushes.Gold, new Point[]
            {
                new Point(x + 10, y),
                new Point(x + 30, y),
                new Point(x + 20, y - 20)
            });

            // 小鳥的眼睛
            g.FillEllipse(Brushes.Black, x + 25, y + 5, 7, 7);


            // 小鳥的嘴巴
            Point[] beakPoints = {
                new Point(x + 35, y + 10),
                new Point(x + 40, y + 12),
                new Point(x + 35, y + 14)
            };
            g.FillPolygon(Brushes.Orange, beakPoints);

            // 小鳥的尾巴
            Point[] tailPoints = {
                new Point(x, y + 10),
                new Point(x - 10, y + 5),
                new Point(x - 10, y + 15)
            };
            g.FillPolygon(Brushes.Gold, tailPoints);
        }

    }
}
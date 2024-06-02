using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace test1
{
    public partial class Form10 : Form
    {
        private System.Windows.Forms.Timer gameTimer; // 使用 System.Windows.Forms.Timer 來控制遊戲時間
        private List<Point> snake; // 用於儲存蛇的每一個節點的位置
        private List<Monster> monsters; // 用 Monster 類別來定義怪物並儲存怪物的位置
        private Point food; // 食物的位置
        private int direction; // 蛇移動的方向：0=上, 1=右, 2=下, 3=左
        private int score; // 玩家分數
        private bool gameOver; // 判斷遊戲是否結束
        private Label scoreLabel; // 顯示分數的標籤
        private Button restartButton; // 重新開始遊戲的按鈕

        public Form10()
        {
            InitializeComponent();
            InitializeGame(); // 初始化遊戲
        }

        private void InitializeGame()
        {
            this.ClientSize = new Size(800, 800); // 設定視窗大小為800x800
            this.Text = "Snake Game"; // 設定視窗標題

            // 初始化並添加顯示分數的標籤
            scoreLabel = new Label();
            scoreLabel.Text = "Score: 0";
            scoreLabel.Location = new Point(10, 760); // 設定分數標籤的位置
            this.Controls.Add(scoreLabel); // 將分數標籤加入視窗中

            // 初始化並添加重新開始按鈕
            restartButton = new Button();
            restartButton.Text = "Restart";
            restartButton.Location = new Point(100, 760); // 設定重新開始按鈕的位置
            restartButton.Click += RestartButton_Click; // 為重新開始按鈕添加點擊事件
            this.Controls.Add(restartButton); // 將重新開始按鈕加入視窗中

            // 設定視窗獲取鍵盤焦點以確保鍵盤事件生效
            this.KeyPreview = true;
            this.Focus();

            // 初始化遊戲計時器
            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 100; // 設定遊戲速度（每100毫秒更新一次）
            gameTimer.Tick += UpdateScreen; // 設定計時器的觸發事件

            StartNewGame(); // 開始新遊戲
        }

        private void StartNewGame()
        {
            // 初始化蛇的初始位置
            snake = new List<Point>
            {
                new Point(10, 10),
                new Point(10, 11),
                new Point(10, 12)
            };

            // 初始化怪物的位置和方向
            monsters = new List<Monster>
            {
                new Monster(new Point(5, 5), 1),
                new Monster(new Point(15, 15), 2),
                new Monster(new Point(20, 20), 3)
            };

            direction = 1; // 蛇初始向右移動
            score = 0; // 初始化分數
            gameOver = false; // 設定遊戲狀態為未結束
            GenerateFood(); // 生成食物位置
            gameTimer.Start(); // 啟動遊戲計時器
            restartButton.Enabled = false; // 禁用重新開始按鈕
        }

        private void GenerateFood()
        {
            // 隨機生成食物的位置
            Random rnd = new Random();
            food = new Point(rnd.Next(0, this.ClientSize.Width / 20), rnd.Next(0, this.ClientSize.Height / 20));
        }

        private void UpdateScreen(object sender, EventArgs e)
        {
            if (!gameOver)
            {
                MoveSnake(); // 移動蛇
                MoveMonsters(); // 移動怪物
                CheckCollision(); // 檢查碰撞
                this.Invalidate(); // 重新繪製畫面
                scoreLabel.Text = $"Score: {score}"; // 更新分數標籤
            }
        }

        private void MoveSnake()
        {
            // 移動蛇的身體（從尾部開始）
            for (int i = snake.Count - 1; i > 0; i--)
            {
                snake[i] = snake[i - 1];
            }

            // 根據方向移動蛇的頭部
            switch (direction)
            {
                case 0: snake[0] = new Point(snake[0].X, snake[0].Y - 1); break; // 上
                case 1: snake[0] = new Point(snake[0].X + 1, snake[0].Y); break; // 右
                case 2: snake[0] = new Point(snake[0].X, snake[0].Y + 1); break; // 下
                case 3: snake[0] = new Point(snake[0].X - 1, snake[0].Y); break; // 左
            }
        }

        private void MoveMonsters()
        {
            // 移動每個怪物
            foreach (Monster monster in monsters)
            {
                monster.Move();
            }
        }

        private void CheckCollision()
        {
            // 檢查蛇是否撞到牆壁
            if (snake[0].X < 0 || snake[0].X >= this.ClientSize.Width / 20 || snake[0].Y < 0 || snake[0].Y >= this.ClientSize.Height / 20)
            {
                GameOver();
            }

            // 檢查蛇是否撞到自己
            for (int i = 1; i < snake.Count; i++)
            {
                if (snake[0] == snake[i])
                {
                    GameOver();
                }
            }

            // 檢查蛇是否吃到食物
            if (snake[0] == food)
            {
                // 增加蛇的長度
                snake.Add(new Point(snake[snake.Count - 1].X, snake[snake.Count - 1].Y));
                score++; // 增加分數
                GenerateFood(); // 生成新的食物
            }

            // 檢查蛇是否碰到怪物
            foreach (Monster monster in monsters)
            {
                if (snake[0] == monster.Position)
                {
                    Console.WriteLine($"Collision with monster at: {monster.Position}");
                    GameOver();
                }
            }
        }

        private void GameOver()
        {
            gameOver = true; // 設定遊戲狀態為結束
            gameTimer.Stop(); // 停止遊戲計時器
            MessageBox.Show($"Game Over! Your score is {score}"); // 顯示遊戲結束訊息
            restartButton.Enabled = true; // 啟用重新開始按鈕
        }

        private void RestartButton_Click(object sender, EventArgs e)
        {
            StartNewGame(); // 重新開始新遊戲
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;

            if (!gameOver)
            {
                Brush snakeColor;
                // 繪製蛇
                for (int i = 0; i < snake.Count; i++)
                {
                    if (i == 0)
                        snakeColor = Brushes.Black; // 蛇頭顏色
                    else
                        snakeColor = Brushes.Green; // 蛇身顏色

                    canvas.FillRectangle(snakeColor, new Rectangle(snake[i].X * 20, snake[i].Y * 20, 20, 20));
                }

                // 繪製食物
                canvas.FillRectangle(Brushes.Red, new Rectangle(food.X * 20, food.Y * 20, 20, 20));

                // 繪製怪物
                Brush monsterColor = Brushes.Blue;
                foreach (Monster monster in monsters)
                {
                    canvas.FillRectangle(monsterColor, new Rectangle(monster.Position.X * 20, monster.Position.Y * 20, 20, 20));
                }
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            // 根據鍵盤按鍵改變蛇的移動方向
            switch (e.KeyCode)
            {
                case Keys.Left:
                    if (direction != 1) direction = 3; // 當前方向不為右時可以向左轉
                    break;
                case Keys.Right:
                    if (direction != 3) direction = 1; // 當前方向不為左時可以向右轉
                    break;
                case Keys.Up:
                    if (direction != 2) direction = 0; // 當前方向不為下時可以向上轉
                    break;
                case Keys.Down:
                    if (direction != 0) direction = 2; // 當前方向不為上時可以向下轉
                    break;
            }
        }
    }

    public class Monster
    {
        public Point Position { get; private set; } // 怪物的位置
        private int direction; // 怪物移動的方向：0=上, 1=右, 2=下, 3=左
        private Random rnd; // 用於隨機生成方向

        public Monster(Point startPosition, int startDirection)
        {
            Position = startPosition; // 初始化怪物位置
            direction = startDirection; // 初始化怪物方向
            rnd = new Random(); // 初始化隨機數生成器
        }

        public void Move()
        {
            // 隨機改變方向
            if (rnd.Next(0, 10) < 2) // 20%的機率改變方向
            {
                direction = rnd.Next(0, 4); // 隨機選擇一個方向
            }

            // 根據方向移動怪物
            switch (direction)
            {
                case 0: Position = new Point(Position.X, Position.Y - 1); break; // 上
                case 1: Position = new Point(Position.X + 1, Position.Y); break; // 右
                case 2: Position = new Point(Position.X, Position.Y + 1); break; // 下
                case 3: Position = new Point(Position.X - 1, Position.Y); break; // 左
            }

            // 確保怪物不會移出邊界
            if (Position.X < 0) Position = new Point(0, Position.Y);
            if (Position.X >= 40) Position = new Point(39, Position.Y);
            if (Position.Y < 0) Position = new Point(Position.X, 0);
            if (Position.Y >= 40) Position = new Point(Position.X, 39);
        }
    }
}

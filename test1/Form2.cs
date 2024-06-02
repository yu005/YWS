using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace test1
{
    public partial class Form2 : Form
    {
        private PictureBox pictureBoxPlayer; // 用於顯示玩家選擇的圖片
        private PictureBox pictureBoxComputer; // 用於顯示電腦選擇的圖片
        private Label labelVS; // 用於顯示 "V.S." 的標籤
        private Button buttonRock; // "石頭" 按鈕
        private Button buttonScissors; // "剪刀" 按鈕
        private Button buttonPaper; // "布" 按鈕
        private System.Windows.Forms.Timer timerVS; // 用於定時更新 "V.S." 標籤

        public Form2()
        {
            InitializeComponent();
            InitializeUI(); // 初始化UI元素
            this.Paint += Form2_Paint; // 註冊繪製事件
        }

        // 繪製窗體背景的事件處理
        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            Color startColor = Color.White; // 漸層色的起始顏色
            Color endColor = Color.LightGray; // 漸層色的結束顏色

            // 繪製漸層色背景
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            LinearGradientBrush brush = new LinearGradientBrush(rect, startColor, endColor, LinearGradientMode.Vertical);
            e.Graphics.FillRectangle(brush, rect);
        }

        // 初始化UI元素
        private void InitializeUI()
        {
            this.ClientSize = new Size(600, 400); // 設置窗體大小
            this.Text = "Rock Paper Scissors Game"; // 設置窗體標題

            // 添加玩家圖片框
            pictureBoxPlayer = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.StretchImage,
                Location = new Point(50, 20),
                Size = new Size(180, 180),
                Image = Properties.Resources.finger_png // 設置初始圖片
            };
            this.Controls.Add(pictureBoxPlayer);

            // 添加電腦圖片框
            pictureBoxComputer = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.StretchImage,
                Location = new Point(370, 20),
                Size = new Size(180, 180),
                Image = Properties.Resources.pc // 設置初始圖片
            };
            this.Controls.Add(pictureBoxComputer);

            // 添加“V.S.”標籤
            labelVS = new Label
            {
                Text = "V.S.",
                Font = new Font("Arial", 24, FontStyle.Bold),
                Location = new Point(270, 90),
                AutoSize = true
            };
            this.Controls.Add(labelVS);

            // 初始化定時器
            timerVS = new System.Windows.Forms.Timer();
            timerVS.Interval = 100; // 每100毫秒更新一次
            timerVS.Tick += TimerVS_Tick; // 註冊定時器事件
            timerVS.Start(); // 啟動定時器

            // 添加“石頭”按鈕
            buttonRock = new Button
            {
                Text = "石頭",
                Location = new Point(50, 250),
                Size = new Size(80, 30)
            };
            buttonRock.Click += ButtonRock_Click; // 註冊按鈕點擊事件
            this.Controls.Add(buttonRock);

            // 添加“剪刀”按鈕
            buttonScissors = new Button
            {
                Text = "剪刀",
                Location = new Point(250, 250),
                Size = new Size(80, 30)
            };
            buttonScissors.Click += ButtonScissors_Click; // 註冊按鈕點擊事件
            this.Controls.Add(buttonScissors);

            // 添加“布”按鈕
            buttonPaper = new Button
            {
                Text = "布",
                Location = new Point(450, 250),
                Size = new Size(80, 30)
            };
            buttonPaper.Click += ButtonPaper_Click; // 註冊按鈕點擊事件
            this.Controls.Add(buttonPaper);

            // 添加對應選項的圖片
            AddOptionImages();
        }

        // 定時器的Tick事件處理，用於更新“V.S.”標籤的顏色和字體
        private void TimerVS_Tick(object sender, EventArgs e)
        {
            Random random = new Random();

            // 隨機生成顏色
            Color randomColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));

            // 隨機生成字體大小
            float randomFontSize = 24 + random.Next(-2, 3); // 在24基礎上隨機調整大小

            // 更新標籤顏色和字體
            labelVS.ForeColor = randomColor;
            labelVS.Font = new Font("Arial", randomFontSize, FontStyle.Bold);
        }

        // 添加對應選項的圖片框
        private void AddOptionImages()
        {
            PictureBox pictureBoxRock = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.StretchImage,
                Location = new Point(50, 290),
                Size = new Size(80, 80),
                Image = Properties.Resources.stone
            };
            this.Controls.Add(pictureBoxRock);

            PictureBox pictureBoxScissors = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.StretchImage,
                Location = new Point(250, 290),
                Size = new Size(80, 80),
                Image = Properties.Resources.scissors
            };
            this.Controls.Add(pictureBoxScissors);

            PictureBox pictureBoxPaper = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.StretchImage,
                Location = new Point(450, 290),
                Size = new Size(80, 80),
                Image = Properties.Resources.paper
            };
            this.Controls.Add(pictureBoxPaper);
        }

        // "石頭"按鈕點擊事件處理
        private void ButtonRock_Click(object sender, EventArgs e)
        {
            pictureBoxPlayer.Image = Properties.Resources.stone; // 更新玩家圖片為石頭
            PlayGame(0); // 0代表石頭
        }

        // "剪刀"按鈕點擊事件處理
        private void ButtonScissors_Click(object sender, EventArgs e)
        {
            pictureBoxPlayer.Image = Properties.Resources.scissors; // 更新玩家圖片為剪刀
            PlayGame(1); // 1代表剪刀
        }

        // "布"按鈕點擊事件處理
        private void ButtonPaper_Click(object sender, EventArgs e)
        {
            pictureBoxPlayer.Image = Properties.Resources.paper; // 更新玩家圖片為布
            PlayGame(2); // 2代表布
        }

        // 遊戲邏輯處理
        private void PlayGame(int playerChoice)
        {
            Random random = new Random();
            int computerChoice = random.Next(0, 3); // 電腦隨機選擇 0, 1, 2

            // 根據選擇更新電腦圖片
            switch (computerChoice)
            {
                case 0:
                    pictureBoxComputer.Image = Properties.Resources.stone;
                    break;
                case 1:
                    pictureBoxComputer.Image = Properties.Resources.scissors;
                    break;
                case 2:
                    pictureBoxComputer.Image = Properties.Resources.paper;
                    break;
            }

            // 判斷輸贏
            string resultMessage;
            Color resultColor;
            if (playerChoice == computerChoice)
            {
                resultMessage = "平局！╮(′～‵〞)╭";
                resultColor = Color.Blue;
            }
            else if ((playerChoice + 1) % 3 == computerChoice)
            {
                resultMessage = "你赢了！(๑˃́ꇴ˂̀๑)";
                resultColor = Color.Green;
            }
            else
            {
                resultMessage = "你输了！(｀0´)";
                resultColor = Color.Red;
            }

            // 顯示結果
            ShowResult(resultMessage, resultColor);

            // 重置圖片框
            pictureBoxPlayer.Image = Properties.Resources.finger_png;
            pictureBoxComputer.Image = Properties.Resources.pc;
        }

        // 顯示結果的窗體
        private void ShowResult(string message, Color color)
        {
            Form resultForm = new Form
            {
                Size = new Size(400, 250),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            Label labelMessage = new Label
            {
                Text = message,
                ForeColor = color,
                Font = new Font("Arial", 18, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(50, 50),
                TextAlign = ContentAlignment.MiddleCenter
            };

            Button buttonClose = new Button
            {
                Text = "知道啦",
                Location = new Point(100, 120),
                Size = new Size(100, 30)
            };
            buttonClose.Click += (s, e) => resultForm.Close(); // 按下關閉按鈕時關閉結果窗體

            resultForm.Controls.Add(labelMessage); // 將結果標籤添加到結果窗體
            resultForm.Controls.Add(buttonClose); // 將關閉按鈕添加到結果窗體

            resultForm.ShowDialog(); // 顯示結果窗體，並等待用戶操作
        }
    }
}

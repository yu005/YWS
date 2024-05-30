using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace test1
{
    public partial class Form2 : Form
    {
        private PictureBox pictureBoxPlayer;
        private PictureBox pictureBoxComputer;
        private Label labelVS;
        private Button buttonRock;
        private Button buttonScissors;
        private Button buttonPaper;
        private System.Windows.Forms.Timer timerVS;

        public Form2()
        {
            InitializeComponent();
            InitializeUI();
            this.Paint += Form2_Paint;
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            // 定義漸層色的起始顏色和結束顏色
            Color startColor = Color.White;
            Color endColor = Color.LightPink;

            // 繪製漸層色背景
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            LinearGradientBrush brush = new LinearGradientBrush(rect, startColor, endColor, LinearGradientMode.Vertical);
            e.Graphics.FillRectangle(brush, rect);
        }

        private void InitializeUI()
        {
            // 设置窗体大小和标题
            this.ClientSize = new Size(600, 400);
            this.Text = "Rock Paper Scissors Game";

            // 添加玩家图片框
            pictureBoxPlayer = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.StretchImage,
                Location = new Point(50, 20),
                Size = new Size(180, 180),
                Image = Properties.Resources.finger_png // 设置初始图片
            };
            this.Controls.Add(pictureBoxPlayer);

            // 添加电脑图片框
            pictureBoxComputer = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.StretchImage,
                Location = new Point(370, 20),
                Size = new Size(180, 180),
                Image = Properties.Resources.pc // 设置初始图片
            };
            this.Controls.Add(pictureBoxComputer);

            // 添加“V.S.”标签
            labelVS = new Label
            {
                Text = "V.S.",
                Font = new Font("Arial", 24, FontStyle.Bold),
                Location = new Point(270, 90),
                AutoSize = true
            };
            this.Controls.Add(labelVS);

            // 初始化定时器
            timerVS = new System.Windows.Forms.Timer();
            timerVS.Interval = 100; // 每100毫秒更新一次
            timerVS.Tick += TimerVS_Tick;
            timerVS.Start();

            // 添加按钮：石头
            buttonRock = new Button
            {
                Text = "石头",
                Location = new Point(50, 250),
                Size = new Size(80, 30)
            };
            buttonRock.Click += ButtonRock_Click;
            this.Controls.Add(buttonRock);

            // 添加按钮：剪刀
            buttonScissors = new Button
            {
                Text = "剪刀",
                Location = new Point(250, 250),
                Size = new Size(80, 30)
            };
            buttonScissors.Click += ButtonScissors_Click;
            this.Controls.Add(buttonScissors);

            // 添加按钮：布
            buttonPaper = new Button
            {
                Text = "布",
                Location = new Point(450, 250),
                Size = new Size(80, 30)
            };
            buttonPaper.Click += ButtonPaper_Click;
            this.Controls.Add(buttonPaper);

            // 添加对应该选项的图片
            AddOptionImages();
        }

        private void TimerVS_Tick(object sender, EventArgs e)
        {
            Random random = new Random();

            // 随机生成颜色
            Color randomColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));

            // 随机生成字体大小
            float randomFontSize = 24 + random.Next(-2, 3); // 在24基础上随机调整大小

            // 更新标签颜色和字体
            labelVS.ForeColor = randomColor;
            labelVS.Font = new Font("Arial", randomFontSize, FontStyle.Bold);
        }

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

        private void ButtonRock_Click(object sender, EventArgs e)
        {
            // 玩家选择石头
            pictureBoxPlayer.Image = Properties.Resources.stone;
            PlayGame(0); // 0代表石头
        }

        private void ButtonScissors_Click(object sender, EventArgs e)
        {
            // 玩家选择剪刀
            pictureBoxPlayer.Image = Properties.Resources.scissors;
            PlayGame(1); // 1代表剪刀
        }

        private void ButtonPaper_Click(object sender, EventArgs e)
        {
            // 玩家选择布
            pictureBoxPlayer.Image = Properties.Resources.paper;
            PlayGame(2); // 2代表布
        }

        private void PlayGame(int playerChoice)
        {
            // 电脑随机选择
            Random random = new Random();
            int computerChoice = random.Next(0, 3); // 随机生成 0, 1, 2

            // 根据选择显示电脑的图片
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

            // 判断输赢
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

            ShowResult(resultMessage, resultColor);

            // 重置图片框
            pictureBoxPlayer.Image = Properties.Resources.finger_png;
            pictureBoxComputer.Image = Properties.Resources.pc;
        }

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
            buttonClose.Click += (s, e) => resultForm.Close();

            resultForm.Controls.Add(labelMessage);
            resultForm.Controls.Add(buttonClose);

            resultForm.ShowDialog();
        }
    }
}

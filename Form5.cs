using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace test1
{
    public partial class Form5 : Form
    {
        public delegate void form3EventHandler(string sMsg); //宣告委派
        public form3EventHandler eventForm3trigger; //傳遞資料event

        private int groupCount;
        private List<List<string>> groups;

        public Form5()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GenerateAndShowGroups();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GenerateAndSaveGroups();
        }

        private void GenerateAndShowGroups()
        {
            // 確保組數和人數都不為空
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("請輸入組別數量和班級人數。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 讀取用戶輸入的組數和人數
            int personCount;
            if (!int.TryParse(textBox1.Text, out groupCount) || !int.TryParse(textBox2.Text, out personCount))
            {
                MessageBox.Show("請輸入有效的數字。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 創建人員列表
            List<string> people = new List<string>();
            for (int i = 1; i <= personCount; i++)
            {
                people.Add($"{i}號");
            }

            // 初始化分組列表
            groups = new List<List<string>>();
            for (int i = 0; i < groupCount; i++)
            {
                groups.Add(new List<string>());
            }

            // 隨機分組
            Random random = new Random();
            int currentGroup = 0;
            while (people.Count > 0)
            {
                int index = random.Next(0, people.Count);
                groups[currentGroup].Add(people[index]);
                people.RemoveAt(index);
                currentGroup = (currentGroup + 1) % groupCount;
            }

            // 顯示分組結果
            ShowGroupsInMessageBox();
        }

        private void ShowGroupsInMessageBox()
        {
            string result = "分組結果：\n";
            for (int i = 0; i < groupCount; i++)
            {
                result += $"第{i + 1}: {string.Join(", ", groups[i])}組\n";
            }
            MessageBox.Show(result, "分組結果");
        }

        private void GenerateAndSaveGroups()
        {
            // 將分組結果保存到文件中
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            saveFileDialog.FileName = "分組結果.txt";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                string result = "分組結果：\n";
                for (int i = 0; i < groupCount; i++)
                {
                    result += $"第{i + 1}: {string.Join(", ", groups[i])}組\n";
                }
                File.WriteAllText(filePath, result);
            }
        }


        private void button2_Click_1(object sender, EventArgs e)
        {
            // 按下按鈕2時生成新的分組
            GenerateAndShowGroups();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GenerateAndSaveGroups();
        }
    }
}

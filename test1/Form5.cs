using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace test1
{
    public partial class Form5 : Form
    {
        // 委派用於觸發事件從 Form3 傳遞資料
        public delegate void form3EventHandler(string sMsg);
        public form3EventHandler eventForm3trigger; // 觸發傳遞資料事件

        private int groupCount; // 組別數量
        private List<List<string>> groups; // 分組列表

        public Form5()
        {
            InitializeComponent();
        }

        // 當按鈕1點擊時，生成並顯示分組
        private void button1_Click(object sender, EventArgs e)
        {
            GenerateAndShowGroups();
        }

        // 當選單項目1點擊時，生成並保存分組
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GenerateAndSaveGroups();
        }

        // 生成並顯示分組
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

        // 顯示分組結果在訊息框中
        private void ShowGroupsInMessageBox()
        {
            string result = "分組結果：\n";
            for (int i = 0; i < groupCount; i++)
            {
                result += $"第{i + 1}組: {string.Join(", ", groups[i])}\n";
            }
            MessageBox.Show(result, "分組結果");
        }

        // 生成並保存分組
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

        // 當按鈕2點擊時，生成新的分組並顯示
        private void button2_Click_1(object sender, EventArgs e)
        {
            GenerateAndShowGroups();
        }

        // 當按鈕3點擊時，生成並保存分組
        private void button3_Click(object sender, EventArgs e)
        {
            GenerateAndSaveGroups();
        }
    }
}

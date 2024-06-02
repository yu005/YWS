using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace test1
{
    public partial class Form4 : Form
    {
        private Random random; // 用於生成隨機數
        private int operand1, operand2, answer; // 運算元和答案
        private char operation; // 運算符
        private List<string> history = new List<string>(); // 保存歷史紀錄
        private int totalQuestions = 0; // 總問題數
        private int correctAnswers = 0; // 正確答案數
        private int wrongAnswers = 0; // 錯誤答案數

        public Form4()
        {
            InitializeComponent();
            random = new Random(); // 初始化隨機數生成器
            GenerateQuestion(); // 生成第一個問題
        }

        private void GenerateQuestion()
        {
            operand1 = random.Next(1, 11); // 隨機生成第一個運算元（1到10之間）
            operand2 = random.Next(1, 11); // 隨機生成第二個運算元（1到10之間）
            operation = GetRandomOperation(); // 隨機獲取一個運算符
            UpdateQuestionLabel(); // 更新問題標籤

            switch (operation)
            {
                case '+':
                    answer = operand1 + operand2; // 加法
                    break;
                case '-':
                    answer = operand1 - operand2; // 減法
                    break;
                case 'x':
                    answer = operand1 * operand2; // 乘法
                    break;
                case '/':
                    operand2 = random.Next(1, 11); // 確保除數非零
                    answer = (int)((double)operand1 / operand2); // 除法
                    break;
            }
        }

        private char GetRandomOperation()
        {
            char[] operations = { '+', '-', 'x', '/' }; // 定義可能的運算符
            return operations[random.Next(0, operations.Length)]; // 隨機選擇一個運算符
        }

        private void UpdateQuestionLabel()
        {
            questionLabel.Text = $"{operand1} {operation} {operand2} ="; // 更新問題標籤
            questionLabel.Font = new Font(questionLabel.Font.FontFamily, 16, questionLabel.Font.Style); // 設置字體大小為 16
            answerTextBox.Font = new Font(answerTextBox.Font.FontFamily, 16, answerTextBox.Font.Style); // 設置答案文本框字體大小為 16
        }

        private void CheckAnswer()
        {
            totalQuestions++; // 總問題數加一
            double userAnswer;
            if (double.TryParse(answerTextBox.Text, out userAnswer)) // 檢查用戶輸入的答案是否為數字
            {
                bool isCorrect = Math.Abs(userAnswer - answer) < 0.0001; // 檢查答案是否正確
                string newRecord = $"{operand1} {operation} {operand2} = {userAnswer}, Correct: {isCorrect}";

                // 檢查歷史紀錄中是否已存在相同的記錄
                bool recordExists = history.Any(record => record.StartsWith($"{operand1} {operation} {operand2} =") && record.EndsWith($"Correct: {isCorrect}"));

                if (!recordExists) // 如果不存在相同的記錄
                {
                    if (isCorrect)
                    {
                        MessageBox.Show("Correct!"); // 正確提示
                        correctAnswers++; // 正確答案數加一
                    }
                    else
                    {
                        MessageBox.Show($"Incorrect. The correct answer is {answer}"); // 錯誤提示
                        wrongAnswers++; // 錯誤答案數加一
                        // 將正確答案添加到記錄中
                        newRecord += $", Correct Answer: {answer}";
                    }

                    // 將問題和答案添加到歷史紀錄中
                    history.Add(newRecord);
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid number answer."); // 提示請輸入有效的數字答案
            }
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            CheckAnswer(); // 檢查答案
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            GenerateQuestion(); // 生成下一個問題
            answerTextBox.Text = ""; // 清空答案文本框
            answerTextBox.Focus(); // 將焦點設置到答案文本框
        }

        private void showHistoryButton_Click(object sender, EventArgs e)
        {
            // 清空歷史紀錄文本框
            historyRichTextBox.Clear();

            foreach (string record in history)
            {
                string[] parts = record.Split(','); // 分割記錄字符串
                if (parts.Length > 1)
                {
                    string answerPart = parts[1].Trim(); // 獲取答案部分
                    bool isCorrect = answerPart.EndsWith("True", StringComparison.OrdinalIgnoreCase); // 檢查答案是否正確
                    string formattedAnswer = isCorrect ? answerPart : answerPart; // 格式化答案
                    Color color = isCorrect ? Color.Blue : Color.Red; // 正確答案為藍色，錯誤答案為紅色
                    string recordText = $"{parts[0]}, Correct: {formattedAnswer}"; // 構建記錄文本

                    if (!isCorrect)
                    {
                        // 追加正確答案到記錄中
                        recordText += $"\n The correct answer is {parts[2]}";
                    }
                    AppendColoredText(historyRichTextBox, recordText + "\n", color); // 將格式化後的文本添加到歷史紀錄文本框中
                }
                else
                {
                    historyRichTextBox.AppendText(record + "\n"); // 將未格式化的記錄添加到歷史紀錄文本框中
                }
            }
            // 添加總問題數和正確/錯誤答案數到最後一行
            historyRichTextBox.AppendText($"Total: {totalQuestions}, Correct: {correctAnswers}, Wrong: {wrongAnswers}\n");
        }

        private void AppendColoredText(RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength; // 設置選擇的起始位置為文本框的長度
            box.SelectionLength = 0; // 設置選擇的長度為 0，即不選擇任何文本
            box.SelectionColor = color; // 設置選擇文本的顏色
            box.AppendText(text); // 追加文本
            box.SelectionColor = box.ForeColor; // 將選擇的文本顏色重置為文本框的前景顏色
        }
    }
}

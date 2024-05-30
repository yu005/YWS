using System;
using System.Text;
using System.Windows.Forms;
namespace test1
{
    public partial class Form4 : Form
    {
        private Random random;
        private int operand1, operand2, answer;
        private char operation;
        private List<string> history = new List<string>();
        private int totalQuestions = 0;
        private int correctAnswers = 0;
        private int wrongAnswers = 0;

        public Form4()
        {
            InitializeComponent();
            random = new Random();
            GenerateQuestion();

        }

        private void GenerateQuestion()
        {
            operand1 = random.Next(1, 11);
            operand2 = random.Next(1, 11);
            operation = GetRandomOperation();
            UpdateQuestionLabel();

            switch (operation)
            {
                case '+':
                    answer = operand1 + operand2;
                    break;
                case '-':
                    answer = operand1 - operand2;
                    break;
                case 'x':
                    answer = operand1 * operand2;
                    break;
                case '/':
                    // Ensure non-zero divisor
                    operand2 = random.Next(1, 11);
                    answer = (int)((double)operand1 / operand2);
                    break;
            }
        }

        private char GetRandomOperation()
        {
            char[] operations = { '+', '-', 'x', '/' };
            return operations[random.Next(0, operations.Length)];
        }

        private void UpdateQuestionLabel()
        {
            questionLabel.Text = $"{operand1} {operation} {operand2} =";
            questionLabel.Font = new Font(questionLabel.Font.FontFamily, 16, questionLabel.Font.Style); // 設置字體大小為 16
            answerTextBox.Font = new Font(answerTextBox.Font.FontFamily, 16, answerTextBox.Font.Style); // 設置答案文本框字體大小為 16
        }

        private void CheckAnswer()
        {
            totalQuestions++;
            double userAnswer;
            if (double.TryParse(answerTextBox.Text, out userAnswer))
            {
                bool isCorrect = Math.Abs(userAnswer - answer) < 0.0001;
                string newRecord = $"{operand1} {operation} {operand2} = {userAnswer}, Correct: {isCorrect}";

                // Check if the same record already exists in history
                bool recordExists = history.Any(record => record.StartsWith($"{operand1} {operation} {operand2} =") && record.EndsWith($"Correct: {isCorrect}"));

                if (!recordExists)
                {
                    if (isCorrect)
                    {
                        MessageBox.Show("Correct!");
                        correctAnswers++;
                    }
                    else
                    {
                        MessageBox.Show($"Incorrect. The correct answer is {answer}");
                        wrongAnswers++;
                        // Add correct answer to the record
                        newRecord += $", Correct Answer: {answer}";
                    }

                    // Add question and answer to history
                    history.Add(newRecord);
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid number answer.");
            }
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            CheckAnswer();
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            GenerateQuestion();
            answerTextBox.Text = "";
            answerTextBox.Focus();
        }

        private void showHistoryButton_Click(object sender, EventArgs e)
        {
            // 清除現有文本
            historyRichTextBox.Clear();

            foreach (string record in history)
            {
                string[] parts = record.Split(',');
                if (parts.Length > 1)
                {
                    string answerPart = parts[1].Trim();
                    bool isCorrect = answerPart.EndsWith("True", StringComparison.OrdinalIgnoreCase);
                    string formattedAnswer = isCorrect ? answerPart : answerPart;
                    Color color = isCorrect ? Color.Blue : Color.Red;
                    string recordText = $"{parts[0]}, Correct: {formattedAnswer}";

                    if (!isCorrect)
                    {
                        // Append correct answer to the record
                        recordText += $"\n The correct answer is {parts[2]}";
                    }
                    AppendColoredText(historyRichTextBox, recordText + "\n", color);
                }
                else
                {
                    historyRichTextBox.AppendText(record + "\n");
                }
            }
            // Add total and correct/incorrect counts to the last line
            historyRichTextBox.AppendText($"Total: {totalQuestions}, Correct: {correctAnswers}, Wrong: {wrongAnswers}\n");
        }

        private void AppendColoredText(RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;
            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;

        }
    }
}

using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.Xml.Linq;

namespace test1
{
    partial class Form4
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            questionLabel = new Label();
            answerTextBox = new TextBox();
            submitButton = new Button();
            nextButton = new Button();
            showHistoryButton = new Button();
            historyRichTextBox = new RichTextBox();
            SuspendLayout();
            // 
            // questionLabel
            // 
            questionLabel.AutoSize = true;
            questionLabel.Location = new Point(96, 55);
            questionLabel.Name = "questionLabel";
            questionLabel.Size = new Size(51, 19);
            questionLabel.TabIndex = 0;
            questionLabel.Text = "label1";
            // 
            // answerTextBox
            // 
            answerTextBox.Location = new Point(211, 55);
            answerTextBox.Multiline = true;
            answerTextBox.Name = "answerTextBox";
            answerTextBox.Size = new Size(171, 43);
            answerTextBox.TabIndex = 1;
            // 
            // submitButton
            // 
            submitButton.Location = new Point(96, 148);
            submitButton.Name = "submitButton";
            submitButton.Size = new Size(120, 55);
            submitButton.TabIndex = 2;
            submitButton.Text = "confirm";
            submitButton.UseVisualStyleBackColor = true;
            submitButton.Click += submitButton_Click;
            // 
            // nextButton
            // 
            nextButton.Location = new Point(268, 148);
            nextButton.Name = "nextButton";
            nextButton.Size = new Size(132, 55);
            nextButton.TabIndex = 3;
            nextButton.Text = "next question";
            nextButton.UseVisualStyleBackColor = true;
            nextButton.Click += nextButton_Click;
            // 
            // showHistoryButton
            // 
            showHistoryButton.Location = new Point(437, 148);
            showHistoryButton.Name = "showHistoryButton";
            showHistoryButton.Size = new Size(149, 55);
            showHistoryButton.TabIndex = 4;
            showHistoryButton.Text = "historical record";
            showHistoryButton.UseVisualStyleBackColor = true;
            showHistoryButton.Click += showHistoryButton_Click;
            // 
            // historyRichTextBox
            // 
            historyRichTextBox.Location = new Point(91, 235);
            historyRichTextBox.Name = "historyRichTextBox";
            historyRichTextBox.Size = new Size(504, 186);
            historyRichTextBox.TabIndex = 5;
            historyRichTextBox.Text = "";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(historyRichTextBox);
            Controls.Add(showHistoryButton);
            Controls.Add(nextButton);
            Controls.Add(submitButton);
            Controls.Add(answerTextBox);
            Controls.Add(questionLabel);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();

            this.BackColor = System.Drawing.Color.LightBlue; // 更改背景顏色
        }

        #endregion

        private Label questionLabel;
        private TextBox answerTextBox;
        private Button submitButton;
        private Button nextButton;
        private Button showHistoryButton;
        private RichTextBox historyRichTextBox;
    }
}

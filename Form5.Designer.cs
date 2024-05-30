using System.Windows.Forms;

namespace test1
{
    partial class Form5
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的資源。
        /// </summary>
        /// <param name="disposing">如果應該釋放托管資源，則為 true；否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 設計工具支援所需的方法 - 不要修改
        /// 使用程式碼編輯器修改此方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            button1 = new Button();
            button2 = new Button();
            menuStrip1 = new MenuStrip();
            toolStripMenuItem1 = new ToolStripMenuItem();
            button3 = new Button();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(40, 46);
            label1.Name = "label1";
            label1.Size = new Size(117, 19);
            label1.TabIndex = 3;
            label1.Text = "請輸入組別數量:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(40, 108);
            label2.Name = "label2";
            label2.Size = new Size(117, 19);
            label2.TabIndex = 4;
            label2.Text = "請輸入班級人數:";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(153, 43);
            textBox1.Margin = new Padding(4);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(148, 27);
            textBox1.TabIndex = 0;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(153, 105);
            textBox2.Margin = new Padding(4);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(148, 27);
            textBox2.TabIndex = 1;
            // 
            // button1
            // 
            button1.Location = new Point(40, 174);
            button1.Margin = new Padding(4);
            button1.Name = "button1";
            button1.Size = new Size(150, 34);
            button1.TabIndex = 2;
            button1.Text = "執行分組";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(212, 174);
            button2.Name = "button2";
            button2.Size = new Size(131, 35);
            button2.TabIndex = 5;
            button2.Text = "重新分組";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click_1;
            // 

            // button3
            // 
            button3.Location = new Point(328, 4);
            button3.Name = "button3";
            button3.Size = new Size(94, 29);
            button3.TabIndex = 6;
            button3.Text = "SAVE";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(426, 381);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button1);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Margin = new Padding(4);
            Name = "Form1";
            Text = "隨機分組";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;
        private Label label1;
        private Label label2;
        private Button button2;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private Button button3;
    }
}

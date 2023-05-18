
using System.Windows.Forms;

namespace D.YMX
{
    partial class FrmKeyWords
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmKeyWords));
            this.btnStart = new System.Windows.Forms.Button();
            this.btnEnd = new System.Windows.Forms.Button();
            this.checkBoxLeftAll = new System.Windows.Forms.CheckBox();
            this.checkBoxRightAll = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.checkBoxLeft = new System.Windows.Forms.CheckedListBox();
            this.checkBoxRight = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(500, 34);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(141, 43);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "开始";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnEnd
            // 
            this.btnEnd.Location = new System.Drawing.Point(500, 97);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(141, 43);
            this.btnEnd.TabIndex = 3;
            this.btnEnd.Text = "停止";
            this.btnEnd.UseVisualStyleBackColor = true;
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // checkBoxLeftAll
            // 
            this.checkBoxLeftAll.AutoSize = true;
            this.checkBoxLeftAll.Location = new System.Drawing.Point(500, 520);
            this.checkBoxLeftAll.Name = "checkBoxLeftAll";
            this.checkBoxLeftAll.Size = new System.Drawing.Size(129, 28);
            this.checkBoxLeftAll.TabIndex = 4;
            this.checkBoxLeftAll.Text = "<-全部选中";
            this.checkBoxLeftAll.UseVisualStyleBackColor = true;
            // 
            // checkBoxRightAll
            // 
            this.checkBoxRightAll.AutoSize = true;
            this.checkBoxRightAll.Location = new System.Drawing.Point(500, 633);
            this.checkBoxRightAll.Name = "checkBoxRightAll";
            this.checkBoxRightAll.Size = new System.Drawing.Size(129, 28);
            this.checkBoxRightAll.TabIndex = 5;
            this.checkBoxRightAll.Text = "全部选中->";
            this.checkBoxRightAll.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(500, 554);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 34);
            this.button1.TabIndex = 6;
            this.button1.Text = "挑选加入->";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(500, 679);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 34);
            this.button2.TabIndex = 7;
            this.button2.Text = "<-移除列表";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // checkBoxLeft
            // 
            this.checkBoxLeft.FormattingEnabled = true;
            this.checkBoxLeft.Location = new System.Drawing.Point(12, 25);
            this.checkBoxLeft.Name = "checkBoxLeft";
            this.checkBoxLeft.Size = new System.Drawing.Size(470, 706);
            this.checkBoxLeft.TabIndex = 8;
            // 
            // checkBoxRight
            // 
            this.checkBoxRight.FormattingEnabled = true;
            this.checkBoxRight.Location = new System.Drawing.Point(649, 25);
            this.checkBoxRight.Name = "checkBoxRight";
            this.checkBoxRight.Size = new System.Drawing.Size(470, 706);
            this.checkBoxRight.TabIndex = 9;
            // 
            // FrmKeyWords
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1131, 739);
            this.Controls.Add(this.checkBoxRight);
            this.Controls.Add(this.checkBoxLeft);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkBoxRightAll);
            this.Controls.Add(this.checkBoxLeftAll);
            this.Controls.Add(this.btnEnd);
            this.Controls.Add(this.btnStart);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmKeyWords";
            this.Text = "FrmKeyWords";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnStart;
        private Button btnEnd;
        private CheckBox checkBoxLeftAll;
        private CheckBox checkBoxRightAll;
        private Button button1;
        private Button button2;
        private CheckedListBox checkBoxLeft;
        private CheckedListBox checkBoxRight;
    }
}
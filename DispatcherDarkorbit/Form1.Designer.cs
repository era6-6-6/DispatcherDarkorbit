namespace DispatcherDarkorbit
{
    partial class Form1
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
            this.SidTxt = new System.Windows.Forms.TextBox();
            this.ServerTxt = new System.Windows.Forms.TextBox();
            this.LoginBtn = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // SidTxt
            // 
            this.SidTxt.Location = new System.Drawing.Point(12, 202);
            this.SidTxt.Name = "SidTxt";
            this.SidTxt.PlaceholderText = "SID";
            this.SidTxt.Size = new System.Drawing.Size(279, 23);
            this.SidTxt.TabIndex = 0;
            // 
            // ServerTxt
            // 
            this.ServerTxt.Location = new System.Drawing.Point(297, 202);
            this.ServerTxt.Name = "ServerTxt";
            this.ServerTxt.PlaceholderText = "Server";
            this.ServerTxt.Size = new System.Drawing.Size(75, 23);
            this.ServerTxt.TabIndex = 1;
            // 
            // LoginBtn
            // 
            this.LoginBtn.Location = new System.Drawing.Point(378, 202);
            this.LoginBtn.Name = "LoginBtn";
            this.LoginBtn.Size = new System.Drawing.Size(64, 24);
            this.LoginBtn.TabIndex = 2;
            this.LoginBtn.Text = "Login";
            this.LoginBtn.UseVisualStyleBackColor = true;
            this.LoginBtn.Click += new System.EventHandler(this.LoginBtn_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(-1, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(464, 196);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 433);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.LoginBtn);
            this.Controls.Add(this.ServerTxt);
            this.Controls.Add(this.SidTxt);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox SidTxt;
        private TextBox ServerTxt;
        private Button LoginBtn;
        private RichTextBox richTextBox1;
    }
}
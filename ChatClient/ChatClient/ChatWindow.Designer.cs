
namespace ChatClient
{
    partial class ChatClient
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
            this.ChatBox = new System.Windows.Forms.RichTextBox();
            this.MessageBox = new System.Windows.Forms.TextBox();
            this.SendBut = new System.Windows.Forms.Button();
            this.LeaveBut = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ChatBox
            // 
            this.ChatBox.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ChatBox.Location = new System.Drawing.Point(13, 13);
            this.ChatBox.Name = "ChatBox";
            this.ChatBox.ReadOnly = true;
            this.ChatBox.Size = new System.Drawing.Size(437, 468);
            this.ChatBox.TabIndex = 0;
            this.ChatBox.TabStop = false;
            this.ChatBox.Text = "";
            // 
            // MessageBox
            // 
            this.MessageBox.Location = new System.Drawing.Point(12, 506);
            this.MessageBox.Name = "MessageBox";
            this.MessageBox.Size = new System.Drawing.Size(437, 27);
            this.MessageBox.TabIndex = 1;
            // 
            // SendBut
            // 
            this.SendBut.Location = new System.Drawing.Point(13, 539);
            this.SendBut.Name = "SendBut";
            this.SendBut.Size = new System.Drawing.Size(94, 29);
            this.SendBut.TabIndex = 2;
            this.SendBut.Text = "Send";
            this.SendBut.UseVisualStyleBackColor = true;
            this.SendBut.Click += new System.EventHandler(this.SendBut_Click);
            // 
            // LeaveBut
            // 
            this.LeaveBut.Location = new System.Drawing.Point(354, 539);
            this.LeaveBut.Name = "LeaveBut";
            this.LeaveBut.Size = new System.Drawing.Size(94, 29);
            this.LeaveBut.TabIndex = 3;
            this.LeaveBut.Text = "Leave";
            this.LeaveBut.UseVisualStyleBackColor = true;
            this.LeaveBut.Click += new System.EventHandler(this.LeaveBut_Click);
            // 
            // ChatClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 580);
            this.ControlBox = false;
            this.Controls.Add(this.LeaveBut);
            this.Controls.Add(this.SendBut);
            this.Controls.Add(this.MessageBox);
            this.Controls.Add(this.ChatBox);
            this.Name = "ChatClient";
            this.Text = "Chat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChatClient_FormClosing);
            this.Load += new System.EventHandler(this.ChatClient_Load);
            this.EnabledChanged += new System.EventHandler(this.ChatClient_EnabledChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox ChatBox;
        private System.Windows.Forms.TextBox MessageBox;
        private System.Windows.Forms.Button SendBut;
        private System.Windows.Forms.Button LeaveBut;
    }
}


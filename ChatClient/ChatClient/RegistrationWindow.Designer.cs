
namespace ChatClient
{
    partial class RegistrationWindow
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
            this.PortBox = new System.Windows.Forms.TextBox();
            this.LoginLable = new System.Windows.Forms.Label();
            this.IPLable = new System.Windows.Forms.Label();
            this.PortLable = new System.Windows.Forms.Label();
            this.IPBox = new System.Windows.Forms.TextBox();
            this.LoginBox = new System.Windows.Forms.TextBox();
            this.ConnectBut = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PortBox
            // 
            this.PortBox.Location = new System.Drawing.Point(102, 86);
            this.PortBox.Name = "PortBox";
            this.PortBox.Size = new System.Drawing.Size(125, 27);
            this.PortBox.TabIndex = 0;
            this.PortBox.Text = "8080";
            // 
            // LoginLable
            // 
            this.LoginLable.AutoSize = true;
            this.LoginLable.Location = new System.Drawing.Point(13, 13);
            this.LoginLable.Name = "LoginLable";
            this.LoginLable.Size = new System.Drawing.Size(49, 20);
            this.LoginLable.TabIndex = 1;
            this.LoginLable.Text = "Login:";
            // 
            // IPLable
            // 
            this.IPLable.AutoSize = true;
            this.IPLable.Location = new System.Drawing.Point(13, 49);
            this.IPLable.Name = "IPLable";
            this.IPLable.Size = new System.Drawing.Size(69, 20);
            this.IPLable.TabIndex = 2;
            this.IPLable.Text = "Server IP:";
            // 
            // PortLable
            // 
            this.PortLable.AutoSize = true;
            this.PortLable.Location = new System.Drawing.Point(13, 89);
            this.PortLable.Name = "PortLable";
            this.PortLable.Size = new System.Drawing.Size(83, 20);
            this.PortLable.TabIndex = 3;
            this.PortLable.Text = "Server Port:";
            // 
            // IPBox
            // 
            this.IPBox.Location = new System.Drawing.Point(102, 46);
            this.IPBox.Name = "IPBox";
            this.IPBox.Size = new System.Drawing.Size(125, 27);
            this.IPBox.TabIndex = 4;
            this.IPBox.Text = "127.0.0.1";
            // 
            // LoginBox
            // 
            this.LoginBox.Location = new System.Drawing.Point(102, 10);
            this.LoginBox.Name = "LoginBox";
            this.LoginBox.Size = new System.Drawing.Size(125, 27);
            this.LoginBox.TabIndex = 5;
            this.LoginBox.Text = "User";
            // 
            // ConnectBut
            // 
            this.ConnectBut.Location = new System.Drawing.Point(250, 10);
            this.ConnectBut.Name = "ConnectBut";
            this.ConnectBut.Size = new System.Drawing.Size(94, 103);
            this.ConnectBut.TabIndex = 6;
            this.ConnectBut.Text = "Connect";
            this.ConnectBut.UseVisualStyleBackColor = true;
            this.ConnectBut.Click += new System.EventHandler(this.ConnectBut_Click);
            // 
            // RegistrationWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(356, 137);
            this.Controls.Add(this.ConnectBut);
            this.Controls.Add(this.LoginBox);
            this.Controls.Add(this.IPBox);
            this.Controls.Add(this.PortLable);
            this.Controls.Add(this.IPLable);
            this.Controls.Add(this.LoginLable);
            this.Controls.Add(this.PortBox);
            this.Name = "RegistrationWindow";
            this.Text = "RegistrationWindow";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RegistrationWindow_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox PortBox;
        private System.Windows.Forms.Label LoginLable;
        private System.Windows.Forms.Label IPLable;
        private System.Windows.Forms.Label PortLable;
        private System.Windows.Forms.TextBox IPBox;
        private System.Windows.Forms.TextBox LoginBox;
        private System.Windows.Forms.Button ConnectBut;
    }
}
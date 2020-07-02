namespace WindowsFormsApp1
{
    partial class MainForm
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
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnExecute = new System.Windows.Forms.Button();
            this.txtResultFolderPath = new System.Windows.Forms.TextBox();
            this.cbFBLogin = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(10, 11);
            this.txtUrl.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(279, 20);
            this.txtUrl.TabIndex = 0;
            this.txtUrl.Text = "Url";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(293, 10);
            this.txtUsername.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(76, 20);
            this.txtUsername.TabIndex = 1;
            this.txtUsername.Text = "Username";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(374, 10);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(76, 20);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.Text = "Password";
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(645, 9);
            this.btnExecute.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(56, 19);
            this.btnExecute.TabIndex = 3;
            this.btnExecute.Text = "Execute";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // txtResultFolderPath
            // 
            this.txtResultFolderPath.Location = new System.Drawing.Point(565, 9);
            this.txtResultFolderPath.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtResultFolderPath.Name = "txtResultFolderPath";
            this.txtResultFolderPath.Size = new System.Drawing.Size(76, 20);
            this.txtResultFolderPath.TabIndex = 4;
            this.txtResultFolderPath.Text = "Output folder path";
            this.txtResultFolderPath.TextChanged += new System.EventHandler(this.txtResultFolderPath_TextChanged);
            // 
            // cbFBLogin
            // 
            this.cbFBLogin.AutoSize = true;
            this.cbFBLogin.Location = new System.Drawing.Point(455, 11);
            this.cbFBLogin.Name = "cbFBLogin";
            this.cbFBLogin.Size = new System.Drawing.Size(97, 17);
            this.cbFBLogin.TabIndex = 5;
            this.cbFBLogin.Text = "facebookLogin";
            this.cbFBLogin.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 535);
            this.Controls.Add(this.cbFBLogin);
            this.Controls.Add(this.txtResultFolderPath);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.txtUrl);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.TextBox txtResultFolderPath;
        private System.Windows.Forms.CheckBox cbFBLogin;
    }
}


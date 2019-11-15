namespace CheggSolutionsCrawlerNetCore
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
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtResultFolderPath = new System.Windows.Forms.TextBox();
            this.btnExecute = new System.Windows.Forms.Button();
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(20, 21);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(610, 31);
            this.txtUrl.TabIndex = 0;
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(648, 21);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(150, 31);
            this.txtUsername.TabIndex = 1;
            this.txtUsername.Text = "Username";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(819, 21);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(150, 31);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.Text = "Password";
            // 
            // txtResultFolderPath
            // 
            this.txtResultFolderPath.Location = new System.Drawing.Point(987, 20);
            this.txtResultFolderPath.Name = "txtResultFolderPath";
            this.txtResultFolderPath.Size = new System.Drawing.Size(204, 31);
            this.txtResultFolderPath.TabIndex = 1;
            this.txtResultFolderPath.Text = "Output folder eg C:\\";
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(1210, 18);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(112, 34);
            this.btnExecute.TabIndex = 2;
            this.btnExecute.Text = "Execute";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1339, 765);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtResultFolderPath);
            this.Name = "Form1";
            this.Text = "Form1";

        }

        #endregion

        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtResultFolderPath;
        private System.Windows.Forms.Button btnExecute;
    }
}


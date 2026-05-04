namespace LoginModulForm
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.panel_hoca = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_ksifre = new System.Windows.Forms.TextBox();
            this.textBox_kad = new System.Windows.Forms.TextBox();
            this.btn_login = new System.Windows.Forms.Button();
            this.panel_hoca.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_hoca
            // 
            this.panel_hoca.BackColor = System.Drawing.Color.Transparent;
            this.panel_hoca.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel_hoca.Controls.Add(this.label3);
            this.panel_hoca.Controls.Add(this.label2);
            this.panel_hoca.Controls.Add(this.textBox_ksifre);
            this.panel_hoca.Controls.Add(this.textBox_kad);
            this.panel_hoca.Controls.Add(this.btn_login);
            this.panel_hoca.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.panel_hoca.Location = new System.Drawing.Point(76, 62);
            this.panel_hoca.Name = "panel_hoca";
            this.panel_hoca.Size = new System.Drawing.Size(337, 272);
            this.panel_hoca.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(49, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Şifre...:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(49, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Kullanıcı Adı...:";
            // 
            // textBox_ksifre
            // 
            this.textBox_ksifre.Location = new System.Drawing.Point(184, 130);
            this.textBox_ksifre.Name = "textBox_ksifre";
            this.textBox_ksifre.Size = new System.Drawing.Size(124, 20);
            this.textBox_ksifre.TabIndex = 5;
            this.textBox_ksifre.UseSystemPasswordChar = true;
            // 
            // textBox_kad
            // 
            this.textBox_kad.Location = new System.Drawing.Point(184, 79);
            this.textBox_kad.Name = "textBox_kad";
            this.textBox_kad.Size = new System.Drawing.Size(124, 20);
            this.textBox_kad.TabIndex = 4;
            // 
            // btn_login
            // 
            this.btn_login.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
            this.btn_login.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.btn_login.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_login.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_login.Location = new System.Drawing.Point(103, 199);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(111, 23);
            this.btn_login.TabIndex = 3;
            this.btn_login.Text = "Giriş";
            this.btn_login.UseVisualStyleBackColor = true;
            this.btn_login.Click += new System.EventHandler(this.btn_login_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(489, 410);
            this.Controls.Add(this.panel_hoca);
            this.DoubleBuffered = true;
            this.Name = "LoginForm";
            this.Text = "LoginForm";
            this.panel_hoca.ResumeLayout(false);
            this.panel_hoca.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_hoca;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_ksifre;
        private System.Windows.Forms.TextBox textBox_kad;
        private System.Windows.Forms.Button btn_login;
    }
}
namespace LoginModulForm
{
    partial class KullaniciArama
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KullaniciArama));
            this.text_aramaad = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.text_aramarol = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.text_aramabolum = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.lbl_klytm = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // text_aramaad
            // 
            this.text_aramaad.Location = new System.Drawing.Point(179, 67);
            this.text_aramaad.Name = "text_aramaad";
            this.text_aramaad.Size = new System.Drawing.Size(100, 20);
            this.text_aramaad.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button1.ForeColor = System.Drawing.Color.Transparent;
            this.button1.Location = new System.Drawing.Point(64, 210);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(215, 26);
            this.button1.TabIndex = 2;
            this.button1.Text = "Ara 🔍";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.text_aramarol);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.text_aramabolum);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.text_aramaad);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox1.Location = new System.Drawing.Point(122, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(351, 268);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Arama";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(61, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 34;
            this.label2.Text = "Kullanıcı Rolü...:";
            // 
            // text_aramarol
            // 
            this.text_aramarol.Location = new System.Drawing.Point(179, 102);
            this.text_aramarol.Name = "text_aramarol";
            this.text_aramarol.Size = new System.Drawing.Size(100, 20);
            this.text_aramarol.TabIndex = 33;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(61, 141);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "Kullanıcı Bölüm...:";
            // 
            // text_aramabolum
            // 
            this.text_aramabolum.Location = new System.Drawing.Point(179, 138);
            this.text_aramabolum.Name = "text_aramabolum";
            this.text_aramabolum.Size = new System.Drawing.Size(100, 20);
            this.text_aramabolum.TabIndex = 31;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label11.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label11.Location = new System.Drawing.Point(61, 70);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(93, 13);
            this.label11.TabIndex = 30;
            this.label11.Text = "Kullanıcı Adı...:";
            // 
            // lbl_klytm
            // 
            this.lbl_klytm.AutoSize = true;
            this.lbl_klytm.BackColor = System.Drawing.Color.Transparent;
            this.lbl_klytm.Font = new System.Drawing.Font("Impact", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbl_klytm.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbl_klytm.Location = new System.Drawing.Point(13, 13);
            this.lbl_klytm.Name = "lbl_klytm";
            this.lbl_klytm.Size = new System.Drawing.Size(30, 23);
            this.lbl_klytm.TabIndex = 4;
            this.lbl_klytm.Text = "←";
            this.lbl_klytm.Click += new System.EventHandler(this.lbl_klytm_Click);
            // 
            // KullaniciArama
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(612, 405);
            this.Controls.Add(this.lbl_klytm);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.Name = "KullaniciArama";
            this.Text = "KullaniciArama";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox text_aramaad;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox text_aramarol;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox text_aramabolum;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lbl_klytm;
    }
}
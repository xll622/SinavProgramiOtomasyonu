namespace LoginModulForm
{
    partial class BolumArama
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BolumArama));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_arabolum = new System.Windows.Forms.Button();
            this.cmb_bolum = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.cmb_bolum);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.btn_arabolum);
            this.groupBox3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox3.Location = new System.Drawing.Point(140, 73);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(303, 205);
            this.groupBox3.TabIndex = 40;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Bölüm Ekle";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(46, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "Bölüm Adı...:";
            // 
            // btn_arabolum
            // 
            this.btn_arabolum.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
            this.btn_arabolum.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.btn_arabolum.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_arabolum.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btn_arabolum.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_arabolum.Location = new System.Drawing.Point(49, 134);
            this.btn_arabolum.Name = "btn_arabolum";
            this.btn_arabolum.Size = new System.Drawing.Size(222, 23);
            this.btn_arabolum.TabIndex = 38;
            this.btn_arabolum.Text = "Ara";
            this.btn_arabolum.UseVisualStyleBackColor = true;
            this.btn_arabolum.Click += new System.EventHandler(this.btn_arabolum_Click);
            // 
            // cmb_bolum
            // 
            this.cmb_bolum.FormattingEnabled = true;
            this.cmb_bolum.Location = new System.Drawing.Point(135, 59);
            this.cmb_bolum.Name = "cmb_bolum";
            this.cmb_bolum.Size = new System.Drawing.Size(136, 21);
            this.cmb_bolum.TabIndex = 39;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 20);
            this.label1.TabIndex = 41;
            this.label1.Text = "←";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // BolumArama
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(602, 410);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox3);
            this.DoubleBuffered = true;
            this.Name = "BolumArama";
            this.Text = "BolumArama";
            this.Load += new System.EventHandler(this.BolumArama_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_arabolum;
        private System.Windows.Forms.ComboBox cmb_bolum;
        private System.Windows.Forms.Label label1;
    }
}
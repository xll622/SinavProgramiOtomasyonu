namespace LoginModulForm
{
    partial class TakvimZamanDilimiAraForm
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnAra = new System.Windows.Forms.Button();
            this.mtxAraBitis = new System.Windows.Forms.MaskedTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.mtxAraBaslangic = new System.Windows.Forms.MaskedTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpAraTarih = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbAraDonemTipi = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.btnAra);
            this.groupBox2.Controls.Add(this.mtxAraBitis);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.mtxAraBaslangic);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.dtpAraTarih);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.cmbAraDonemTipi);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.groupBox2.Location = new System.Drawing.Point(139, 44);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(380, 312);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Güncelle";
            // 
            // btnAra
            // 
            this.btnAra.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnAra.Location = new System.Drawing.Point(83, 222);
            this.btnAra.Name = "btnAra";
            this.btnAra.Size = new System.Drawing.Size(173, 23);
            this.btnAra.TabIndex = 9;
            this.btnAra.Text = "Ara";
            this.btnAra.UseVisualStyleBackColor = true;
            // 
            // mtxAraBitis
            // 
            this.mtxAraBitis.Location = new System.Drawing.Point(199, 183);
            this.mtxAraBitis.Mask = "00:00";
            this.mtxAraBitis.Name = "mtxAraBitis";
            this.mtxAraBitis.Size = new System.Drawing.Size(123, 22);
            this.mtxAraBitis.TabIndex = 7;
            this.mtxAraBitis.ValidatingType = typeof(System.DateTime);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 178);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 16);
            this.label5.TabIndex = 6;
            this.label5.Text = "Bitiş Saati..:";
            // 
            // mtxAraBaslangic
            // 
            this.mtxAraBaslangic.Location = new System.Drawing.Point(199, 149);
            this.mtxAraBaslangic.Mask = "00:00";
            this.mtxAraBaslangic.Name = "mtxAraBaslangic";
            this.mtxAraBaslangic.Size = new System.Drawing.Size(123, 22);
            this.mtxAraBaslangic.TabIndex = 5;
            this.mtxAraBaslangic.ValidatingType = typeof(System.DateTime);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 144);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 16);
            this.label6.TabIndex = 4;
            this.label6.Text = "Başlangıç Saati..:";
            // 
            // dtpAraTarih
            // 
            this.dtpAraTarih.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpAraTarih.Location = new System.Drawing.Point(199, 105);
            this.dtpAraTarih.Name = "dtpAraTarih";
            this.dtpAraTarih.Size = new System.Drawing.Size(123, 22);
            this.dtpAraTarih.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 102);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 16);
            this.label7.TabIndex = 2;
            this.label7.Text = "Tarih..:";
            // 
            // cmbAraDonemTipi
            // 
            this.cmbAraDonemTipi.FormattingEnabled = true;
            this.cmbAraDonemTipi.Items.AddRange(new object[] {
            "Güz - Vize,",
            "Güz - Final,",
            "Bahar - Vize,",
            "Bahar - Final"});
            this.cmbAraDonemTipi.Location = new System.Drawing.Point(199, 54);
            this.cmbAraDonemTipi.Name = "cmbAraDonemTipi";
            this.cmbAraDonemTipi.Size = new System.Drawing.Size(123, 24);
            this.cmbAraDonemTipi.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label8.Location = new System.Drawing.Point(21, 54);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(86, 16);
            this.label8.TabIndex = 0;
            this.label8.Text = "Dönem Tipi..:";
            // 
            // TakvimZamanDilimiAraForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::LoginModulForm.Properties.Resources.WhatsApp_Image_2026_05_04_at_16_02_33;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(680, 391);
            this.Controls.Add(this.groupBox2);
            this.Name = "TakvimZamanDilimiAraForm";
            this.Text = "TakvimZamanDilimiAraForm";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.MaskedTextBox mtxAraBitis;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.MaskedTextBox mtxAraBaslangic;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpAraTarih;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbAraDonemTipi;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnAra;
    }
}
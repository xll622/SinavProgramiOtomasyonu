namespace LoginModulForm
{
    partial class SinavProgramiOlustur
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
            this.cmb_takvim = new System.Windows.Forms.ComboBox();
            this.chk_cumartesiKullan = new System.Windows.Forms.CheckBox();
            this.cmb_programVersiyon = new System.Windows.Forms.ComboBox();
            this.btn_programOlustur = new System.Windows.Forms.Button();
            this.btn_listele = new System.Windows.Forms.Button();
            this.btn_temizle = new System.Windows.Forms.Button();
            this.dataGridView_program = new System.Windows.Forms.DataGridView();
            this.lbl_baslik = new System.Windows.Forms.Label();
            this.lbl_aciklama = new System.Windows.Forms.Label();
            this.lbl_durum = new System.Windows.Forms.Label();
            this.lbl_takvim = new System.Windows.Forms.Label();
            this.lbl_program = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_program)).BeginInit();
            this.SuspendLayout();
            // 
            // cmb_takvim
            // 
            this.cmb_takvim.FormattingEnabled = true;
            this.cmb_takvim.Location = new System.Drawing.Point(215, 111);
            this.cmb_takvim.Name = "cmb_takvim";
            this.cmb_takvim.Size = new System.Drawing.Size(183, 24);
            this.cmb_takvim.TabIndex = 0;
            // 
            // chk_cumartesiKullan
            // 
            this.chk_cumartesiKullan.AutoSize = true;
            this.chk_cumartesiKullan.Location = new System.Drawing.Point(58, 145);
            this.chk_cumartesiKullan.Name = "chk_cumartesiKullan";
            this.chk_cumartesiKullan.Size = new System.Drawing.Size(185, 20);
            this.chk_cumartesiKullan.TabIndex = 1;
            this.chk_cumartesiKullan.Text = "Cumartesi sınav yapılabilir";
            this.chk_cumartesiKullan.UseVisualStyleBackColor = true;
            // 
            // cmb_programVersiyon
            // 
            this.cmb_programVersiyon.FormattingEnabled = true;
            this.cmb_programVersiyon.Location = new System.Drawing.Point(215, 176);
            this.cmb_programVersiyon.Name = "cmb_programVersiyon";
            this.cmb_programVersiyon.Size = new System.Drawing.Size(183, 24);
            this.cmb_programVersiyon.TabIndex = 2;
            // 
            // btn_programOlustur
            // 
            this.btn_programOlustur.Location = new System.Drawing.Point(28, 223);
            this.btn_programOlustur.Name = "btn_programOlustur";
            this.btn_programOlustur.Size = new System.Drawing.Size(229, 23);
            this.btn_programOlustur.TabIndex = 3;
            this.btn_programOlustur.Text = "3 Program Oluştur";
            this.btn_programOlustur.UseVisualStyleBackColor = true;
            this.btn_programOlustur.Click += new System.EventHandler(this.btn_programOlustur_Click);
            // 
            // btn_listele
            // 
            this.btn_listele.Location = new System.Drawing.Point(720, 223);
            this.btn_listele.Name = "btn_listele";
            this.btn_listele.Size = new System.Drawing.Size(229, 23);
            this.btn_listele.TabIndex = 4;
            this.btn_listele.Text = "Programı Listele";
            this.btn_listele.UseVisualStyleBackColor = true;
            this.btn_listele.Click += new System.EventHandler(this.btn_listele_Click);
            // 
            // btn_temizle
            // 
            this.btn_temizle.Location = new System.Drawing.Point(361, 223);
            this.btn_temizle.Name = "btn_temizle";
            this.btn_temizle.Size = new System.Drawing.Size(229, 23);
            this.btn_temizle.TabIndex = 5;
            this.btn_temizle.Text = "Temizle";
            this.btn_temizle.UseVisualStyleBackColor = true;
            this.btn_temizle.Click += new System.EventHandler(this.btn_temizle_Click);
            // 
            // dataGridView_program
            // 
            this.dataGridView_program.AllowUserToAddRows = false;
            this.dataGridView_program.AllowUserToDeleteRows = false;
            this.dataGridView_program.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_program.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_program.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_program.Location = new System.Drawing.Point(12, 252);
            this.dataGridView_program.Name = "dataGridView_program";
            this.dataGridView_program.ReadOnly = true;
            this.dataGridView_program.RowHeadersWidth = 51;
            this.dataGridView_program.RowTemplate.Height = 24;
            this.dataGridView_program.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_program.Size = new System.Drawing.Size(937, 388);
            this.dataGridView_program.TabIndex = 6;
            // 
            // lbl_baslik
            // 
            this.lbl_baslik.AutoSize = true;
            this.lbl_baslik.Location = new System.Drawing.Point(55, 13);
            this.lbl_baslik.Name = "lbl_baslik";
            this.lbl_baslik.Size = new System.Drawing.Size(152, 16);
            this.lbl_baslik.TabIndex = 7;
            this.lbl_baslik.Text = "Sınav Programı Oluştur..:";
            // 
            // lbl_aciklama
            // 
            this.lbl_aciklama.AutoSize = true;
            this.lbl_aciklama.Location = new System.Drawing.Point(55, 46);
            this.lbl_aciklama.Name = "lbl_aciklama";
            this.lbl_aciklama.Size = new System.Drawing.Size(275, 16);
            this.lbl_aciklama.TabIndex = 8;
            this.lbl_aciklama.Text = "Bu ekranda 3 farklı sınav programı oluşturulur.";
            // 
            // lbl_durum
            // 
            this.lbl_durum.AutoSize = true;
            this.lbl_durum.Location = new System.Drawing.Point(55, 73);
            this.lbl_durum.Name = "lbl_durum";
            this.lbl_durum.Size = new System.Drawing.Size(55, 16);
            this.lbl_durum.TabIndex = 9;
            this.lbl_durum.Text = "Durum : ";
            // 
            // lbl_takvim
            // 
            this.lbl_takvim.AutoSize = true;
            this.lbl_takvim.Location = new System.Drawing.Point(55, 114);
            this.lbl_takvim.Name = "lbl_takvim";
            this.lbl_takvim.Size = new System.Drawing.Size(124, 16);
            this.lbl_takvim.TabIndex = 10;
            this.lbl_takvim.Text = "Dönem / Sınav Tipi:";
            // 
            // lbl_program
            // 
            this.lbl_program.AutoSize = true;
            this.lbl_program.Location = new System.Drawing.Point(54, 184);
            this.lbl_program.Name = "lbl_program";
            this.lbl_program.Size = new System.Drawing.Size(125, 16);
            this.lbl_program.TabIndex = 11;
            this.lbl_program.Text = "Program Versiyonu:";
            // 
            // SinavProgramiOlustur
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(961, 652);
            this.Controls.Add(this.lbl_program);
            this.Controls.Add(this.lbl_takvim);
            this.Controls.Add(this.lbl_durum);
            this.Controls.Add(this.lbl_aciklama);
            this.Controls.Add(this.lbl_baslik);
            this.Controls.Add(this.dataGridView_program);
            this.Controls.Add(this.btn_temizle);
            this.Controls.Add(this.btn_listele);
            this.Controls.Add(this.btn_programOlustur);
            this.Controls.Add(this.cmb_programVersiyon);
            this.Controls.Add(this.chk_cumartesiKullan);
            this.Controls.Add(this.cmb_takvim);
            this.Name = "SinavProgramiOlustur";
            this.Text = "SinavProgramiOlustur";
            this.Click += new System.EventHandler(this.SinavProgramiOlustur_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_program)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmb_takvim;
        private System.Windows.Forms.CheckBox chk_cumartesiKullan;
        private System.Windows.Forms.ComboBox cmb_programVersiyon;
        private System.Windows.Forms.Button btn_programOlustur;
        private System.Windows.Forms.Button btn_listele;
        private System.Windows.Forms.Button btn_temizle;
        private System.Windows.Forms.DataGridView dataGridView_program;
        private System.Windows.Forms.Label lbl_baslik;
        private System.Windows.Forms.Label lbl_aciklama;
        private System.Windows.Forms.Label lbl_durum;
        private System.Windows.Forms.Label lbl_takvim;
        private System.Windows.Forms.Label lbl_program;
    }
}
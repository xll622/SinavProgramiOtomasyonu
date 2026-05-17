namespace LoginModulForm
{
    partial class TumProgramlar
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
            this.dataGridView_programlar = new System.Windows.Forms.DataGridView();
            this.lbl_durum = new System.Windows.Forms.Label();
            this.btn_temizle = new System.Windows.Forms.Button();
            this.btn_excelAktar = new System.Windows.Forms.Button();
            this.btn_listele = new System.Windows.Forms.Button();
            this.cmb_takvim = new System.Windows.Forms.ComboBox();
            this.lbl_takvim = new System.Windows.Forms.Label();
            this.cmb_bolum = new System.Windows.Forms.ComboBox();
            this.lbl_bolum = new System.Windows.Forms.Label();
            this.lbl_baslik = new System.Windows.Forms.Label();
            this.lbl_program = new System.Windows.Forms.Label();
            this.cmb_programVersiyon = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_programlar)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView_programlar
            // 
            this.dataGridView_programlar.AllowUserToAddRows = false;
            this.dataGridView_programlar.AllowUserToDeleteRows = false;
            this.dataGridView_programlar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_programlar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_programlar.Location = new System.Drawing.Point(12, 203);
            this.dataGridView_programlar.Name = "dataGridView_programlar";
            this.dataGridView_programlar.ReadOnly = true;
            this.dataGridView_programlar.RowHeadersWidth = 51;
            this.dataGridView_programlar.RowTemplate.Height = 24;
            this.dataGridView_programlar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_programlar.Size = new System.Drawing.Size(776, 237);
            this.dataGridView_programlar.TabIndex = 19;
            // 
            // lbl_durum
            // 
            this.lbl_durum.AutoSize = true;
            this.lbl_durum.Location = new System.Drawing.Point(33, 134);
            this.lbl_durum.Name = "lbl_durum";
            this.lbl_durum.Size = new System.Drawing.Size(49, 16);
            this.lbl_durum.TabIndex = 18;
            this.lbl_durum.Text = "Durum:";
            // 
            // btn_temizle
            // 
            this.btn_temizle.Location = new System.Drawing.Point(589, 174);
            this.btn_temizle.Name = "btn_temizle";
            this.btn_temizle.Size = new System.Drawing.Size(199, 23);
            this.btn_temizle.TabIndex = 17;
            this.btn_temizle.Text = "Temizle";
            this.btn_temizle.UseVisualStyleBackColor = true;
            // 
            // btn_excelAktar
            // 
            this.btn_excelAktar.Location = new System.Drawing.Point(12, 174);
            this.btn_excelAktar.Name = "btn_excelAktar";
            this.btn_excelAktar.Size = new System.Drawing.Size(199, 23);
            this.btn_excelAktar.TabIndex = 16;
            this.btn_excelAktar.Text = "Excel\'e Aktar";
            this.btn_excelAktar.UseVisualStyleBackColor = true;
            // 
            // btn_listele
            // 
            this.btn_listele.Location = new System.Drawing.Point(297, 174);
            this.btn_listele.Name = "btn_listele";
            this.btn_listele.Size = new System.Drawing.Size(199, 23);
            this.btn_listele.TabIndex = 15;
            this.btn_listele.Text = "Listele";
            this.btn_listele.UseVisualStyleBackColor = true;
            // 
            // cmb_takvim
            // 
            this.cmb_takvim.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_takvim.FormattingEnabled = true;
            this.cmb_takvim.Location = new System.Drawing.Point(187, 68);
            this.cmb_takvim.Name = "cmb_takvim";
            this.cmb_takvim.Size = new System.Drawing.Size(121, 24);
            this.cmb_takvim.TabIndex = 14;
            // 
            // lbl_takvim
            // 
            this.lbl_takvim.AutoSize = true;
            this.lbl_takvim.Location = new System.Drawing.Point(33, 76);
            this.lbl_takvim.Name = "lbl_takvim";
            this.lbl_takvim.Size = new System.Drawing.Size(118, 16);
            this.lbl_takvim.TabIndex = 13;
            this.lbl_takvim.Text = "Dönem/Sınav Tipi:";
            // 
            // cmb_bolum
            // 
            this.cmb_bolum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_bolum.FormattingEnabled = true;
            this.cmb_bolum.Location = new System.Drawing.Point(187, 35);
            this.cmb_bolum.Name = "cmb_bolum";
            this.cmb_bolum.Size = new System.Drawing.Size(121, 24);
            this.cmb_bolum.TabIndex = 12;
            // 
            // lbl_bolum
            // 
            this.lbl_bolum.AutoSize = true;
            this.lbl_bolum.Location = new System.Drawing.Point(33, 43);
            this.lbl_bolum.Name = "lbl_bolum";
            this.lbl_bolum.Size = new System.Drawing.Size(48, 16);
            this.lbl_bolum.TabIndex = 11;
            this.lbl_bolum.Text = "Bölüm:";
            // 
            // lbl_baslik
            // 
            this.lbl_baslik.AutoSize = true;
            this.lbl_baslik.Location = new System.Drawing.Point(33, 11);
            this.lbl_baslik.Name = "lbl_baslik";
            this.lbl_baslik.Size = new System.Drawing.Size(104, 16);
            this.lbl_baslik.TabIndex = 10;
            this.lbl_baslik.Text = "Tüm Programlar";
            // 
            // lbl_program
            // 
            this.lbl_program.AutoSize = true;
            this.lbl_program.Location = new System.Drawing.Point(33, 101);
            this.lbl_program.Name = "lbl_program";
            this.lbl_program.Size = new System.Drawing.Size(62, 16);
            this.lbl_program.TabIndex = 20;
            this.lbl_program.Text = "Program:";
            // 
            // cmb_programVersiyon
            // 
            this.cmb_programVersiyon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_programVersiyon.FormattingEnabled = true;
            this.cmb_programVersiyon.Location = new System.Drawing.Point(187, 106);
            this.cmb_programVersiyon.Name = "cmb_programVersiyon";
            this.cmb_programVersiyon.Size = new System.Drawing.Size(121, 24);
            this.cmb_programVersiyon.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(656, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 22;
            this.label1.Text = "Anasayfa";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // TumProgramlar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmb_programVersiyon);
            this.Controls.Add(this.lbl_program);
            this.Controls.Add(this.dataGridView_programlar);
            this.Controls.Add(this.lbl_durum);
            this.Controls.Add(this.btn_temizle);
            this.Controls.Add(this.btn_excelAktar);
            this.Controls.Add(this.btn_listele);
            this.Controls.Add(this.cmb_takvim);
            this.Controls.Add(this.lbl_takvim);
            this.Controls.Add(this.cmb_bolum);
            this.Controls.Add(this.lbl_bolum);
            this.Controls.Add(this.lbl_baslik);
            this.Name = "TumProgramlar";
            this.Text = "TumProgramlar";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_programlar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_programlar;
        private System.Windows.Forms.Label lbl_durum;
        private System.Windows.Forms.Button btn_temizle;
        private System.Windows.Forms.Button btn_excelAktar;
        private System.Windows.Forms.Button btn_listele;
        private System.Windows.Forms.ComboBox cmb_takvim;
        private System.Windows.Forms.Label lbl_takvim;
        private System.Windows.Forms.ComboBox cmb_bolum;
        private System.Windows.Forms.Label lbl_bolum;
        private System.Windows.Forms.Label lbl_baslik;
        private System.Windows.Forms.Label lbl_program;
        private System.Windows.Forms.ComboBox cmb_programVersiyon;
        private System.Windows.Forms.Label label1;
    }
}
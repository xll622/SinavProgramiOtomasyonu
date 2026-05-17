namespace LoginModulForm
{
    partial class HocaProgramim
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
            this.lbl_baslik = new System.Windows.Forms.Label();
            this.lbl_takvim = new System.Windows.Forms.Label();
            this.cmb_takvim = new System.Windows.Forms.ComboBox();
            this.lbl_program = new System.Windows.Forms.Label();
            this.cmb_programVersiyon = new System.Windows.Forms.ComboBox();
            this.btn_listele = new System.Windows.Forms.Button();
            this.btn_excelAktar = new System.Windows.Forms.Button();
            this.btn_temizle = new System.Windows.Forms.Button();
            this.lbl_durum = new System.Windows.Forms.Label();
            this.dataGridView_programim = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_programim)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_baslik
            // 
            this.lbl_baslik.AutoSize = true;
            this.lbl_baslik.Location = new System.Drawing.Point(33, 9);
            this.lbl_baslik.Name = "lbl_baslik";
            this.lbl_baslik.Size = new System.Drawing.Size(73, 16);
            this.lbl_baslik.TabIndex = 0;
            this.lbl_baslik.Text = "Programım";
            // 
            // lbl_takvim
            // 
            this.lbl_takvim.AutoSize = true;
            this.lbl_takvim.Location = new System.Drawing.Point(33, 41);
            this.lbl_takvim.Name = "lbl_takvim";
            this.lbl_takvim.Size = new System.Drawing.Size(124, 16);
            this.lbl_takvim.TabIndex = 1;
            this.lbl_takvim.Text = "Dönem / Sınav Tipi:";
            // 
            // cmb_takvim
            // 
            this.cmb_takvim.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_takvim.FormattingEnabled = true;
            this.cmb_takvim.Location = new System.Drawing.Point(187, 41);
            this.cmb_takvim.Name = "cmb_takvim";
            this.cmb_takvim.Size = new System.Drawing.Size(121, 24);
            this.cmb_takvim.TabIndex = 2;
            // 
            // lbl_program
            // 
            this.lbl_program.AutoSize = true;
            this.lbl_program.Location = new System.Drawing.Point(33, 74);
            this.lbl_program.Name = "lbl_program";
            this.lbl_program.Size = new System.Drawing.Size(62, 16);
            this.lbl_program.TabIndex = 3;
            this.lbl_program.Text = "Program:";
            // 
            // cmb_programVersiyon
            // 
            this.cmb_programVersiyon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_programVersiyon.FormattingEnabled = true;
            this.cmb_programVersiyon.Location = new System.Drawing.Point(187, 74);
            this.cmb_programVersiyon.Name = "cmb_programVersiyon";
            this.cmb_programVersiyon.Size = new System.Drawing.Size(121, 24);
            this.cmb_programVersiyon.TabIndex = 4;
            // 
            // btn_listele
            // 
            this.btn_listele.Location = new System.Drawing.Point(297, 172);
            this.btn_listele.Name = "btn_listele";
            this.btn_listele.Size = new System.Drawing.Size(199, 23);
            this.btn_listele.TabIndex = 5;
            this.btn_listele.Text = "Listele";
            this.btn_listele.UseVisualStyleBackColor = true;
            this.btn_listele.Click += new System.EventHandler(this.btn_listele_Click);
            // 
            // btn_excelAktar
            // 
            this.btn_excelAktar.Location = new System.Drawing.Point(12, 172);
            this.btn_excelAktar.Name = "btn_excelAktar";
            this.btn_excelAktar.Size = new System.Drawing.Size(199, 23);
            this.btn_excelAktar.TabIndex = 6;
            this.btn_excelAktar.Text = "Excel\'e Aktar";
            this.btn_excelAktar.UseVisualStyleBackColor = true;
            this.btn_excelAktar.Click += new System.EventHandler(this.btn_excelAktar_Click);
            // 
            // btn_temizle
            // 
            this.btn_temizle.Location = new System.Drawing.Point(589, 172);
            this.btn_temizle.Name = "btn_temizle";
            this.btn_temizle.Size = new System.Drawing.Size(199, 23);
            this.btn_temizle.TabIndex = 7;
            this.btn_temizle.Text = "Temizle";
            this.btn_temizle.UseVisualStyleBackColor = true;
            this.btn_temizle.Click += new System.EventHandler(this.btn_temizle_Click);
            // 
            // lbl_durum
            // 
            this.lbl_durum.AutoSize = true;
            this.lbl_durum.Location = new System.Drawing.Point(33, 115);
            this.lbl_durum.Name = "lbl_durum";
            this.lbl_durum.Size = new System.Drawing.Size(49, 16);
            this.lbl_durum.TabIndex = 8;
            this.lbl_durum.Text = "Durum:";
            // 
            // dataGridView_programim
            // 
            this.dataGridView_programim.AllowUserToAddRows = false;
            this.dataGridView_programim.AllowUserToDeleteRows = false;
            this.dataGridView_programim.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_programim.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_programim.Location = new System.Drawing.Point(12, 201);
            this.dataGridView_programim.Name = "dataGridView_programim";
            this.dataGridView_programim.ReadOnly = true;
            this.dataGridView_programim.RowHeadersWidth = 51;
            this.dataGridView_programim.RowTemplate.Height = 24;
            this.dataGridView_programim.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_programim.Size = new System.Drawing.Size(776, 237);
            this.dataGridView_programim.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(651, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "label1";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // HocaProgramim
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView_programim);
            this.Controls.Add(this.lbl_durum);
            this.Controls.Add(this.btn_temizle);
            this.Controls.Add(this.btn_excelAktar);
            this.Controls.Add(this.btn_listele);
            this.Controls.Add(this.cmb_programVersiyon);
            this.Controls.Add(this.lbl_program);
            this.Controls.Add(this.cmb_takvim);
            this.Controls.Add(this.lbl_takvim);
            this.Controls.Add(this.lbl_baslik);
            this.Name = "HocaProgramim";
            this.Text = "HocaProgramim";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_programim)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_baslik;
        private System.Windows.Forms.Label lbl_takvim;
        private System.Windows.Forms.ComboBox cmb_takvim;
        private System.Windows.Forms.Label lbl_program;
        private System.Windows.Forms.ComboBox cmb_programVersiyon;
        private System.Windows.Forms.Button btn_listele;
        private System.Windows.Forms.Button btn_excelAktar;
        private System.Windows.Forms.Button btn_temizle;
        private System.Windows.Forms.Label lbl_durum;
        private System.Windows.Forms.DataGridView dataGridView_programim;
        private System.Windows.Forms.Label label1;
    }
}
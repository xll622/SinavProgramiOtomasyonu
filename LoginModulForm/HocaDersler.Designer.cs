namespace LoginModulForm
{
    partial class HocaDersler
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
            this.dataGridView_dersler = new System.Windows.Forms.DataGridView();
            this.btn_listele = new System.Windows.Forms.Button();
            this.btn_kapat = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_dersler)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_baslik
            // 
            this.lbl_baslik.AutoSize = true;
            this.lbl_baslik.Location = new System.Drawing.Point(181, 22);
            this.lbl_baslik.Name = "lbl_baslik";
            this.lbl_baslik.Size = new System.Drawing.Size(65, 16);
            this.lbl_baslik.TabIndex = 0;
            this.lbl_baslik.Text = "Derslerim";
            // 
            // dataGridView_dersler
            // 
            this.dataGridView_dersler.AllowUserToAddRows = false;
            this.dataGridView_dersler.AllowUserToDeleteRows = false;
            this.dataGridView_dersler.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_dersler.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_dersler.Location = new System.Drawing.Point(12, 55);
            this.dataGridView_dersler.Name = "dataGridView_dersler";
            this.dataGridView_dersler.ReadOnly = true;
            this.dataGridView_dersler.RowHeadersWidth = 51;
            this.dataGridView_dersler.RowTemplate.Height = 24;
            this.dataGridView_dersler.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullColumnSelect;
            this.dataGridView_dersler.Size = new System.Drawing.Size(759, 307);
            this.dataGridView_dersler.TabIndex = 1;
            // 
            // btn_listele
            // 
            this.btn_listele.Location = new System.Drawing.Point(165, 390);
            this.btn_listele.Name = "btn_listele";
            this.btn_listele.Size = new System.Drawing.Size(75, 23);
            this.btn_listele.TabIndex = 2;
            this.btn_listele.Text = "Listele";
            this.btn_listele.UseVisualStyleBackColor = true;
            this.btn_listele.Click += new System.EventHandler(this.btn_listele_Click);
            // 
            // btn_kapat
            // 
            this.btn_kapat.Location = new System.Drawing.Point(405, 390);
            this.btn_kapat.Name = "btn_kapat";
            this.btn_kapat.Size = new System.Drawing.Size(75, 23);
            this.btn_kapat.TabIndex = 3;
            this.btn_kapat.Text = "Kapat";
            this.btn_kapat.UseVisualStyleBackColor = true;
            this.btn_kapat.Click += new System.EventHandler(this.btn_kapat_Click);
            // 
            // HocaDersler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_kapat);
            this.Controls.Add(this.btn_listele);
            this.Controls.Add(this.dataGridView_dersler);
            this.Controls.Add(this.lbl_baslik);
            this.Name = "HocaDersler";
            this.Text = "HocaDersler";
            this.Load += new System.EventHandler(this.HocaDersler_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_dersler)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_baslik;
        private System.Windows.Forms.DataGridView dataGridView_dersler;
        private System.Windows.Forms.Button btn_listele;
        private System.Windows.Forms.Button btn_kapat;
    }
}
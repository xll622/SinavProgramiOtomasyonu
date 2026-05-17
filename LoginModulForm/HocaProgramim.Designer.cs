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
            this.dataGridView_programim = new System.Windows.Forms.DataGridView();
            this.btn_listele = new System.Windows.Forms.Button();
            this.cmb_programVersiyon = new System.Windows.Forms.ComboBox();
            this.cmb_takvim = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_programim)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView_programim
            // 
            this.dataGridView_programim.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_programim.Location = new System.Drawing.Point(12, 70);
            this.dataGridView_programim.Name = "dataGridView_programim";
            this.dataGridView_programim.RowHeadersWidth = 51;
            this.dataGridView_programim.RowTemplate.Height = 24;
            this.dataGridView_programim.Size = new System.Drawing.Size(776, 322);
            this.dataGridView_programim.TabIndex = 7;
            // 
            // btn_listele
            // 
            this.btn_listele.Location = new System.Drawing.Point(265, 410);
            this.btn_listele.Name = "btn_listele";
            this.btn_listele.Size = new System.Drawing.Size(213, 23);
            this.btn_listele.TabIndex = 6;
            this.btn_listele.UseVisualStyleBackColor = true;
            this.btn_listele.Click += new System.EventHandler(this.btn_listele_Click);
            // 
            // cmb_programVersiyon
            // 
            this.cmb_programVersiyon.FormattingEnabled = true;
            this.cmb_programVersiyon.Location = new System.Drawing.Point(442, 17);
            this.cmb_programVersiyon.Name = "cmb_programVersiyon";
            this.cmb_programVersiyon.Size = new System.Drawing.Size(121, 24);
            this.cmb_programVersiyon.TabIndex = 5;
            // 
            // cmb_takvim
            // 
            this.cmb_takvim.FormattingEnabled = true;
            this.cmb_takvim.Location = new System.Drawing.Point(163, 17);
            this.cmb_takvim.Name = "cmb_takvim";
            this.cmb_takvim.Size = new System.Drawing.Size(121, 24);
            this.cmb_takvim.TabIndex = 4;
            // 
            // HocaProgramim
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView_programim);
            this.Controls.Add(this.btn_listele);
            this.Controls.Add(this.cmb_programVersiyon);
            this.Controls.Add(this.cmb_takvim);
            this.Name = "HocaProgramim";
            this.Text = "HocaProgramim";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_programim)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_programim;
        private System.Windows.Forms.Button btn_listele;
        private System.Windows.Forms.ComboBox cmb_programVersiyon;
        private System.Windows.Forms.ComboBox cmb_takvim;
    }
}
namespace BigJetGas.Menus.Movimentacao
{
    partial class FrmPerguntaImpressao
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkPaisagem = new System.Windows.Forms.CheckBox();
            this.chkPapelA5 = new System.Windows.Forms.CheckBox();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnMemo = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.Location = new System.Drawing.Point(9, 16);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(218, 197);
            this.dataGridView1.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnMemo);
            this.groupBox1.Controls.Add(this.btnImprimir);
            this.groupBox1.Controls.Add(this.chkPapelA5);
            this.groupBox1.Controls.Add(this.chkPaisagem);
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Location = new System.Drawing.Point(6, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(235, 298);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // chkPaisagem
            // 
            this.chkPaisagem.AutoSize = true;
            this.chkPaisagem.Location = new System.Drawing.Point(9, 219);
            this.chkPaisagem.Name = "chkPaisagem";
            this.chkPaisagem.Size = new System.Drawing.Size(72, 17);
            this.chkPaisagem.TabIndex = 6;
            this.chkPaisagem.Text = "Paisagem";
            this.chkPaisagem.UseVisualStyleBackColor = true;
            // 
            // chkPapelA5
            // 
            this.chkPapelA5.AutoSize = true;
            this.chkPapelA5.Location = new System.Drawing.Point(9, 242);
            this.chkPapelA5.Name = "chkPapelA5";
            this.chkPapelA5.Size = new System.Drawing.Size(69, 17);
            this.chkPapelA5.TabIndex = 7;
            this.chkPapelA5.Text = "Papel A5";
            this.chkPapelA5.UseVisualStyleBackColor = true;
            // 
            // btnImprimir
            // 
            this.btnImprimir.Location = new System.Drawing.Point(9, 267);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(218, 23);
            this.btnImprimir.TabIndex = 6;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = true;
            // 
            // btnMemo
            // 
            this.btnMemo.Location = new System.Drawing.Point(144, 219);
            this.btnMemo.Name = "btnMemo";
            this.btnMemo.Size = new System.Drawing.Size(83, 23);
            this.btnMemo.TabIndex = 6;
            this.btnMemo.Text = "Memorizar";
            this.btnMemo.UseVisualStyleBackColor = true;
            // 
            // FrmPerguntaImpressao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(247, 304);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPerguntaImpressao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Selecionar Impressora";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.CheckBox chkPapelA5;
        private System.Windows.Forms.CheckBox chkPaisagem;
        private System.Windows.Forms.Button btnMemo;
    }
}
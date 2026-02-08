namespace WindowsFormsApp6.Menus.Utilitarios
{
    partial class FrmConfiguracoes
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtValorFrete = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkPerguntarImpressora = new System.Windows.Forms.CheckBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtPortaImp = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnTestar = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkMostrar = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.numTamanhoFonteImpressao = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.cboFonteImpressao = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.numTamanhoFonteRelatorio = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.cboFonteRelatorio = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTamanhoFonteImpressao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTamanhoFonteRelatorio)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtValorFrete);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(159, 84);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Venda";
            // 
            // txtValorFrete
            // 
            this.txtValorFrete.Location = new System.Drawing.Point(74, 23);
            this.txtValorFrete.Name = "txtValorFrete";
            this.txtValorFrete.Size = new System.Drawing.Size(72, 20);
            this.txtValorFrete.TabIndex = 1;
            this.txtValorFrete.Text = "0,00";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Valor Frete:";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(328, 301);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnSalvar
            // 
            this.btnSalvar.Location = new System.Drawing.Point(244, 301);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(75, 23);
            this.btnSalvar.TabIndex = 1;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkPerguntarImpressora);
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Controls.Add(this.txtPortaImp);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(173, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(230, 176);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Impressão";
            // 
            // chkPerguntarImpressora
            // 
            this.chkPerguntarImpressora.AutoSize = true;
            this.chkPerguntarImpressora.Location = new System.Drawing.Point(9, 149);
            this.chkPerguntarImpressora.Name = "chkPerguntarImpressora";
            this.chkPerguntarImpressora.Size = new System.Drawing.Size(216, 17);
            this.chkPerguntarImpressora.TabIndex = 2;
            this.chkPerguntarImpressora.Text = "Perguntar impressora na hora de imprimir";
            this.chkPerguntarImpressora.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.Location = new System.Drawing.Point(6, 51);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(218, 88);
            this.dataGridView1.TabIndex = 2;
            // 
            // txtPortaImp
            // 
            this.txtPortaImp.Location = new System.Drawing.Point(56, 23);
            this.txtPortaImp.Name = "txtPortaImp";
            this.txtPortaImp.ReadOnly = true;
            this.txtPortaImp.Size = new System.Drawing.Size(168, 20);
            this.txtPortaImp.TabIndex = 1;
            this.txtPortaImp.Text = "LPT1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Padrão:";
            // 
            // btnTestar
            // 
            this.btnTestar.Location = new System.Drawing.Point(107, 301);
            this.btnTestar.Name = "btnTestar";
            this.btnTestar.Size = new System.Drawing.Size(131, 23);
            this.btnTestar.TabIndex = 1;
            this.btnTestar.Text = "Testar Impressão";
            this.btnTestar.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkMostrar);
            this.groupBox3.Location = new System.Drawing.Point(8, 87);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(159, 92);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Cadastros";
            // 
            // chkMostrar
            // 
            this.chkMostrar.AutoSize = true;
            this.chkMostrar.Location = new System.Drawing.Point(16, 25);
            this.chkMostrar.Name = "chkMostrar";
            this.chkMostrar.Size = new System.Drawing.Size(110, 17);
            this.chkMostrar.TabIndex = 2;
            this.chkMostrar.Text = "Mostrar excluídos";
            this.chkMostrar.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.numTamanhoFonteImpressao);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.cboFonteImpressao);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.numTamanhoFonteRelatorio);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.cboFonteRelatorio);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Location = new System.Drawing.Point(8, 185);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(395, 110);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Configuração de Fontes";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Gray;
            this.label5.Location = new System.Drawing.Point(9, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(343, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Relatórios: visualização em tela/PDF  |  Impressão: matricial (LPT/USB)";
            // 
            // numTamanhoFonteImpressao
            // 
            this.numTamanhoFonteImpressao.Location = new System.Drawing.Point(331, 50);
            this.numTamanhoFonteImpressao.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.numTamanhoFonteImpressao.Minimum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numTamanhoFonteImpressao.Name = "numTamanhoFonteImpressao";
            this.numTamanhoFonteImpressao.Size = new System.Drawing.Size(50, 20);
            this.numTamanhoFonteImpressao.TabIndex = 6;
            this.numTamanhoFonteImpressao.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(272, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Tamanho:";
            // 
            // cboFonteImpressao
            // 
            this.cboFonteImpressao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFonteImpressao.FormattingEnabled = true;
            this.cboFonteImpressao.Location = new System.Drawing.Point(107, 49);
            this.cboFonteImpressao.Name = "cboFonteImpressao";
            this.cboFonteImpressao.Size = new System.Drawing.Size(159, 21);
            this.cboFonteImpressao.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Fonte Impressão:";
            // 
            // numTamanhoFonteRelatorio
            // 
            this.numTamanhoFonteRelatorio.Location = new System.Drawing.Point(331, 23);
            this.numTamanhoFonteRelatorio.Maximum = new decimal(new int[] {
            72,
            0,
            0,
            0});
            this.numTamanhoFonteRelatorio.Minimum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numTamanhoFonteRelatorio.Name = "numTamanhoFonteRelatorio";
            this.numTamanhoFonteRelatorio.Size = new System.Drawing.Size(50, 20);
            this.numTamanhoFonteRelatorio.TabIndex = 3;
            this.numTamanhoFonteRelatorio.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(272, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Tamanho:";
            // 
            // cboFonteRelatorio
            // 
            this.cboFonteRelatorio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFonteRelatorio.FormattingEnabled = true;
            this.cboFonteRelatorio.Location = new System.Drawing.Point(107, 22);
            this.cboFonteRelatorio.Name = "cboFonteRelatorio";
            this.cboFonteRelatorio.Size = new System.Drawing.Size(159, 21);
            this.cboFonteRelatorio.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Fonte Relatórios:";
            // 
            // FrmConfiguracoes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 336);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.btnTestar);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmConfiguracoes";
            this.Text = "Configurações";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTamanhoFonteImpressao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTamanhoFonteRelatorio)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtValorFrete;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtPortaImp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnTestar;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkMostrar;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.CheckBox chkPerguntarImpressora;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboFonteRelatorio;
        private System.Windows.Forms.NumericUpDown numTamanhoFonteRelatorio;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboFonteImpressao;
        private System.Windows.Forms.NumericUpDown numTamanhoFonteImpressao;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
    }
}
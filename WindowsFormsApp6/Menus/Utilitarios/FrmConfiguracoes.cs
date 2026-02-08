using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp6.Interface.Utilitarios;

namespace WindowsFormsApp6.Menus.Utilitarios
{
    public partial class FrmConfiguracoes : Form, IConfiguracao
    {
        public FrmConfiguracoes() 
        { 
            InitializeComponent();
            CarregarFontes();
        }

        public Form ConfiguracaoView => this;

        public TextBox TxtValorFrete => txtValorFrete;

        public TextBox TxtPortaImpressora => txtPortaImp;

        public Button BtnTesteImpressao => btnTestar;

        public Button BtnSalvar => btnSalvar;

        public Button BtnCancelar => btnCancelar;

        public CheckBox ChkMostrarExc => chkMostrar;

        public CheckBox ChkPerguntarImpressora => chkPerguntarImpressora;

        public DataGridView DgvImpressora => dataGridView1;

        // Controles de fonte (agora definidos no Designer)
        public ComboBox CboFonteRelatorio => cboFonteRelatorio;
        
        public NumericUpDown NumTamanhoFonteRelatorio => numTamanhoFonteRelatorio;

        public ComboBox CboFonteImpressao => cboFonteImpressao;
        
        public NumericUpDown NumTamanhoFonteImpressao => numTamanhoFonteImpressao;

        /// <summary>
        /// Carrega as fontes do sistema nos ComboBoxes
        /// </summary>
        private void CarregarFontes()
        {
            try
            {
                // Limpa os comboboxes
                cboFonteRelatorio.Items.Clear();
                cboFonteImpressao.Items.Clear();
                
                // Carrega todas as fontes do sistema
                foreach (FontFamily font in FontFamily.Families)
                {
                    cboFonteRelatorio.Items.Add(font.Name);
                    cboFonteImpressao.Items.Add(font.Name);
                }
                
                // Define valores padrão
                cboFonteRelatorio.SelectedItem = "Arial";
                cboFonteImpressao.SelectedItem = "Courier New";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar fontes: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

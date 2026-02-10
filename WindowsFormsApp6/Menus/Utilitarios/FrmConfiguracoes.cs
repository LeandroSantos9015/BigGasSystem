using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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
        
        /// <summary>
        /// Abre o programa de backup do banco de dados
        /// </summary>
        private void btnBackupBanco_Click(object sender, EventArgs e)
        {
            try
            {
                // Caminho do executável de backup (mesma pasta do sistema)
                string caminhoBackup = Path.Combine(
                    Application.StartupPath,
                    "BackupDatabase.exe"
                );
                
                // Verifica se o arquivo existe
                if (!File.Exists(caminhoBackup))
                {
                    DialogResult resultado = MessageBox.Show(
                        "Programa de backup não encontrado!\n\n" +
                        $"Esperado em: {caminhoBackup}\n\n" +
                        "Certifique-se de que o arquivo 'BackupDatabase.exe' está " +
                        "na mesma pasta do sistema.\n\n" +
                        "Deseja abrir a pasta do sistema?",
                        "Aviso",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );
                    
                    if (resultado == DialogResult.Yes)
                    {
                        Process.Start("explorer.exe", Application.StartupPath);
                    }
                    
                    return;
                }
                
                // Inicia o processo de backup
                Process.Start(caminhoBackup);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao abrir programa de backup:\n\n{ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}

using Relatorios.Enumeradores;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using WindowsFormsApp6.Utilitarios;
using DevExpress.XtraReports.UI;

namespace Relatorios.View.Cadastros.Venda
{
    [Serializable]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Retrato"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1010:CollectionsShouldImplementGenericInterface")]
    public partial class RelatorioBase : DevExpress.XtraReports.UI.XtraReport
    {
        public RelatorioBase(ERelatorio relatorio, string nomeTotal, string valor)
        {
            InitializeComponent();

            this.lblTitulo.Text = relatorio.Descricao();
            this.xrNome.Text = nomeTotal;
            this.xrTotal.Text = valor;
            
            // NÃO aplica fonte aqui - será aplicado DEPOIS de definir DataSource
        }

        private RelatorioBase() { }

        public RelatorioBase(ERelatorio relatorio)
        {
            InitializeComponent();

            this.xrNome.Visible = false;
            this.xrTotal.Visible = false;

            this.lblTitulo.Text = relatorio.Descricao();
            
            // NÃO aplica fonte aqui - será aplicado DEPOIS de definir DataSource
        }
        
        /// <summary>
        /// Aplica a fonte configurada em todos os controles do relatório
        /// CHAME ESTE MÉTODO DEPOIS DE DEFINIR O DATASOURCE
        /// Este método é PÚBLICO para que todos os relatórios filhos possam chamar
        /// </summary>
        public void AplicarFonteConfigurada()
        {
            try
            {
                // Obtém a fonte configurada para relatórios
                Font fonteRelatorio = FonteHelper.ObterFonteRelatorio();
                
                System.Diagnostics.Debug.WriteLine($"[{this.GetType().Name}] Iniciando aplicação de fonte: {fonteRelatorio.Name} {fonteRelatorio.Size}pt");
                
                // Aplica nos controles principais
                if (this.lblTitulo != null)
                {
                    this.lblTitulo.Font = new Font(fonteRelatorio.FontFamily, fonteRelatorio.Size + 2, FontStyle.Bold);
                }
                
                if (this.xrNome != null)
                {
                    this.xrNome.Font = fonteRelatorio;
                }
                
                if (this.xrTotal != null)
                {
                    this.xrTotal.Font = new Font(fonteRelatorio.FontFamily, fonteRelatorio.Size, FontStyle.Bold);
                }
                
                // Aplica recursivamente em TODAS as bands do relatório
                if (this.Bands != null && this.Bands.Count > 0)
                {
                    AplicarFonteRecursiva(this.Bands, fonteRelatorio);
                }
                
                System.Diagnostics.Debug.WriteLine($"[{this.GetType().Name}] Aplicação de fonte concluída");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[{this.GetType().Name}] Erro ao aplicar fonte: {ex.Message}");
            }
        }
        
        /// <summary>
        /// Aplica fonte recursivamente em todas as bands e controles
        /// </summary>
        private void AplicarFonteRecursiva(DevExpress.XtraReports.UI.BandCollection bands, Font fonte)
        {
            if (bands == null) return;
            
            foreach (DevExpress.XtraReports.UI.Band band in bands)
            {
                if (band == null || band.Controls == null) continue;
                
                System.Diagnostics.Debug.WriteLine($"  Band: {band.GetType().Name} - {band.Name}");
                
                foreach (DevExpress.XtraReports.UI.XRControl control in band.Controls)
                {
                    try
                    {
                        // Aplica em Labels
                        if (control is DevExpress.XtraReports.UI.XRLabel)
                        {
                            DevExpress.XtraReports.UI.XRLabel label = control as DevExpress.XtraReports.UI.XRLabel;
                            FontStyle style = label.Font.Bold ? FontStyle.Bold : FontStyle.Regular;
                            label.Font = new Font(fonte.FontFamily, fonte.Size, style);
                            System.Diagnostics.Debug.WriteLine($"    - Label: {label.Name}");
                        }
                        // Aplica em Tabelas e suas células
                        else if (control is DevExpress.XtraReports.UI.XRTable)
                        {
                            DevExpress.XtraReports.UI.XRTable table = control as DevExpress.XtraReports.UI.XRTable;
                            table.Font = fonte;
                            
                            // Aplica em cada linha
                            foreach (DevExpress.XtraReports.UI.XRTableRow row in table.Rows)
                            {
                                row.Font = fonte;
                                
                                // Aplica em cada célula
                                foreach (DevExpress.XtraReports.UI.XRTableCell cell in row.Cells)
                                {
                                    FontStyle style = cell.Font.Bold ? FontStyle.Bold : FontStyle.Regular;
                                    cell.Font = new Font(fonte.FontFamily, fonte.Size, style);
                                }
                            }
                            System.Diagnostics.Debug.WriteLine($"    - Table: {table.Name}");
                        }
                        // Aplica em Rich Text
                        else if (control is DevExpress.XtraReports.UI.XRRichText)
                        {
                            control.Font = fonte;
                            System.Diagnostics.Debug.WriteLine($"    - RichText: {control.Name}");
                        }
                        // Aplica em controles genéricos que suportam Font
                        else if (control.GetType().GetProperty("Font") != null)
                        {
                            control.Font = fonte;
                            System.Diagnostics.Debug.WriteLine($"    - Generic: {control.Name}");
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"    ERRO em {control.Name}: {ex.Message}");
                    }
                }
            }
        }
    }
}
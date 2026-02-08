using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using DevExpress.Data;
using Relatorios.Enumeradores;
using Relatorios.View.Cadastros.Venda;
using WindowsFormsApp6.Utilitarios;

namespace Relatorios.Impressao
{
    [Serializable]
    public partial class ImpressaoSaida : XtraReport
    {
        public ImpressaoSaida()
        {
            InitializeComponent();
            
            // NÃO aplica fonte aqui - será aplicado DEPOIS de definir DataSource
        }
        
        /// <summary>
        /// Aplica a fonte configurada em todos os controles da impressão
        /// CHAME ESTE MÉTODO DEPOIS DE DEFINIR O DATASOURCE
        /// </summary>
        public void AplicarFonteConfigurada()
        {
            try
            {
                // Obtém a fonte configurada para relatórios
                Font fonteRelatorio = FonteHelper.ObterFonteRelatorio();
                
                // Aplica recursivamente em TODAS as bands principais
                if (this.Bands != null && this.Bands.Count > 0)
                {
                    AplicarFonteRecursiva(this.Bands, fonteRelatorio);
                }
                
                // ⭐ IMPORTANTE: Aplica em TODOS os DetailReportBands (onde estão os DADOS)
                foreach (Band band in this.Bands)
                {
                    if (band is DetailReportBand)
                    {
                        DetailReportBand detailReport = band as DetailReportBand;
                        
                        // Aplica em todas as bands do DetailReport (Detail1, GroupHeader2, etc)
                        if (detailReport.Bands != null && detailReport.Bands.Count > 0)
                        {
                            AplicarFonteRecursiva(detailReport.Bands, fonteRelatorio);
                        }
                        
                        // Se o DetailReport tiver um DataSource, pode ter sub-reports aninhados
                        foreach (Band subBand in detailReport.Bands)
                        {
                            if (subBand is DetailReportBand)
                            {
                                DetailReportBand subDetailReport = subBand as DetailReportBand;
                                if (subDetailReport.Bands != null && subDetailReport.Bands.Count > 0)
                                {
                                    AplicarFonteRecursiva(subDetailReport.Bands, fonteRelatorio);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // DEBUG: Se houver erro, mostra para debug
                System.Diagnostics.Debug.WriteLine($"Erro ao aplicar fonte: {ex.Message}");
            }
        }
        
        /// <summary>
        /// Aplica fonte recursivamente em todas as bands e controles
        /// </summary>
        private void AplicarFonteRecursiva(BandCollection bands, Font fonte)
        {
            if (bands == null) return;
            
            foreach (Band band in bands)
            {
                if (band == null) continue;
                
                // DEBUG: mostra qual band está sendo processada
                System.Diagnostics.Debug.WriteLine($"Aplicando fonte na band: {band.GetType().Name} - {band.Name}");
                
                if (band.Controls == null || band.Controls.Count == 0) continue;
                
                foreach (XRControl control in band.Controls)
                {
                    try
                    {
                        // Aplica em labels (incluindo labels com DataBinding)
                        if (control is XRLabel)
                        {
                            XRLabel label = control as XRLabel;
                            // Mantém negrito se já for negrito
                            FontStyle style = label.Font.Bold ? FontStyle.Bold : FontStyle.Regular;
                            label.Font = new Font(fonte.FontFamily, fonte.Size, style);
                            
                            // DEBUG
                            System.Diagnostics.Debug.WriteLine($"  - Label aplicado: {label.Name}");
                        }
                        // Aplica em tabelas
                        else if (control is XRTable)
                        {
                            XRTable table = control as XRTable;
                            table.Font = fonte;
                            
                            // Aplica em cada linha da tabela
                            foreach (XRTableRow row in table.Rows)
                            {
                                row.Font = fonte;
                                
                                // Aplica em cada célula
                                foreach (XRTableCell cell in row.Cells)
                                {
                                    // Mantém negrito se já for negrito
                                    FontStyle style = cell.Font.Bold ? FontStyle.Bold : FontStyle.Regular;
                                    cell.Font = new Font(fonte.FontFamily, fonte.Size, style);
                                }
                            }
                            
                            System.Diagnostics.Debug.WriteLine($"  - Table aplicado: {table.Name}");
                        }
                        // Aplica em outros controles de texto
                        else if (control is XRRichText)
                        {
                            control.Font = fonte;
                            System.Diagnostics.Debug.WriteLine($"  - RichText aplicado: {control.Name}");
                        }
                        // Tenta aplicar em qualquer controle que tenha propriedade Font
                        else if (control.GetType().GetProperty("Font") != null)
                        {
                            control.Font = fonte;
                            System.Diagnostics.Debug.WriteLine($"  - Controle genérico aplicado: {control.Name}");
                        }
                    }
                    catch (Exception ex)
                    {
                        // DEBUG: mostra qual controle deu erro
                        System.Diagnostics.Debug.WriteLine($"  ERRO no controle {control.Name}: {ex.Message}");
                    }
                }
            }
        }
    }
}

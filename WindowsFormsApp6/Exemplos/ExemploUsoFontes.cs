using System;
using System.Drawing;
using WindowsFormsApp6.Utilitarios;

namespace WindowsFormsApp6.Exemplos
{
    /// <summary>
    /// Exemplo de como usar as configurações de fonte em seus relatórios e impressões
    /// </summary>
    public class ExemploUsoFontes
    {
        /// <summary>
        /// Exemplo de uso da fonte em XtraReports
        /// </summary>
        public void ConfigurarFonteRelatorio()
        {
            // Obtém a fonte configurada para relatórios
            Font fonteRelatorio = FonteHelper.ObterFonteRelatorio();
            
            // Ou pode obter nome e tamanho separadamente
            string nomeFonte = FonteHelper.ObterNomeFonteRelatorio();
            int tamanhoFonte = FonteHelper.ObterTamanhoFonteRelatorio();

            // Exemplo de aplicação em XtraReport
            // xrLabel1.Font = fonteRelatorio;
            // 
            // Ou criar uma fonte com estilo específico:
            // Font fonteBold = new Font(nomeFonte, tamanhoFonte, FontStyle.Bold);
            // xrLabel2.Font = fonteBold;
        }

        /// <summary>
        /// Exemplo de uso da fonte em impressão matricial
        /// </summary>
        public void ConfigurarFonteImpressao()
        {
            // Obtém a fonte configurada para impressão matricial
            Font fonteImpressao = FonteHelper.ObterFonteImpressao();
            
            // Ou pode obter nome e tamanho separadamente
            string nomeFonte = FonteHelper.ObterNomeFonteImpressao();
            int tamanhoFonte = FonteHelper.ObterTamanhoFonteImpressao();

            // Exemplo de aplicação em Graphics
            // Graphics g = ...;
            // g.DrawString("Texto", fonteImpressao, Brushes.Black, x, y);
            //
            // Ou em PrintDocument:
            // e.Graphics.DrawString(texto, fonteImpressao, Brushes.Black, x, y);
        }

        /// <summary>
        /// Exemplo completo de impressão usando fonte configurada
        /// </summary>
        public void ExemploImpressaoCompleta()
        {
            /*
            Font fonteImpressao = FonteHelper.ObterFonteImpressao();
            
            void PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
            {
                float yPos = 0;
                int leftMargin = e.MarginBounds.Left;
                int topMargin = e.MarginBounds.Top;
                
                // Usa a fonte configurada
                e.Graphics.DrawString("Cabeçalho do Documento", fonteImpressao, Brushes.Black, leftMargin, topMargin + yPos);
                yPos += fonteImpressao.GetHeight(e.Graphics);
                
                e.Graphics.DrawString("Linha 2", fonteImpressao, Brushes.Black, leftMargin, topMargin + yPos);
                yPos += fonteImpressao.GetHeight(e.Graphics);
            }
            */
        }
    }
}

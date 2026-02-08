using System;
using System.Drawing;
using WindowsFormsApp6.Modelos;
using WindowsFormsApp6.Repositorios.Utilitarios;

namespace WindowsFormsApp6.Utilitarios
{
    /// <summary>
    /// Helper para obter configurações de fonte do sistema
    /// </summary>
    public static class FonteHelper
    {
        private static ModelConfiguracao _configuracaoCache;

        /// <summary>
        /// Limpa o cache de configurações
        /// DEVE SER CHAMADO APÓS SALVAR CONFIGURAÇÕES
        /// </summary>
        public static void LimparCache()
        {
            _configuracaoCache = null;
        }

        /// <summary>
        /// Carrega as configurações do banco
        /// </summary>
        private static ModelConfiguracao ObterConfiguracoes()
        {
            if (_configuracaoCache == null)
            {
                try
                {
                    var repo = new RepositorioConfiguracao();
                    _configuracaoCache = repo.Listar();
                }
                catch
                {
                    // Retorna valores padrão em caso de erro
                    _configuracaoCache = new ModelConfiguracao
                    {
                        FonteRelatorioNome = "Arial",
                        FonteRelatorioTamanho = 10,
                        FonteImpressaoNome = "Courier New",
                        FonteImpressaoTamanho = 8
                    };
                }
            }
            return _configuracaoCache;
        }

        /// <summary>
        /// Obtém a fonte configurada para relatórios (XtraReports)
        /// </summary>
        public static Font ObterFonteRelatorio()
        {
            var config = ObterConfiguracoes();
            
            string nomeFonte = string.IsNullOrEmpty(config.FonteRelatorioNome) ? "Arial" : config.FonteRelatorioNome;
            int tamanhoFonte = config.FonteRelatorioTamanho > 0 ? config.FonteRelatorioTamanho : 10;

            try
            {
                return new Font(nomeFonte, tamanhoFonte);
            }
            catch
            {
                // Se a fonte não existir, retorna Arial
                return new Font("Arial", tamanhoFonte);
            }
        }

        /// <summary>
        /// Obtém a fonte configurada para impressão matricial
        /// </summary>
        public static Font ObterFonteImpressao()
        {
            var config = ObterConfiguracoes();
            
            string nomeFonte = string.IsNullOrEmpty(config.FonteImpressaoNome) ? "Courier New" : config.FonteImpressaoNome;
            int tamanhoFonte = config.FonteImpressaoTamanho > 0 ? config.FonteImpressaoTamanho : 8;

            try
            {
                return new Font(nomeFonte, tamanhoFonte);
            }
            catch
            {
                // Se a fonte não existir, retorna Courier New
                return new Font("Courier New", tamanhoFonte);
            }
        }

        /// <summary>
        /// Obtém o nome da fonte para relatórios
        /// </summary>
        public static string ObterNomeFonteRelatorio()
        {
            var config = ObterConfiguracoes();
            return string.IsNullOrEmpty(config.FonteRelatorioNome) ? "Arial" : config.FonteRelatorioNome;
        }

        /// <summary>
        /// Obtém o tamanho da fonte para relatórios
        /// </summary>
        public static int ObterTamanhoFonteRelatorio()
        {
            var config = ObterConfiguracoes();
            return config.FonteRelatorioTamanho > 0 ? config.FonteRelatorioTamanho : 10;
        }

        /// <summary>
        /// Obtém o nome da fonte para impressão
        /// </summary>
        public static string ObterNomeFonteImpressao()
        {
            var config = ObterConfiguracoes();
            return string.IsNullOrEmpty(config.FonteImpressaoNome) ? "Courier New" : config.FonteImpressaoNome;
        }

        /// <summary>
        /// Obtém o tamanho da fonte para impressão
        /// </summary>
        public static int ObterTamanhoFonteImpressao()
        {
            var config = ObterConfiguracoes();
            return config.FonteImpressaoTamanho > 0 ? config.FonteImpressaoTamanho : 8;
        }
    }
}

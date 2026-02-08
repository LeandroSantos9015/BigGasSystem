using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp6.Modelos.Movimentacao;

namespace WindowsFormsApp6.Modelos
{
    public class ModelConfiguracao : DapperDinamico
    {
        public decimal ValorFrete { get; set; }

        public string PortaImpressora { get; set; }

        public bool MostrarExcluidos { get; set; }

        public bool PerguntarImpressora { get; set; }

        // Configurações de Fonte para Relatórios (XtraReports)
        public string FonteRelatorioNome { get; set; }
        
        public int FonteRelatorioTamanho { get; set; }

        // Configurações de Fonte para Impressão Matricial
        public string FonteImpressaoNome { get; set; }
        
        public int FonteImpressaoTamanho { get; set; }

        public DynamicParameters Save => Salvar(this);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp6.Relatorio.ModeloRelatorio
{
    public class Relatorio03_NotaDeEntrada
    {
        public Int64 Id { get; set; }

        public DateTime Data { get; set; }
        public string DescricaoNota { get; set; }
        public string Fornecedor { get; set; }
        public decimal ValorLiquidoTotal { get; set; }


        public string Mercadoria { get; set; }
        public decimal PrecoVenda { get; set; }
        public decimal PrecoCusto { get; set; }
        public decimal Quantidade { get; set; }


        public string QtdTotal => (PrecoCusto * Quantidade).ToString("C2");


    }

    public class AgrupadorRelatorio03
    {
        public Int64 Id { get; set; }
        public string DescricaoNota { get; set; }
        public string Fornecedor { get; set; }

        public string Data { get; set; }
        public string ValorLiquidoTotal { get; set; }

        public IList<Relatorio03_NotaDeEntrada> Lista { get; set; }
    }
}

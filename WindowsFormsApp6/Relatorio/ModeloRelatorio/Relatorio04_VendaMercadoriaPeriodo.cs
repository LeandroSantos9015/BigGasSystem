using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp6.Relatorio.ModeloRelatorio
{
    public class Relatorio04_VendaMercadoriaPeriodo
    {
        public string Nome { get; set; }
        public decimal Quantidade { get; set; }
        public decimal PrecoVenda { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }

        public decimal ValorTotal { get; set; }

    }

    public class AgrupadorRelatorio04
    {
        public string Nome { get; set; }
        public decimal Quantidade { get; set; }
        public decimal PrecoVenda { get; set; }
        public string Data { get; set; }

        public decimal Valor { get; set; }

        public IList<Relatorio04_VendaMercadoriaPeriodo> Lista { get; set; }
    }
}

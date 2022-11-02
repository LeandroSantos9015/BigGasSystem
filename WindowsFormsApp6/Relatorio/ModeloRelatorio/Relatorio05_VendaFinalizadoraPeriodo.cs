using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp6.Relatorio.ModeloRelatorio
{
    public class Relatorio05_VendaFinalizadoraPeriodo
    {
        public string Finalizadora { get; set; }

        public decimal Valor { get; set; }

        public decimal Frete { get; set; }

        public DateTime Data { get; set; }

        public string ProxyFinalizadora => ValorDia.ToString("C2");

        public string ProxyValorDia => (ValorDia + Frete).ToString("C2");

        public string ProxyValor => Valor.ToString("C2");

        public string ProxyFrete => Frete.ToString("C2");

        public decimal ValorDia { get; set; }

        public decimal ValorTotal => ValorDia + Frete;
    }

    public class AgrupadorRelatorio05
    {
        public string Finalizadora { get; set; }

        public string Data { get; set; }

        public decimal Valor { get; set; }

        public IList<Relatorio05_VendaFinalizadoraPeriodo> Lista { get; set; }
    }
}
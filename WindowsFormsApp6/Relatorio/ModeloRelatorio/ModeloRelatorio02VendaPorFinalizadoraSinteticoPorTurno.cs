
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relatorios.ModeloRelatorio
{
    [Serializable]
    public class ModeloRelatorio02VendaPorFinalizadoraSinteticoPorTurno
    {
        public Int64 IdTurno { get; set; }

        public DateTime Entrada { get; set; }

        public DateTime Saida { get; set; }

        public Decimal Total { get; set; }

        public Int64 Finalizadora { get; set; }

        public String ProxyFinalizadora => "";// { get { return ((EFinalizadora)Finalizadora).Descricao(); ; } }

    }

    [Serializable]
    public class AgrupadorRelatorio02
    {
        public Int64 IdTurno { get; set; }

        public DateTime Entrada { get; set; }

        public DateTime Saida { get; set; }

        public string ProxySaida { get { return Saida.Equals(Entrada) ? "ABERTO" : Saida.ToString(); } }

        public List<ModeloRelatorio02VendaPorFinalizadoraSinteticoPorTurno> Lista { get; set; }

    }


}

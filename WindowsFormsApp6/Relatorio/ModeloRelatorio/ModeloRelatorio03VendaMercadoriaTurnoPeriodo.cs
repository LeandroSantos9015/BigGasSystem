using System;
using System.Collections.Generic;

namespace Relatorios.ModeloRelatorio
{
    [Serializable]
    public class ModeloRelatorio03VendaMercadoriaTurnoPeriodo
    {
        public Int64 IdMercadoria { get; set; }

        public String Descricao { get; set; }

        public Decimal ValorUnitario { get; set; }

        public Decimal Quantidade { get; set; }

        public Decimal Total { get; set; }

        public Int64? IdTurno { get; set; }

        public DateTime TurnoEntrada { get; set; }

        public DateTime TurnoSaida { get; set; }

    }

    [Serializable]
    public class AgrupadorRelatorio03___
    {
        public Int64? IdTurno { get; set; }

        public DateTime TurnoEntrada { get; set; }

        public DateTime TurnoSaida { get; set; }

        public string ProxySaida { get { return TurnoSaida.Equals(TurnoEntrada) ? "ABERTO" : TurnoSaida.ToString(); } }

        public List<ModeloRelatorio03VendaMercadoriaTurnoPeriodo> Lista { get; set; }

        public Decimal TotalTurno { get; set; }

    }


}

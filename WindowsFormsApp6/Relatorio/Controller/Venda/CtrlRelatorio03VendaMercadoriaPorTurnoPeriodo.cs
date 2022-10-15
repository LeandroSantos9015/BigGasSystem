using DevExpress.XtraReports.UI;
using Relatorios.Controller.Abstrato;
using Relatorios.Enumeradores;
using Relatorios.ModeloRelatorio;
using Relatorios.Query.Venda;
using Relatorios.View.Cadastros.Venda;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Relatorios.Controller.Venda
{
    public class CtrlRelatorio03VendaMercadoriaPorTurnoPeriodo : IDisposable
    {
        QueryRelatorio03VendaMercadoriaTurnoPeriodo QueryRelatorio = new QueryRelatorio03VendaMercadoriaTurnoPeriodo();

       // Relatorio03VendaMercadoriaTurno RelatorioTurno = new Relatorio03VendaMercadoriaTurno(ERelatorio.ERelatorio03VendaDeMercadorias);

        IList<ModeloRelatorio03VendaMercadoriaTurnoPeriodo> lista = null;

        IList<AgrupadorRelatorio03___> Lista = null;

        public void Dispose()
        {
            //if (RelatorioTurno != null)
            //{
            //    RelatorioTurno.Dispose();
            //    RelatorioTurno = null;
            //}
        }
        public CtrlRelatorio03VendaMercadoriaPorTurnoPeriodo(object[] parametros)
        {
            string inicio = (string)((DateTime)parametros[0]).ToString();//.ConvertParaDateTimeBanco();
            string fim = (string)((DateTime)parametros[1]).ToString();//.ConvertParaDateTimeBanco();

            this.lista = QueryRelatorio.QueryRelatorioDapper(inicio, fim, true);

            this.MontaRelatorio();

            if (VerificaRelatorioVazio.Verificar(Lista))
                return;

            //this.RelatorioTurno.DataSource = this.Lista;

            this.Inicializacao();

           // this.RelatorioTurno.ShowPreview();
        }

        public void Inicializacao()
        {
            //this.RelatorioTurno.LblId.DataBindings.Add(new XRBinding("Text", null, "IdTurno", "Nº {0:}"));
            //this.RelatorioTurno.LblEntrada.DataBindings.Add(new XRBinding("Text", null, "TurnoEntrada", "{0:dd/MM/yy HH:mm}"));
            //this.RelatorioTurno.LblSaida.DataBindings.Add(new XRBinding("Text", null, "ProxySaida", "{0:dd/MM/yy HH:mm}"));
            //this.RelatorioTurno.LblTotalTurno.DataBindings.Add(new XRBinding("Text", null, "TotalTurno", "{0:C2}"));

            //this.RelatorioTurno.LblMercadoria.DataBindings.Add(new XRBinding("Text", null, "Lista.Descricao", ""));
            //this.RelatorioTurno.LblPreco.DataBindings.Add(new XRBinding("Text", null, "Lista.ValorUnitario", "{0:C2}"));
            //this.RelatorioTurno.LblQtd.DataBindings.Add(new XRBinding("Text", null, "Lista.Quantidade", "{0:N2}"));
            //this.RelatorioTurno.LblTotal.DataBindings.Add(new XRBinding("Text", null, "Lista.Total", "{0:C2}"));
        }

        private void MontaRelatorio()
        {
            this.Lista =

                lista
                .GroupBy(x => new { x.IdTurno, x.TurnoEntrada, x.TurnoSaida }).Select(agrupado => new AgrupadorRelatorio03___()
                {
                    IdTurno = agrupado.Key.IdTurno,
                    TurnoEntrada = agrupado.Key.TurnoEntrada,
                    TurnoSaida = agrupado.Key.TurnoSaida,
                    TotalTurno = agrupado.Sum(x => x.Total),

                    Lista = lista.Where(x => x.IdTurno == agrupado.Key.IdTurno)
                         .ToList<ModeloRelatorio03VendaMercadoriaTurnoPeriodo>()

                }).ToList();
        }
    }
}

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
    public class CtrlRelatorio02VendaPorFinalizadoraSinteticoPorTurno : IDisposable
    {
        QueryRelatorio02VendaPorFinalizadoraSinteticoPorTurno QueryRelatorio = new QueryRelatorio02VendaPorFinalizadoraSinteticoPorTurno();

        Relatorio02VendaPorFinalizadoraSinteticoPorTurno Relatorio = new Relatorio02VendaPorFinalizadoraSinteticoPorTurno(ERelatorio.ERelatorio02VendaPorFinalizadoraPorTurno);

        IList<ModeloRelatorio02VendaPorFinalizadoraSinteticoPorTurno> lista = null;

        IList<AgrupadorRelatorio02> Lista = null;

        public void Dispose()
        {
            if (Relatorio != null)
            {
                Relatorio.Dispose();
                Relatorio = null;
            }
        }
        public CtrlRelatorio02VendaPorFinalizadoraSinteticoPorTurno(object[] parametros)
        {
            string inicio = (string)((DateTime)parametros[0]).ToString();//.ConvertParaDateTimeBanco();
            string fim = (string)((DateTime)parametros[1]).ToString();//;.ConvertParaDateTimeBanco();

            this.lista = QueryRelatorio.QueryRelatorioDapper(inicio, fim);

            this.MontaRelatorio();

            if (VerificaRelatorioVazio.Verificar(Lista))
                return;
            this.Relatorio.DataSource = this.Lista;

            this.Inicializacao();

            this.Relatorio.ShowPreview();
        }

        public void Inicializacao()
        {
            this.Relatorio.LblId.DataBindings.Add(new XRBinding("Text", null, "IdTurno", ""));
            this.Relatorio.LblEntrada.DataBindings.Add(new XRBinding("Text", null, "Entrada", "{0:dd/MM/yy HH:mm}"));
            this.Relatorio.LblSaida.DataBindings.Add(new XRBinding("Text", null, "ProxySaida", "{0:dd/MM/yy HH:mm}"));
            this.Relatorio.LblFinalizadora.DataBindings.Add(new XRBinding("Text", null, "Lista.ProxyFinalizadora", ""));
            this.Relatorio.LblTotal.DataBindings.Add(new XRBinding("Text", null, "Lista.Total", "{0:C2}"));
        }

        private void MontaRelatorio()
        {
            this.Lista =

                lista
                .GroupBy(x => new { x.IdTurno, x.Entrada, x.Saida }).Select(agrupado => new AgrupadorRelatorio02()
                {
                    IdTurno = agrupado.Key.IdTurno,
                    Entrada = agrupado.Key.Entrada,
                    Saida = agrupado.Key.Saida,

                    Lista = lista.Where(x => x.IdTurno == agrupado.Key.IdTurno)
                        .ToList<ModeloRelatorio02VendaPorFinalizadoraSinteticoPorTurno>()

                }).ToList();
        }
    }
}

using Relatorios.View.Cadastros.Mercadorias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraReports.UI;
using Relatorios.Enumeradores;
using Relatorios.Controller.Abstrato;
using WindowsFormsApp6.Relatorio.ModeloRelatorio;
using Relatorios.Query.Entrada;
using Relatorios.View.Cadastros.Entrada;

namespace Relatorios.Controller.Cadastros
{
    public class CtrlRelatorio05VendaFinalizadoraPeriodo : IDisposable
    {
        QueryRelatorio05VendaFinalizadoraPeriodo QueryRelatorio01 = new QueryRelatorio05VendaFinalizadoraPeriodo();

        Relatorio05VendaPorFinalizadoraPorPeriodo Relatorio = null;

        IList<Relatorio05_VendaFinalizadoraPeriodo> lista = null;

        IList<AgrupadorRelatorio05> Lista = null;

        public void Dispose()
        {
            if (Relatorio != null)
            {
                Relatorio.Dispose();
                Relatorio = null;
            }
        }
        public CtrlRelatorio05VendaFinalizadoraPeriodo(object[] parametros)
        {
            lista = QueryRelatorio01.QueryRelatorio(parametros);

            MontaRelatorio();

            if (VerificaRelatorioVazio.Verificar(Lista))
                return;

            string nomeFooter = "Valor Total Frete e Finalizadoras:";
            decimal valor = 0;


            foreach (var item in this.Lista)
                valor += item.Lista.LastOrDefault().ValorTotal;

            string valorString = (valor).ToString("C2");

            this.Relatorio = new Relatorio05VendaPorFinalizadoraPorPeriodo(ERelatorio.VendaFinalizadoraPorPeriodo05, nomeFooter, valorString);

            this.Relatorio.DataSource = Lista;
            
            // ⭐ Aplica fonte DEPOIS de definir DataSource
            this.Relatorio.AplicarFonteConfigurada();

            Inicializacao();

            Relatorio.ShowPreview();
        }

        private void MontaRelatorio()
        {
            this.Lista =

                lista
                .GroupBy(x => new { x.Data }).Select(agrupado => new AgrupadorRelatorio05()
                {
                    Data = agrupado.Key.Data.ToString("dd/MM/yyyy"),
                    Lista = lista.Where(x => x.Data == agrupado.Key.Data)
                          .ToList<Relatorio05_VendaFinalizadoraPeriodo>(),
                    Valor = agrupado.Sum(o => o.ValorTotal)
                   
                }).ToList();

            foreach (var item in Lista)
            {
                item.Lista.LastOrDefault().ValorDia = item.Lista.Sum(x => x.Valor);
            }


            //this.Lista.FirstOrDefault().Lista.LastOrDefault().ValorDia = lista.Sum(x => x.Valor);

        }


        private void Inicializacao()
        {

            //Relatorio.LblPrecoVenda.DataBindings.Add(new XRBinding("Text", null, "PrecoVenda", "{0:C2}"));

            //Relatorio.LblEstoque.DataBindings.Add(new XRBinding("Text", null, "QtdEstoque", "{0:N3}"));
        }


    }
}


using Relatorios.View.Cadastros.Mercadorias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraReports.UI;
using Relatorios.Enumeradores;
using Relatorios.ModeloRelatorio;
using Relatorios.Controller.Abstrato;

namespace Relatorios.Controller.Cadastros
{
    public class CtrlRelatorio01ListaMercadorias : IDisposable
    {
        //QueryRelatorio01ListaMercadorias QueryRelatorio01 = new QueryRelatorio01ListaMercadorias();

      //  Relatorio01ListaMercadorias Relatorio = new Relatorio01ListaMercadorias(ERelatorio.ERelatorio01ListaMercadorias);

        IList<ModeloRelatorio01ListaMercadorias> Lista = null;

        public void Dispose()
        {
            //if (Relatorio != null)
            //{
            //    Relatorio.Dispose();
            //    Relatorio = null;
            //}
        }
        public CtrlRelatorio01ListaMercadorias(object[] parametros)
        {
            Lista = null;// QueryRelatorio01.QueryRelatorio(parametros);

            if (VerificaRelatorioVazio.Verificar(Lista))
                return;


           // this.Relatorio.DataSource = Lista;

            Inicializacao();

           // Relatorio.ShowPreview();
        }

        private void Inicializacao()
        {

          //  Relatorio.LblPrecoVenda.DataBindings.Add(new XRBinding("Text", null, "PrecoVenda", "{0:C2}"));

          //  Relatorio.LblEstoque.DataBindings.Add(new XRBinding("Text", null, "QtdEstoque", "{0:N3}"));
        }
       

    }
}

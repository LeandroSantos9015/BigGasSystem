using Relatorios.Controller.Cadastros;

using Relatorios.ControllerFiltros;
using Relatorios.Enumeradores;
using Relatorios.Filtros.Venda;
using System.Windows.Forms;

namespace Relatorios.CtrlFiltros.Entrada
{
    public class BehaviorFiltro005 : ACtrlFiltroRelatorio
    {
        private UCFiltro005 filtroRelatorio = new UCFiltro005();

        public BehaviorFiltro005(ERelatorio relatorio) : base(relatorio) { }

        public override object[] dadosFiltro => new object[] { this.filtroRelatorio.DateInicio.Value, this.filtroRelatorio.DateFim.Value };

        public override UserControl Controle => filtroRelatorio;

        public override object ControlRelatorio() => new CtrlRelatorio05VendaFinalizadoraPeriodo(dadosFiltro);


    }
}

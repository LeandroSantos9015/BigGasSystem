using Relatorios.Controller.Venda;
using Relatorios.ControllerFiltros;
using Relatorios.Enumeradores;
using Relatorios.Filtros.Venda;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Relatorios.CtrlFiltros.Venda
{
    public class BehaviorFiltro003 : ACtrlFiltroRelatorio
    {
        private UCFiltro003 filtroRelatorio = new UCFiltro003();

        public BehaviorFiltro003(ERelatorio relatorio) : base(relatorio) { }

        public override object[] dadosFiltro => new object[] { this.filtroRelatorio.DateInicio.Value, this.filtroRelatorio.DateFim.Value, this.filtroRelatorio.ChkTurno.Checked };

        public override UserControl Controle => filtroRelatorio;

        public override object ControlRelatorio() => new CtrlRelatorio03VendaMercadoriaPorTurnoPeriodo(dadosFiltro);


    }
}

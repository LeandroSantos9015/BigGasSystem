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
    public class BehaviorFiltro002 : ACtrlFiltroRelatorio
    {
        private UCFiltro002 filtroRelatorio = new UCFiltro002();

        public BehaviorFiltro002(ERelatorio relatorio) : base(relatorio)
        {


        }

        public override object[] dadosFiltro => new object[2] { this.filtroRelatorio.DateInicio.Value, this.filtroRelatorio.DateFim.Value };

        public override UserControl Controle => filtroRelatorio;

        public override object ControlRelatorio() => new CtrlRelatorio02VendaPorFinalizadoraSinteticoPorTurno(dadosFiltro);


    }
}

using Relatorios.Controller.Cadastros;
using Relatorios.ControllerFiltros;
using Relatorios.Enumeradores;
using Relatorios.Filtros.Cadastro;
using System.Windows.Forms;
using static WindowsFormsApp6.Utilitarios.Util;

namespace Relatorios.CtrlFiltros.Cadastro
{
    public class BehaviorFiltro001 : ACtrlFiltroRelatorio
    {
        private UCFiltro001 filtroRelatorio = new UCFiltro001();

        public override void ConfiguraUserControl()
        {
            this.filtroRelatorio.Situacao.DataSource = SetDataSource.Carregar(typeof(EAtivo));
            this.filtroRelatorio.Situacao.DisplayMember = SetDataSource.Mostrador;
            this.filtroRelatorio.Situacao.ValueMember = SetDataSource.Valor;
        }

        public override object[] dadosFiltro => new object[1] { this.filtroRelatorio.Situacao.SelectedValue };

        public BehaviorFiltro001(ERelatorio relatorio) : base(relatorio) { }

        public override UserControl Controle => this.filtroRelatorio;

        public override object ControlRelatorio() => new CtrlRelatorio01ListaMercadorias(dadosFiltro);
    }

   

}

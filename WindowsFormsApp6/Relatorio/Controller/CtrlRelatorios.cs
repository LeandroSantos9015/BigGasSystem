using Relatorios.CtrlFiltros.Cadastro;
using Relatorios.CtrlFiltros.Venda;
using Relatorios.Enumeradores;
using WindowsFormsApp6;

namespace Relatorios.Controller
{
    /// <summary>
    /// Nesta classe fica as chamadas para os relatorios
    /// separa a regra de geração com a inserção de relatórios
    /// </summary>

    public class CtrlRelatorios : ARelatorios
    {
        public CtrlRelatorios(IPrincipalView Pai, string categoria) : base(Pai, categoria) { }

        public override void AbrirRelatorioDesejado()
        {
            ERelatorio relatorio = (ERelatorio)this.RelatorioView.ListaRelatorio.SelectedValue;

            switch (relatorio)
            {
                case ERelatorio.ERelatorio01ListaMercadorias:
                    new BehaviorFiltro001(ERelatorio.ERelatorio01ListaMercadorias);
                    break;

                case ERelatorio.ERelatorio02VendaPorFinalizadoraPorTurno:
                    new BehaviorFiltro002(ERelatorio.ERelatorio02VendaPorFinalizadoraPorTurno);
                    break;

                case ERelatorio.ERelatorio03VendaDeMercadorias:
                    new BehaviorFiltro003(ERelatorio.ERelatorio03VendaDeMercadorias);
                    break;

                default:
                    //Alerta("Relatório", "Relatório ainda não foi implementado");
                    break;
            }
        }


    }
}

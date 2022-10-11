using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp6.Controles.Cadastros;
using WindowsFormsApp6.Interface.Movimentacao;
using WindowsFormsApp6.Menus.Movimentacao;
using WindowsFormsApp6.Modelos;
using WindowsFormsApp6.Utilitarios;

namespace WindowsFormsApp6.Controles.Movimentacao
{
    public class CtrlSaida
    {
        IMovimentacaoSaidaView SaidaView { get; set; }

        RegraCliente regraCliente = new RegraCliente();

        IList<ModeloCidade> listaCidades = new List<ModeloCidade>();

        IPrincipalView Pai;

        public CtrlSaida(IPrincipalView Pai)
        {
            SaidaView = new FrmSaidas();

            this.Pai = Pai;

            SaidaView.SaidaView.MdiParent = Pai.PrincipalView;

            SaidaView.SaidaView.Show();

            DelegarEventos();

            ObjetoParaTela();

        }

        private void DelegarEventos()
        {
            SaidaView.BtnPesquisar.Click += BtnPesquisar_Click;
            SaidaView.BtnAdd.Click += BtnAdd_Click;
            SaidaView.BtnExc.Click += BtnExc_Click;
            SaidaView.BtnFinalizar.Click += BtnFinalizar_Click;

            listaCidades = regraCliente.ListarCidades();

        }

        private void BtnFinalizar_Click(object sender, EventArgs e)
        {

        }

        private void BtnExc_Click(object sender, EventArgs e)
        {

        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {

        }

        private void BtnPesquisar_Click(object sender, EventArgs e)
        {
            var lista = regraCliente.ListaClientes().Cast<Object>().ToList();

            CtrlPesquisar Pesquisa = new CtrlPesquisar(Pai, lista, 690, "Pesquisa de Clientes");

            ObjetoParaTela(Pesquisa.RetornaObjetoSelecionado() as ModelCliente);

        }

        private void ObjetoParaTela(ModelCliente modelCliente = null)
        {
            if (modelCliente != null)
            {
                ModeloCidade cidade = listaCidades.Where(x => x.Id == modelCliente.Cidade).FirstOrDefault();

                SaidaView.TxtCliente.Text = modelCliente.Nome;
                SaidaView.LblEndereco.Text = "Endereço: " + modelCliente.Endereco + "," + modelCliente.Numero + " - " + modelCliente.Bairro;
                SaidaView.LblTelefone.Text = "Telefone: " + modelCliente.Telefone.TelefoneMascara();
                SaidaView.LblCidade.Text = "Cidade: " + cidade.Nome;
            }
            else
            {
                SaidaView.TxtCliente.Text = null;
                SaidaView.LblEndereco.Text = "...";
                SaidaView.LblTelefone.Text = "...";
                SaidaView.LblCidade.Text = "...";
            }
        }
    }

}


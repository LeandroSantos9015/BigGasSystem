using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp6.Interface.Movimentacao;
using WindowsFormsApp6.Modelos;
using WindowsFormsApp6.Movimentacao;

namespace WindowsFormsApp6.Controles.Movimentacao
{


    public class CtrlEntrada
    {
        public IMovimentacaoEntradaView MovimentacaoEntradaView { get; set; }

        IPrincipalView Pai;

        public CtrlEntrada(IPrincipalView Pai)
        {
            this.Pai = Pai;

            MovimentacaoEntradaView = new FrmEntradas();

            MovimentacaoEntradaView.MovimentacaoEntradaView.MdiParent = Pai.PrincipalView;

            MovimentacaoEntradaView.MovimentacaoEntradaView.Show();

            DelegarEventos();
        }

        private void DelegarEventos()
        {
            MovimentacaoEntradaView.BtnAdd.Click += BtnAdd_Click;
            MovimentacaoEntradaView.BtnEstornar.Click += BtnEstornar_Click;
            MovimentacaoEntradaView.BtnPesquisar.Click += BtnPesquisar_Click;
            MovimentacaoEntradaView.BtnProcessar.Click += BtnProcessar_Click;
            MovimentacaoEntradaView.BtnRemove.Click += BtnRemove_Click;
            MovimentacaoEntradaView.BtnSalvar.Click += BtnSalvar_Click;


        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {

        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {

        }

        private void BtnProcessar_Click(object sender, EventArgs e)
        {

        }

        private void BtnPesquisar_Click(object sender, EventArgs e)
        {

        }

        private void BtnEstornar_Click(object sender, EventArgs e)
        {

        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var add = new CtrlAddMercadoria();

            AtualizaListaMercadoria(add.RetornaObjetoSelecionado());
        }

        private void AtualizaListaMercadoria(ModelMercadoriaEntrada mercadoria = null)
        {
            if (mercadoria == null)
                return;

            IList<ModelMercadoriaEntrada> listaMercadoria = MovimentacaoEntradaView.DgvMercadorias.DataSource as List<ModelMercadoriaEntrada> ?? new List<ModelMercadoriaEntrada>();

            listaMercadoria.Add(mercadoria);

            MovimentacaoEntradaView.DgvMercadorias.DataSource = listaMercadoria.ToList();

            MovimentacaoEntradaView.DgvMercadorias.Columns["Descricao"].DisplayIndex = 0;
            MovimentacaoEntradaView.DgvMercadorias.Columns["Descricao"].Width = 115;


            if (MovimentacaoEntradaView.DgvMercadorias.Columns["Id"] != null)
                MovimentacaoEntradaView.DgvMercadorias.Columns["Id"].Visible = false;

            if (MovimentacaoEntradaView.DgvMercadorias.Columns["PrecoVenda"] != null)
            {
                MovimentacaoEntradaView.DgvMercadorias.Columns["PrecoVenda"].DisplayIndex = 1;
                MovimentacaoEntradaView.DgvMercadorias.Columns["PrecoVenda"].Width = 50;
            }

            if (MovimentacaoEntradaView.DgvMercadorias.Columns["PrecoCusto"] != null)
            {
                MovimentacaoEntradaView.DgvMercadorias.Columns["PrecoCusto"].Visible = false;

                //MovimentacaoEntradaView.DgvMercadorias.Columns["PrecoCusto"].DisplayIndex = 2;
                //MovimentacaoEntradaView.DgvMercadorias.Columns["PrecoCusto"].Width = 50;
            }

            if (MovimentacaoEntradaView.DgvMercadorias.Columns["Quantidade"] != null)
            {
                MovimentacaoEntradaView.DgvMercadorias.Columns["Quantidade"].DisplayIndex = 3;
                MovimentacaoEntradaView.DgvMercadorias.Columns["Quantidade"].Width = 30;
            }

            if (MovimentacaoEntradaView.DgvMercadorias.Columns["Total"] != null)
            {
                MovimentacaoEntradaView.DgvMercadorias.Columns["Total"].DisplayIndex = 4;
                MovimentacaoEntradaView.DgvMercadorias.Columns["Total"].Width = 50;
            }


            MovimentacaoEntradaView.ValorTotal.Text = listaMercadoria.Sum(x=>x.Total).ToString("C2");
        }
    }
}

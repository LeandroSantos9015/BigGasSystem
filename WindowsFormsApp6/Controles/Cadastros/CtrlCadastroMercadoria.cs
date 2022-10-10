using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp6.Interface.Cadastros;
using WindowsFormsApp6.Menus;
using WindowsFormsApp6.Modelos;
using WindowsFormsApp6.Repositorios.Cliente;

namespace WindowsFormsApp6.Controles.Cadastros
{
    public class CtrlCadastroMercadoria
    {
        private RegraMercadoria regraMercadoria = new RegraMercadoria();
        public IMercadoriaCadastroView MercadoriaView { get; set; }

        IPrincipalView Pai;

        public CtrlCadastroMercadoria(IPrincipalView Pai)
        {
            this.Pai = Pai;

            MercadoriaView = new FrmCadastroMercadorias();

            MercadoriaView.MercadoriaView.MdiParent = Pai.PrincipalView;

            MercadoriaView.MercadoriaView.StartPosition = FormStartPosition.CenterParent;

            MercadoriaView.MercadoriaView.Show();

            DelegarEventos();
        }

        private void DelegarEventos()
        {
            MercadoriaView.BtnPesquisar.Click += BtnPesquisar_Click;
            MercadoriaView.BtnExcluir.Click += BtnExcluir_Click;
            MercadoriaView.BtnLimpar.Click += BtnLimpar_Click;
            MercadoriaView.BtnSalvar.Click += BtnSalvar_Click;
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            ModelMercadoria mercadoria = TelaParaObjeto();

            bool camposObrig = false;///CamposObrigatorios(cliente);

            if (!camposObrig)
            {
                bool salvou = regraMercadoria.Salvar(mercadoria);

                if (salvou)
                    ObjetoParaTela();
            }
        }

        private void BtnLimpar_Click(object sender, EventArgs e)
        {

        }

        private void BtnExcluir_Click(object sender, EventArgs e)
        {

        }

        private void BtnPesquisar_Click(object sender, EventArgs e)
        {
            var lista = regraMercadoria.ListaMercadorias().Cast<Object>().ToList();

            CtrlPesquisar Pesquisa = new CtrlPesquisar(Pai, lista, 690, "Pesquisa de Mercadorias");

            ObjetoParaTela(Pesquisa.RetornaObjetoSelecionado() as ModelMercadoria);

        }


        private void ObjetoParaTela(ModelMercadoria mercadoria = null)
        {
            if (mercadoria is null)
            {
                MercadoriaView.TxtId.Text = null;
                MercadoriaView.TxtDescricao.Text = null;
                MercadoriaView.TxtPrecoCusto.Text = null;
                MercadoriaView.TxtPrecoVenda.Text = null;
                MercadoriaView.TxtQtd.Text = null;

                MercadoriaView.TxtDescricao.BackColor = Color.White;
                MercadoriaView.TxtPrecoCusto.BackColor = Color.White;
                MercadoriaView.TxtPrecoVenda.BackColor = Color.White;
                MercadoriaView.TxtQtd.BackColor = Color.White;

            }
            else
            {
                MercadoriaView.TxtId.Text = mercadoria.Id.ToString();
                MercadoriaView.TxtDescricao.Text = mercadoria.Descricao;
                MercadoriaView.TxtPrecoCusto.Text = mercadoria.PrecoCusto.ToString();
                MercadoriaView.TxtPrecoVenda.Text = mercadoria.PrecoVenda.ToString();
                MercadoriaView.TxtQtd.Text = mercadoria.Quantidade.ToString();

            }
        }

        private ModelMercadoria TelaParaObjeto()
        {
            ModelMercadoria mercadoria = new ModelMercadoria();

            long.TryParse(MercadoriaView.TxtId.Text, out long id);

            decimal.TryParse(MercadoriaView.TxtPrecoCusto.Text, out decimal custo);
            decimal.TryParse(MercadoriaView.TxtPrecoVenda.Text, out decimal venda);
            decimal.TryParse(MercadoriaView.TxtQtd.Text, out decimal qtd);

            mercadoria.Id = id;
            mercadoria.Descricao = MercadoriaView.TxtDescricao.Text;
            mercadoria.PrecoCusto = custo;
            mercadoria.PrecoVenda = venda;
            mercadoria.Quantidade = qtd;

            return mercadoria;
        }
    }
}
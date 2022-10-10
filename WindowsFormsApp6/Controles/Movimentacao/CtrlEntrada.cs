using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp6.Controles.Cadastros;
using WindowsFormsApp6.Enumeradores;
using WindowsFormsApp6.Interface.Movimentacao;
using WindowsFormsApp6.Modelos;
using WindowsFormsApp6.Modelos.Movimentacao;
using WindowsFormsApp6.Movimentacao;
using WindowsFormsApp6.Repositorios.Movimentacao;

namespace WindowsFormsApp6.Controles.Movimentacao
{

    public class CtrlEntrada
    {
        private IList<ModelCliente> listaFornecedores = new RegraCliente().ListaClientes().Where(x => x.Fornecedor).ToList();

        private RepositorioMovimentacao repositorio = new RepositorioMovimentacao();

        public IMovimentacaoEntradaView MovimentacaoEntradaView { get; set; }

        IPrincipalView Pai;

        public CtrlEntrada(IPrincipalView Pai)
        {
            this.Pai = Pai;

            MovimentacaoEntradaView = new FrmEntradas();

            MovimentacaoEntradaView.MovimentacaoEntradaView.MdiParent = Pai.PrincipalView;

            MovimentacaoEntradaView.MovimentacaoEntradaView.Show();

            DelegarEventos();

            ObjetoParaTela();
        }

        private void DelegarEventos()
        {
            MovimentacaoEntradaView.BtnAdd.Click += BtnAdd_Click;
            MovimentacaoEntradaView.BtnEstornar.Click += BtnEstornar_Click;
            MovimentacaoEntradaView.BtnPesquisar.Click += BtnPesquisar_Click;
            MovimentacaoEntradaView.BtnProcessar.Click += BtnProcessar_Click;
            MovimentacaoEntradaView.BtnRemove.Click += BtnRemove_Click;
            MovimentacaoEntradaView.BtnSalvar.Click += BtnSalvar_Click;

            MovimentacaoEntradaView.CbmFornecedor.DataSource = listaFornecedores;
            MovimentacaoEntradaView.CbmFornecedor.DisplayMember = "Nome";
            MovimentacaoEntradaView.CbmFornecedor.ValueMember = "Id";
            MovimentacaoEntradaView.CbmFornecedor.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        #region Eventos

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            EStatusMovimento eStatus = EStatusMovimento.M;

            GravacaoNota(eStatus);
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {

        }

        private void BtnProcessar_Click(object sender, EventArgs e)
        {
            EStatusMovimento eStatus = EStatusMovimento.P;

            GravacaoNota(eStatus);
        }

        private void BtnPesquisar_Click(object sender, EventArgs e)
        {
            EStatusMovimento status = EStatusMovimento.E | EStatusMovimento.P | EStatusMovimento.M;

            var lista = repositorio.Listar(EOperacaoMovimento.Entrada, status).Cast<Object>().ToList();

            CtrlPesquisar Pesquisa = new CtrlPesquisar(Pai, lista, 690, "Pesquisa de Clientes");

            ObjetoParaTela(Pesquisa.RetornaObjetoSelecionado() as ModelMovimentacao);

        }

        private void BtnEstornar_Click(object sender, EventArgs e)
        {
            EStatusMovimento eStatus = EStatusMovimento.E;

            GravacaoNota(eStatus);
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var add = new CtrlAddMercadoria();

            AtualizaListaMercadoria(add.RetornaObjetoSelecionado());
        }

        #endregion

        private void AtualizaListaMercadoria(ModelItemMovimentacao mercadoria = null)
        {
            if (mercadoria == null)
                return;

            IList<ModelItemMovimentacao> listaMercadoria = MovimentacaoEntradaView.DgvMercadorias.DataSource as List<ModelItemMovimentacao> ?? new List<ModelItemMovimentacao>();

            listaMercadoria.Add(mercadoria);

            MovimentacaoEntradaView.DgvMercadorias.DataSource = listaMercadoria.ToList();

            CustomizaGridMercadoria();

            MovimentacaoEntradaView.ValorTotal.Text = listaMercadoria.Sum(x => x.ValorTotal).ToString("C2");
        }

        private void CustomizaGridMercadoria()
        {
            MovimentacaoEntradaView.DgvMercadorias.Columns["Descricao"].DisplayIndex = 0;
            MovimentacaoEntradaView.DgvMercadorias.Columns["Descricao"].Width = 115;


            if (MovimentacaoEntradaView.DgvMercadorias.Columns["Id"] != null)
                MovimentacaoEntradaView.DgvMercadorias.Columns["Id"].Visible = false;

            if (MovimentacaoEntradaView.DgvMercadorias.Columns["IdDocumento"] != null)
                MovimentacaoEntradaView.DgvMercadorias.Columns["IdDocumento"].Visible = false;

            if (MovimentacaoEntradaView.DgvMercadorias.Columns["IdMercadoria"] != null)
                MovimentacaoEntradaView.DgvMercadorias.Columns["IdMercadoria"].Visible = false;

            if (MovimentacaoEntradaView.DgvMercadorias.Columns["PrecoVenda"] != null)
            {
                MovimentacaoEntradaView.DgvMercadorias.Columns["PrecoCusto"].DisplayIndex = 1;
                MovimentacaoEntradaView.DgvMercadorias.Columns["PrecoCusto"].Width = 50;
            }

            if (MovimentacaoEntradaView.DgvMercadorias.Columns["PrecoVenda"] != null)
                MovimentacaoEntradaView.DgvMercadorias.Columns["PrecoVenda"].Visible = false;

            //MovimentacaoEntradaView.DgvMercadorias.Columns["PrecoCusto"].DisplayIndex = 2;
            //MovimentacaoEntradaView.DgvMercadorias.Columns["PrecoCusto"].Width = 50;

            if (MovimentacaoEntradaView.DgvMercadorias.Columns["Quantidade"] != null)
            {
                MovimentacaoEntradaView.DgvMercadorias.Columns["Quantidade"].DisplayIndex = 3;
                MovimentacaoEntradaView.DgvMercadorias.Columns["Quantidade"].Width = 30;
            }

            if (MovimentacaoEntradaView.DgvMercadorias.Columns["ValorTotal"] != null)
            {
                MovimentacaoEntradaView.DgvMercadorias.Columns["ValorTotal"].DisplayIndex = 4;
                MovimentacaoEntradaView.DgvMercadorias.Columns["ValorTotal"].Width = 50;
            }
        }

        private void GravacaoNota(EStatusMovimento eStatus)
        {
            try
            {
                ModelMovimentacao movimentacao = TelaParaObjeto(eStatus);

                Int64 idDocumento = repositorio.Salvar(movimentacao);

                IList<ModelItemMovimentacao> lista = this.MovimentacaoEntradaView.DgvMercadorias.DataSource as List<ModelItemMovimentacao>;

                var listaAtualizada = lista.Select(c =>
                {
                    c.IdDocumento = idDocumento;
                    c.Operacao = EOperacaoMovimento.Entrada;
                    c.Status = eStatus;
                    return c;
                }).ToList();

                repositorio.SalvarItens(listaAtualizada);

                string tipoGravacao = "estornada";

                if (movimentacao.Status.Equals(EStatusMovimento.M))
                    tipoGravacao = "salva";
                else if (movimentacao.Status.Equals(EStatusMovimento.P))
                    tipoGravacao = "processada";

                MessageBox.Show($"Nota de entrada {tipoGravacao} com sucesso!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ObjetoParaTela(ModelMovimentacao modelMovimentacao = null)
        {
            if (modelMovimentacao != null)
            {
                this.MovimentacaoEntradaView.TxtId.Text = modelMovimentacao.Id.ToString();
                this.MovimentacaoEntradaView.TxtDescAcres.Text = modelMovimentacao.DescAcres.ToString();

                this.MovimentacaoEntradaView.DteData.Value = modelMovimentacao.Data;
                this.MovimentacaoEntradaView.TxtNumeroNota.Text = modelMovimentacao.NumeroNota;
                this.MovimentacaoEntradaView.TxtDescricao.Text = modelMovimentacao.Descricao;

                this.MovimentacaoEntradaView.ValorTotal.Text = modelMovimentacao.ValorTotal.ToString("C2");


                this.MovimentacaoEntradaView.CbmFornecedor.SelectedValue = modelMovimentacao.IdParceiro;

                EStatusMovimento status = EStatusMovimento.E | EStatusMovimento.P | EStatusMovimento.M;
                EOperacaoMovimento eOperacao = EOperacaoMovimento.Entrada;

                MovimentacaoEntradaView.DgvMercadorias.DataSource = repositorio.ListaMercadoriasNotas(modelMovimentacao.Id, eOperacao, status);


                Color cor = Color.Orange;
                string textoProc = "Memória";

                switch (modelMovimentacao.Status)
                {
                    case EStatusMovimento.E:
                        cor = Color.Red;
                        textoProc = "Estornada";
                        break;
                    case EStatusMovimento.P:
                        cor = Color.Green;
                        textoProc = "Processada";
                        break;
                    case EStatusMovimento.M:
                    default:
                        cor = Color.Orange;
                        textoProc = "Memória";
                        break;
                }

                this.MovimentacaoEntradaView.Status.Text = textoProc;
                this.MovimentacaoEntradaView.Status.ForeColor = cor;


                CustomizaGridMercadoria();

            }
            else
            {
                this.MovimentacaoEntradaView.TxtId.Text = null;
                this.MovimentacaoEntradaView.TxtDescAcres.Text = null;

                this.MovimentacaoEntradaView.DteData.Value = DateTime.Now;
                this.MovimentacaoEntradaView.TxtNumeroNota.Text = null;
                this.MovimentacaoEntradaView.TxtDescricao.Text = null; ;

                this.MovimentacaoEntradaView.CbmFornecedor.SelectedIndex = 0;

                this.MovimentacaoEntradaView.DgvMercadorias.DataSource = null;

                this.MovimentacaoEntradaView.Status.Text = "Memória";
                this.MovimentacaoEntradaView.Status.ForeColor = Color.Orange;
            }
        }

        private ModelMovimentacao TelaParaObjeto(EStatusMovimento status)
        {
            string valorString = this.MovimentacaoEntradaView.ValorTotal.Text.Replace("R$ ", "");//.Replace(",", ".");

            long.TryParse(this.MovimentacaoEntradaView.TxtId.Text, out long id);
            long.TryParse(this.MovimentacaoEntradaView.TxtDescAcres.Text, out long descAcres);
            decimal.TryParse(valorString, out decimal valorTotal);

            DateTime data = this.MovimentacaoEntradaView.DteData.Value;
            string numero = this.MovimentacaoEntradaView.TxtNumeroNota.Text;
            string descricao = this.MovimentacaoEntradaView.TxtDescricao.Text;

            ModelCliente fornec = MovimentacaoEntradaView.CbmFornecedor.SelectedItem as ModelCliente;

            return new ModelMovimentacao
            {
                Descricao = descricao,
                Id = id,
                Data = data,
                DescAcres = descAcres,
                IdParceiro = fornec.Id,
                Operacao = EOperacaoMovimento.Entrada,
                Status = status,
                ValorTotal = valorTotal,
                NumeroNota = numero
            };
        }


    }
}

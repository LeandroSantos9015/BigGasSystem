using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp6.Enumeradores;
using WindowsFormsApp6.Interface.Movimentacao;
using WindowsFormsApp6.Menus.Movimentacao;
using WindowsFormsApp6.Modelos;
using WindowsFormsApp6.Modelos.Movimentacao;
using WindowsFormsApp6.Relatorio.Impressao;
using WindowsFormsApp6.Repositorios.Movimentacao;
using WindowsFormsApp6.Repositorios.Utilitarios;

namespace WindowsFormsApp6.Controles.Movimentacao
{
    public class CtrlCancelamentoSaida
    {
        IManutencaoSaida CancelamentoSaidaView { get; set; }

        RepositorioMovimentacao repositorio = new RepositorioMovimentacao();

        public CtrlCancelamentoSaida(IPrincipalView pai)
        {
            CancelamentoSaidaView = new FrmManutencaoSaida();

            CancelamentoSaidaView.CancelamentoView.MdiParent = pai.PrincipalView;

            CancelamentoSaidaView.CancelamentoView.StartPosition = FormStartPosition.CenterScreen;

            CancelamentoSaidaView.CancelamentoView.Show();

            DelegarEventos();

        }

        private void DelegarEventos()
        {
            this.CancelamentoSaidaView.BtnBuscar.Click += BtnBuscar_Click;
            this.CancelamentoSaidaView.BtnExecutar.Click += BtnExecutar_Click;

            this.CancelamentoSaidaView.BtnReimprimir.Click += BtnReimprimir_Click; ;

        }

        private void BtnExecutar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.CancelamentoSaidaView.DgvLista.RowCount < 1)
                    return;

                if (this.CancelamentoSaidaView.DgvLista.CurrentRow is null)
                    this.CancelamentoSaidaView.DgvLista.Focus();


                var result = MessageBox.Show(this.CancelamentoSaidaView.CancelamentoView, "Deseja realmente cancelar?", "Confirmação", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    ModelMovimentacaoPeriodo nota = this.CancelamentoSaidaView.DgvLista.CurrentRow.DataBoundItem as ModelMovimentacaoPeriodo;
                    repositorio.CancelarNotaDeSaida(nota.Id);

                    MessageBox.Show("Cancelamento da venda realizado com sucesso");

                    RealizarBusca();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao tentar realizar cancelamento\n\n" + ex.Message);
            }
        }

        private void BtnReimprimir_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.CancelamentoSaidaView.DgvLista.RowCount < 1)
                    return;

                if (this.CancelamentoSaidaView.DgvLista.CurrentRow is null)
                    this.CancelamentoSaidaView.DgvLista.Focus();

                ModelMovimentacaoPeriodo nota = this.CancelamentoSaidaView.DgvLista.CurrentRow.DataBoundItem as ModelMovimentacaoPeriodo;

                var reimpressao = repositorio.Reimpressao(nota.Id);

                var mov = reimpressao.FirstOrDefault();

                IList<ModelItemMovimentacao> itens = new List<ModelItemMovimentacao>();

                var movimentacao = new ModelMovimentacao
                {
                    Data = mov.Data,
                    DescAcres = mov.DescAcres,
                    ValorTotal = mov.ValorLiquidoTotal,
                    NumeroNota = mov.Id.ToString(),

                };

                foreach (var item in reimpressao)
                {
                    itens.Add(new ModelItemMovimentacao
                    {
                        Descricao = item.Descricao,
                        Id = 1,
                        IdDocumento = 1,
                        IdMercadoria = 1,
                        Operacao = EOperacaoMovimento.Saida,
                        PrecoCusto = 1,
                        PrecoVenda = item.PrecoVenda,
                        Quantidade = item.Quantidade,
                        ValorTotal = item.ValorTotal

                    });
                }

                ModelCliente cliente = new ModelCliente
                {
                    Bairro = mov.Bairro,
                    Cidade = mov.Cidade,
                    Complemento = mov.Complemento,
                    Cpf = "",
                    Endereco = mov.Endereco,
                    Nome = mov.Nome,
                    Numero = mov.Numero,
                    Telefone = mov.Telefone
                };



                string porta = new RepositorioConfiguracao().Listar().PortaImpressora;

                new CtrlImpressaoReport(cliente, itens, mov.Id, mov.ValorLiquidoTotal, porta, mov.Finalizadora.ToUpper(), true);

                MessageBox.Show("Reimpressão de venda realizado com sucesso");

                RealizarBusca();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao tentar realizar cancelamento\n\n" + ex.Message);
            }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            RealizarBusca();
        }

        private void RealizarBusca()
        {
            DateTime inicio = this.CancelamentoSaidaView.DteInicio.Value;
            DateTime fim = this.CancelamentoSaidaView.DteFim.Value;

            CarregarNotas(inicio, fim);

        }

        private void CarregarNotas(DateTime inicio, DateTime final)
        {
            string ini = inicio.ToString("yyyy-MM-ddT00:00:00");
            string fim = final.ToString("yyyy-MM-ddT23:59:59");

            var notas = repositorio.ListarNotasPorPeriodo(EOperacaoMovimento.Saida, EStatusMovimento.P, ini, fim);

            CancelamentoSaidaView.DgvLista.DataSource = notas.ToList();

            this.CancelamentoSaidaView.DgvLista.Columns["Id"].Width = 70;
            this.CancelamentoSaidaView.DgvLista.Columns["Nome"].Width = 150;
            this.CancelamentoSaidaView.DgvLista.Columns["ValorLiquidoTotal"].Width = 80;
        }
    }
}

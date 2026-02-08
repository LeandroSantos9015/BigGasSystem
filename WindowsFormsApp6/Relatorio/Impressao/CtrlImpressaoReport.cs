using BigJetGas.Controles.Movimentacao;
using DevExpress.XtraReports.UI;
using Relatorios.Impressao;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp6.Controles.Cadastros;
using WindowsFormsApp6.Modelos;
using WindowsFormsApp6.Modelos.Movimentacao;
using WindowsFormsApp6.Repositorios.Utilitarios;
using WindowsFormsApp6.Utilitarios;

namespace WindowsFormsApp6.Relatorio.Impressao
{
    public class CtrlImpressaoReport
    {
        ImpressaoSaida Relatorio = new ImpressaoSaida();

        IList<ModeloImpressaoReport> Lista = null;

        RegraCliente cli = new RegraCliente();

        RepositorioConfiguracao repCfg = new RepositorioConfiguracao();

        public CtrlImpressaoReport(ModelCliente cliente, IList<ModelItemMovimentacao> mercadorias, Int64 idPedido, decimal totalPedido, string nomeImp, string finalizadora, bool reimp)
        {
            Imprimir(cliente, mercadorias, idPedido.ToString(), totalPedido.ToString("C2"), nomeImp, finalizadora, reimp);

        }

        private void Imprimir(ModelCliente cliente, IList<ModelItemMovimentacao> mercadorias, string idPedido, string totalPedido, string nomeImp, string finalizadora, bool reimp)
        {

            Lista = new List<ModeloImpressaoReport>();

            ModelEmpresa empresa = new ModelEmpresa
            {
                Nome = "BIG JET GAS",
                Bairro = "Centro",
                Cidade = "Assai-PR",
                Endereco = "Rua Brasil, 173 Ao lado da BIG BURGUER",
                Telefone = "(43) 3262-2436"
            };

            IList<ModelItemImpressao> listaModel = new List<ModelItemImpressao>();

            foreach (var item in mercadorias)
            {
                listaModel.Add(
                    new ModelItemImpressao
                    {
                        Descricao = item.Descricao,
                        Id = item.Id,
                        IdDocumento = item.IdDocumento,
                        IdMercadoria = item.IdMercadoria,
                        PrecoCusto = item.PrecoCusto,
                        PrecoVenda = item.PrecoVenda,
                        Quantidade = item.Quantidade,
                        Status = item.Status,
                        ValorTotal = item.ValorTotal
                    });
            }

            ModeloImpressaoReport modelo = new ModeloImpressaoReport
            {
                EmpresaCidade = empresa.Cidade,
                EmpresaEndereco = empresa.Endereco,
                EmpresaNome = empresa.Nome,
                EmpresaTelefone = empresa.Telefone,
                ClienteBairro = cliente.Bairro,
                ClienteCidade = Cidade(cliente.Cidade).Nome,
                ClienteComplemento = cliente.Complemento,
                ClienteCondicaoPagamento = finalizadora,
                ClienteTelefone = cliente.Telefone,
                ClienteEndereco = cliente.Endereco,
                ClienteNumero = cliente.Numero,
                ClienteNome = cliente.Nome,
                ClienteVencimento = "30 dias",
                Hora = reimp ? "REIMPRESSÃO" : DateTime.Now.ToString("HH:mm"),
                Lista = listaModel,
                NumeroPedido = idPedido,
                TotalPedido = totalPedido,
                Finalizadora = finalizadora
            };

            Lista.Add(modelo);


            this.Relatorio.DataSource = Lista;
            
            // ⭐ IMPORTANTE: Aplica fonte DEPOIS de definir DataSource
            this.Relatorio.AplicarFonteConfigurada();

            this.Relatorio.ShowPrintMarginsWarning = false;


            if (repCfg.Listar().PerguntarImpressora)
            {
                var selecao = new CtrlSelecionarImpressora();

                if (selecao.SelecaoView.FrmSelecao.DialogResult.Equals(DialogResult.OK))
                {
                    this.Relatorio.Landscape = selecao.Impressao.Paisagem;
                    this.Relatorio.PaperKind = selecao.Impressao.PapelA5 ? PaperKind.A5 : PaperKind.Letter;
                    this.Relatorio.PrinterName = selecao.Impressao.Impressora;

                    this.Relatorio.Margins.Right = selecao.Impressao.MargemDireita;
                    this.Relatorio.Margins.Left = selecao.Impressao.MargemEsquerda;

                    try
                    {
                        this.Relatorio.Print();
                        MessageBox.Show("Reimpressão de venda realizado com sucesso");
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Ocorreu um erro ao tentar imprimir\n\n\n" + e.ToString());
                    }
                }

            }
            else
            {
                this.Relatorio.PrinterName = nomeImp;

                // Relatorio.ShowPreview();
                try
                {
                    this.Relatorio.Print();
                    MessageBox.Show("Reimpressão de venda realizado com sucesso");
                }
                catch (Exception e)
                {
                    MessageBox.Show("Ocorreu um erro ao tentar imprimir\n\n\n" + e.ToString());
                }
            }



        }

        private IList<string> ListaImpressoras()
        {
            IList<string> impressoras = new List<string>();

            String pkInstalledPrinters;
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                pkInstalledPrinters = PrinterSettings.InstalledPrinters[i];
                impressoras.Add(pkInstalledPrinters);
            }

            return impressoras;
        }


        private ModeloCidade Cidade(Int64 id)
        {
            return cli.ListarCidades().Where(x => x.Id == id).FirstOrDefault();
        }


    }
}

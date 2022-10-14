using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp6.Controles.Impressao;
using WindowsFormsApp6.Interface.Utilitarios;
using WindowsFormsApp6.Menus.Utilitarios;
using WindowsFormsApp6.Modelos;
using WindowsFormsApp6.Repositorios.Utilitarios;
using WindowsFormsApp6.Utilitarios;

namespace WindowsFormsApp6.Controles.Utilitarios
{
    public class CtrlConfiguracao
    {
        public IConfiguracao ConfiguracaoView { get; set; }

        private RepositorioConfiguracao repositorio = new RepositorioConfiguracao();

        public CtrlConfiguracao(IPrincipalView Pai)
        {

            this.ConfiguracaoView = new FrmConfiguracoes();

            this.ConfiguracaoView.ConfiguracaoView.MdiParent = Pai.PrincipalView;

            DelegarEventos();

            //CarregarDadosTela();

            this.ConfiguracaoView.ConfiguracaoView.Show();

            
        }

        private void CarregarDadosTela()
        {
            ModelConfiguracao conf = repositorio.Listar();

            ObjetoParaTela(conf);

        }

        private void DelegarEventos()
        {
            ConfiguracaoView.BtnCancelar.Click += BtnCancelar_Click;
            ConfiguracaoView.BtnSalvar.Click += BtnSalvar_Click;
            ConfiguracaoView.BtnTesteImpressao.Click += BtnTesteImpressao_Click;
        }

        private void BtnTesteImpressao_Click(object sender, EventArgs e)
        {
            TesteDeImpressao();

        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                ModelConfiguracao cfg = TelaParaObjeto();

                repositorio.Salvar(cfg);

                MessageBox.Show("Configurações salves com sucesso");

                this.ConfiguracaoView.ConfiguracaoView.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ocorreu um problema ao tentar salvar\n" + ex.Message);
            }

        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            CarregarDadosTela();
        }

        private ModelConfiguracao TelaParaObjeto()
        {
            decimal.TryParse(this.ConfiguracaoView.TxtValorFrete.Text, out decimal valorFrete);

            return new ModelConfiguracao
            {
                PortaImpressora = this.ConfiguracaoView.TxtPortaImpressora.Text,
                ValorFrete = valorFrete
            };
        }

        private void ObjetoParaTela(ModelConfiguracao cfg)
        {
            if (cfg != null)
            {
                this.ConfiguracaoView.TxtValorFrete.Text = cfg.ValorFrete.ToString();
                this.ConfiguracaoView.TxtPortaImpressora.Text = cfg.PortaImpressora;
            }
            else
            {
                this.ConfiguracaoView.TxtValorFrete.Text = null;
                this.ConfiguracaoView.TxtPortaImpressora.Text = null;
            }

        }

        private void TesteDeImpressao()
        {
            ModelCliente cliente = new ModelCliente
            {
                Bairro = "Centro",
                Cidade = 1,//"Assai",
                Complemento = "Complemento",
                Cpf = "00000000000".CpfCnpjMascara(),
                Endereco = "Avenida Rio de Janeiro",
                Nome = "Paulo da Silva Sauro",
                Numero = "123456",
                Telefone = "1234567890".TelefoneMascara()
            };

            ModelMercadoria mercadoria1 = new ModelMercadoria
            {
                Descricao = "Mercadoria 1",
                PrecoVenda = 10.25M,
                Quantidade = 5
            };
            ModelMercadoria mercadoria2 = new ModelMercadoria
            {
                Descricao = "Mercadoria 2",
                PrecoVenda = 123.45M,
                Quantidade = 2
            };
            ModelMercadoria mercadoria3 = new ModelMercadoria
            {
                Descricao = "Mercadoria 3",
                PrecoVenda = 34.54M,
                Quantidade = 3
            };

            IList<ModelMercadoria> lista = new List<ModelMercadoria>();

            lista.Add(mercadoria1);
            lista.Add(mercadoria2);
            lista.Add(mercadoria3);

            int idPedido = 99999999;

            decimal total = lista.Sum(x => x.PrecoVenda * x.Quantidade);

            string porta = this.ConfiguracaoView.TxtPortaImpressora.Text;

            var testes = new CtrlImpressao(cliente, lista, idPedido, total, porta);

        }
    }
}
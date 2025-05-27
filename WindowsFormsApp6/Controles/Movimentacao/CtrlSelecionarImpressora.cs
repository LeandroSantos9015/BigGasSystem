using BigJetGas.Interface.Movimentacao;
using BigJetGas.Menus.Movimentacao;
using BigJetGas.Modelos.Movimentacao;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using WindowsFormsApp6.Modelos;
using WindowsFormsApp6.Repositorios.Utilitarios;

namespace BigJetGas.Controles.Movimentacao
{
    public class CtrlSelecionarImpressora
    {
        public ISelecionarImpressora SelecaoView { get; set; }

        public ModelSelecaoImpressao Impressao;
        private RepositorioConfiguracao repCfg = new RepositorioConfiguracao();
        private string arquivo = Environment.CurrentDirectory + "//cfgSelecaoImpressao.json";
        private int MargemEsquerda = 0;
        private int MargemDireita = 35;
        public CtrlSelecionarImpressora()
        {
            SelecaoView = new FrmPerguntaImpressao();

            DelegarEventos();

            CriarArquivoImpressao();

            ListaImpressoras();

            CarregaConfiguracao();

            SelecaoView.FrmSelecao.ShowDialog();



        }

        private void CarregaConfiguracao()
        {
            var obj = JsonConvert.DeserializeObject<ModelSelecaoImpressao>(File.ReadAllText(arquivo));

            this.SelecaoView.ChkPapelA5.Checked = obj.PapelA5;
            this.SelecaoView.ChkPaisagem.Checked = obj.Paisagem;

            MargemEsquerda = obj.MargemEsquerda;
            MargemDireita = obj.MargemDireita;

            //this.SelecaoView.GrdImpressoras.ClearSelection();

            var cont = 0;
            foreach (DataGridViewRow row in this.SelecaoView.GrdImpressoras.Rows)
            {
                if (row.Cells[0].Value.Equals(obj.Impressora))
                {
                    this.SelecaoView.GrdImpressoras.Rows[cont].Cells[0].Selected = true;

                }
                cont++;
            }
        }

        private void CriarArquivoImpressao()
        {
            var existe = File.Exists(arquivo);

            if (!existe)
                File.WriteAllText(arquivo, JsonConvert.SerializeObject(
                    new ModelSelecaoImpressao
                    {
                        Impressora = "LPT",
                        Paisagem = false,
                        PapelA5 = false,
                        MargemDireita = 0,
                        MargemEsquerda = 35
                    }, Formatting.Indented));
        }

        private void ListaImpressoras()
        {
            IList<ModelImpressora> impressoras = new List<ModelImpressora>();

            String pkInstalledPrinters;
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                pkInstalledPrinters = PrinterSettings.InstalledPrinters[i];
                impressoras.Add(new ModelImpressora { Nome = pkInstalledPrinters });
            }

            this.SelecaoView.GrdImpressoras.DataSource = impressoras;
            this.SelecaoView.GrdImpressoras.Columns[0].Width = 195;

        }

        private void DelegarEventos()
        {

            this.SelecaoView.BtnImprimir.Click += BtnImprimir_Click;
            this.SelecaoView.BtnMemorizar.Click += BtnMemorizar_Click;

        }

        private void ObjetoParaTela()
        {
            var json = File.ReadAllText(arquivo);

            JsonConvert.DeserializeObject(json);
        }

        private void BtnMemorizar_Click(object sender, EventArgs e)
        {
            var objet = this.SelecaoView.GrdImpressoras.CurrentRow.DataBoundItem as ModelImpressora;

            File.WriteAllText(arquivo, JsonConvert.SerializeObject(
                   new ModelSelecaoImpressao
                   {
                       Impressora = objet.Nome,
                       Paisagem = this.SelecaoView.ChkPaisagem.Checked,
                       PapelA5 = this.SelecaoView.ChkPapelA5.Checked,
                       MargemDireita = this.MargemDireita,
                       MargemEsquerda = this.MargemEsquerda
                   }, Formatting.Indented));

            MessageBox.Show("Configuração salva com sucesso!");
        }

        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            if (this.SelecaoView.GrdImpressoras.CurrentRow != null)
            {
                var obj = this.SelecaoView.GrdImpressoras.CurrentRow.DataBoundItem as ModelImpressora;

                Impressao = new ModelSelecaoImpressao
                {
                    Impressora = obj.Nome,
                    Paisagem = this.SelecaoView.ChkPaisagem.Checked,
                    PapelA5 = this.SelecaoView.ChkPapelA5.Checked,
                    MargemEsquerda = this.MargemEsquerda,
                    MargemDireita = this.MargemDireita
                };
                this.SelecaoView.FrmSelecao.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }
    }
}
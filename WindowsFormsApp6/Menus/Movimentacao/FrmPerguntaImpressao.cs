using BigJetGas.Interface.Movimentacao;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BigJetGas.Menus.Movimentacao
{
    public partial class FrmPerguntaImpressao : Form, ISelecionarImpressora
    {
        public FrmPerguntaImpressao() { InitializeComponent(); }

        public Form FrmSelecao => this;
        public DataGridView GrdImpressoras => dataGridView1;
        public CheckBox ChkPaisagem => chkPaisagem;
        public CheckBox ChkPapelA5 => chkPapelA5;
        public Button BtnImprimir => btnImprimir;
        public Button BtnMemorizar => btnMemo;


    }
}

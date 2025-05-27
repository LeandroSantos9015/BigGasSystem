using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BigJetGas.Interface.Movimentacao
{
    public interface ISelecionarImpressora
    {
        Form FrmSelecao { get; }
        DataGridView GrdImpressoras { get; }
        CheckBox ChkPaisagem { get; }
        CheckBox ChkPapelA5 { get; }
        Button BtnImprimir { get; }
        Button BtnMemorizar { get; }
    }
}
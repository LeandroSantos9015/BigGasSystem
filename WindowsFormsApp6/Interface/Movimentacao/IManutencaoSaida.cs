using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp6.Interface.Movimentacao
{
    public interface IManutencaoSaida
    {
        Form CancelamentoView { get; }

        Button BtnBuscar { get; }

        Button BtnExecutar { get; }

        Button BtnReimprimir { get; }
        DateTimePicker DteInicio { get; }

        DateTimePicker DteFim { get; }

        DataGridView DgvLista { get; }


    }
}

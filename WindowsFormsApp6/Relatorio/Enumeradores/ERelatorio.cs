using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relatorios.Enumeradores
{
    /// <summary>
    /// Atenção 
    /// O numero do enumerador DEVE ser o mesmo número do relatório
    /// </summary>
    public enum ERelatorio
    {
        [Description("Relatório 01 - Lista de Parceiros (Clientes e Fornecedores)"), Category("Cadastro")]
        ListaClientes01 = 1,

        //[Description("02 - Venda Por Finalizadora Por Turno"), Category("Venda")]
        //ERelatorio02VendaPorFinalizadoraPorTurno = 2,

        //[Description("03 - Venda de Mercadoria por período"), Category("Venda")]
        //ERelatorio03VendaDeMercadorias = 3

        [Description("Relatório 03 - Notas de Entrada por período"), Category("Entrada")]
        NotaEntadaPeriodo03 = 3


    }
}

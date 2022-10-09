using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp6.Modelos.Movimentacao
{
    public class ModelItemMovimentacao
    {
        public Int64 Id { get; set; }
        public Int64 IdDocumento { get; set; }
        public string Descricao { get; set; }
        public decimal Quantidade { get; set; }
        public decimal PrecoCusto { get; set; }
        public decimal PrecoVenda { get; set; }
        public Int64 DescAcres { get; set; }
        public decimal ValorTotal { get; set; }
        public byte Operacao { get; set; }
    }
}
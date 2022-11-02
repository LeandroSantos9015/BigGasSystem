using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp6.Enumeradores;
using WindowsFormsApp6.Interface;

namespace WindowsFormsApp6.Modelos.Movimentacao
{
    public class ModelReimpressao
    {
        public Int64 Id { get; set; }
        public string Nome { get; set; }
        public string Bairro { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public int Cidade { get; set; }
        public string Telefone { get; set; }
        public string Finalizadora { get; set; }
        public string Descricao { get; set; }
        public Decimal PrecoVenda { get; set; }

        public int Quantidade { get; set; }
        public Decimal ValorTotal { get; set; }

        public Decimal DescAcres { get; set; }

        public Decimal ValorLiquidoTotal { get; set; }

        
        public DateTime Data { get; set; }





    }
}

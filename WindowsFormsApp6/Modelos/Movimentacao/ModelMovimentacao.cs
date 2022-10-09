using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp6.Modelos.Movimentacao
{
    public class ModelMovimentacao
    {
        public Int64 Id { get; set; }
        public string Descricao { get; set; }
        public Int64 IdMercadoria { get; set; }
        public Int64 IdParceiro { get; set; }
        public DateTime Data { get; set; }
        public byte Status { get; set; }
        public byte Operacao { get; set; }
        public Int64 DescAcres { get; set; }
        public decimal ValorTotal { get; set; }


        [Browsable(false)]
        public string Consulta => Descricao;

        [Browsable(false)]
        public ModelMovimentacaoDapper Salvar => new ModelMovimentacaoDapper(this);
    }

    public class ModelMovimentacaoDapper : DynamicParameters
    {
        //todo desafio --> deixar dinamico 
        public ModelMovimentacaoDapper(ModelMovimentacao cli)
        {
            base.Add("@Id", cli.Id);
            base.Add("@Descricao", cli.Descricao);
            base.Add("@Data", cli.Data);
            base.Add("@DescAcres", cli.DescAcres);
            base.Add("@IdMercadoria", cli.IdMercadoria);
            base.Add("@IdParceiro", cli.IdParceiro);
            base.Add("@Status", cli.Status);
            base.Add("@Operacao", cli.Operacao);
            base.Add("@ValorTotal", cli.ValorTotal);
            base.Add("@Return", dbType: DbType.Int64, direction: ParameterDirection.ReturnValue);
        }
    }
}

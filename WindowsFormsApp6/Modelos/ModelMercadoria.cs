using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp6.Interface;

namespace WindowsFormsApp6.Modelos
{
    public class ModelMercadoria : IModeloGenerico
    {
        public long Id { get; set; }

        [DisplayName("Descrição")]
        public string Descricao { get; set; }

        [DisplayName("Venda")]
        public decimal PrecoVenda { get; set; }

        [DisplayName("Custo")]
        public decimal PrecoCusto { get; set; }

        [DisplayName("Qtd")]
        public decimal Quantidade { get; set; }

        [Browsable(false)]
        public string Consulta => Descricao;

        [Browsable(false)]
        public ModelMercadoriaDapper Salvar => new ModelMercadoriaDapper(this);

    }

    public class ModelMercadoriaDapper : DynamicParameters
    {
        public ModelMercadoriaDapper(ModelMercadoria cli)
        {
            base.Add("@Id", cli.Id);
            base.Add("@Descricao", cli.Descricao);
            base.Add("@PrecoCusto", cli.PrecoCusto);
            base.Add("@PrecoVenda", cli.PrecoVenda);
            base.Add("@Quantidade", cli.Quantidade);
            base.Add("@Return", dbType: DbType.Int64, direction: ParameterDirection.ReturnValue);
        }
    }

    public class ModelMercadoriaEntrada : ModelMercadoria
    {
        public decimal Total => base.PrecoVenda * base.Quantidade;
    }
}
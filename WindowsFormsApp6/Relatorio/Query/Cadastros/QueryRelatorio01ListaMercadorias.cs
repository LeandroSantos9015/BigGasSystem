using Relatorios.ModeloRelatorio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Relatorios.Query.Cadastros
{
    public class QueryRelatorio01ListaMercadorias
    {
        // Banco banco;

        public QueryRelatorio01ListaMercadorias()
        {
            //   banco = new Banco();


        }

        public IList<ModeloRelatorio01ListaMercadorias> QueryRelatorio01ListaTodasMercadoriasDapper(object[] parametros)
        {
            //int status = (int)parametros[0];

            //string query = String.Format(@"Select * from FN_CAD_Mercadoria({0}, 1)", status);

            //IList<ModeloRelatorio01ListaMercadorias> lista = OperacoesDapper.Consulta<ModeloRelatorio01ListaMercadorias>(query).ToList();


            //return lista;
            return null;

        }

        public IList<ModeloRelatorio01ListaMercadorias> QueryRelatorio01ListaTodasMercadorias()
        {

            //int status = 4;

            //banco.AbrirConexao();

            //string sql = String.Format(@"Select * from FN_CAD_Mercadoria({0}, 1)", status);

            //DataTable table = banco.ExecuteQuery(sql);

            //return table.AsEnumerable().Select(row =>
            //        new ModeloRelatorio01ListaMercadorias
            //        {
            //            //Codigo = row["CodigoBarra"].ToString(),
            //            //Situacao = Convert.ToString(row["Ativo"].ToString()),
            //            //Un = EnumPelaDescricao.ObterDescricao((EUnidade)Convert.ToInt64(row["Unidade"].ToString())),
            //            //Desc = row["Descricao"].ToString(),
            //            //Estoque = Convert.ToDecimal(row["QtdEstoque"].ToString()),
            //            //Unitario = Convert.ToDecimal(row["PrecoVenda"].ToString())
            //            //= (EUnidade)Convert.ToInt64(row["Unidade"].ToString())
            //        }).ToList();
            return null;
        }

    }
}

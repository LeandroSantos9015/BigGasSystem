using Dapper;
using Relatorios.ModeloRelatorio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using WindowsFormsApp6.Repositorios;

namespace Relatorios.Query.Venda
{
    public class QueryRelatorio02VendaPorFinalizadoraSinteticoPorTurno : RepositorioBase
    {

        SqlConnection Conexao;
        public QueryRelatorio02VendaPorFinalizadoraSinteticoPorTurno() : base()
        {

            Conexao = base.Connection;
        }


        public IList<ModeloRelatorio02VendaPorFinalizadoraSinteticoPorTurno> QueryRelatorioDapper(string inicio, string fim)
        {
            string query = String.Format(@"Select * from FN_Relatorio02_VendaPorFinalizadoraSinteticoPorTurno('{0}', '{1}')", inicio, fim);

            var p = new DynamicParameters();

            IList<ModeloRelatorio02VendaPorFinalizadoraSinteticoPorTurno> lista = null;
            
            Conexao.Execute("SalvarMovimentacao", p, commandType: CommandType.StoredProcedure);
            //.Consulta<ModeloRelatorio02VendaPorFinalizadoraSinteticoPorTurno>(query).ToList();


            return lista;

        }


        public IList<ModeloRelatorio02VendaPorFinalizadoraSinteticoPorTurno> QueryRelatorio()
        {

            // var consulta = Conexao.Query<ModelMovimentacao>($"SELECT * FROM ConsultaNotas({(byte)operacao},{(byte)status})").ToList();

            //banco.AbrirConexao();

            //string sql = String.Format(@"Select * from FN_Relatorio02_VendaPorFinalizadoraSinteticoPorTurno('2019-12-01', '2019-12-11')");

            //DataTable table = banco.ExecuteQuery(sql);

            //return table.AsEnumerable().Select(row =>
            //        new ModeloRelatorio02VendaPorFinalizadoraSinteticoPorTurno
            //        {
            //            Entrada = Convert.ToDateTime(row["Entrada"].ToString()),
            //            Saida = Convert.ToDateTime(row["Saida"].ToString()),
            //            Finalizadora = Convert.ToInt64(row["Finalizadora"].ToString()),
            //            IdTurno = Convert.ToInt64(row["IdTurno"].ToString()),
            //            Total = Convert.ToDecimal(row["Total"].ToString()),
            //        }).ToList();

            return null;
        }
    }
}

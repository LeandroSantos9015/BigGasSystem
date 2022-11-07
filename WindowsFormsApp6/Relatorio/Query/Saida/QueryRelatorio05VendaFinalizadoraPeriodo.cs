using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using WindowsFormsApp6.Relatorio.ModeloRelatorio;
using WindowsFormsApp6.Repositorios;

namespace Relatorios.Query.Entrada
{
    public class QueryRelatorio05VendaFinalizadoraPeriodo : RepositorioBase
    {
        SqlConnection Connection;

        public QueryRelatorio05VendaFinalizadoraPeriodo() : base()
        {
            Connection = base.Connection;
        }

        public IList<Relatorio05_VendaFinalizadoraPeriodo> QueryRelatorio(object[] parametros)
        {
            DateTime inicio = (DateTime)parametros[0];
            DateTime final = (DateTime)parametros[1];

            string ini = inicio.ToString("yyyy-MM-ddT00:00:00");
            string fim = final.ToString("yyyy-MM-ddT23:59:59");

            string query = $"select * from [Relatorio05_FinalizadoraPorPeriodo]('{ini}','{fim}')";

            IList<Relatorio05_VendaFinalizadoraPeriodo> retorno = Connection.Query<Relatorio05_VendaFinalizadoraPeriodo>(query).ToList().OrderBy(x=> x.Data).ToList();

            return retorno;

        }
    }
}
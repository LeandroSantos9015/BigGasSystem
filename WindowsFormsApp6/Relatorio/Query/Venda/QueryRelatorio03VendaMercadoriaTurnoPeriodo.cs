using Relatorios.ModeloRelatorio;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Relatorios.Query.Venda
{
    public class QueryRelatorio03VendaMercadoriaTurnoPeriodo
    {
        //Banco banco;

        public QueryRelatorio03VendaMercadoriaTurnoPeriodo()
        {
          //  banco = new Banco();
        }

        public IList<ModeloRelatorio03VendaMercadoriaTurnoPeriodo> QueryRelatorioDapper(string inicio, string fim, bool agruparTurno)
        {
            //string query = String.Format(@"Select * from FN_Relatorio03_VendaMercadoria('{0}', '{1}', {2})", inicio, fim, Convert.ToInt32(agruparTurno));

            //IList<ModeloRelatorio03VendaMercadoriaTurnoPeriodo> lista = OperacoesDapper.Consulta<ModeloRelatorio03VendaMercadoriaTurnoPeriodo>(query).ToList();


            //return lista;
            return null;

        }
       
    }
}

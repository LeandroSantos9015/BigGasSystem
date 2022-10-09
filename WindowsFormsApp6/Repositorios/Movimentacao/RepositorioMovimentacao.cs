using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp6.Modelos.Movimentacao;

namespace WindowsFormsApp6.Repositorios.Movimentacao
{
    public class RepositorioMovimentacao : RepositorioBase
    {
        SqlConnection Conexao;

        public RepositorioMovimentacao() : base()
        {
            Conexao = base.Connection;
        }


        public void Salvar(ModelMovimentacao cli)
        {
            try
            {
                var p = cli.Salvar;
                Conexao.Execute("SalvarCliente", p, commandType: CommandType.StoredProcedure);
                var b = p.Get<object>("@Return");
                b.ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IList<ModelMovimentacao> Listar()
        {
            var consulta = Conexao.Query<ModelMovimentacao>("SELECT * FROM ConsultarCliente()").ToList();

            return consulta;
        }



    }
}

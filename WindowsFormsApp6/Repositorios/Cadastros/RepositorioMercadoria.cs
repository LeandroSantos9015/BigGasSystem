using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp6.Modelos;

namespace WindowsFormsApp6.Repositorios.Cliente
{
    public class RepositorioMercadoria : RepositorioBase
    {
        SqlConnection Conexao;

        public RepositorioMercadoria() : base()
        {
            Conexao = base.Connection;
        }

        public void Salvar(ModelMercadoria cli)
        {
            try
            {
                var p = cli.Save;
                Conexao.Execute("SalvarMercadoria", p, commandType: CommandType.StoredProcedure);
                var b = p.Get<object>("@Return");
                b.ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IList<ModelMercadoria> Listar()
        {
            var consulta = Conexao.Query<ModelMercadoria>("SELECT * FROM ConsultarMercadoria()").ToList();

            return consulta;
        }

        public IList<ModelMercadoriaEntrada> ListarEntrada()
        {
            var consulta = Conexao.Query<ModelMercadoriaEntrada>("SELECT * FROM ConsultarMercadoria()").ToList();

            return consulta;
        }
    }
}
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WindowsFormsApp6.Modelos.Seguranca;

namespace WindowsFormsApp6.Repositorios.Seguranca
{
    public class RepositorioUsuario : RepositorioBase
    {
        public RepositorioUsuario() : base()
        {
            EnsureConnectionOpen();
        }

        public ModelUsuario Autenticar(string login, string senha)
        {
            try
            {
                EnsureConnectionOpen();
                var parametros = new DynamicParameters();
                parametros.Add("@Login", login, DbType.String);
                parametros.Add("@Senha", senha, DbType.String);

                var usuario = Connection.Query<ModelUsuario>(
                    "AutenticarUsuario",
                    parametros,
                    commandType: CommandType.StoredProcedure
                ).FirstOrDefault();

                return usuario;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao autenticar usuário: {ex.Message}", ex);
            }
        }

        public void Salvar(ModelUsuario usuario)
        {
            try
            {
                EnsureConnectionOpen();
                var p = usuario.Save;
                Connection.Execute("SalvarUsuario", p, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao salvar usuário: {ex.Message}", ex);
            }
        }

        public IList<ModelUsuario> Listar()
        {
            try
            {
                EnsureConnectionOpen();
                var consulta = Connection.Query<ModelUsuario>("SELECT * FROM ConsultarUsuarios()").ToList();
                return consulta;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar usuários: {ex.Message}", ex);
            }
        }
    }
}

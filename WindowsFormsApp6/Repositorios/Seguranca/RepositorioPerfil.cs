using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WindowsFormsApp6.Modelos.Seguranca;

namespace WindowsFormsApp6.Repositorios.Seguranca
{
    public class RepositorioPerfil : RepositorioBase
    {
        public RepositorioPerfil() : base()
        {
            EnsureConnectionOpen();
        }

        public void Salvar(ModelPerfil perfil)
        {
            try
            {
                EnsureConnectionOpen();
                var p = perfil.Save;
                Connection.Execute("SalvarPerfil", p, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao salvar perfil: {ex.Message}", ex);
            }
        }

        public IList<ModelPerfil> Listar()
        {
            try
            {
                EnsureConnectionOpen();
                var consulta = Connection.Query<ModelPerfil>("SELECT * FROM ConsultarPerfis()").ToList();
                return consulta;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar perfis: {ex.Message}", ex);
            }
        }

        public IList<ModelMenu> ListarPermissoes(long idPerfil)
        {
            try
            {
                EnsureConnectionOpen();
                var parametros = new DynamicParameters();
                parametros.Add("@IdPerfil", idPerfil, DbType.Int64);

                var permissoes = Connection.Query<ModelMenu>(
                    "ListarPermissoesPerfil",
                    parametros,
                    commandType: CommandType.StoredProcedure
                ).ToList();

                return permissoes;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar permissões: {ex.Message}", ex);
            }
        }

        public void SalvarPermissao(long idPerfil, int idMenu, bool visualizar)
        {
            try
            {
                EnsureConnectionOpen();
                var parametros = new DynamicParameters();
                parametros.Add("@IdPerfil", idPerfil, DbType.Int64);
                parametros.Add("@IdMenu", idMenu, DbType.Int32);
                parametros.Add("@Visualizar", visualizar, DbType.Boolean);

                Connection.Execute("SalvarPermissaoPerfil", parametros, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao salvar permissão: {ex.Message}", ex);
            }
        }
    }
}

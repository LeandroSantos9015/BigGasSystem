using Dapper;
using System;
using System.ComponentModel;
using System.Data;
using WindowsFormsApp6.Interface;

namespace WindowsFormsApp6.Modelos.Seguranca
{
    public class ModelUsuario : IModeloGenerico
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        
        [Browsable(false)]
        public string Senha { get; set; }
        
        public long IdPerfil { get; set; }
        public string NomePerfil { get; set; }
        public bool Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }
        public DateTime? UltimoAcesso { get; set; }

        [Browsable(false)]
        public string Consulta => Nome + " - " + Login;

        [Browsable(false)]
        public DynamicParameters Save
        {
            get
            {
                DynamicParameters p = new DynamicParameters();
                p.Add("@Id", Id, DbType.Int64);
                p.Add("@Nome", Nome, DbType.String);
                p.Add("@Login", Login, DbType.String);
                p.Add("@Senha", Senha, DbType.String);
                p.Add("@IdPerfil", IdPerfil, DbType.Int64);
                p.Add("@Ativo", Ativo, DbType.Boolean);
                p.Add("@Return", dbType: DbType.Int64, direction: ParameterDirection.Output);
                return p;
            }
        }
    }
}

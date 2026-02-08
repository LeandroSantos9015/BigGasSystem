using Dapper;
using System;
using System.ComponentModel;
using System.Data;
using WindowsFormsApp6.Interface;

namespace WindowsFormsApp6.Modelos.Seguranca
{
    public class ModelPerfil : IModeloGenerico
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }

        [Browsable(false)]
        public string Consulta => Nome;

        [Browsable(false)]
        public DynamicParameters Save
        {
            get
            {
                DynamicParameters p = new DynamicParameters();
                p.Add("@Id", Id, DbType.Int64);
                p.Add("@Nome", Nome, DbType.String);
                p.Add("@Descricao", Descricao, DbType.String);
                p.Add("@Ativo", Ativo, DbType.Boolean);
                p.Add("@Return", dbType: DbType.Int64, direction: ParameterDirection.Output);
                return p;
            }
        }
    }
}

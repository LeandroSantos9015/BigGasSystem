using System;
using System.Data.SqlClient;

namespace WindowsFormsApp6.Modelos
{
    public class ModelConfiguracaoBancoDados
    {
        public string NomeComputador { get; set; }

        public string Instancia { get; set; }

        public string Usuario { get; set; }

        public string Senha { get; set; }

        public string Banco { get; set; }

        public bool Local { get; set; }

        // Gera a connection string de forma segura usando SqlConnectionStringBuilder.
        public string GetConnectionString()
        {
            var builder = new SqlConnectionStringBuilder
            {
                InitialCatalog = Banco,
                MultipleActiveResultSets = true
            };

            if (Local)
            {
                builder.DataSource = string.IsNullOrWhiteSpace(Instancia) || Instancia == "INSTANCIA"
                                     ? "(" + NomeComputador + ")"
                                     : $"({NomeComputador})\\{Instancia}";
                builder.IntegratedSecurity = true;
            }
            else
            {
                builder.DataSource = NomeComputador;
                builder.IntegratedSecurity = false;
                builder.UserID = Usuario;
                builder.Password = Senha;
            }

            return builder.ConnectionString;
        }

        // Valida configuração mínima necessária.
        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(NomeComputador))
            {
                throw new InvalidOperationException("NomeComputador não pode ser vazio na configuração do banco.");
            }

            if (string.IsNullOrWhiteSpace(Banco))
            {
                throw new InvalidOperationException("Banco não pode ser vazio na configuração do banco.");
            }

            if (!Local)
            {
                if (string.IsNullOrWhiteSpace(Usuario) || string.IsNullOrWhiteSpace(Senha))
                {
                    throw new InvalidOperationException("Usuário e senha são obrigatórios quando 'Local' é false.");
                }
            }
        }
    }
}

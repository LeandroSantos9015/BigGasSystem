using Newtonsoft.Json;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using WindowsFormsApp6.Modelos;

namespace WindowsFormsApp6.Repositorios
{
    public class RepositorioBase : IDisposable
    {
        private readonly string _configFilePath;
        private readonly string _connectionString;

        public SqlConnection Connection { get; private set; }

        public RepositorioBase()
        {
            _configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configuracao.json");
            CriarArquivoJsonCasoNaoExista(_configFilePath);

            string json;
            try
            {
                json = File.ReadAllText(_configFilePath, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao ler o arquivo de configuração '{_configFilePath}': {ex.Message}", ex);
            }

            ModelConfiguracaoBancoDados cfg;
            try
            {
                cfg = JsonConvert.DeserializeObject<ModelConfiguracaoBancoDados>(json)
                      ?? throw new InvalidOperationException("Arquivo de configuração inválido ou vazio.");
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException("Erro ao desserializar o arquivo de configuração do banco de dados.", ex);
            }

            // Valida e gera connection string de forma segura
            cfg.Validate();
            _connectionString = cfg.GetConnectionString();

            if (Connection == null) EnsureConnectionOpen();
        }

        private void CriarArquivoJsonCasoNaoExista(string path)
        {
            if (!File.Exists(path))
            {
                var cfg = new ModelConfiguracaoBancoDados()
                {
                    NomeComputador = Environment.MachineName,
                    Instancia = "INSTANCIA",
                    Usuario = "USUARIO",
                    Senha = "SENHA",
                    Banco = "BANCO_DADOS",
                    Local = true
                };

                string json = JsonConvert.SerializeObject(cfg, Formatting.Indented);
                File.WriteAllText(path, json, Encoding.UTF8);
            }
        }

        // Cria uma conexão nova (não aberta).
        protected SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        // Retorna uma SqlConnection já aberta. O chamador é responsável por dar Dispose (usar using).
        public SqlConnection GetOpenConnection()
        {
            var conn = CreateConnection();
            conn.Open();
            return conn;
        }

        // Conveniência para uso interno quando a classe mantém uma Connection de instância.
        protected void EnsureConnectionOpen()
        {
            if (Connection == null)
            {
                Connection = CreateConnection();
            }

            if (Connection.State != System.Data.ConnectionState.Open)
            {
                Connection.Open();
            }
        }

        public void Dispose()
        {
            if (Connection != null)
            {
                try
                {
                    if (Connection.State != System.Data.ConnectionState.Closed)
                    {
                        Connection.Close();
                    }
                }
                finally
                {
                    Connection.Dispose();
                    Connection = null;
                }
            }
        }
    }
}
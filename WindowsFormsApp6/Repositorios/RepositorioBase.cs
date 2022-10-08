using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp6.Modelos;

namespace WindowsFormsApp6.Repositorios
{
    public class RepositorioBase
    {
        public SqlConnection Connection { get; private set; }

        public RepositorioBase()
        {
            string conexao = File.ReadAllText(Environment.CurrentDirectory + "/Configuracao.json");

            ModelConfiguracao cfg = JsonConvert.DeserializeObject<ModelConfiguracao>(conexao);

            Connection = new SqlConnection(cfg.StringConexao);
        }
    }
}
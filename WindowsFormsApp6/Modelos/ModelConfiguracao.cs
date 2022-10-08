using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp6.Modelos
{
    public class ModelConfiguracao
    {
        public string NomeComputador { get; set; }

        public string Usuario { get; set; }

        public string Senha { get; set; }

        public string Banco { get; set; }


        public string StringConexao => $"server={NomeComputador};database={Banco};user={Usuario};password={Senha}";

    }
}

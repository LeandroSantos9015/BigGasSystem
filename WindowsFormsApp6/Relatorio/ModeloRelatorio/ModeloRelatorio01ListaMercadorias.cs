
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relatorios.ModeloRelatorio
{
    public class ModeloRelatorio01ListaMercadorias
    {
        [DisplayName("CodigoBarra")]
        public String CodigoBarra { get; set; }

        [DisplayName("Descricao")]
        public String Descricao { get; set; }

        [DisplayName("PrecoVenda")]
        public Decimal PrecoVenda { get; set; }

        [DisplayName("Unidade")]
        public Int64 Unidade { get; set; }

        public String ProxyUnidade { get { return ""; } }// ((EUnidade)Unidade).Descricao(); } }


        [DisplayName("QtdEstoque")]
        public Decimal QtdEstoque { get; set; }


        public String Sit
        {
            get
            {
                if (Ativo.Equals("True"))
                    return "Ativo";
                else return "Inativo";
            }

            set { value = Ativo; }
        }

        [DisplayName("Ativo")]
        public String Ativo { get; set; }

    }
}


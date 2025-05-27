using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigJetGas.Modelos.Movimentacao
{
    public class ModelSelecaoImpressao
    {
        public string Impressora { get; set; }
        public bool Paisagem { get; set; }
        public bool PapelA5 { get; set; }
        public int MargemEsquerda { get; set; }
        public int MargemDireita { get; set; }
    }
}

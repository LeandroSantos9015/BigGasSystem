using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp6.Enumeradores;
using WindowsFormsApp6.Interface;

namespace WindowsFormsApp6.Modelos.Movimentacao
{
    public class ModelFinalizadora
    {
        public Int64 Id { get; set; }
        public string Descricao { get; set; }

        [Browsable(false)]
        public decimal Taxa { get; set; }

    }
}
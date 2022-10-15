﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relatorios.Enumeradores
{
    /// <summary>
    /// Atenção 
    /// O numero do enumerador DEVE ser o mesmo número do relatório
    /// </summary>
    public enum EAtivo
    {
        [Description("Sim")]
        Sim = 1,

        [Description("Não")]
        Nao = 2

    }
}
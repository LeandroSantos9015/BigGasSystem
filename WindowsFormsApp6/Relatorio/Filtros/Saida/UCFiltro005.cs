﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Relatorios.Filtros.Venda
{
    public partial class UCFiltro005 : UserControl
    {
        public UCFiltro005()
        {
            InitializeComponent();
        }

        public UserControl UCFiltro { get { return this; } }

        public DateTimePicker DateInicio { get { return this.dteInicio; } }

        public DateTimePicker DateFim { get { return this.dteFim; } }

    }
}

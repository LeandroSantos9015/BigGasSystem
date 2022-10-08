using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp6.Controles.Cadastros;
using WindowsFormsApp6.Controles.Movimentacao;

namespace WindowsFormsApp6
{
    public class CtrlPrincipal
    {
        bool usarTelaClassica = true;
        public IPrincipalView Principal { get; set; }

        public CtrlPrincipal()
        {
           // if (usarTelaClassica)
                Principal = new FormPrincipal();
            //else
            //    Principal = new FrmPrincipal2();

            DelegarEventos();
        }

        private void DelegarEventos()
        {
            Principal.MenuClientes.Click += MenuClientes_Click;

            Principal.MenuEntradas.Click += MenuEntradas_Click;

            Principal.MenuMercadorias.Click += MenuMercadorias_Click;

            Principal.MenuSaidas.Click += MenuSaidas_Click;

        }

        private void MenuSaidas_Click(object sender, EventArgs e)
        {
            
        }

        private void MenuMercadorias_Click(object sender, EventArgs e)
        {
            new CtrlCadastroMercadoria(Principal);
        }

        private void MenuEntradas_Click(object sender, EventArgs e)
        {
            new CtrlEntrada(Principal);
        }

        private void MenuClientes_Click(object sender, EventArgs e)
        {
            new CtrlCadastroCliente(Principal);
        }

        public string Fatorial(int valor)
        {
            int resultado = valor;

            for (int i = (int)valor - 1; i > 0; i--)
                resultado *= i;

            return resultado.ToString();
        }

        public void Teste()
        {



        }

    }
}

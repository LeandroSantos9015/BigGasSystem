using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp6.Controles;
using WindowsFormsApp6.Controles.Seguranca;
using WindowsFormsApp6.Modelos.Seguranca;

namespace WindowsFormsApp6
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Inicia a instância do SQL LocalDB
            try 
            { 
                new ExecCMD().Execute("sqllocaldb start venda"); 
            }
            catch 
            { 
                MessageBox.Show("Problema ao startar a instancia venda", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
            }

            // Exibe tela de login
            CtrlLogin ctrlLogin = new CtrlLogin();
            
            // Verifica se o login foi bem-sucedido
            if (ctrlLogin.LoginView.LoginView.DialogResult == DialogResult.OK && ModelSessao.EstaLogado())
            {
                // Se login OK, abre o sistema principal
                CtrlPrincipal ctrl = new CtrlPrincipal();
                Application.Run(ctrl.Principal.PrincipalView);
                
                // Ao fechar o sistema, limpa a sessão
                ModelSessao.Limpar();
            }
            else
            {
                // Se cancelou o login ou falhou, encerra a aplicação
                MessageBox.Show("Sistema encerrado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

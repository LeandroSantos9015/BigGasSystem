using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp6.Controles;

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

            new ExecCMD().Execute("sqllocaldb start venda");

            CtrlPrincipal ctrl = new CtrlPrincipal();

            Application.Run(ctrl.Principal.PrincipalView);
        }
    }
}

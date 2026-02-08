using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp6
{
    public interface IPrincipalView
    {
        Form PrincipalView { get; }

        ToolStripMenuItem MenuClientes { get; }

        ToolStripMenuItem MenuMercadorias { get; }

        ToolStripMenuItem MenuEntradas { get; }

        ToolStripMenuItem MenuSaidas { get; }

        ToolStripMenuItem MenuCancelamentoSaida { get; }

        ToolStripMenuItem MenuConfiguracoes { get; }

        ToolStripMenuItem MenuRelatorios { get; }

        ToolStripMenuItem MenuImportar { get; }
        
        // Novos menus de segurança
        ToolStripMenuItem MenuPerfis { get; }
        ToolStripMenuItem MenuUsuarios { get; }
        ToolStripMenuItem MenuMudarUsuario { get; }
        
        // Label para exibir usuário logado
        ToolStripStatusLabel LblUsuarioLogado { get; }
    }
}
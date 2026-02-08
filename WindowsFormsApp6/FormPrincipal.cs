using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp6
{
    public partial class FormPrincipal : Form, IPrincipalView
    {
        private ToolStripMenuItem menuPerfis;
        private ToolStripMenuItem menuUsuarios;
        private ToolStripMenuItem menuMudarUsuario;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblUsuarioLogado;

        public FormPrincipal() 
        { 
            InitializeComponent();
            InicializarMenusSeguranca();
        }

        public Form PrincipalView => this;

        public ToolStripMenuItem MenuClientes => clientesToolStripMenuItem;
        public ToolStripMenuItem MenuMercadorias => mercadoriasToolStripMenuItem;
        public ToolStripMenuItem MenuEntradas => entradaToolStripMenuItem;
        public ToolStripMenuItem MenuSaidas => saidaToolStripMenuItem;
        public ToolStripMenuItem MenuCancelamentoSaida => cancelamentoSaidaToolStripMenuItem;
        public ToolStripMenuItem MenuConfiguracoes => configuracoesToolStripMenuItem;
        public ToolStripMenuItem MenuRelatorios => relatoriosToolStripMenuItem;
        public ToolStripMenuItem MenuImportar => importarToolStripMenuItem;
        
        // Novos menus
        public ToolStripMenuItem MenuPerfis => menuPerfis;
        public ToolStripMenuItem MenuUsuarios => menuUsuarios;
        public ToolStripMenuItem MenuMudarUsuario => menuMudarUsuario;
        public ToolStripStatusLabel LblUsuarioLogado => lblUsuarioLogado;

        private void InicializarMenusSeguranca()
        {
            // Procura pelo MenuStrip existente
            MenuStrip menuStrip = null;
            foreach (Control control in this.Controls)
            {
                if (control is MenuStrip)
                {
                    menuStrip = control as MenuStrip;
                    break;
                }
            }

            if (menuStrip == null)
                return;

            // Cria menu Segurança
            ToolStripMenuItem menuSeguranca = new ToolStripMenuItem("Segurança");
            
            // Menu Perfis
            menuPerfis = new ToolStripMenuItem("Perfis");
            menuSeguranca.DropDownItems.Add(menuPerfis);
            
            // Menu Usuários
            menuUsuarios = new ToolStripMenuItem("Usuários");
            menuSeguranca.DropDownItems.Add(menuUsuarios);
            
            // Separador
            menuSeguranca.DropDownItems.Add(new ToolStripSeparator());
            
            // Menu Mudar Usuário
            menuMudarUsuario = new ToolStripMenuItem("Mudar Usuário");
            menuMudarUsuario.ShortcutKeys = Keys.Control | Keys.U;
            menuSeguranca.DropDownItems.Add(menuMudarUsuario);
            
            // Adiciona o menu Segurança ao MenuStrip
            menuStrip.Items.Add(menuSeguranca);
            
            // Cria StatusStrip para exibir usuário logado
            statusStrip = new StatusStrip();
            statusStrip.Dock = DockStyle.Bottom;
            lblUsuarioLogado = new ToolStripStatusLabel();
            lblUsuarioLogado.Text = "Sistema iniciando...";
            lblUsuarioLogado.Spring = true;
            lblUsuarioLogado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            statusStrip.Items.Add(lblUsuarioLogado);
            this.Controls.Add(statusStrip);
        }
    }
}

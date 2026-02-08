using Relatorios.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp6.Controles.Cadastros;
using WindowsFormsApp6.Controles.Movimentacao;
using WindowsFormsApp6.Controles.Utilitarios;
using WindowsFormsApp6.Controles.Seguranca;
using WindowsFormsApp6.Modelos.Seguranca;
using WindowsFormsApp6.Utilitarios;

namespace WindowsFormsApp6
{
    public class CtrlPrincipal
    {
        bool usarTelaClassica = true;
        public IPrincipalView Principal { get; set; }

        public CtrlPrincipal()
        {
            Principal = new FormPrincipal();
            
            DelegarEventos();
            ExibirUsuarioLogado();
        }

        private void DelegarEventos()
        {
            Principal.MenuClientes.Click += MenuClientes_Click;
            Principal.MenuEntradas.Click += MenuEntradas_Click;
            Principal.MenuMercadorias.Click += MenuMercadorias_Click;
            Principal.MenuSaidas.Click += MenuSaidas_Click;
            Principal.MenuCancelamentoSaida.Click += MenuCancelamentoSaida_Click;
            Principal.MenuConfiguracoes.Click += MenuConfiguracoes_Click;
            Principal.MenuRelatorios.Click += MenuRelatorios_Click;
            Principal.MenuImportar.Click += MenuImportar_Click;
            Principal.MenuPerfis.Click += MenuPerfis_Click;
            Principal.MenuUsuarios.Click += MenuUsuarios_Click;
            Principal.MenuMudarUsuario.Click += MenuMudarUsuario_Click;
            Principal.PrincipalView.FormClosing += FormClosing;
        }

        private void ExibirUsuarioLogado()
        {
            if (ModelSessao.EstaLogado())
            {
                Principal.LblUsuarioLogado.Text = $"Usuário: {ModelSessao.UsuarioNome} | Perfil: {ModelSessao.PerfilNome}";
            }
        }

        // Método para validar permissão antes de abrir tela
        private bool ValidarPermissao(string chaveMenu, string nomeMenu)
        {
            // Carrega permissões se ainda não foi carregado
            PermissaoHelper.CarregarPermissoes();
            
            // Se tem permissão, libera
            if (PermissaoHelper.TemPermissao(chaveMenu))
                return true;

            // Não tem permissão, pede validação de outro usuário
            var ctrlValidacao = new CtrlValidarPermissao(chaveMenu, nomeMenu);
            
            if (ctrlValidacao.FormValidacao.DialogResult == DialogResult.OK)
            {
                MessageBox.Show("Acesso autorizado!", "Permissão Concedida", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }

            return false;
        }

        private void MenuImportar_Click(object sender, EventArgs e)
        {
            if (ValidarPermissao(PermissaoHelper.MENU_IMPORTAR, "Importar"))
                new CtrlImportacao(Principal);
        }

        private void MenuRelatorios_Click(object sender, EventArgs e)
        {
            if (ValidarPermissao(PermissaoHelper.MENU_RELATORIOS, "Relatórios"))
                new CtrlRelatorios(Principal, null);
        }

        private void MenuCancelamentoSaida_Click(object sender, EventArgs e)
        {
            if (ValidarPermissao(PermissaoHelper.MENU_CANCELAMENTO, "Cancelamento de Saídas"))
                new CtrlCancelamentoSaida(Principal);
        }

        private void MenuConfiguracoes_Click(object sender, EventArgs e)
        {
            if (ValidarPermissao(PermissaoHelper.MENU_CONFIGURACOES, "Configurações"))
                new CtrlConfiguracao(Principal);
        }

        private void MenuSaidas_Click(object sender, EventArgs e)
        {
            if (ValidarPermissao(PermissaoHelper.MENU_SAIDAS, "Saídas"))
                new CtrlSaida(Principal);
        }

        private void MenuMercadorias_Click(object sender, EventArgs e)
        {
            if (ValidarPermissao(PermissaoHelper.MENU_MERCADORIAS, "Mercadorias"))
                new CtrlCadastroMercadoria(Principal);
        }

        private void MenuEntradas_Click(object sender, EventArgs e)
        {
            if (ValidarPermissao(PermissaoHelper.MENU_ENTRADAS, "Entradas"))
                new CtrlEntrada(Principal);
        }

        private void MenuClientes_Click(object sender, EventArgs e)
        {
            if (ValidarPermissao(PermissaoHelper.MENU_CLIENTES, "Clientes"))
                new CtrlCadastroCliente(Principal);
        }

        private void MenuPerfis_Click(object sender, EventArgs e)
        {
            if (ValidarPermissao(PermissaoHelper.MENU_PERFIS, "Perfis"))
                new CtrlCadastroPerfil(Principal.PrincipalView);
        }

        private void MenuUsuarios_Click(object sender, EventArgs e)
        {
            if (ValidarPermissao(PermissaoHelper.MENU_USUARIOS, "Usuários"))
                new CtrlCadastroUsuario(Principal.PrincipalView);
        }

        private void MenuMudarUsuario_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Deseja realmente trocar de usuário?\nTodas as telas abertas serão fechadas.", 
                "Mudar Usuário", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Fecha todas as janelas filhas MDI
                foreach (Form form in Principal.PrincipalView.MdiChildren)
                {
                    form.Close();
                }

                // Limpa sessão atual
                ModelSessao.Limpar();
                PermissaoHelper.LimparCache();

                // Abre tela de login
                CtrlLogin ctrlLogin = new CtrlLogin();

                if (ctrlLogin.LoginView.LoginView.DialogResult == DialogResult.OK && ModelSessao.EstaLogado())
                {
                    // Atualiza label de usuário
                    ExibirUsuarioLogado();
                    MessageBox.Show($"Bem-vindo, {ModelSessao.UsuarioNome}!", "Login Realizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Se cancelou o login, fecha o sistema
                    Principal.PrincipalView.Close();
                }
            }
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

        public virtual void FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                var result = MessageBox.Show(this.Principal.PrincipalView, "Você tem certeza que deseja sair?", "Confirmação", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    // Limpa a sessão ao sair
                    ModelSessao.Limpar();
                    PermissaoHelper.LimparCache();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
    }
}

using System;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp6.Interface.Seguranca;
using WindowsFormsApp6.Menus.Seguranca;
using WindowsFormsApp6.Modelos.Seguranca;
using WindowsFormsApp6.Regras.Seguranca;

namespace WindowsFormsApp6.Controles.Seguranca
{
    public class CtrlLogin
    {
        private RegraUsuario regraUsuario;
        public ILoginView LoginView { get; set; }

        public CtrlLogin()
        {
            LoginView = new FrmLogin();
            regraUsuario = new RegraUsuario();
            
            DelegarEventos();
            CarregarUsuarios();
            
            LoginView.LoginView.ShowDialog();
        }

        private void DelegarEventos()
        {
            LoginView.BtnEntrar.Click += BtnEntrar_Click;
            LoginView.BtnSair.Click += BtnSair_Click;
            LoginView.TxtSenha.KeyPress += TxtSenha_KeyPress;
        }

        private void CarregarUsuarios()
        {
            try
            {
                var usuarios = regraUsuario.Listar().Where(u => u.Ativo).OrderBy(u => u.Nome).ToList();
                LoginView.CboLogin.DataSource = usuarios;
                LoginView.CboLogin.DisplayMember = "Nome";
                LoginView.CboLogin.ValueMember = "Login";
                
                if (usuarios.Count > 0)
                {
                    LoginView.CboLogin.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar usuários: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtSenha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                BtnEntrar_Click(sender, e);
            }
        }

        private void BtnEntrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (LoginView.CboLogin.SelectedValue == null)
                {
                    MessageBox.Show("Selecione um usuário", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string login = LoginView.CboLogin.SelectedValue.ToString();
                string senha = LoginView.TxtSenha.Text;

                var usuario = regraUsuario.Autenticar(login, senha);

                // Armazena dados do usuário na sessão
                ModelSessao.UsuarioId = usuario.Id;
                ModelSessao.UsuarioNome = usuario.Nome;
                ModelSessao.UsuarioLogin = usuario.Login;
                ModelSessao.PerfilId = usuario.IdPerfil;
                ModelSessao.PerfilNome = usuario.NomePerfil;

                LoginView.LoginView.DialogResult = DialogResult.OK;
                LoginView.LoginView.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao fazer login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoginView.TxtSenha.Clear();
                LoginView.TxtSenha.Focus();
            }
        }

        private void BtnSair_Click(object sender, EventArgs e)
        {
            LoginView.LoginView.DialogResult = DialogResult.Cancel;
            LoginView.LoginView.Close();
        }
    }
}

using System;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp6.Menus.Seguranca;
using WindowsFormsApp6.Modelos.Seguranca;
using WindowsFormsApp6.Regras.Seguranca;
using WindowsFormsApp6.Utilitarios;

namespace WindowsFormsApp6.Controles.Seguranca
{
    public class CtrlValidarPermissao
    {
        private RegraUsuario regraUsuario;
        private RegraPerfil regraPerfil;
        private string chaveMenu;
        public FrmValidarPermissao FormValidacao { get; set; }

        public CtrlValidarPermissao(string chaveMenu, string nomeMenu)
        {
            this.chaveMenu = chaveMenu;
            FormValidacao = new FrmValidarPermissao();
            regraUsuario = new RegraUsuario();
            regraPerfil = new RegraPerfil();
            
            FormValidacao.LblMensagem.Text = $"Você não tem permissão para acessar:\n'{nomeMenu}'\n\nDigite as credenciais de um usuário autorizado:";
            
            DelegarEventos();
            CarregarUsuarios();
            
            FormValidacao.ShowDialog();
        }

        private void DelegarEventos()
        {
            FormValidacao.BtnValidar.Click += BtnValidar_Click;
            FormValidacao.BtnCancelar.Click += BtnCancelar_Click;
            FormValidacao.TxtSenha.KeyPress += TxtSenha_KeyPress;
        }

        private void CarregarUsuarios()
        {
            try
            {
                var usuarios = regraUsuario.Listar().Where(u => u.Ativo).OrderBy(u => u.Nome).ToList();
                FormValidacao.CboUsuario.DataSource = usuarios;
                FormValidacao.CboUsuario.DisplayMember = "Nome";
                FormValidacao.CboUsuario.ValueMember = "Login";
                
                if (usuarios.Count > 0)
                {
                    FormValidacao.CboUsuario.SelectedIndex = 0;
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
                BtnValidar_Click(sender, e);
            }
        }

        private void BtnValidar_Click(object sender, EventArgs e)
        {
            try
            {
                if (FormValidacao.CboUsuario.SelectedValue == null)
                {
                    MessageBox.Show("Selecione um usuário", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string login = FormValidacao.CboUsuario.SelectedValue.ToString();
                string senha = FormValidacao.TxtSenha.Text;

                var usuario = regraUsuario.Autenticar(login, senha);
                
                // Verifica se o usuário tem permissão para este menu
                var permissoes = regraPerfil.ListarPermissoes(usuario.IdPerfil);
                var menu = permissoes.FirstOrDefault(m => m.Chave == chaveMenu);
                
                if (menu == null || !menu.Visualizar)
                {
                    MessageBox.Show($"O usuário '{usuario.Nome}' não tem permissão para acessar este recurso.", 
                        "Permissão Negada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    FormValidacao.TxtSenha.Clear();
                    FormValidacao.TxtSenha.Focus();
                    return;
                }

                // Permissão validada
                FormValidacao.DialogResult = DialogResult.OK;
                FormValidacao.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao validar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                FormValidacao.TxtSenha.Clear();
                FormValidacao.TxtSenha.Focus();
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            FormValidacao.DialogResult = DialogResult.Cancel;
            FormValidacao.Close();
        }
    }
}

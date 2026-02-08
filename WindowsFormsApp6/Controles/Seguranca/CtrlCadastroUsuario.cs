using System;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp6.Controles;
using WindowsFormsApp6.Interface.Seguranca;
using WindowsFormsApp6.Menus.Seguranca;
using WindowsFormsApp6.Modelos.Seguranca;
using WindowsFormsApp6.Regras.Seguranca;

namespace WindowsFormsApp6.Controles.Seguranca
{
    public class CtrlCadastroUsuario
    {
        private RegraUsuario regraUsuario;
        private RegraPerfil regraPerfil;
        private ModelUsuario usuarioSelecionado;
        private IPrincipalView Pai;
        public ICadastroUsuarioView CadastroUsuarioView { get; set; }

        public CtrlCadastroUsuario(Form formPai)
        {
            CadastroUsuarioView = new FrmCadastroUsuario();
            CadastroUsuarioView.CadastroUsuarioView.MdiParent = formPai;
            
            // Procura a interface principal
            if (formPai is IPrincipalView)
                Pai = formPai as IPrincipalView;
            
            regraUsuario = new RegraUsuario();
            regraPerfil = new RegraPerfil();
            
            DelegarEventos();
            CarregarPerfis();
            LimparCampos();
            
            CadastroUsuarioView.CadastroUsuarioView.Show();
        }

        private void DelegarEventos()
        {
            CadastroUsuarioView.BtnSalvar.Click += BtnSalvar_Click;
            CadastroUsuarioView.BtnCancelar.Click += BtnCancelar_Click;
            CadastroUsuarioView.BtnPesquisar.Click += BtnPesquisar_Click;
            CadastroUsuarioView.ChkAtivo.CheckedChanged += ChkAtivo_CheckedChanged;
        }

        private void ChkAtivo_CheckedChanged(object sender, EventArgs e)
        {
            CadastroUsuarioView.LblAtivo.ForeColor = CadastroUsuarioView.ChkAtivo.Checked 
                ? System.Drawing.Color.Green 
                : System.Drawing.Color.Red;
        }

        private void CarregarPerfis()
        {
            try
            {
                var perfis = regraPerfil.Listar().Where(p => p.Ativo).ToList();
                CadastroUsuarioView.CboPerfil.DataSource = perfis;
                CadastroUsuarioView.CboPerfil.DisplayMember = "Nome";
                CadastroUsuarioView.CboPerfil.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar perfis: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                var usuarios = regraUsuario.Listar();
                
                if (usuarios.Count > 0)
                {
                    // Usa CtrlPesquisar genérico
                    System.Collections.Generic.IList<Object> listaGenerica = usuarios.Cast<Object>().ToList();
                    CtrlPesquisar ctrl = new CtrlPesquisar(Pai, listaGenerica, 900, "Pesquisar Usuário");
                    
                    var usuarioEncontrado = ctrl.RetornaObjetoSelecionado() as ModelUsuario;
                    
                    if (usuarioEncontrado != null)
                    {
                        usuarioSelecionado = usuarioEncontrado;
                        CadastroUsuarioView.TxtId.Text = usuarioSelecionado.Id.ToString();
                        CadastroUsuarioView.TxtNome.Text = usuarioSelecionado.Nome;
                        CadastroUsuarioView.TxtLogin.Text = usuarioSelecionado.Login;
                        CadastroUsuarioView.TxtSenha.Text = string.Empty; // Não mostra a senha
                        CadastroUsuarioView.CboPerfil.SelectedValue = usuarioSelecionado.IdPerfil;
                        CadastroUsuarioView.ChkAtivo.Checked = usuarioSelecionado.Ativo;
                    }
                }
                else
                {
                    MessageBox.Show("Nenhum usuário encontrado.", "Pesquisa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao pesquisar usuários: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (usuarioSelecionado == null)
                    usuarioSelecionado = new ModelUsuario();

                usuarioSelecionado.Nome = CadastroUsuarioView.TxtNome.Text.Trim();
                usuarioSelecionado.Login = CadastroUsuarioView.TxtLogin.Text.Trim();
                usuarioSelecionado.Senha = CadastroUsuarioView.TxtSenha.Text;
                usuarioSelecionado.IdPerfil = Convert.ToInt64(CadastroUsuarioView.CboPerfil.SelectedValue);
                usuarioSelecionado.Ativo = CadastroUsuarioView.ChkAtivo.Checked;

                regraUsuario.Salvar(usuarioSelecionado);
                
                MessageBox.Show("Usuário salvo com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                LimparCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao salvar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void LimparCampos()
        {
            usuarioSelecionado = null;
            CadastroUsuarioView.TxtId.Clear();
            CadastroUsuarioView.TxtNome.Clear();
            CadastroUsuarioView.TxtLogin.Clear();
            CadastroUsuarioView.TxtSenha.Clear();
            CadastroUsuarioView.ChkAtivo.Checked = true;
            
            if (CadastroUsuarioView.CboPerfil.Items.Count > 0)
                CadastroUsuarioView.CboPerfil.SelectedIndex = 0;
                
            CadastroUsuarioView.TxtNome.Focus();
        }
    }
}

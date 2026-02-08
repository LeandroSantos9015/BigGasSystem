using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp6.Controles;
using WindowsFormsApp6.Interface.Seguranca;
using WindowsFormsApp6.Menus.Seguranca;
using WindowsFormsApp6.Modelos.Seguranca;
using WindowsFormsApp6.Regras.Seguranca;

namespace WindowsFormsApp6.Controles.Seguranca
{
    public class CtrlCadastroPerfil
    {
        private RegraPerfil regraPerfil;
        private ModelPerfil perfilSelecionado;
        private List<ModelMenu> permissoesTemporarias;
        private IPrincipalView Pai;
        public ICadastroPerfilView CadastroPerfilView { get; set; }

        public CtrlCadastroPerfil(Form formPai)
        {
            CadastroPerfilView = new FrmCadastroPerfil();
            CadastroPerfilView.CadastroPerfilView.MdiParent = formPai;
            
            // Procura a interface principal
            if (formPai is IPrincipalView)
                Pai = formPai as IPrincipalView;
            
            regraPerfil = new RegraPerfil();
            
            DelegarEventos();
            LimparCampos();
            
            CadastroPerfilView.CadastroPerfilView.Show();
        }

        private void DelegarEventos()
        {
            CadastroPerfilView.BtnSalvar.Click += BtnSalvar_Click;
            CadastroPerfilView.BtnCancelar.Click += BtnCancelar_Click;
            CadastroPerfilView.BtnPermissoes.Click += BtnPermissoes_Click;
            CadastroPerfilView.BtnPesquisar.Click += BtnPesquisar_Click;
            CadastroPerfilView.ChkAtivo.CheckedChanged += ChkAtivo_CheckedChanged;
        }

        private void ChkAtivo_CheckedChanged(object sender, EventArgs e)
        {
            CadastroPerfilView.LblAtivo.ForeColor = CadastroPerfilView.ChkAtivo.Checked 
                ? System.Drawing.Color.Green 
                : System.Drawing.Color.Red;
        }

        private void BtnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                var perfis = regraPerfil.Listar();
                
                if (perfis.Count > 0)
                {
                    // Usa CtrlPesquisar genérico
                    IList<Object> listaGenerica = perfis.Cast<Object>().ToList();
                    CtrlPesquisar ctrl = new CtrlPesquisar(Pai, listaGenerica, 800, "Pesquisar Perfil");
                    
                    var perfilEncontrado = ctrl.RetornaObjetoSelecionado() as ModelPerfil;
                    
                    if (perfilEncontrado != null)
                    {
                        perfilSelecionado = perfilEncontrado;
                        CadastroPerfilView.TxtId.Text = perfilSelecionado.Id.ToString();
                        CadastroPerfilView.TxtNome.Text = perfilSelecionado.Nome;
                        CadastroPerfilView.TxtDescricao.Text = perfilSelecionado.Descricao;
                        CadastroPerfilView.ChkAtivo.Checked = perfilSelecionado.Ativo;
                        
                        // Limpa permissões temporárias ao carregar perfil existente
                        permissoesTemporarias = null;
                    }
                }
                else
                {
                    MessageBox.Show("Nenhum perfil encontrado.", "Pesquisa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao pesquisar perfis: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                bool isNovo = perfilSelecionado == null || perfilSelecionado.Id == 0;
                
                if (perfilSelecionado == null)
                    perfilSelecionado = new ModelPerfil();

                perfilSelecionado.Nome = CadastroPerfilView.TxtNome.Text.Trim();
                perfilSelecionado.Descricao = CadastroPerfilView.TxtDescricao.Text.Trim();
                perfilSelecionado.Ativo = CadastroPerfilView.ChkAtivo.Checked;

                regraPerfil.Salvar(perfilSelecionado);
                
                // Se for novo, busca o ID gerado
                if (isNovo)
                {
                    var perfis = regraPerfil.Listar()
                        .Where(p => p.Nome == perfilSelecionado.Nome)
                        .OrderByDescending(p => p.Id)
                        .FirstOrDefault();
                    
                    if (perfis != null)
                    {
                        perfilSelecionado.Id = perfis.Id;
                        CadastroPerfilView.TxtId.Text = perfis.Id.ToString();
                    }
                }
                
                // Se tem permissões temporárias, salva agora
                if (permissoesTemporarias != null && permissoesTemporarias.Count > 0)
                {
                    foreach (var menu in permissoesTemporarias)
                    {
                        regraPerfil.SalvarPermissao(perfilSelecionado.Id, menu.Id, menu.Visualizar);
                    }
                    permissoesTemporarias = null;
                }
                
                MessageBox.Show("Perfil salvo com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                LimparCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao salvar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnPermissoes_Click(object sender, EventArgs e)
        {
            try
            {
                // Se está editando perfil existente
                if (perfilSelecionado != null && perfilSelecionado.Id > 0)
                {
                    var ctrl = new CtrlPermissoesPerfil(perfilSelecionado);
                }
                // Se está criando novo perfil
                else
                {
                    // Cria um perfil temporário para configurar permissões
                    var perfilTemp = new ModelPerfil
                    {
                        Id = 0,
                        Nome = string.IsNullOrWhiteSpace(CadastroPerfilView.TxtNome.Text) 
                            ? "Novo Perfil" 
                            : CadastroPerfilView.TxtNome.Text.Trim()
                    };
                    
                    // Carrega todas as permissões desmarcadas se ainda não tem
                    if (permissoesTemporarias == null)
                    {
                        permissoesTemporarias = regraPerfil.ListarPermissoes(0).ToList();
                    }
                    
                    var ctrl = new CtrlPermissoesPerfil(perfilTemp, permissoesTemporarias);
                    
                    // Depois que fechar a tela, atualiza as permissões temporárias
                    permissoesTemporarias = ctrl.ObterPermissoes();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao abrir permissões: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void LimparCampos()
        {
            perfilSelecionado = null;
            permissoesTemporarias = null;
            CadastroPerfilView.TxtId.Clear();
            CadastroPerfilView.TxtNome.Clear();
            CadastroPerfilView.TxtDescricao.Clear();
            CadastroPerfilView.ChkAtivo.Checked = true;
            CadastroPerfilView.TxtNome.Focus();
        }
    }
}

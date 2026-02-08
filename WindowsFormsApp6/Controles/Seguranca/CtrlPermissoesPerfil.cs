using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp6.Interface.Seguranca;
using WindowsFormsApp6.Menus.Seguranca;
using WindowsFormsApp6.Modelos.Seguranca;
using WindowsFormsApp6.Regras.Seguranca;

namespace WindowsFormsApp6.Controles.Seguranca
{
    public class CtrlPermissoesPerfil
    {
        private RegraPerfil regraPerfil;
        private ModelPerfil perfil;
        private List<ModelMenu> permissoesLista;
        private bool modoTemporario;
        public IPermissoesPerfilView PermissoesPerfilView { get; set; }

        // Construtor para perfil existente
        public CtrlPermissoesPerfil(ModelPerfil perfil)
        {
            this.perfil = perfil;
            this.modoTemporario = perfil.Id == 0;
            PermissoesPerfilView = new FrmPermissoesPerfil();
            regraPerfil = new RegraPerfil();
            
            DelegarEventos();
            CarregarPermissoes();
            
            PermissoesPerfilView.PermissoesPerfilView.ShowDialog();
        }

        // Construtor para novo perfil (modo temporário)
        public CtrlPermissoesPerfil(ModelPerfil perfil, List<ModelMenu> permissoesTemporarias)
        {
            this.perfil = perfil;
            this.modoTemporario = true;
            this.permissoesLista = permissoesTemporarias;
            PermissoesPerfilView = new FrmPermissoesPerfil();
            regraPerfil = new RegraPerfil();
            
            DelegarEventos();
            CarregarPermissoes();
            
            PermissoesPerfilView.PermissoesPerfilView.ShowDialog();
        }

        private void DelegarEventos()
        {
            PermissoesPerfilView.BtnSalvar.Click += BtnSalvar_Click;
            PermissoesPerfilView.BtnFechar.Click += BtnFechar_Click;
        }

        private void CarregarPermissoes()
        {
            try
            {
                PermissoesPerfilView.LblPerfil.Text = $"Perfil: {perfil.Nome}";
                
                // Se não tem lista carregada, busca do banco
                if (permissoesLista == null)
                {
                    permissoesLista = regraPerfil.ListarPermissoes(perfil.Id).ToList();
                }
                
                PermissoesPerfilView.ChkMenus.Items.Clear();
                
                foreach (var menu in permissoesLista.OrderBy(m => m.Ordem))
                {
                    int index = PermissoesPerfilView.ChkMenus.Items.Add(menu.Descricao);
                    PermissoesPerfilView.ChkMenus.SetItemChecked(index, menu.Visualizar);
                }
                
                // Armazena os menus em uma propriedade do form
                (PermissoesPerfilView.PermissoesPerfilView as FrmPermissoesPerfil).Tag = permissoesLista;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar permissões: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                var menus = (PermissoesPerfilView.PermissoesPerfilView as FrmPermissoesPerfil).Tag as List<ModelMenu>;
                
                if (menus == null)
                {
                    MessageBox.Show("Erro ao obter lista de menus", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Atualiza o estado das permissões na lista
                for (int i = 0; i < PermissoesPerfilView.ChkMenus.Items.Count; i++)
                {
                    bool marcado = PermissoesPerfilView.ChkMenus.GetItemChecked(i);
                    menus[i].Visualizar = marcado;
                }

                // Se não é modo temporário, salva direto no banco
                if (!modoTemporario && perfil.Id > 0)
                {
                    foreach (var menu in menus)
                    {
                        regraPerfil.SalvarPermissao(perfil.Id, menu.Id, menu.Visualizar);
                    }
                    MessageBox.Show("Permissões salvas com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Modo temporário - apenas atualiza a lista
                    permissoesLista = menus;
                    MessageBox.Show("Permissões configuradas! Salve o perfil para gravar as permissões.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
                PermissoesPerfilView.PermissoesPerfilView.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar permissões: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnFechar_Click(object sender, EventArgs e)
        {
            PermissoesPerfilView.PermissoesPerfilView.Close();
        }

        // Método para obter as permissões configuradas (usado em modo temporário)
        public List<ModelMenu> ObterPermissoes()
        {
            return permissoesLista;
        }
    }
}

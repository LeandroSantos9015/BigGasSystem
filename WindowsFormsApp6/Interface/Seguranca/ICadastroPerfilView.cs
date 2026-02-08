using System;
using System.Windows.Forms;

namespace WindowsFormsApp6.Interface.Seguranca
{
    public interface ICadastroPerfilView
    {
        Form CadastroPerfilView { get; }
        TextBox TxtId { get; }
        TextBox TxtNome { get; }
        TextBox TxtDescricao { get; }
        CheckBox ChkAtivo { get; }
        Button BtnSalvar { get; }
        Button BtnPesquisar { get; }
        Button BtnPermissoes { get; }
        Button BtnCancelar { get; }
        Label LblAtivo { get; }
    }
}

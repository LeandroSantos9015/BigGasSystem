using System;
using System.Windows.Forms;

namespace WindowsFormsApp6.Interface.Seguranca
{
    public interface ICadastroUsuarioView
    {
        Form CadastroUsuarioView { get; }
        TextBox TxtId { get; }
        TextBox TxtNome { get; }
        TextBox TxtLogin { get; }
        TextBox TxtSenha { get; }
        ComboBox CboPerfil { get; }
        CheckBox ChkAtivo { get; }
        Button BtnSalvar { get; }
        Button BtnPesquisar { get; }
        Button BtnCancelar { get; }
        Label LblAtivo { get; }
    }
}

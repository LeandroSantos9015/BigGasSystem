using System;
using System.Windows.Forms;

namespace WindowsFormsApp6.Interface.Seguranca
{
    public interface IPermissoesPerfilView
    {
        Form PermissoesPerfilView { get; }
        CheckedListBox ChkMenus { get; }
        Label LblPerfil { get; }
        Button BtnSalvar { get; }
        Button BtnFechar { get; }
    }
}

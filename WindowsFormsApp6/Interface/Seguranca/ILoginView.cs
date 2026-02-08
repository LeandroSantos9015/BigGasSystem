using System;
using System.Windows.Forms;

namespace WindowsFormsApp6.Interface.Seguranca
{
    public interface ILoginView
    {
        Form LoginView { get; }
        ComboBox CboLogin { get; }
        TextBox TxtSenha { get; }
        Button BtnEntrar { get; }
        Button BtnSair { get; }
    }
}

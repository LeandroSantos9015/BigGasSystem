using System;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp6.Interface.Seguranca;

namespace WindowsFormsApp6.Menus.Seguranca
{
    public partial class FrmLogin : Form, ILoginView
    {
        public Form LoginView => this;
        public ComboBox CboLogin { get; private set; }
        public TextBox TxtSenha { get; private set; }
        public Button BtnEntrar { get; private set; }
        public Button BtnSair { get; private set; }

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form
            this.Text = "Login - Sistema de Vendas";
            this.Size = new Size(400, 250);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Label Título
            Label lblTitulo = new Label();
            lblTitulo.Text = "BEM-VINDO AO SISTEMA";
            lblTitulo.Font = new Font("Arial", 14, FontStyle.Bold);
            lblTitulo.Location = new Point(70, 20);
            lblTitulo.Size = new Size(260, 25);
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblTitulo);

            // Label Login
            Label lblLogin = new Label();
            lblLogin.Text = "Usuário:";
            lblLogin.Location = new Point(50, 70);
            lblLogin.Size = new Size(80, 20);
            this.Controls.Add(lblLogin);

            // ComboBox Login
            CboLogin = new ComboBox();
            CboLogin.Location = new Point(140, 68);
            CboLogin.Size = new Size(200, 22);
            CboLogin.TabIndex = 0;
            CboLogin.DropDownStyle = ComboBoxStyle.DropDownList;
            this.Controls.Add(CboLogin);

            // Label Senha
            Label lblSenha = new Label();
            lblSenha.Text = "Senha:";
            lblSenha.Location = new Point(50, 110);
            lblSenha.Size = new Size(80, 20);
            this.Controls.Add(lblSenha);

            // TextBox Senha
            TxtSenha = new TextBox();
            TxtSenha.Location = new Point(140, 108);
            TxtSenha.Size = new Size(200, 22);
            TxtSenha.PasswordChar = '●';
            TxtSenha.TabIndex = 1;
            this.Controls.Add(TxtSenha);

            // Button Entrar
            BtnEntrar = new Button();
            BtnEntrar.Text = "Entrar";
            BtnEntrar.Location = new Point(140, 155);
            BtnEntrar.Size = new Size(90, 30);
            BtnEntrar.TabIndex = 2;
            BtnEntrar.BackColor = Color.FromArgb(0, 122, 204);
            BtnEntrar.ForeColor = Color.White;
            BtnEntrar.FlatStyle = FlatStyle.Flat;
            this.Controls.Add(BtnEntrar);

            // Button Sair
            BtnSair = new Button();
            BtnSair.Text = "Sair";
            BtnSair.Location = new Point(250, 155);
            BtnSair.Size = new Size(90, 30);
            BtnSair.TabIndex = 3;
            BtnSair.BackColor = Color.FromArgb(220, 53, 69);
            BtnSair.ForeColor = Color.White;
            BtnSair.FlatStyle = FlatStyle.Flat;
            this.Controls.Add(BtnSair);

            this.AcceptButton = BtnEntrar;
            this.ResumeLayout(false);
        }
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp6.Interface.Seguranca;

namespace WindowsFormsApp6.Menus.Seguranca
{
    public partial class FrmCadastroUsuario : Form, ICadastroUsuarioView
    {
        public Form CadastroUsuarioView => this;
        public TextBox TxtId { get; private set; }
        public TextBox TxtNome { get; private set; }
        public TextBox TxtLogin { get; private set; }
        public TextBox TxtSenha { get; private set; }
        public ComboBox CboPerfil { get; private set; }
        public CheckBox ChkAtivo { get; private set; }
        public Button BtnSalvar { get; private set; }
        public Button BtnPesquisar { get; private set; }
        public Button BtnCancelar { get; private set; }
        public Label LblAtivo { get; private set; }

        public FrmCadastroUsuario()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form
            this.Text = "Cadastro de Usuários";
            this.Size = new Size(750, 280);
            this.StartPosition = FormStartPosition.CenterScreen;

            // GroupBox Dados
            GroupBox grpDados = new GroupBox();
            grpDados.Text = "Dados do Usuário";
            grpDados.Location = new Point(10, 10);
            grpDados.Size = new Size(710, 210);
            this.Controls.Add(grpDados);

            // Label/TextBox Id
            Label lblId = new Label();
            lblId.Text = "Código:";
            lblId.Location = new Point(20, 30);
            lblId.Size = new Size(80, 20);
            grpDados.Controls.Add(lblId);

            TxtId = new TextBox();
            TxtId.Location = new Point(110, 28);
            TxtId.Size = new Size(80, 22);
            TxtId.ReadOnly = true;
            TxtId.BackColor = Color.LightGray;
            grpDados.Controls.Add(TxtId);

            // Label Nome
            Label lblNome = new Label();
            lblNome.Text = "Nome:";
            lblNome.Location = new Point(210, 30);
            lblNome.Size = new Size(80, 20);
            grpDados.Controls.Add(lblNome);

            // TextBox Nome
            TxtNome = new TextBox();
            TxtNome.Location = new Point(300, 28);
            TxtNome.Size = new Size(280, 22);
            TxtNome.MaxLength = 100;
            grpDados.Controls.Add(TxtNome);

            // Label Login
            Label lblLogin = new Label();
            lblLogin.Text = "Login:";
            lblLogin.Location = new Point(20, 65);
            lblLogin.Size = new Size(80, 20);
            grpDados.Controls.Add(lblLogin);

            // TextBox Login
            TxtLogin = new TextBox();
            TxtLogin.Location = new Point(110, 63);
            TxtLogin.Size = new Size(200, 22);
            TxtLogin.MaxLength = 50;
            grpDados.Controls.Add(TxtLogin);

            // Label Senha
            Label lblSenha = new Label();
            lblSenha.Text = "Senha:";
            lblSenha.Location = new Point(330, 65);
            lblSenha.Size = new Size(80, 20);
            grpDados.Controls.Add(lblSenha);

            // TextBox Senha
            TxtSenha = new TextBox();
            TxtSenha.Location = new Point(420, 63);
            TxtSenha.Size = new Size(160, 22);
            TxtSenha.PasswordChar = '●';
            TxtSenha.MaxLength = 50;
            grpDados.Controls.Add(TxtSenha);

            // Label Perfil
            Label lblPerfil = new Label();
            lblPerfil.Text = "Perfil:";
            lblPerfil.Location = new Point(20, 100);
            lblPerfil.Size = new Size(80, 20);
            grpDados.Controls.Add(lblPerfil);

            // ComboBox Perfil
            CboPerfil = new ComboBox();
            CboPerfil.Location = new Point(110, 98);
            CboPerfil.Size = new Size(300, 22);
            CboPerfil.DropDownStyle = ComboBoxStyle.DropDownList;
            grpDados.Controls.Add(CboPerfil);

            // CheckBox Ativo
            ChkAtivo = new CheckBox();
            ChkAtivo.Text = "Ativo";
            ChkAtivo.Location = new Point(110, 133);
            ChkAtivo.Size = new Size(80, 20);
            ChkAtivo.Checked = true;
            grpDados.Controls.Add(ChkAtivo);

            // Label Ativo (status visual)
            LblAtivo = new Label();
            LblAtivo.Text = "●";
            LblAtivo.Font = new Font("Arial", 16, FontStyle.Bold);
            LblAtivo.ForeColor = Color.Green;
            LblAtivo.Location = new Point(190, 130);
            LblAtivo.Size = new Size(20, 25);
            grpDados.Controls.Add(LblAtivo);

            // Buttons
            BtnSalvar = new Button();
            BtnSalvar.Text = "Salvar";
            BtnSalvar.Location = new Point(240, 165);
            BtnSalvar.Size = new Size(100, 30);
            BtnSalvar.BackColor = Color.FromArgb(40, 167, 69);
            BtnSalvar.ForeColor = Color.White;
            BtnSalvar.FlatStyle = FlatStyle.Flat;
            grpDados.Controls.Add(BtnSalvar);

            BtnPesquisar = new Button();
            BtnPesquisar.Text = "Pesquisar";
            BtnPesquisar.Location = new Point(350, 165);
            BtnPesquisar.Size = new Size(100, 30);
            BtnPesquisar.BackColor = Color.FromArgb(0, 122, 204);
            BtnPesquisar.ForeColor = Color.White;
            BtnPesquisar.FlatStyle = FlatStyle.Flat;
            grpDados.Controls.Add(BtnPesquisar);

            BtnCancelar = new Button();
            BtnCancelar.Text = "Cancelar";
            BtnCancelar.Location = new Point(460, 165);
            BtnCancelar.Size = new Size(100, 30);
            BtnCancelar.BackColor = Color.FromArgb(220, 53, 69);
            BtnCancelar.ForeColor = Color.White;
            BtnCancelar.FlatStyle = FlatStyle.Flat;
            grpDados.Controls.Add(BtnCancelar);

            this.ResumeLayout(false);
        }
    }
}

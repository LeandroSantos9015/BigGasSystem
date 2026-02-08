using System;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp6.Interface.Seguranca;

namespace WindowsFormsApp6.Menus.Seguranca
{
    public partial class FrmCadastroPerfil : Form, ICadastroPerfilView
    {
        public Form CadastroPerfilView => this;
        public TextBox TxtId { get; private set; }
        public TextBox TxtNome { get; private set; }
        public TextBox TxtDescricao { get; private set; }
        public CheckBox ChkAtivo { get; private set; }
        public Button BtnSalvar { get; private set; }
        public Button BtnPesquisar { get; private set; }
        public Button BtnPermissoes { get; private set; }
        public Button BtnCancelar { get; private set; }
        public Label LblAtivo { get; private set; }

        public FrmCadastroPerfil()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form
            this.Text = "Cadastro de Perfis";
            this.Size = new Size(750, 280);
            this.StartPosition = FormStartPosition.CenterScreen;

            // GroupBox Dados
            GroupBox grpDados = new GroupBox();
            grpDados.Text = "Dados do Perfil";
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

            // Label Descrição
            Label lblDescricao = new Label();
            lblDescricao.Text = "Descrição:";
            lblDescricao.Location = new Point(20, 65);
            lblDescricao.Size = new Size(80, 20);
            grpDados.Controls.Add(lblDescricao);

            // TextBox Descrição
            TxtDescricao = new TextBox();
            TxtDescricao.Location = new Point(110, 63);
            TxtDescricao.Size = new Size(580, 22);
            TxtDescricao.MaxLength = 500;
            TxtDescricao.Multiline = true;
            TxtDescricao.Height = 60;
            grpDados.Controls.Add(TxtDescricao);

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

            BtnPermissoes = new Button();
            BtnPermissoes.Text = "Permissões";
            BtnPermissoes.Location = new Point(350, 165);
            BtnPermissoes.Size = new Size(100, 30);
            BtnPermissoes.BackColor = Color.FromArgb(255, 193, 7);
            BtnPermissoes.ForeColor = Color.Black;
            BtnPermissoes.FlatStyle = FlatStyle.Flat;
            grpDados.Controls.Add(BtnPermissoes);

            BtnPesquisar = new Button();
            BtnPesquisar.Text = "Pesquisar";
            BtnPesquisar.Location = new Point(460, 165);
            BtnPesquisar.Size = new Size(100, 30);
            BtnPesquisar.BackColor = Color.FromArgb(0, 122, 204);
            BtnPesquisar.ForeColor = Color.White;
            BtnPesquisar.FlatStyle = FlatStyle.Flat;
            grpDados.Controls.Add(BtnPesquisar);

            BtnCancelar = new Button();
            BtnCancelar.Text = "Cancelar";
            BtnCancelar.Location = new Point(570, 165);
            BtnCancelar.Size = new Size(100, 30);
            BtnCancelar.BackColor = Color.FromArgb(220, 53, 69);
            BtnCancelar.ForeColor = Color.White;
            BtnCancelar.FlatStyle = FlatStyle.Flat;
            grpDados.Controls.Add(BtnCancelar);

            this.ResumeLayout(false);
        }
    }
}

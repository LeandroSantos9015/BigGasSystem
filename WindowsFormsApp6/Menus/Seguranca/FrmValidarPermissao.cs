using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp6.Menus.Seguranca
{
    public partial class FrmValidarPermissao : Form
    {
        public ComboBox CboUsuario { get; private set; }
        public TextBox TxtSenha { get; private set; }
        public Button BtnValidar { get; private set; }
        public Button BtnCancelar { get; private set; }
        public Label LblMensagem { get; private set; }

        public FrmValidarPermissao()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form
            this.Text = "Validação de Permissão";
            this.Size = new Size(450, 260);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Label Mensagem
            LblMensagem = new Label();
            LblMensagem.Text = "Você não tem permissão para acessar este recurso.\nDigite as credenciais de um usuário autorizado:";
            LblMensagem.Font = new Font("Arial", 9, FontStyle.Regular);
            LblMensagem.Location = new Point(20, 20);
            LblMensagem.Size = new Size(400, 40);
            LblMensagem.TextAlign = ContentAlignment.MiddleLeft;
            this.Controls.Add(LblMensagem);

            // Label Usuário
            Label lblUsuario = new Label();
            lblUsuario.Text = "Usuário:";
            lblUsuario.Location = new Point(50, 80);
            lblUsuario.Size = new Size(80, 20);
            this.Controls.Add(lblUsuario);

            // ComboBox Usuário
            CboUsuario = new ComboBox();
            CboUsuario.Location = new Point(140, 78);
            CboUsuario.Size = new Size(250, 22);
            CboUsuario.TabIndex = 0;
            CboUsuario.DropDownStyle = ComboBoxStyle.DropDownList;
            this.Controls.Add(CboUsuario);

            // Label Senha
            Label lblSenha = new Label();
            lblSenha.Text = "Senha:";
            lblSenha.Location = new Point(50, 120);
            lblSenha.Size = new Size(80, 20);
            this.Controls.Add(lblSenha);

            // TextBox Senha
            TxtSenha = new TextBox();
            TxtSenha.Location = new Point(140, 118);
            TxtSenha.Size = new Size(250, 22);
            TxtSenha.PasswordChar = '●';
            TxtSenha.TabIndex = 1;
            this.Controls.Add(TxtSenha);

            // Button Validar
            BtnValidar = new Button();
            BtnValidar.Text = "Validar";
            BtnValidar.Location = new Point(190, 170);
            BtnValidar.Size = new Size(100, 30);
            BtnValidar.TabIndex = 2;
            BtnValidar.BackColor = Color.FromArgb(40, 167, 69);
            BtnValidar.ForeColor = Color.White;
            BtnValidar.FlatStyle = FlatStyle.Flat;
            this.Controls.Add(BtnValidar);

            // Button Cancelar
            BtnCancelar = new Button();
            BtnCancelar.Text = "Cancelar";
            BtnCancelar.Location = new Point(300, 170);
            BtnCancelar.Size = new Size(100, 30);
            BtnCancelar.TabIndex = 3;
            BtnCancelar.BackColor = Color.FromArgb(220, 53, 69);
            BtnCancelar.ForeColor = Color.White;
            BtnCancelar.FlatStyle = FlatStyle.Flat;
            this.Controls.Add(BtnCancelar);

            this.AcceptButton = BtnValidar;
            this.ResumeLayout(false);
        }
    }
}

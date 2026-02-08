using System;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp6.Interface.Seguranca;

namespace WindowsFormsApp6.Menus.Seguranca
{
    public partial class FrmPermissoesPerfil : Form, IPermissoesPerfilView
    {
        public Form PermissoesPerfilView => this;
        public CheckedListBox ChkMenus { get; private set; }
        public Label LblPerfil { get; private set; }
        public Button BtnSalvar { get; private set; }
        public Button BtnFechar { get; private set; }

        public FrmPermissoesPerfil()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form
            this.Text = "Permissões do Perfil";
            this.Size = new Size(500, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Label Perfil
            LblPerfil = new Label();
            LblPerfil.Text = "Perfil: ";
            LblPerfil.Font = new Font("Arial", 10, FontStyle.Bold);
            LblPerfil.Location = new Point(20, 20);
            LblPerfil.Size = new Size(450, 25);
            this.Controls.Add(LblPerfil);

            // Label Instrução
            Label lblInstrucao = new Label();
            lblInstrucao.Text = "Marque os menus que este perfil terá acesso:";
            lblInstrucao.Location = new Point(20, 55);
            lblInstrucao.Size = new Size(450, 20);
            this.Controls.Add(lblInstrucao);

            // CheckedListBox Menus
            ChkMenus = new CheckedListBox();
            ChkMenus.Location = new Point(20, 85);
            ChkMenus.Size = new Size(445, 300);
            ChkMenus.CheckOnClick = true;
            this.Controls.Add(ChkMenus);

            // Button Salvar
            BtnSalvar = new Button();
            BtnSalvar.Text = "Salvar";
            BtnSalvar.Location = new Point(255, 405);
            BtnSalvar.Size = new Size(100, 35);
            BtnSalvar.BackColor = Color.FromArgb(40, 167, 69);
            BtnSalvar.ForeColor = Color.White;
            BtnSalvar.FlatStyle = FlatStyle.Flat;
            this.Controls.Add(BtnSalvar);

            // Button Fechar
            BtnFechar = new Button();
            BtnFechar.Text = "Fechar";
            BtnFechar.Location = new Point(365, 405);
            BtnFechar.Size = new Size(100, 35);
            BtnFechar.BackColor = Color.FromArgb(220, 53, 69);
            BtnFechar.ForeColor = Color.White;
            BtnFechar.FlatStyle = FlatStyle.Flat;
            this.Controls.Add(BtnFechar);

            this.ResumeLayout(false);
        }
    }
}

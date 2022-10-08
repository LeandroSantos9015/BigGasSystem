using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp6.Modelos;
using WindowsFormsApp6.Repositorios.Cliente;

namespace WindowsFormsApp6.Controles.Cadastros
{
    public class RegraMercadoria
    {
        private RepositorioMercadoria repositorio = new RepositorioMercadoria();

        public bool Salvar(ModelMercadoria mercadoria)
        {
            try
            {
                repositorio.Salvar(mercadoria);

                MessageBox.Show("Salvo com sucesso");
                
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro na hora de salvar\n\n\n\n" + ex.Message.ToString());

                return false;
            }
        }

        public IList<ModelMercadoria> ListaMercadorias()
        {
            return repositorio.Listar();
        }

        public IList<ModelMercadoriaEntrada> ListaMercadoriasEntrada()
        {
            return repositorio.ListarEntrada();
        }
    }
}

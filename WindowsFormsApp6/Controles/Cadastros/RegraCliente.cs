using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp6.Modelos;
using WindowsFormsApp6.Modelos.Historico;
using WindowsFormsApp6.Repositorios.Cliente;

namespace WindowsFormsApp6.Controles.Cadastros
{
    public class RegraCliente
    {
        private RepositorioCliente repositorio = new RepositorioCliente();

        public bool Salvar(ModelCliente cliente)
        {
            try
            {
                repositorio.Salvar(cliente);

                string salvarExc = cliente.Ativo ? "Salvo" : "Excluido";

                MessageBox.Show($"{salvarExc} com sucesso");
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro na hora de salvar\n\n\n\n" + ex.Message.ToString());

                return false;
            }
        }

        public IList<ModelCliente> ListaClientes()
        {
            return repositorio.Listar();
        }

        public IList<ModelHistoricoCliente> ListaNotasHistoricas(Int64 id)
        {
            return repositorio.ListaNotasHistoricas(id).OrderByDescending(x => x.Data).ThenByDescending(y => y.Nota).ToList();
        }

        public IList<ModeloCidade> ListarCidades()
        {
            IList<ModeloCidade> cidades = new List<ModeloCidade>();

            cidades.Add(new ModeloCidade { Id = 1, Nome = "Assaí" });
            cidades.Add(new ModeloCidade { Id = 2, Nome = "São Sebastião da Amoreira" });
            cidades.Add(new ModeloCidade { Id = 3, Nome = "Santa Cecília do Pavão" });
            cidades.Add(new ModeloCidade { Id = 4, Nome = "Nova Santa Bárbara" });
            cidades.Add(new ModeloCidade { Id = 5, Nome = "Jataízinho" });
            cidades.Add(new ModeloCidade { Id = 6, Nome = "São Jerônimo da Serra" });
            cidades.Add(new ModeloCidade { Id = 7, Nome = "Sapopema" });
            cidades.Add(new ModeloCidade { Id = 8, Nome = "Londrina" });
            cidades.Add(new ModeloCidade { Id = 9, Nome = "Cambé" });

            return cidades;
        }
    }
}

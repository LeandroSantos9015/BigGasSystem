using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp6.Enumeradores;
using WindowsFormsApp6.Modelos;
using WindowsFormsApp6.Modelos.Movimentacao;
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

        public IList<ModelItemMovimentacao> ListaMercadoriasEntrada()
        {
            var ret = repositorio.ListarEntrada();

            IList<ModelItemMovimentacao> lista = new List<ModelItemMovimentacao>();

            foreach (var item in ret)
            {
                lista.Add(
                    new ModelItemMovimentacao
                    {
                        Descricao = item.Descricao,
                        ValorTotal = item.Quantidade * item.PrecoCusto,
                        IdMercadoria = item.Id,
                        Operacao = EOperacaoMovimento.Entrada,
                        PrecoCusto = item.PrecoCusto,
                        PrecoVenda = item.PrecoVenda,
                        Quantidade = item.Quantidade,
                        Status = EStatusMovimento.M
                    }
                );
            }

            return lista;
        }
    }
}

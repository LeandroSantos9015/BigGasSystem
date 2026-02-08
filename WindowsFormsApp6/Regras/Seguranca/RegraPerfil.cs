using System;
using System.Collections.Generic;
using WindowsFormsApp6.Modelos.Seguranca;
using WindowsFormsApp6.Repositorios.Seguranca;

namespace WindowsFormsApp6.Regras.Seguranca
{
    public class RegraPerfil
    {
        private RepositorioPerfil repositorio;

        public RegraPerfil()
        {
            repositorio = new RepositorioPerfil();
        }

        public void Salvar(ModelPerfil perfil)
        {
            if (string.IsNullOrWhiteSpace(perfil.Nome))
                throw new Exception("Nome do perfil não pode ser vazio");

            repositorio.Salvar(perfil);
        }

        public IList<ModelPerfil> Listar()
        {
            return repositorio.Listar();
        }

        public IList<ModelMenu> ListarPermissoes(long idPerfil)
        {
            if (idPerfil == 0)
                throw new Exception("Perfil inválido");

            return repositorio.ListarPermissoes(idPerfil);
        }

        public void SalvarPermissao(long idPerfil, int idMenu, bool visualizar)
        {
            if (idPerfil == 0)
                throw new Exception("Perfil inválido");

            if (idMenu == 0)
                throw new Exception("Menu inválido");

            repositorio.SalvarPermissao(idPerfil, idMenu, visualizar);
        }
    }
}

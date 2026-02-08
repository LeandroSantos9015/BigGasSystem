using System;
using System.Collections.Generic;
using WindowsFormsApp6.Modelos.Seguranca;
using WindowsFormsApp6.Repositorios.Seguranca;

namespace WindowsFormsApp6.Regras.Seguranca
{
    public class RegraUsuario
    {
        private RepositorioUsuario repositorio;

        public RegraUsuario()
        {
            repositorio = new RepositorioUsuario();
        }

        public ModelUsuario Autenticar(string login, string senha)
        {
            if (string.IsNullOrWhiteSpace(login))
                throw new Exception("Login não pode ser vazio");

            if (string.IsNullOrWhiteSpace(senha))
                throw new Exception("Senha não pode ser vazia");

            var usuario = repositorio.Autenticar(login, senha);

            if (usuario == null)
                throw new Exception("Login ou senha inválidos");

            return usuario;
        }

        public void Salvar(ModelUsuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Nome))
                throw new Exception("Nome não pode ser vazio");

            if (string.IsNullOrWhiteSpace(usuario.Login))
                throw new Exception("Login não pode ser vazio");

            if (usuario.Id == 0 && string.IsNullOrWhiteSpace(usuario.Senha))
                throw new Exception("Senha não pode ser vazia");

            if (usuario.IdPerfil == 0)
                throw new Exception("Perfil deve ser selecionado");

            repositorio.Salvar(usuario);
        }

        public IList<ModelUsuario> Listar()
        {
            return repositorio.Listar();
        }
    }
}

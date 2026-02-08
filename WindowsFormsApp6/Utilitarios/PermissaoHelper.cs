using System.Collections.Generic;
using System.Linq;
using WindowsFormsApp6.Modelos.Seguranca;
using WindowsFormsApp6.Regras.Seguranca;

namespace WindowsFormsApp6.Utilitarios
{
    public static class PermissaoHelper
    {
        private static List<ModelMenu> _permissoesCache;

        // Chaves dos menus (devem corresponder ao banco de dados)
        public const string MENU_CLIENTES = "MENU_CLIENTES";
        public const string MENU_MERCADORIAS = "MENU_MERCADORIAS";
        public const string MENU_ENTRADAS = "MENU_ENTRADAS";
        public const string MENU_SAIDAS = "MENU_SAIDAS";
        public const string MENU_CANCELAMENTO = "MENU_CANCELAMENTO";
        public const string MENU_CONFIGURACOES = "MENU_CONFIGURACOES";
        public const string MENU_RELATORIOS = "MENU_RELATORIOS";
        public const string MENU_IMPORTAR = "MENU_IMPORTAR";
        public const string MENU_PERFIS = "MENU_PERFIS";
        public const string MENU_USUARIOS = "MENU_USUARIOS";

        /// <summary>
        /// Carrega as permissões do usuário logado
        /// </summary>
        public static void CarregarPermissoes()
        {
            if (!ModelSessao.EstaLogado())
            {
                _permissoesCache = new List<ModelMenu>();
                return;
            }

            try
            {
                var regraPerfil = new RegraPerfil();
                _permissoesCache = regraPerfil.ListarPermissoes(ModelSessao.PerfilId).ToList();
            }
            catch
            {
                _permissoesCache = new List<ModelMenu>();
            }
        }

        /// <summary>
        /// Verifica se o usuário tem permissão para visualizar um menu
        /// </summary>
        public static bool TemPermissao(string chaveMenu)
        {
            if (!ModelSessao.EstaLogado())
                return false;

            if (_permissoesCache == null)
                CarregarPermissoes();

            var menu = _permissoesCache?.FirstOrDefault(m => m.Chave == chaveMenu);
            return menu != null && menu.Visualizar;
        }

        /// <summary>
        /// Limpa o cache de permissões (útil ao fazer logout)
        /// </summary>
        public static void LimparCache()
        {
            _permissoesCache = null;
        }
    }
}

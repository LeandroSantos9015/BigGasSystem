namespace WindowsFormsApp6.Modelos.Seguranca
{
    /// <summary>
    /// Classe estática para armazenar informações do usuário logado
    /// </summary>
    public static class ModelSessao
    {
        public static long UsuarioId { get; set; }
        public static string UsuarioNome { get; set; }
        public static string UsuarioLogin { get; set; }
        public static long PerfilId { get; set; }
        public static string PerfilNome { get; set; }
        
        public static void Limpar()
        {
            UsuarioId = 0;
            UsuarioNome = string.Empty;
            UsuarioLogin = string.Empty;
            PerfilId = 0;
            PerfilNome = string.Empty;
        }

        public static bool EstaLogado()
        {
            return UsuarioId > 0;
        }
    }
}

namespace WindowsFormsApp6.Modelos.Seguranca
{
    public class ModelMenu
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Chave { get; set; }
        public int Ordem { get; set; }
        public bool Visualizar { get; set; }
        public long IdPerfilMenu { get; set; }
    }
}

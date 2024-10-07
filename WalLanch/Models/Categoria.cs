namespace WalLanches.Models
{
    public class Categoria
    {
        public int CategoriraId{ get; set; }
        public string CategotiaNome { get; set; }
        public string Descricao { get; set; }

        public List<Lanche> Lanches { get; set; }
    }
}

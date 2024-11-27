namespace PIMFazendaUrbanaLib
{
    public class Cultivo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Variedade { get; set; }
        public int? TempoProdTradicional { get; set; }
        public int? TempoProdControlado { get; set; }
        public string Categoria { get; set; }
        public bool StatusAtivo { get; set; }
    }
}

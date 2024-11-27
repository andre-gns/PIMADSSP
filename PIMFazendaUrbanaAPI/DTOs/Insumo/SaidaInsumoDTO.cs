namespace PIMFazendaUrbanaAPI.DTOs
{
    public class SaidaInsumoDTO  // precisa atualizar com composição
    {
        public int Id { get; set; }
        public int Qtd { get; set; }
        public DateTime Data { get; set; }
        public int IdInsumo { get; set; }
        public string NomeInsumo { get; set; }
        public string CategoriaInsumo { get; set; }
        public string Unidqtd { get; set; }
    }
}

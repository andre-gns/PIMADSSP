namespace PIMFazendaUrbanaAPI.DTOs
{
    public class ExportacaoRequestDTO
    {
        public string Formato { get; set; } // "csv" ou "xlsx"
        public string NomeArquivo { get; set; }
        public List<object> Dados { get; set; } // Dados para exportação
    }
}

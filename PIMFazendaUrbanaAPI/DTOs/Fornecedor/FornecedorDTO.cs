namespace PIMFazendaUrbanaAPI.DTOs

{
    public class FornecedorDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string CNPJ { get; set; }
        public bool StatusAtivo { get; set; }
        public EnderecoDTO Endereco { get; set; }
        public TelefoneDTO Telefone { get; set; }
    }
}
namespace PIMFazendaUrbanaAPI.DTOs
{
    public class FuncionarioDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Sexo { get; set; }
        public string Email { get; set; }
        public string Cargo { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; } // senha existe aqui só pelo cadastro, não é retornada
        public bool StatusAtivo { get; set; }
        public EnderecoDTO Endereco { get; set; }
        public TelefoneDTO Telefone { get; set; }
    }
}

namespace PIMFazendaUrbanaLib
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Sexo { get; set; }
        public string Email { get; set; }
        public string Cargo { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public bool StatusAtivo { get; set; }
        public Endereco Endereco { get; set; }
        public Telefone Telefone { get; set; }
    }
}
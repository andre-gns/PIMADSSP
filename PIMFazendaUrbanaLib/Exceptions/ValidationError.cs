namespace PIMFazendaUrbanaLib
{
    public class ValidationError
    {
        public string Campo { get; set; }
        public string Mensagem { get; set; }

        public ValidationError(string campo, string mensagem)
        {
            Campo = campo;
            Mensagem = mensagem;
        }
    }
}

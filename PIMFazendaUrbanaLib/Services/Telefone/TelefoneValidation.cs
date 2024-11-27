namespace PIMFazendaUrbanaLib
{
    public class TelefoneValidation
    {
        // validação unificada para telefones
        public List<ValidationError> ValidarTelefone(Telefone telefone, List<ValidationError> erros)
        {
            if (string.IsNullOrEmpty(telefone.DDD) || telefone.DDD.Length != 2 || !telefone.DDD.All(char.IsDigit))
            {
                erros.Add(new ValidationError("DDD", "O DDD deve conter 2 dígitos"));
            }
            if (string.IsNullOrEmpty(telefone.Numero) || telefone.Numero.Length < 8 || telefone.Numero.Length > 9 || !telefone.Numero.All(char.IsDigit))
            {
                erros.Add(new ValidationError("Telefone", "O telefone deve conter 8 ou 9 dígitos"));
            }

            return erros;
        }

    }
}

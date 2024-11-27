using System.Text.RegularExpressions;

namespace PIMFazendaUrbanaLib
{
    public class EnderecoValidation
    {
        // validação unificada para endereços
        public List<ValidationError> ValidarEndereco(Endereco endereco, List<ValidationError> erros)
        {
            if (string.IsNullOrEmpty(endereco.Logradouro) || endereco.Logradouro.Length < 3)
            {
                erros.Add(new ValidationError("Logradouro", "O logradouro deve conter pelo menos 3 caracteres"));
            }
            if (string.IsNullOrEmpty(endereco.Numero) || !Regex.IsMatch(endereco.Numero, @"^\d+$"))
            {
                erros.Add(new ValidationError("Número", "O número deve conter apenas dígitos"));
            }
            if (string.IsNullOrEmpty(endereco.Bairro) || endereco.Bairro.Length < 3)
            {
                erros.Add(new ValidationError("Bairro", "O bairro deve conter pelo menos 3 caracteres"));
            }
            if (string.IsNullOrEmpty(endereco.Cidade) || endereco.Cidade.Length < 3)
            {
                erros.Add(new ValidationError("Cidade", "A cidade deve conter pelo menos 3 caracteres"));
            }
            if (string.IsNullOrEmpty(endereco.UF) || !Regex.IsMatch(endereco.UF, @"^(AC|AL|AP|AM|BA|CE|DF|ES|GO|MA|MT|MS|MG|PA|PB|PR|PE|PI|RJ|RN|RS|RO|RR|SC|SP|SE|TO)$"))
            {
                erros.Add(new ValidationError("UF", "A UF deve ser uma das siglas válidas"));
            }
            if (string.IsNullOrEmpty(endereco.CEP) || !Regex.IsMatch(endereco.CEP, @"^\d{8}$"))
            {
                erros.Add(new ValidationError("CEP", "O CEP deve conter 8 dígitos"));
            }

            return erros;
        }

    }
}

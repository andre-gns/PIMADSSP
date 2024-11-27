using System.Text.RegularExpressions;

namespace PIMFazendaUrbanaLib
{
    public class FornecedorService : IFornecedorService
    {
        private readonly IFornecedorDAO fornecedorDAO;

        public FornecedorService(string connectionString) // construtor  atualizado para receber a connection string como parâmetro
        {
            this.fornecedorDAO = new FornecedorDAO(connectionString); // repassa a connection string para o construtor do FornecedorDAO
        }

        public List<Fornecedor> ListarFornecedoresComFiltros(string search)
        {
            try
            {
                List<Fornecedor> fornecedores = fornecedorDAO.ListarFornecedoresComFiltros(search);
                return fornecedores; // Retorna a lista filtrada de fornecedores
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar fornecedores filtrados: " + ex.Message); // Lança uma exceção indicando que ocorreu um erro ao listar fornecedores filtrados
            }
        }

        public void CadastrarFornecedor(Fornecedor fornecedor)
        {
            try
            {
                ValidarFornecedor(fornecedor); // Chama o método dedicado para validar os dados do fornecedor
                fornecedorDAO.CadastrarFornecedor(fornecedor); // Chama o método CadastrarFornecedor do DAO para inserir o novo fornecedor no banco de dados, passando o objeto fornecedor como argumento
            }
            catch (ValidationException ex) // Se ocorrer uma exceção do tipo ValidationException
            {
                throw; // Repassa a exceção de validação para o Controller manipular
            }
            catch (Exception ex) // Se ocorrer uma exceção de qualquer outro tipo
            {
                throw new Exception("Erro ao cadastrar fornecedor: " + ex.Message);
            }
        }

        public void AlterarFornecedor(Fornecedor fornecedor)
        {
            try
            {
                ValidarFornecedor(fornecedor); // Chama o método dedicado para validar os dados do fornecedor
                fornecedorDAO.AlterarFornecedor(fornecedor); // Chama o método AlterarFornecedor do DAO para alterar os dados do fornecedor no banco de dados, passando o objeto fornecedor como argumento
            }
            catch (ValidationException ex) // Se ocorrer uma exceção do tipo ValidationException
            {
                throw; // Repassa a exceção de validação para o Controller manipular
            }
            catch (Exception ex) // Se ocorrer uma exceção de qualquer outro tipo
            {
                throw new Exception("Erro ao editar fornecedor: " + ex.Message); // Lança uma exceção indicando que ocorreu um erro ao alterar o fornecedor
            }
        }

        public void ExcluirFornecedor(int fornecedorId)
        {
            try
            {
                fornecedorDAO.ExcluirFornecedor(fornecedorId); // Chama o método ExcluirFornecedor da classe FornecedorDAO, passando o ID do fornecedor como argumento
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir fornecedor: " + ex.Message); // Lança uma exceção indicando que ocorreu um erro ao excluir o fornecedor
            }
        }

        public List<Fornecedor> ListarFornecedoresAtivos()
        {
            try
            {
                List<Fornecedor> fornecedores = fornecedorDAO.ListarFornecedoresAtivos();
                return fornecedores; // Retorna a lista de fornecedores quando tudo corre bem
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar fornecedores ativos: " + ex.Message); // Lança uma exceção indicando que ocorreu um erro ao listar fornecedores ativos
            }
        }

        public List<Fornecedor> ListarFornecedoresInativos()
        {
            try
            {
                List<Fornecedor> fornecedoresInativos = fornecedorDAO.ListarFornecedoresInativos();
                return fornecedoresInativos; // Retorna a lista de fornecedores inativos quando tudo corre bem
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar fornecedores inativos: " + ex.Message); // Lança uma exceção indicando que ocorreu um erro ao listar fornecedores inativos
            }
        }

        public Fornecedor ConsultarFornecedorPorID(int fornecedorId)
        {
            try
            {
                Fornecedor fornecedor = fornecedorDAO.ConsultarFornecedorPorID(fornecedorId); // Chama o método ConsultarFornecedor da classe FornecedorDAO para obter os dados de um fornecedor pelo ID
                return fornecedor; // Retorna o fornecedor encontrado
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao consultar fornecedor: " + ex.Message); // Lança uma exceção indicando que ocorreu um erro ao consultar o fornecedor
            }
        }

        public Fornecedor ConsultarFornecedorPorNome(string fornecedorNome)
        {
            try
            {
                Fornecedor fornecedor = fornecedorDAO.ConsultarFornecedorPorNome(fornecedorNome); // Chama o método ConsultarFornecedor da classe FornecedorDAO para obter os dados de um fornecedor pelo nome
                return fornecedor; // Retorna o fornecedor encontrado
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao consultar fornecedor: " + ex.Message); // Lança uma exceção indicando que ocorreu um erro ao consultar o fornecedor
            }
        }

        public Fornecedor ConsultarFornecedorPorCNPJ(string fornecedorCNPJ)
        {
            try
            {
                Fornecedor fornecedor = fornecedorDAO.ConsultarFornecedorPorCNPJ(fornecedorCNPJ); // Chama o método ConsultarFornecedor da classe FornecedorDAO para obter os dados de um fornecedor pelo cnpj
                return fornecedor; // Retorna o fornecedor encontrado
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao consultar fornecedor: " + ex.Message); // Lança uma exceção indicando que ocorreu um erro ao consultar o fornecedor
            }
        }

        public List<Fornecedor> FiltrarFornecedoresPorNome(string fornecedorNome)
        {
            try
            {
                List<Fornecedor> fornecedores = fornecedorDAO.FiltrarFornecedoresPorNome(fornecedorNome);
                return fornecedores;
            }
            catch (Exception ex)
            {
                return new List<Fornecedor>();
                throw new Exception("Erro ao filtrar fornecedores por nome: " + ex.Message);
            }
        }

        public void ValidarFornecedor(Fornecedor fornecedor) // método para validação dos dados do fornecedor contendo as regras de negócio e mensagens de erro
        {
            var erros = new List<ValidationError>();

            if (string.IsNullOrEmpty(fornecedor.Nome) || fornecedor.Nome.Length < 3)
            {
                erros.Add(new ValidationError("Nome", "O nome deve ter pelo menos 3 caracteres"));
            }
            if (!Regex.IsMatch(fornecedor.CNPJ, @"^\d{14}$") || !fornecedor.CNPJ.All(char.IsDigit))
            {
                erros.Add(new ValidationError("CNPJ", "O CNPJ deve conter 14 dígitos"));
            }
            if (!Regex.IsMatch(fornecedor.Email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                erros.Add(new ValidationError("Email", "Email inválido. O email deve ter o formato exemplo@exemplo.exeplo"));
            }
            TelefoneValidation telefoneValidation = new TelefoneValidation();
            telefoneValidation.ValidarTelefone(fornecedor.Telefone, erros);
            EnderecoValidation enderecoValidation = new EnderecoValidation();
            enderecoValidation.ValidarEndereco(fornecedor.Endereco, erros);

            if (erros.Any()) // se teve algum erro, lança exceção com a lista de erros
            {
                throw new ValidationException(erros);
            }
        }
    }
}
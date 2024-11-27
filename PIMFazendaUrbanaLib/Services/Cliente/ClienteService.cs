using System.Text.RegularExpressions;

namespace PIMFazendaUrbanaLib
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteDAO clienteDAO;

        public ClienteService(string connectionString) // construtor  atualizado para receber a connection string como parâmetro
        {
            this.clienteDAO = new ClienteDAO(connectionString); // repassa a connection string para o construtor do ClienteDAO
        }

        public List<Cliente> ListarClientesComFiltros(string search)
        {
            try
            {
                List<Cliente> clientes = clienteDAO.ListarClientesComFiltros(search);
                return clientes; // Retorna a lista filtrada de clientes
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar clientes filtrados: " + ex.Message); // Lança uma exceção indicando que ocorreu um erro ao listar clientes filtrados
            }
        }

        public void CadastrarCliente(Cliente cliente)
        {
            try
            {
                ValidarCliente(cliente); // Chama o método dedicado para validar os dados do cliente
                clienteDAO.CadastrarCliente(cliente); // Chama o método CadastrarCliente do DAO para inserir o novo cliente no banco de dados, passando o objeto cliente como argumento
            }
            catch (ValidationException ex) // Se ocorrer uma exceção do tipo ValidationException
            {
                throw; // Repassa a exceção de validação para o Controller manipular
            }
            catch (Exception ex) // Se ocorrer uma exceção de qualquer outro tipo
            {
                throw new Exception("Erro ao cadastrar cliente: " + ex.Message);
            }
        }

        public void AlterarCliente(Cliente cliente)
        {
            try
            {
                ValidarCliente(cliente); // Chama o método dedicado para validar os dados do cliente
                clienteDAO.AlterarCliente(cliente); // Chama o método AlterarCliente do DAO para alterar os dados do cliente no banco de dados, passando o objeto cliente como argumento
            }
            catch (ValidationException ex) // Se ocorrer uma exceção do tipo ValidationException
            {
                throw; // Repassa a exceção de validação para o Controller manipular
            }
            catch (Exception ex) // Se ocorrer uma exceção de qualquer outro tipo
            {
                throw new Exception("Erro ao editar cliente: " + ex.Message); // Lança uma exceção indicando que ocorreu um erro ao alterar o cliente
            }
        }

        public void ExcluirCliente(int clienteId)
        {
            try
            {
                clienteDAO.ExcluirCliente(clienteId); // Chama o método ExcluirCliente da classe ClienteDAO, passando o ID do cliente como argumento
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir cliente: " + ex.Message); // Lança uma exceção indicando que ocorreu um erro ao excluir o cliente
            }
        }

        public List<Cliente> ListarClientesAtivos()
        {
            try
            {
                List<Cliente> clientes = clienteDAO.ListarClientesAtivos();
                return clientes; // Retorna a lista de clientes quando tudo corre bem
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar clientes ativos: " + ex.Message); // Lança uma exceção indicando que ocorreu um erro ao listar clientes ativos
            }
        }

        public List<Cliente> ListarClientesInativos()
        {
            try
            {
                List<Cliente> clientesInativos = clienteDAO.ListarClientesInativos();
                return clientesInativos; // Retorna a lista de clientes inativos quando tudo corre bem
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar clientes inativos: " + ex.Message); // Lança uma exceção indicando que ocorreu um erro ao listar clientes inativos
            }
        }

        public Cliente ConsultarClientePorID(int clienteId)
        {
            try
            {
                Cliente cliente = clienteDAO.ConsultarClientePorID(clienteId); // Chama o método ConsultarCliente da classe ClienteDAO para obter os dados de um cliente pelo ID
                return cliente; // Retorna o cliente encontrado
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao consultar cliente: " + ex.Message); // Lança uma exceção indicando que ocorreu um erro ao consultar o cliente
            }
        }

        public Cliente ConsultarClientePorNome(string clienteNome)
        {
            try
            {
                Cliente cliente = clienteDAO.ConsultarClientePorNome(clienteNome); // Chama o método ConsultarCliente da classe ClienteDAO para obter os dados de um cliente pelo nome
                return cliente; // Retorna o cliente encontrado
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao consultar cliente: " + ex.Message); // Lança uma exceção indicando que ocorreu um erro ao consultar o cliente
            }
        }

        public Cliente ConsultarClientePorCNPJ(string clienteCNPJ)
        {
            try
            {
                Cliente cliente = clienteDAO.ConsultarClientePorCNPJ(clienteCNPJ); // Chama o método ConsultarCliente da classe ClienteDAO para obter os dados de um cliente pelo cnpj
                return cliente; // Retorna o cliente encontrado
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao consultar cliente: " + ex.Message); // Lança uma exceção indicando que ocorreu um erro ao consultar o cliente
            }
        }

        public List<Cliente> FiltrarClientesPorNome(string clienteNome)
        {
            try
            {
                List<Cliente> clientes = clienteDAO.FiltrarClientesPorNome(clienteNome);
                return clientes;
            }
            catch (Exception ex)
            {
                return new List<Cliente>();
                throw new Exception("Erro ao filtrar clientes por nome: " + ex.Message);
            }
        }

        public void ValidarCliente(Cliente cliente) // método para validação dos dados do cliente contendo as regras de negócio e mensagens de erro
        {
            var erros = new List<ValidationError>();

            if (string.IsNullOrEmpty(cliente.Nome) || cliente.Nome.Length < 3)
            {
                erros.Add(new ValidationError("Nome", "O nome deve ter pelo menos 3 caracteres"));
            }
            if (!Regex.IsMatch(cliente.CNPJ, @"^\d{14}$") || !cliente.CNPJ.All(char.IsDigit))
            {
                erros.Add(new ValidationError("CNPJ", "O CNPJ deve conter 14 dígitos"));
            }
            if (!Regex.IsMatch(cliente.Email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                erros.Add(new ValidationError("Email", "Email inválido. O email deve ter o formato exemplo@exemplo.exeplo"));
            }
            TelefoneValidation telefoneValidation = new TelefoneValidation();
            telefoneValidation.ValidarTelefone(cliente.Telefone, erros);
            EnderecoValidation enderecoValidation = new EnderecoValidation();
            enderecoValidation.ValidarEndereco(cliente.Endereco, erros);

            if (erros.Any()) // se teve algum erro, lança exceção com a lista de erros
            {
                throw new ValidationException(erros);
            }
        }
    }
}
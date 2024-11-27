namespace PIMFazendaUrbanaLib
{
    public class CultivoService : ICultivoService
    {
        private readonly ICultivoDAO cultivoDAO;

        public CultivoService(string connectionString)
        {
            this.cultivoDAO = new CultivoDAO(connectionString);
        }

        public List<Cultivo> ListarCultivosComFiltros(string search)
        {
            try
            {
                List<Cultivo> cultivos = cultivoDAO.ListarCultivosComFiltros(search);
                return cultivos;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar cultivos filtrados: " + ex.Message);
            }
        }

        // 1- Cadastrar Cultivo
        public void CadastrarCultivo(Cultivo cultivo)
        {
            try
            {
                ValidarCultivo(cultivo); // <--- Validação do Cultivo <---
                cultivoDAO.CadastrarCultivo(cultivo); // Chama o método CadastrarCultivo do DAO para inserir o novo cultivo no banco de dados, passando o objeto cultivo como argumento
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao cadastrar cultivo: " + ex.Message);
            }
        }

        // 2- Alterar Cultivo
        public void AlterarCultivo(Cultivo cultivo)
        {
            try
            {
                ValidarCultivo(cultivo); // <--- Validação do Cultivo <---
                cultivoDAO.AlterarCultivo(cultivo); // Chama o método AlterarCultivo do DAO para atualizar os dados do cultivo no banco de dados
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao editar cultivo: " + ex.Message);
            }
        }

        // 3- Excluir (DESATIVAR) Cultivo
        public void ExcluirCultivo(int cultivoId)
        {
            try
            {
                cultivoDAO.ExcluirCultivo(cultivoId); // Chama o método ExcluirCultivo do DAO para desativar o cultivo no banco de dados
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir cultivo: " + ex.Message);
            }
        }

        // 4- Listagem
        // 4.1- Listar Cultivos Ativos
        public List<Cultivo> ListarCultivosAtivos()
        {
            try
            {
                List<Cultivo> cultivos = cultivoDAO.ListarCultivosAtivos(); // Chama o método ListarCultivosAtivos do DAO para obter a lista de cultivos ativos
                return cultivos; // Retorna a lista de cultivos ativos quando tudo corre bem
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar cultivos ativos: " + ex.Message);
            }
        }

        // 4.2- Listar Cultivos Inativos
        public List<Cultivo> ListarCultivosInativos()
        {
            try
            {
                List<Cultivo> cultivosInativos = cultivoDAO.ListarCultivosInativos(); // Chama o método ListarCultivosInativos do DAO para obter a lista de cultivos inativos
                return cultivosInativos; // Retorna a lista de cultivos inativos quando tudo corre bem
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar cultivos inativos: " + ex.Message);
            }
        }

        // 4.3 - Listar Categorias
        public List<string> ListarCategorias()
        {
            try
            {
                return cultivoDAO.ListarCategorias();
            }
            catch (Exception ex)
            {
                return new List<string>();
                throw new Exception("Erro ao listar categorias: " + ex.Message);
            }
        }

        // 5- Consulta
        // 5.1 - Consultar Cultivo por ID
        public Cultivo ConsultarCultivoPorID(int cultivoId)
        {
            try
            {
                Cultivo cultivo = cultivoDAO.ConsultarCultivoPorID(cultivoId); // Chama o método ConsultarCultivoID do DAO para obter os dados de um cultivo pelo ID
                return cultivo; // Retorna o cultivo encontrado
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao consultar cultivo: " + ex.Message);
            }
        }

        // 5.2 - Consultar Cultivo por nome
        public Cultivo ConsultarCultivoNome(string cultivoNome)
        {
            try
            {
                Cultivo cultivo = cultivoDAO.ConsultarCultivoNome(cultivoNome); // Chama o método ConsultarCultivoNome do DAO para obter os dados de um cultivo pelo nome
                return cultivo; // Retorna o cultivo encontrado
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao consultar cultivo: " + ex.Message);
            }
        }

        // 6- Filtragem
        // 6.1 - Filtrar lista de cultivos por nome
        public List<Cultivo> FiltrarCultivosNome(string cultivoNome)
        {
            try
            {
                return cultivoDAO.FiltrarCultivosNome(cultivoNome);
            }
            catch (Exception ex)
            {
                return new List<Cultivo>();
                throw new Exception("Erro ao filtrar cultivos por nome: " + ex.Message);
            }

        }

        //=-=-=-=-=-=-=-=-=-=-=-=- VALIDAÇÃO CULTIVO =-=-=-=-=-=-=-=-=-=-=-=-

        public void ValidarCultivo(Cultivo cultivo)
        {
            var erros = new List<ValidationError>();
            if (cultivo.Nome.Length < 3)
            {
                erros.Add((new ValidationError("Nome", "O nome do produto deve conter pelo menos 3 caracteres.")));
            }
            if (cultivo.Variedade.Length < 3)
            {
                erros.Add((new ValidationError("Variedade", "A variedade deve conter pelo menos 3 caracteres.")));
            }

            // Lista de categorias permitidas
            List<string> categoriasPermitidas = new List<string>
            {
                "Verdura",
                "Legume",
                "Fruta",
                "Outro"
            };
            // Verifica se a categoria está na lista de permitidas
            if (cultivo.Categoria == null || !categoriasPermitidas.Contains(cultivo.Categoria))
            {
                erros.Add((new ValidationError("Categoria", "Por favor, selecione uma categoria válida.")));
            }

            string tempoTradicional = cultivo.TempoProdTradicional.ToString();
            if (string.IsNullOrWhiteSpace(tempoTradicional) || !int.TryParse(tempoTradicional, out _) || int.Parse(tempoTradicional) <= 0)
            {
                erros.Add((new ValidationError("Tempo Prod. Tradicional", "Por favor, insira um valor válido para o tempo de plantio tradicional (número inteiro).")));
            }

            string tempoControlado = cultivo.TempoProdControlado.ToString();
            if (string.IsNullOrWhiteSpace(tempoControlado) || !int.TryParse(tempoControlado, out _) || int.Parse(tempoControlado) <= 0)
            {
                erros.Add((new ValidationError("Tempo Prod. Controlado", "Por favor, insira um valor válido para o tempo de plantio controlado (número inteiro).")));
            }

            if (erros.Any()) // se teve algum erro, lança exceção com a lista de erros
            {
                throw new ValidationException(erros);
            }
        }

    }
}

namespace PIMFazendaUrbanaLib
{
    public class InsumoService : IInsumoService
    {
        private readonly IInsumoDAO insumoDAO;

        public InsumoService(string connectionString)
        {
            this.insumoDAO = new InsumoDAO(connectionString);
        }

        public List<Insumo> ListarInsumosComFiltros(string search)
        {
            try
            {
                List<Insumo> insumos = insumoDAO.ListarInsumosComFiltros(search);
                return insumos;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar insumos filtrados: " + ex.Message);
            }
        }

        public void CadastrarInsumo(Insumo insumo)
        {
            try
            {
                this.ValidarInsumo(insumo); // <--- Validação para cadastrar um insumo <---
                insumoDAO.CadastrarInsumo(insumo);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao cadastrar insumo: " + ex.Message);
            }
        }

        public void AlterarInsumo(Insumo insumo)
        {
            try
            {
                this.ValidarInsumo(insumo); // <--- Validação para alterar um insumo (Geralmente são os mesmos) <---
                insumoDAO.AlterarInsumo(insumo);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao alterar insumo: " + ex.Message);
            }
        }

        public Insumo ConsultarInsumoPorID(int insumoID)
        {
            try
            {
                Insumo insumo = insumoDAO.ConsultarInsumoPorID(insumoID);
                return insumo;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao consultar insumo por ID: " + ex.Message);
            }
        }

        public List<Insumo> ListarInsumosAtivos()
        {
            try
            {
                List<Insumo> insumos = insumoDAO.ListarInsumosAtivos();
                return insumos;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar insumos ativos: " + ex.Message);
            }
        }

        public List<Insumo> ListarInsumosEmEstoque()
        {
            try
            {
                List<Insumo> insumos = insumoDAO.ListarInsumosEmEstoque();
                return insumos;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar insumos em estoque: " + ex.Message);
            }
        }

        public List<Insumo> ListarInsumosInativos()
        {
            try
            {
                List<Insumo> insumosInativos = insumoDAO.ListarInsumosInativos();
                return insumosInativos;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar insumos inativos: " + ex.Message);
            }
        }

        public void DesativarInsumo(int insumoID)
        {
            try
            {
                insumoDAO.DesativarInsumo(insumoID);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao desativar insumo: " + ex.Message);
            }
        }

        public List<Insumo> FiltrarInsumosNome(string insumoNome)
        {
            try
            {
                List<Insumo> insumos = insumoDAO.FiltrarInsumosNome(insumoNome);
                return insumos;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao filtrar insumos por nome: " + ex.Message);
            }
        }

        public List<SaidaInsumo> ListarSaidaInsumos()
        {
            try
            {
                List<SaidaInsumo> saidainsumos = insumoDAO.ListarSaidaInsumos();
                return saidainsumos;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar registros de saída de insumos: " + ex.Message);
            }
        }

        public List<SaidaInsumo> FiltrarSaidaInsumosNome(string insumoNome)
        {
            try
            {
                List<SaidaInsumo> saidainsumos = insumoDAO.FiltrarSaidaInsumosNome(insumoNome);
                return saidainsumos;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao filtrar registros de saída de insumos: " + ex.Message);
            }
        }

        // Método para filtrar insumos pela unidade
        public List<Insumo> FiltrarInsumosPorUnidade(string unidade)
        {
            try
            {
                return insumoDAO.FiltrarInsumosPorUnidade(unidade);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao filtrar insumos pela unidade: " + ex.Message);
            }
        }

        // Método para obter a categoria do insumo pelo nome
        public string ObterCategoriaPorNomeInsumo(string nomeInsumo)
        {
            try
            {
                return insumoDAO.ObterCategoriaPorNomeInsumo(nomeInsumo);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter categoria do insumo pelo nome: " + ex.Message);
            }
        }

        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-

        public void CadastrarSaidaInsumo(SaidaInsumo saidainsumo, Insumo insumo)
        {
            try
            {
                this.ValidarSaidaInsumo(saidainsumo, insumo); // <--- Validação para cadastrar insumo <---
                insumoDAO.CadastrarSaidaInsumo(saidainsumo, insumo);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao cadastrar saída de insumo: " + ex.Message);
            }
        }

        public bool AumentarQtdInsumo(Insumo insumo, int qtd)
        {
            try
            {
                insumoDAO.AumentarQtdInsumo(insumo, qtd);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao alterar quantidade do insumo: " + ex.Message);
            }
        }

        public List<SaidaInsumo> FiltrarSaidaInsumosPorNomeEPeriodo(string insumoNome, DateTime dataInicio, DateTime dataFim)
        {
            try
            {
                return insumoDAO.FiltrarSaidaInsumosPorNomeEPeriodo(insumoNome, dataInicio, dataFim);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao filtrar registros de saída por nome de insumo e período: " + ex.Message);
            }
        }

        // =-=-=-=-=-=-=-=-=-=-=-=- VALIDAÇÃO INSUMO =-=-=-=-=-=-=-=-=-=-=-=-
        public void ValidarInsumo(Insumo insumo)
        {
            var erros = new List<ValidationError>();
            if (string.IsNullOrEmpty(insumo.Nome) || insumo.Nome.Length < 3) //Valida se o nome é maior que 3 caracteres
            {
                erros.Add(new ValidationError("Nome", "O nome deve ter pelo menos 3 caracteres"));
            }

            List<string> categoriasPermitidas = new List<string> { "Sementes", "Fertilizantes" };
            if (insumo.Categoria == null || !categoriasPermitidas.Contains(insumo.Categoria)) //Valida se a categoria é "Sementes" ou "Fertilizantes"
            {
                erros.Add(new ValidationError("Categoria", "Selecione uma categoria válida (Sementes ou Fertilizantes)."));
            }

            // Lista de unidades permitidas
            List<string> unidadesPermitidas = new List<string>
            {
                "kg", "g", "l", "ml", "m", "cm", "mm", "unidade", "caixa", "tambor"
            };
            if (insumo.Unidqtd == null || !unidadesPermitidas.Contains(insumo.Unidqtd)) //Valida se a unidade está na lista de permitidas
            {
                erros.Add(new ValidationError("Unidade", "Selecione uma unidade válida."));
            }

            if (erros.Any()) // se teve algum erro, lança exceção com a lista de erros
            {
                throw new ValidationException(erros);
            }

        }

        // =-=-=-=-=-=-=-=-=-=-=-=- VALIDAÇÃO SAIDA INSUMO =-=-=-=-=-=-=-=-=-=-=-=-
        public void ValidarSaidaInsumo(SaidaInsumo saidainsumo, Insumo insumo)
        {
            var erros = new List<ValidationError>();
            if (saidainsumo.Qtd <= 0)
            {
                erros.Add(new ValidationError("Quantidade", "A quantidade deve ser um número inteiro maior que zero."));
            }

            if (saidainsumo.Qtd > insumo.Qtd)
            {
                erros.Add(new ValidationError("Quantidade", "A quantidade de saída deve ser menor ou igual à quantidade atual no estoque."));
            }

            if (erros.Any()) // se teve algum erro, lança exceção com a lista de erros
            {
                throw new ValidationException(erros);
            }
        }

    }
}

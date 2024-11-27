namespace PIMFazendaUrbanaLib
{
    public class ProducaoService : IProducaoService
    {
        private readonly IProducaoDAO producaoDAO;

        public ProducaoService(string connectionString)
        {
            this.producaoDAO = new ProducaoDAO(connectionString);
        }

        public List<Producao> ListarProducoesComFiltros(string search)
        {
            try
            {
                List<Producao> producoes = producaoDAO.ListarProducoesComFiltros(search);
                return producoes;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar produções filtradas: " + ex.Message);
            }
        }

        // 1 - MÉTODO CADASTRAR PRODUCAO
        public void CadastrarProducao(Producao producao)
        {
            try
            {
                ValidarProducao(producao); // <--- Validação da Produção <---
                producaoDAO.CadastrarProducao(producao);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao cadastrar produção: " + ex.Message);
            }
        }

        // 2 - MÉTODO ALTERAR PRODUCAO
        public void AlterarProducao(Producao producao)
        {
            try
            {
                ValidarProducao(producao); // <--- Validação da Produção <---
                producaoDAO.AlterarProducao(producao);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao alterar produção: " + ex.Message);
            }
        }

        // 3 - MÉTODO FINALIZAR PRODUCAO
        public void FinalizarProducao(int producaoId)
        {
            try
            {
                producaoDAO.FinalizarProducao(producaoId);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao finalizar produção: " + ex.Message);
            }
        }

        // 4 - Listagem
        // 4.1 - MÉTODO LISTAR PRODUCOES
        public List<Producao> ListarProducoes()
        {
            try
            {
                List<Producao> producoes = producaoDAO.ListarProducoes();
                return producoes; // Retorna a lista de produções quando tudo corre bem
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar produções: " + ex.Message); // Lança uma exceção indicando que ocorreu um erro
            }
        }

        // 4.2 - MÉTODO LISTAR PRODUCOES (NÃO FINALIZADAS)
        public List<Producao> ListarProducoesNaoFinalizadas()
        {
            try
            {
                List<Producao> producoes = producaoDAO.ListarProducoesNaoFinalizadas();
                return producoes; // Retorna a lista de produções quando tudo corre bem
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar produções: " + ex.Message); // Lança uma exceção indicando que ocorreu um erro
            }
        }

        // 4.3 - MÉTODO FILTRAR PRODUCOES POR NOME DE CULTIVO
        public List<Producao> FiltrarProducoesPorNome(string nomeCultivo)
        {
            try
            {
                List<Producao> producoes = producaoDAO.FiltrarProducoesPorNome(nomeCultivo);
                return producoes; // Retorna a lista de produções quando tudo corre bem
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao filtrar produções por nome de cultivo: " + ex.Message); // Lança uma exceção indicando que ocorreu um erro
            }
        }

        // 5 - MÉTODO CONSULTAR PRODUCAO POR ID
        public Producao ConsultarProducaoPorID(int producaoId)
        {
            try
            {
                Producao producao = producaoDAO.ConsultarProducaoPorID(producaoId);
                return producao;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao consultar produção: " + ex.Message);
            }
        }

        public List<Producao> FiltrarProducoesPorNomeEPeriodo(string cultivoNome, DateTime dataInicio, DateTime dataFim)
        {
            try
            {
                return producaoDAO.FiltrarProducoesPorNomeEPeriodo(cultivoNome, dataInicio, dataFim);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao filtrar produções por nome de cultivo e período: " + ex.Message);
            }
        }

        // =-=-=-=-=-=-=-=-=-=-=-=- VALIDAÇAÕ PRODUÇÃO =-=-=-=-=-=-=-=-=-=-=-=-

        public void ValidarProducao(Producao producao)
        {
            var erros = new List<ValidationError>();

            // Lista de unidades permitidas
            List<string> unidadesPermitidas = new List<string>
            {
                "kg", "g", "unidade"
            };

            // Verifica se a unidade está na lista de permitidas
            if (producao.Unidqtd == null || !unidadesPermitidas.Contains(producao.Unidqtd))
            {
                erros.Add(new ValidationError("Unidade", "Por favor, selecione uma unidade válida."));
            }

            // Verifica se a quantidade é válida
            if (producao.Qtd <= 0)
            {
                erros.Add(new ValidationError("Quantidade", "A quantidade deve ser um número inteiro maior que zero."));
            }
        }

    }
}
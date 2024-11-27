using System.Text.RegularExpressions;

namespace PIMFazendaUrbanaLib
{
    public class RecomendacaoService : IRecomendacaoService
    {
        private readonly IRecomendacaoDAO recomendacaoDAO;
        public RecomendacaoService(string connectionString)
        {
            this.recomendacaoDAO = new RecomendacaoDAO(connectionString);
        }

        public List<Cultivo> GerarRecomendacao(string regiao, string estacao, bool ambienteControlado)
        {
            try
            {
                ValidarRecomendacao(regiao, estacao, ambienteControlado);

                if (ambienteControlado == true)
                {
                    estacao = InverterEstacoes(estacao);
                }

                List<Cultivo> recomendacoes = recomendacaoDAO.GerarRecomendacao(regiao, estacao);

                return recomendacoes;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar cultivos: " + ex.Message);
            }
        }

        public void ValidarRecomendacao(string regiao, string estacao, bool ambienteControlado)
        {
            var erros = new List<ValidationError>();

            string[] estacoesAceitas = { "Verão", "Outono", "Inverno", "Primavera" };

            // Verificando se a estação selecionada está na lista de estações aceitas, ignorando a diferença de caixa alta/baixa
            if (!estacoesAceitas.Any(r => string.Equals(r, estacao, StringComparison.OrdinalIgnoreCase)))
            {
                erros.Add(new ValidationError("Estação", "Estação inválida"));
            }

            string[] regioesAceitas = { "Norte", "Nordeste", "Sul", "Sudeste", "Centro-Oeste" };

            // Verificando se a região selecionada está na lista de regiões aceitas, ignorando a diferença de caixa alta/baixa
            if (!regioesAceitas.Any(r => string.Equals(r, regiao, StringComparison.OrdinalIgnoreCase)))
            {
                erros.Add(new ValidationError("Região", "Região inválida"));
            }

            if (erros.Any()) // se teve algum erro, lança exceção com a lista de erros
            {
                throw new ValidationException(erros);
            }
        }

        public string InverterEstacoes(string estacao)
        {
            switch (estacao)
            {
                case "Verão":
                    estacao = "Inverno";
                    break;
                case "Outono":
                    estacao = "Primavera";
                    break;
                case "Inverno":
                    estacao = "Verão";
                    break;
                case "Primavera":
                    estacao = "Outono";
                    break;
                default:
                    break;
            }
            return estacao;
        }






    }
}

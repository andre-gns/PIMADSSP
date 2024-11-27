namespace PIMFazendaUrbanaLib
{
    public interface IRecomendacaoService
    {
        List<Cultivo> GerarRecomendacao(string regiao, string estacao, bool ambienteControlado);
        void ValidarRecomendacao(string regiao, string estacao, bool ambienteControlado);
        string InverterEstacoes(string estacao);
    }
}
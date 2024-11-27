namespace PIMFazendaUrbanaLib
{
    public interface IRecomendacaoDAO
    {
        List<Cultivo> GerarRecomendacao(string regiao, string estacao);
    }
}
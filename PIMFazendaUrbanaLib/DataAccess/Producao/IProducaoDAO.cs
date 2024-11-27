namespace PIMFazendaUrbanaLib
{
    public interface IProducaoDAO
    {
        List<Producao> ListarProducoesComFiltros(string search);
        void CadastrarProducao(Producao producao);
        void AlterarProducao(Producao producao);
        void FinalizarProducao(int producaoId);
        List<Producao> ListarProducoes();
        List<Producao> ListarProducoesNaoFinalizadas();
        List<Producao> FiltrarProducoesPorNome(string cultivoNome);
        List<Producao> FiltrarProducoesPorNomeEPeriodo(string cultivoNome, DateTime dataInicio, DateTime dataFim);
        Producao ConsultarProducaoPorID(int producaoId);
    }
}
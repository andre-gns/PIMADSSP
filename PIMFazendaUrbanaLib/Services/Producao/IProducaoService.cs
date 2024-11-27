namespace PIMFazendaUrbanaLib
{
    public interface IProducaoService
    {
        List<Producao> ListarProducoesComFiltros(string search);
        void CadastrarProducao(Producao producao);
        void AlterarProducao(Producao producao);
        void FinalizarProducao(int producaoId);
        List<Producao> ListarProducoes();
        List<Producao> ListarProducoesNaoFinalizadas();
        List<Producao> FiltrarProducoesPorNome(string cultivoNome);
        Producao ConsultarProducaoPorID(int producaoId);
        List<Producao> FiltrarProducoesPorNomeEPeriodo(string cultivoNome, DateTime dataInicio, DateTime dataFim);
        void ValidarProducao(Producao producao);
    }
}
namespace PIMFazendaUrbanaLib
{
    public interface IInsumoDAO
    {
        List<Insumo> ListarInsumosComFiltros(string search);
        void CadastrarInsumo(Insumo insumo);
        void AlterarInsumo(Insumo insumo);
        void DesativarInsumo(int idInsumo);
        List<Insumo> ListarInsumosAtivos();
        List<Insumo> ListarInsumosEmEstoque();
        List<SaidaInsumo> ListarSaidaInsumos();
        List<SaidaInsumo> FiltrarSaidaInsumosNome(string insumoNome);
        List<Insumo> ListarInsumosInativos();
        Insumo ConsultarInsumoPorID(int idInsumo);
        List<Insumo> FiltrarInsumosNome(string insumoNome);
        List<Insumo> FiltrarInsumosPorUnidade(string unidade);
        string ObterCategoriaPorNomeInsumo(string nomeInsumo);
        void CadastrarSaidaInsumo(SaidaInsumo saidainsumo, Insumo insumo);
        void AumentarQtdInsumo(Insumo insumo, int qtd);
        List<SaidaInsumo> FiltrarSaidaInsumosPorNomeEPeriodo(string insumoNome, DateTime dataInicio, DateTime dataFim);

    }
}
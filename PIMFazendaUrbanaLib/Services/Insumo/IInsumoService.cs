namespace PIMFazendaUrbanaLib
{
    public interface IInsumoService
    {
        List<Insumo> ListarInsumosComFiltros(string search);
        void CadastrarInsumo(Insumo insumo);
        void AlterarInsumo(Insumo insumo);
        Insumo ConsultarInsumoPorID(int insumoID);
        List<Insumo> ListarInsumosAtivos();

        List<Insumo> ListarInsumosEmEstoque();
        List<Insumo> ListarInsumosInativos();
        void DesativarInsumo(int insumoID);
        List<Insumo> FiltrarInsumosNome(string insumoNome);
        List<SaidaInsumo> ListarSaidaInsumos();
        List<SaidaInsumo> FiltrarSaidaInsumosNome(string insumoNome);
        List<Insumo> FiltrarInsumosPorUnidade(string unidade);
        string ObterCategoriaPorNomeInsumo(string nomeInsumo);
        void CadastrarSaidaInsumo(SaidaInsumo saidainsumo, Insumo insumo);
        bool AumentarQtdInsumo(Insumo insumo, int qtd);
        List<SaidaInsumo> FiltrarSaidaInsumosPorNomeEPeriodo(string insumoNome, DateTime dataInicio, DateTime dataFim);
        void ValidarInsumo(Insumo insumo);
        void ValidarSaidaInsumo(SaidaInsumo saidainsumo, Insumo insumo);
    }
}
namespace PIMFazendaUrbanaLib
{
    public interface ICultivoService
    {
        List<Cultivo> ListarCultivosComFiltros(string search);
        void CadastrarCultivo(Cultivo cultivo);
        void AlterarCultivo(Cultivo cultivo);
        void ExcluirCultivo(int cultivoId);
        List<Cultivo> ListarCultivosAtivos();
        List<Cultivo> ListarCultivosInativos();
        List<string> ListarCategorias();
        Cultivo ConsultarCultivoPorID(int cultivoId);
        List<Cultivo> FiltrarCultivosNome(string cultivoNome);
        void ValidarCultivo(Cultivo cultivo);
    }
}
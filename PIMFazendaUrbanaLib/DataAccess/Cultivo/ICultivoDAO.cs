namespace PIMFazendaUrbanaLib
{
    public interface ICultivoDAO
    {
        List<Cultivo> ListarCultivosComFiltros(string search);
        void CadastrarCultivo(Cultivo cultivo);
        void AlterarCultivo(Cultivo cultivo);
        void ExcluirCultivo(int cultivoId);
        List<Cultivo> ListarCultivosAtivos();
        List<Cultivo> ListarCultivosInativos();
        Cultivo ConsultarCultivoPorID(int cultivoId);
        Cultivo ConsultarCultivoNome(string cultivoNome);
        List<Cultivo> FiltrarCultivosNome(string cultivoNome);
        public List<string> ListarCategorias();
    }
}
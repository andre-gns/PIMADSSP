namespace PIMFazendaUrbanaAPI.Services
{
    public interface IExportacaoService
    {
        byte[] Exportar(IEnumerable<object> dados, string formato);
    }
}
namespace PIMFazendaUrbanaLib
{
    public interface IClienteService
    {
        List<Cliente> ListarClientesComFiltros(string search);
        void CadastrarCliente(Cliente cliente);
        void AlterarCliente(Cliente cliente);
        void ExcluirCliente(int clienteId);
        List<Cliente> ListarClientesAtivos();
        List<Cliente> ListarClientesInativos();
        Cliente ConsultarClientePorID(int clienteId);
        Cliente ConsultarClientePorNome(string clienteNome);
        Cliente ConsultarClientePorCNPJ(string clienteCNPJ);
        List<Cliente> FiltrarClientesPorNome(string clienteNome);
        void ValidarCliente(Cliente cliente);
    }
}
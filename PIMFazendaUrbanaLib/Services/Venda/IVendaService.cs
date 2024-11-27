namespace PIMFazendaUrbanaLib
{
    public interface IVendaService
    {
        void CadastrarPedidoVendaComItens(PedidoVenda pedidoVenda, List<PedidoVendaItem> vendaItems);

        List<PedidoVenda> ListarPedidosVenda();

        PedidoVenda ConsultarPedidoVenda(int idPedidoVenda);

        int? ObterUltimoIdPedidoVenda();

        List<PedidoVendaItem> ListarRegistrosDeVenda();

        PedidoVendaItem ConsultarVendaItem(int idVendaItem);

        List<PedidoVendaItem> FiltrarRegistrosDeVendaPorNomeEPeriodo(string produtoNome, DateTime dataInicio, DateTime dataFim);

        List<PedidoVendaItem> FiltrarRegistrosDeVendaNome(string produtoNome);

        void ValidarVenda(PedidoVenda pedidoVenda, List<PedidoVendaItem> vendaItems);
    }
}
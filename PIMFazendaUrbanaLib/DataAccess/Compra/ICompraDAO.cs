using MySql.Data.MySqlClient;

namespace PIMFazendaUrbanaLib
{
    public interface ICompraDAO
    {
        List<PedidoCompra> ListarPedidosCompraComItems();
        List<PedidoCompraItem> ListarItensPedidoCompraPorId(int idPedidoCompra);
        void CadastrarPedidoCompra(PedidoCompra pedidoCompra, MySqlTransaction transaction);
        void CadastrarCompraItem(PedidoCompraItem compraItem, MySqlTransaction transaction);
        List<PedidoCompra> ListarPedidosCompra();
        PedidoCompra ConsultarPedidoCompra(int idPedidoCompra);
        int? ObterUltimoIdPedidoCompra();
        List<PedidoCompraItem> ListarRegistrosDeCompra();
        List<PedidoCompraItem> FiltrarRegistrosDeCompraNome(string insumoNome);
        PedidoCompraItem ConsultarCompraItem(int idCompraItem);
        List<PedidoCompraItem> FiltrarRegistrosDeCompraPorNomeEPeriodo(string insumoNome, DateTime dataInicio, DateTime dataFim);
    }
}

namespace PIMFazendaUrbanaLib
{
    public class PedidoCompra  // atualizado com composição
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public int IdFornecedor { get; set; }
        public string NomeFornecedor { get; set; }
        public List<PedidoCompraItem> Itens { get; set; }
    }
}


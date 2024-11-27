using MySql.Data.MySqlClient;

namespace PIMFazendaUrbanaLib
{
    public class VendaService : IVendaService
    {
        private readonly IVendaDAO pedidoVendaDAO;
        private readonly string connectionString;

        public VendaService(string connectionString)
        {
            this.connectionString = connectionString;
            this.pedidoVendaDAO = new VendaDAO(connectionString);
        }

        // Método para cadastrar um novo pedido de venda
        public void CadastrarPedidoVendaComItens(PedidoVenda pedidoVenda, List<PedidoVendaItem> vendaItems)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        ValidarVenda(pedidoVenda, vendaItems);

                        // Cadastrar PedidoVenda
                        pedidoVendaDAO.CadastrarPedidoVenda(pedidoVenda, transaction);

                        // Cadastrar Itens de Venda
                        foreach (var item in vendaItems)
                        {
                            item.IdPedidoVenda = pedidoVenda.Id;
                            pedidoVendaDAO.CadastrarVendaItem(item, transaction);
                        }
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Erro ao cadastrar pedido de venda: " + ex.Message);
                    }
                }
            }
        }

        // Método para listar todos os pedidos de venda
        public List<PedidoVenda> ListarPedidosVenda()
        {
            try
            {
                return pedidoVendaDAO.ListarPedidosVenda();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar pedidos de venda: " + ex.Message);
            }
        }

        // Método para consultar um pedido de venda pelo ID
        public PedidoVenda ConsultarPedidoVenda(int idPedidoVenda)
        {
            try
            {
                return pedidoVendaDAO.ConsultarPedidoVenda(idPedidoVenda);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao consultar pedido de venda: " + ex.Message);
            }
        }

        // Método para obter o último ID de pedido de venda
        public int? ObterUltimoIdPedidoVenda()
        {
            try
            {
                return pedidoVendaDAO.ObterUltimoIdPedidoVenda();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter último ID de pedido de venda: " + ex.Message);
            }
        }

        // Método para listar todos os itens de venda
        public List<PedidoVendaItem> ListarRegistrosDeVenda()
        {
            try
            {
                return pedidoVendaDAO.ListarRegistrosDeVenda();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar registros de venda: " + ex.Message);
            }
        }

        // Método para consultar um item de venda pelo ID
        public PedidoVendaItem ConsultarVendaItem(int idVendaItem)
        {
            try
            {
                return pedidoVendaDAO.ConsultarVendaItem(idVendaItem);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao consultar item de venda: " + ex.Message);
            }
        }

        
        public List<PedidoVendaItem> FiltrarRegistrosDeVendaPorNomeEPeriodo(string produtoNome, DateTime dataInicio, DateTime dataFim)
        {
            
            try
            {
                return pedidoVendaDAO.FiltrarRegistrosDeVendaPorNomeEPeriodo(produtoNome, dataInicio, dataFim);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao filtrar registros de venda por nome de produto e período: " + ex.Message);
            }
            
            
        }
        

        public List<PedidoVendaItem> FiltrarRegistrosDeVendaNome(string produtoNome)
        {
            try
            {
                List<PedidoVendaItem> vendaItems = pedidoVendaDAO.FiltrarRegistrosDeVendaNome(produtoNome);
                return vendaItems;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao filtrar registros de venda por nome de insumo: " + ex.Message);
            }
            
        }


        /*
         * =-=-=-=-=-=-=-=-=-=-=-=- VALIDAÇÃO VENDA =-=-=-=-=-=-=-=-=-=-=-=-
         */

        public void ValidarVenda(PedidoVenda pedidoVenda, List<PedidoVendaItem> vendaItems)
        {
            var erros = new List<ValidationError>();

            // validar quantidade, valor unitario, cliente e produto

            if (vendaItems.Count <= 0) // Verifica se a quantidade de itens é maior que 0
            {
                erros.Add((new ValidationError("Quantidade", "A venda deve conter pelo menos um item.")));
            }

            // Validar cliente
            if (pedidoVenda.IdCliente <= 0)
            {
                erros.Add(new ValidationError("Cliente", "O cliente deve ser informado."));
            }

            // Valida cada item da compra
            foreach (var item in vendaItems)
            {
                // valida nome do produto
                if (string.IsNullOrEmpty(item.NomeProduto))
                {
                    erros.Add(new ValidationError("Produto", "Produto inválido."));
                }

                // Verifica se a quantidade do item de cada compra é menor ou igual a zero
                if (item.Qtd <= 0)
                {
                    erros.Add(new ValidationError("Quantidade", $"A quantidade do item '{item.NomeProduto}' deve ser um número inteiro maior que zero."));
                }
                // Verifica se o valor do item de cada compra é menor ou igual a zero
                if (item.Valor <= 0)
                {
                    erros.Add(new ValidationError("Valor", $"O valor do item '{item.NomeProduto}' deve ser um número decimal maior que zero."));
                }
            }

            if (erros.Any()) // se teve algum erro, lança exceção com a lista de erros
            {
                throw new ValidationException(erros);
            }

        }

    }
}

using System.Collections.Generic;

namespace PIMFazendaUrbanaLib
{
    public interface IEstoqueProdutoService
    {
        List<EstoqueProduto> ListarEstoqueProdutoComFiltros(string search);

        List<EstoqueProduto> ListarEstoqueProdutoAtivos();

        List<EstoqueProduto> FiltrarProdutosPorNome(string produtoNome);

        List<EstoqueProduto> FiltrarProdutosPorUnidade(string unidade);
    }
}

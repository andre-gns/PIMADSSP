namespace PIMFazendaUrbanaLib
{
    public interface IFornecedorDAO
    {
        List<Fornecedor> ListarFornecedoresComFiltros(string search);
        void CadastrarFornecedor(Fornecedor fornecedor);
        void AlterarFornecedor(Fornecedor fornecedor);
        void ExcluirFornecedor(int id);
        List<Fornecedor> ListarFornecedoresAtivos();
        List<Fornecedor> ListarFornecedoresInativos();
        Fornecedor ConsultarFornecedorPorID(int fornecedorId);
        Fornecedor ConsultarFornecedorPorNome(string fornecedorNome);
        Fornecedor ConsultarFornecedorPorCNPJ(string fornecedorCNPJ);
        List<Fornecedor> FiltrarFornecedoresPorNome(string fornecedorNome);

    }
}
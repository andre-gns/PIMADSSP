namespace PIMFazendaUrbanaLib
{
    public interface IFuncionarioDAO
    {
        List<Funcionario> ListarFuncionariosComFiltros(string search);
        Funcionario AutenticarFuncionario(string usuario, string senha);
        bool AutenticarGerente(string funcionarioUsuario);
        bool VerificarUsuarioDisponivel(string funcionarioUsuario);
        void AlterarSenhaFuncionario(string funcionarioUsuario, string novaSenha);
        void CadastrarFuncionario(Funcionario funcionario);
        void AlterarFuncionario(Funcionario funcionario);
        void ExcluirFuncionario(int id);
        List<Funcionario> ListarFuncionariosAtivos();
        List<Funcionario> ListarFuncionariosInativos();
        Funcionario ConsultarFuncionarioPorID(int funcionarioId);
        Funcionario ConsultarFuncionarioPorNome(string funcionarioNome);
        Funcionario ConsultarFuncionarioPorUsuario(string funcionarioUsuario);
        List<Funcionario> FiltrarFuncionariosPorNome(string funcionarioNome);

    }
}
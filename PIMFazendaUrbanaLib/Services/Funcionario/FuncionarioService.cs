using System.Text.RegularExpressions;

namespace PIMFazendaUrbanaLib
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly IFuncionarioDAO funcionarioDAO;

        public FuncionarioService(string connectionString)
        {
            this.funcionarioDAO = new FuncionarioDAO(connectionString);
        }

        public List<Funcionario> ListarFuncionariosComFiltros(string search)
        {
            try
            {
                List<Funcionario> funcionarios = funcionarioDAO.ListarFuncionariosComFiltros(search);
                return funcionarios;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar funcionários filtrados: " + ex.Message);
            }
        }

        public Funcionario AutenticarFuncionario(string usuario, string senha)
        {
            try
            {
                Funcionario funcionario = funcionarioDAO.AutenticarFuncionario(usuario, senha);
                if (funcionario != null)
                {
                    if (funcionario.StatusAtivo)
                    {
                        return funcionario;
                    }
                    else
                    {
                        throw new UserInactiveException("Usuário inativado.");
                    }
                }
                else
                {
                    throw new AuthenticationException("Usuário ou senha inválidos.");
                }
            }
            catch (UserInactiveException)
            {
                throw; // Propaga a exceção de usuário inativo
            }
            catch (AuthenticationException)
            {
                throw; // Propaga a exceção de autenticação
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao autenticar funcionário: " + ex.Message); // Para demais tipos de exceção
            }
        }

        public string AutenticarGerente(string usuario)
        {
            try
            {
                if (funcionarioDAO.AutenticarGerente(usuario) == true)
                {
                    return "gerente"; // Retorna "gerente" para indicar que o funcionário foi autenticado como gerente
                }
                else
                {
                    return "naogerente"; // Retorna "naogerente" para indicar que o funcionário não foi autenticado como gerente
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao autenticar funcionário: " + ex.Message);
            }
        }

        public bool VerificarUsuarioDisponivel(string usuario)
        {
            try
            {
                if (funcionarioDAO.VerificarUsuarioDisponivel(usuario) == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao verificar disponibilidade de usuário: " + ex.Message);
            }
        }

        public bool AlterarSenhaFuncionario(string usuario, string novaSenha)
        {
            try
            {
                VerificarSenhaForte(novaSenha); // Verifica se a senha é forte o suficiente (método a ser chamado pelo front
                funcionarioDAO.AlterarSenhaFuncionario(usuario, novaSenha);

                return true; // Retorna true para indicar que a alteração da senha foi bem-sucedida
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao alterar senha do funcionário: " + ex.Message);
            }
        }

        // Método para verificar se a senha é forte o suficiente, a ser chamado pelo front
        public bool VerificarSenhaForte(string senha)
        {
            var erros = new List<ValidationError>();

            // Verifica se a senha tem pelo menos 8 caracteres
            if (senha.Length < 8)
            {
                erros.Add(new ValidationError("Senha", "a senha tem pelo menos 8 caracteres"));
            }

            // Verifica se a senha contém pelo menos um número
            bool contemNumero = false;
            foreach (char c in senha)
            {
                if (char.IsDigit(c))
                {
                    contemNumero = true;
                    break;
                }
            }
            if (!contemNumero)
            {
                erros.Add(new ValidationError("Senha", "A senha deve conter pelo menos um número."));
            }

            // Verifica se a senha contém pelo menos uma letra maiúscula
            bool contemMaiuscula = false;
            foreach (char c in senha)
            {
                if (char.IsUpper(c))
                {
                    contemMaiuscula = true;
                    break;
                }
            }
            if (!contemMaiuscula)
            {
                erros.Add(new ValidationError("Senha", "A senha deve conter pelo menos uma letra maiúscula."));
            }

            // Verifica se a senha contém pelo menos uma letra minúscula
            bool contemMinuscula = false;
            foreach (char c in senha)
            {
                if (char.IsLower(c))
                {
                    contemMinuscula = true;
                    break;
                }
            }
            if (!contemMinuscula)
            {

                erros.Add(new ValidationError("Senha", "A senha deve conter pelo menos uma letra minúscula."));
            }

            // Verifica se a senha contém pelo menos um caractere especial
            bool contemEspecial = false;
            foreach (char c in senha)
            {
                if (!char.IsLetterOrDigit(c))
                {
                    contemEspecial = true;
                    break;
                }
            }
            if (!contemEspecial)
            {
                erros.Add(new ValidationError("Senha", "A senha deve conter pelo menos um caractere especial."));
            }

            if (erros.Any()) // se teve algum erro, lança exceção com a lista de erros
            {
                throw new ValidationException(erros);
            }
            else
            {
                return true;
            }
        }

        public void CadastrarFuncionario(Funcionario funcionario)
        {
            try
            {
                ValidarFuncionario(funcionario);
                funcionarioDAO.CadastrarFuncionario(funcionario);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao cadastrar funcionário: " + ex.Message);
            }
        }

        public void AlterarFuncionario(Funcionario funcionario)
        {
            try
            {
                ValidarFuncionario(funcionario);
                funcionarioDAO.AlterarFuncionario(funcionario);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao editar funcionário: " + ex.Message);
            }
        }

        public void ExcluirFuncionario(int funcionarioId)
        {
            try
            {
                funcionarioDAO.ExcluirFuncionario(funcionarioId);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir funcionário: " + ex.Message);
            }
        }

        public List<Funcionario> ListarFuncionariosAtivos()
        {
            try
            {
                List<Funcionario> funcionarios = funcionarioDAO.ListarFuncionariosAtivos();
                return funcionarios; // Retorna a lista de funcionarios quando tudo corre bem
            }
            catch (Exception ex)
            {
                // Lança uma exceção indicando que ocorreu um erro ao listar funcionarios ativos
                throw new Exception("Erro ao listar funcionários ativos: " + ex.Message);
            }
        }

        public List<Funcionario> ListarFuncionariosInativos()
        {
            try
            {
                List<Funcionario> funcionariosInativos = funcionarioDAO.ListarFuncionariosInativos();
                return funcionariosInativos; // Retorna a lista de funcionarios quando tudo corre bem
            }
            catch (Exception ex)
            {
                // Lança uma exceção indicando que ocorreu um erro ao listar funcionarios inativos
                throw new Exception("Erro ao listar funcionários inativos: " + ex.Message);
            }
        }

        public Funcionario ConsultarFuncionarioPorID(int funcionarioId)
        {
            try
            {
                Funcionario funcionario = funcionarioDAO.ConsultarFuncionarioPorID(funcionarioId);
                return funcionario;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao consultar funcionário: " + ex.Message);
            }
        }

        public Funcionario ConsultarFuncionarioPorNome(string funcionarioNome)
        {
            try
            {
                Funcionario funcionario = funcionarioDAO.ConsultarFuncionarioPorNome(funcionarioNome);
                return funcionario;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao consultar funcionário: " + ex.Message);
            }
        }

        public Funcionario ConsultarFuncionarioPorUsuario(string funcionarioUsuario)
        {
            try
            {
                Funcionario funcionario = funcionarioDAO.ConsultarFuncionarioPorUsuario(funcionarioUsuario);
                return funcionario;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao consultar funcionário: " + ex.Message);
            }
        }

        public List<Funcionario> FiltrarFuncionariosPorNome(string funcionarioNome)
        {
            try
            {
                List<Funcionario> funcionarios = funcionarioDAO.FiltrarFuncionariosPorNome(funcionarioNome);
                return funcionarios;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao filtrar funcionários por nome: " + ex.Message);
            }
        }

        // =-=-=-=-=-=-=-=-=-=-=-=- VALIDAÇÃO FUNCIONÁRIO =-=-=-=-=-=-=-=-=-=-=-=-

        public void ValidarFuncionario(Funcionario funcionario)  // método para validação dos dados do funcionario contendo as regras de negócio e mensagens de erro
        {
            var erros = new List<ValidationError>();

            if (string.IsNullOrEmpty(funcionario.Nome) || funcionario.Nome.Length < 3)
            {
                erros.Add(new ValidationError("Nome", "O nome deve ter pelo menos 3 caracteres"));
            }
            if (!Regex.IsMatch(funcionario.CPF, @"^\d{11}$") || !funcionario.CPF.All(char.IsDigit))
            {
                erros.Add(new ValidationError("CPF", "O CPF deve conter 11 dígitos"));
            }
            if (!Regex.IsMatch(funcionario.Email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                erros.Add(new ValidationError("Email", "Email inválido. O email deve ter o formato exemplo@exemplo.exeplo"));
            }
            if (funcionario.Cargo != "Funcionário" && funcionario.Cargo != "Gerente")
            {
                erros.Add(new ValidationError("Cargo", "O cargo deve ser 'Funcionário' ou 'Gerente'"));
            }

            // Lista de sexosPermitidos permitidas
            List<string> sexosPermitidos = new List<string>
            {
                "M", "F", "Outro"
            };
            if (funcionario.Sexo == null || !sexosPermitidos.Contains(funcionario.Sexo)) //Valida se a unidade está na lista de permitidas
            {
                erros.Add(new ValidationError("Sexo", "O sexo deve ser 'M', 'F' ou '-'"));
            }

            if (VerificarUsuarioDisponivel(funcionario.Usuario) == false)
            {
                erros.Add(new ValidationError("Usuário", "O nome de usuário está indisponível"));
            }
            // deixar validação se os dos campos de senha são iguais no front mesmo
            ValidarSenha(funcionario.Senha, erros);

            TelefoneValidation telefoneValidation = new TelefoneValidation();
            erros = telefoneValidation.ValidarTelefone(funcionario.Telefone, erros);
            EnderecoValidation enderecoValidation = new EnderecoValidation();
            erros = enderecoValidation.ValidarEndereco(funcionario.Endereco, erros);

            if (erros.Any()) // se teve algum erro, lança exceção com a lista de erros
            {
                throw new ValidationException(erros);
            }
        }

        public List<ValidationError> ValidarSenha(string senha, List<ValidationError> erros)
        {
            // Verifica se a senha tem pelo menos 8 caracteres
            if (senha.Length < 8)
            {
                erros.Add(new ValidationError("Senha", "a senha tem pelo menos 8 caracteres"));
            }

            // Verifica se a senha contém pelo menos um número
            bool contemNumero = false;
            foreach (char c in senha)
            {
                if (char.IsDigit(c))
                {
                    contemNumero = true;
                    break;
                }
            }
            if (!contemNumero)
            {
                erros.Add(new ValidationError("Senha", "A senha deve conter pelo menos um número."));
            }

            // Verifica se a senha contém pelo menos uma letra maiúscula
            bool contemMaiuscula = false;
            foreach (char c in senha)
            {
                if (char.IsUpper(c))
                {
                    contemMaiuscula = true;
                    break;
                }
            }
            if (!contemMaiuscula)
            {
                erros.Add(new ValidationError("Senha", "A senha deve conter pelo menos uma letra maiúscula."));
            }

            // Verifica se a senha contém pelo menos uma letra minúscula
            bool contemMinuscula = false;
            foreach (char c in senha)
            {
                if (char.IsLower(c))
                {
                    contemMinuscula = true;
                    break;
                }
            }
            if (!contemMinuscula)
            {

                erros.Add(new ValidationError("Senha", "A senha deve conter pelo menos uma letra minúscula."));
            }

            // Verifica se a senha contém pelo menos um caractere especial
            bool contemEspecial = false;
            foreach (char c in senha)
            {
                if (!char.IsLetterOrDigit(c))
                {
                    contemEspecial = true;
                    break;
                }
            }
            if (!contemEspecial)
            {
                erros.Add(new ValidationError("Senha", "A senha deve conter pelo menos um caractere especial."));
            }

            return erros;
        }


    }
}

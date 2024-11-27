using Microsoft.AspNetCore.Components;
using PIMFazendaUrbanaAPI.DTOs;
using PIMFazendaUrbanaRadzen.Services;
using Radzen;

namespace PIMFazendaUrbanaRadzen.Components.Pages.Funcionarios
{
    public partial class CadastrarFuncionario
    {
        [Inject]
        public FuncionarioApiService<FuncionarioDTO> FuncionarioApiService { get; set; }

        [Inject]
        public CepApiService CepApiService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public NotificationService NotificationService { get; set; }

        protected FuncionarioDTO funcionario;
        protected bool errorVisible;

        protected List<string> estados = new List<string>
        {
            "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO", "MA",
            "MT", "MS", "MG", "PA", "PB", "PE", "PI", "PR", "RJ", "RN",
            "RS", "RO", "RR", "SC", "SP", "SE", "TO"
        };

        private List<string> generos = new List<string> { "M", "F", "Outro" };

        private List<string> cargos = new List<string> { "Funcionário", "Gerente" };

        protected override async Task OnInitializedAsync()
        {
            funcionario = new FuncionarioDTO
            {
                Endereco = new EnderecoDTO(),
                Telefone = new TelefoneDTO()
            };
        }

        protected async Task FormSubmit()
        {
            try
            {
                // Limpa e formata os campos antes de enviar
                funcionario.CPF = FormatCPF(funcionario.CPF);
                funcionario.Telefone.Numero = FormatTelefone(funcionario.Telefone.Numero);
                funcionario.Endereco.CEP = FormatCEP(funcionario.Endereco.CEP);

                funcionario.StatusAtivo = true; // Define StatusAtivo como true por padrão

                Console.WriteLine($"Chamando ApiService: CreateAsync" + " hora atual: " + DateTime.Now);
                var response = await FuncionarioApiService.CreateAsync(funcionario); // Chama ApiService para criar o funcionario
                Console.WriteLine("Retornou de ApiService: Create Async" + " hora atual: " + DateTime.Now);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Navegando para /funcionarios");
                    // Redireciona para a página de funcionarios e exibe mensagem de sucesso
                    NavigationManager.NavigateTo("/funcionarios");
                    NotificationService.Notify(NotificationSeverity.Success, "Sucesso", "Funcionario cadastrado com sucesso!", duration: 5000);
                }
                else
                {
                    // Exibe mensagem de erro caso o status não seja de sucesso
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    NotificationService.Notify(NotificationSeverity.Error, "Erro", $"Falha ao cadastrar funcionario: {errorMessage}", duration: 5000);
                }
            }
            catch (Exception ex)
            {
                errorVisible = true; // Exibe mensagem de erro
                Console.WriteLine($"Erro ao cadastrar funcionario: {ex.Message}");
            }
        }


        protected async Task CancelButtonClick()
        {
            // Redireciona para a página de funcionarios
            NavigationManager.NavigateTo("/funcionarios");
        }


        private string FormatCPF(string cpf)
        {
            // Remove caracteres não numéricos e retorna o CPF formatado
            return new string(cpf.Where(char.IsDigit).ToArray());
        }

        private string FormatTelefone(string telefone)
        {
            // Remove caracteres não numéricos e retorna o telefone formatado
            return new string(telefone.Where(char.IsDigit).ToArray());
        }

        private string FormatCEP(string cep)
        {
            // Remove caracteres não numéricos e retorna o CEP formatado
            return new string(cep.Where(char.IsDigit).ToArray());
        }

        protected async Task BuscarEnderecoPorCEP()
        {
            try
            {
                // Limpa o formato do CEP (remove o hífen)
                string cepFormatado = FormatCEP(funcionario.Endereco.CEP);

                // Chama o CepApiService para buscar o endereço
                var endereco = await CepApiService.GetEnderecoViaCep(cepFormatado);

                if (endereco != null)
                {
                    // Preenche os campos de endereço com os dados retornados
                    funcionario.Endereco.Logradouro = endereco.Logradouro;
                    funcionario.Endereco.Bairro = endereco.Bairro;
                    funcionario.Endereco.Cidade = endereco.Cidade;
                    funcionario.Endereco.UF = endereco.UF;
                    funcionario.Endereco.Complemento = endereco.Complemento ?? string.Empty; // Complemento pode ser nulo
                }
                else
                {
                    // Se o endereço não for encontrado, exibe uma mensagem de erro
                    NotificationService.Notify(NotificationSeverity.Error, "Erro", "CEP não encontrado. Verifique o número do CEP e tente novamente.");
                }
            }
            catch (Exception ex)
            {
                // Caso ocorra algum erro na consulta, exibe mensagem de erro
                NotificationService.Notify(NotificationSeverity.Error, "Erro", $"Erro ao consultar o CEP: {ex.Message}");
            }
        }

        private string confirmaSenha;

        private bool senhasCoincidem;
        private string mensagemErroSenha;

        private void VerificarSenhasCoincidem()
        {
            if (funcionario.Senha != confirmaSenha)
            {
                senhasCoincidem = false;
                mensagemErroSenha = "As senhas não coincidem.";
            }
            else
            {
                senhasCoincidem = true;
                mensagemErroSenha = string.Empty;
            }
        }

    }
}

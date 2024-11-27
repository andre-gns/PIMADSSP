using Microsoft.AspNetCore.Components;
using PIMFazendaUrbanaAPI.DTOs;
using PIMFazendaUrbanaRadzen.Services;
using Radzen;

namespace PIMFazendaUrbanaRadzen.Components.Pages.Fornecedores
{
    public partial class CadastrarFornecedor
    {
        [Inject]
        public FornecedorApiService<FornecedorDTO> FornecedorApiService { get; set; }

        [Inject]
        public CepApiService CepApiService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public NotificationService NotificationService { get; set; }

        protected FornecedorDTO fornecedor;
        protected bool errorVisible;

        protected List<string> estados = new List<string>
        {
            "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO", "MA",
            "MT", "MS", "MG", "PA", "PB", "PE", "PI", "PR", "RJ", "RN",
            "RS", "RO", "RR", "SC", "SP", "SE", "TO"
        };


        protected override async Task OnInitializedAsync()
        {
            fornecedor = new FornecedorDTO
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
                fornecedor.CNPJ = FormatCNPJ(fornecedor.CNPJ);
                fornecedor.Telefone.Numero = FormatTelefone(fornecedor.Telefone.Numero);
                fornecedor.Endereco.CEP = FormatCEP(fornecedor.Endereco.CEP);

                fornecedor.StatusAtivo = true; // Define StatusAtivo como true por padrão

                Console.WriteLine($"Chamando ApiService: CreateAsync" + " hora atual: " + DateTime.Now);
                var response = await FornecedorApiService.CreateAsync(fornecedor); // Chama ApiService para criar o fornecedor
                Console.WriteLine("Retornou de ApiService: Create Async" + " hora atual: " + DateTime.Now);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Navegando para /fornecedores");
                    // Redireciona para a página de fornecedores e exibe mensagem de sucesso
                    NavigationManager.NavigateTo("/fornecedores");
                    NotificationService.Notify(NotificationSeverity.Success, "Sucesso", "Fornecedor cadastrado com sucesso!", duration: 5000);
                }
                else
                {
                    // Exibe mensagem de erro caso o status não seja de sucesso
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    NotificationService.Notify(NotificationSeverity.Error, "Erro", $"Falha ao cadastrar fornecedor: {errorMessage}", duration: 5000);
                }
            }
            catch (Exception ex)
            {
                errorVisible = true; // Exibe mensagem de erro
                Console.WriteLine($"Erro ao cadastrar fornecedor: {ex.Message}");
            }
        }


        protected async Task CancelButtonClick()
        {
            // Redireciona para a página de fornecedores
            NavigationManager.NavigateTo("/fornecedores");
        }


        private string FormatCNPJ(string cnpj)
        {
            // Remove caracteres não numéricos e retorna o CNPJ formatado
            return new string(cnpj.Where(char.IsDigit).ToArray());
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
                string cepFormatado = FormatCEP(fornecedor.Endereco.CEP);

                // Chama o CepApiService para buscar o endereço
                var endereco = await CepApiService.GetEnderecoViaCep(cepFormatado);

                if (endereco != null)
                {
                    // Preenche os campos de endereço com os dados retornados
                    fornecedor.Endereco.Logradouro = endereco.Logradouro;
                    fornecedor.Endereco.Bairro = endereco.Bairro;
                    fornecedor.Endereco.Cidade = endereco.Cidade;
                    fornecedor.Endereco.UF = endereco.UF;
                    fornecedor.Endereco.Complemento = endereco.Complemento ?? string.Empty; // Complemento pode ser nulo
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

    }
}

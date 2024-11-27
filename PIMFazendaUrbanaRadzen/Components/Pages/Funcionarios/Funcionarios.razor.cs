using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PIMFazendaUrbanaAPI.DTOs;
using PIMFazendaUrbanaRadzen.Components.Pages.Fornecedores;
using PIMFazendaUrbanaRadzen.Services;
using Radzen;
using Radzen.Blazor;

namespace PIMFazendaUrbanaRadzen.Components.Pages.Funcionarios
{
    public partial class Funcionarios
    {
        [Inject]
        public FuncionarioApiService<FuncionarioDTO> FuncionarioApiService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; } // Inject NavigationManager

        [Inject]
        public NotificationService NotificationService { get; set; }

        [Inject]
        private ExportacaoApiService<object> exportacaoApiService { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        protected List<FuncionarioDTO> funcionarios;
        protected string errorMessage = string.Empty;
        protected string searchQuery = string.Empty;

        protected RadzenDataGrid<FuncionarioDTO> grid0;

        protected override async Task OnInitializedAsync()
        {
            await LoadFuncionarios(); // Carrega funcionarios inicialmente
        }

        protected async Task LoadFuncionarios()
        {
            try
            {
                funcionarios = string.IsNullOrWhiteSpace(searchQuery)
                    ? await FuncionarioApiService.GetAllAsync() // Carrega todos os funcionarios
                    : await FuncionarioApiService.GetFuncionariosFiltradosAsync(searchQuery); // Busca funcionarios filtrados

                errorMessage = string.Empty; // Limpa mensagens de erro
            }
            catch (Exception ex)
            {
                errorMessage = $"Erro ao carregar funcionarios: {ex.Message}";
                Console.WriteLine(errorMessage);
            }
        }

        protected async Task OnSearch(string search)
        {
            searchQuery = search;
            await LoadFuncionarios(); // Atualiza a lista de funcionarios com base na pesquisa
        }

        protected void AddButtonClick()
        {
            // Ação ao clicar no botão "Adicionar"
            NavigationManager.NavigateTo("/cadastrar-funcionario"); // Redireciona para a página de cadastro de funcionario
        }

        protected void OnRowSelect(FuncionarioDTO funcionario)
        {
            // Ação ao selecionar uma linha (funcionario)
        }

        protected void EditarFuncionario(FuncionarioDTO funcionario)
        {
            // Implementar lógica de edição de funcionario
        }

        protected void ExcluirFuncionario(FuncionarioDTO funcionario)
        {
            // Implementar lógica de exclusão de funcionario
        }

        protected string FormatarTelefone(string ddd, string numero)
        {
            if (numero.Length == 8)
            {
                return $"({ddd}) {numero.Substring(0, 4)}-{numero.Substring(4)}";
            }
            else if (numero.Length == 9)
            {
                return $"({ddd}) {numero.Substring(0, 5)}-{numero.Substring(5)}";
            }
            else
            {
                return "Telefone inválido";
            }
        }

        protected string FormatarCPF(string cpf)
        {
            if (cpf.Length == 11)
            {
                return $"{cpf.Substring(0, 3)}.{cpf.Substring(3, 3)}.{cpf.Substring(6, 3)}-{cpf.Substring(9)}";
            }
            else
            {
                return "CPF inválido";
            }
        }

        protected string FormatarCEP(string cep)
        {
            if (cep.Length == 8)
            {
                return $"{cep.Substring(0, 5)}-{cep.Substring(5)}";
            }
            else
            {
                return "CEP inválido";
            }
        }

        protected async Task OnExportarClick(RadzenSplitButtonItem args)
        {
            if (args == null || string.IsNullOrEmpty(args.Value.ToString()))
            {
                NotificationService.Notify(NotificationSeverity.Error, "Erro", "Por favor, selecione um formato de exportação.");
                return;
            }

            string format = args.Value.ToString(); // "xlsx" ou "csv"
            string fileName = $"Funcionarios_{DateTime.Now:yyyyMMdd_HHmmss}";

            try
            {
                // Verifique se há dados
                if (funcionarios == null || !funcionarios.Any())
                {
                    NotificationService.Notify(NotificationSeverity.Error, "Erro", "Não há dados para exportar.");
                    return;
                }

                // Chama o serviço para exportação com base no formato selecionado
                var fileBytes = await exportacaoApiService.ExportarAsync(funcionarios, format, fileName);

                if (fileBytes != null)
                {
                    // Gera o download no navegador
                    await JSRuntime.InvokeVoidAsync("downloadFile", fileName + "." + format, Convert.ToBase64String(fileBytes));
                }
                else
                {
                    NotificationService.Notify(NotificationSeverity.Error, "Erro ao exportar", "Nenhum arquivo foi gerado.");
                }
            }
            catch (Exception ex)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Erro ao exportar", ex.Message);
            }
        }

    }
}

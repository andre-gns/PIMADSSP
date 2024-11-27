using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PIMFazendaUrbanaAPI.DTOs;
using PIMFazendaUrbanaRadzen.Services;
using Radzen;
using Radzen.Blazor;

namespace PIMFazendaUrbanaRadzen.Components.Pages.Clientes
{
    public partial class Clientes
    {
        [Inject]
        public ClienteApiService<ClienteDTO> ClienteApiService { get; set; } // serviço que chama a API

        [Inject]
        public NavigationManager NavigationManager { get; set; } // serviço de navegação

        [Inject]
        public NotificationService NotificationService { get; set; } // serviço de notificação

        [Inject]
        private ExportacaoApiService<object> exportacaoApiService { get; set; } // serviço de exportação para xlsx e csv

        [Inject]
        public IJSRuntime JSRuntime { get; set; } // para chamadas JavaScript

        protected List<ClienteDTO> clientes; // lista de clientes

        protected string errorMessage = string.Empty; // mensagem de erro

        protected string searchQuery = string.Empty; // query da barra pesquisar

        protected RadzenDataGrid<ClienteDTO> grid0; // grid de clientes

        protected string enderecoFiltro = string.Empty; // filtro para todos as propriedades de endereço, que o usuário pode digitar

        protected override async Task OnInitializedAsync()
        {
            await LoadClientes(); // Carrega clientes inicialmente
        }

        protected async Task OnEnderecoFilterChanged()
        {
            await LoadClientes(); // Recarrega os dados aplicando o filtro de endereço
        }

        protected async Task OnEnderecoFilterClear()
        {
            enderecoFiltro = string.Empty; // Limpa o filtro
            await LoadClientes();          // Recarrega os dados sem filtro
        }

        protected async Task LoadClientes()
        {
            try
            {
                var todosClientes = string.IsNullOrWhiteSpace(searchQuery)
                    ? await ClienteApiService.GetAllAsync()
                    : await ClienteApiService.GetClientesFiltradosAsync(searchQuery);

                // Filtro de endereço personalizado
                if (!string.IsNullOrWhiteSpace(enderecoFiltro))
                {
                    todosClientes = todosClientes.Where(cliente =>
                        cliente.Endereco != null && (
                            (cliente.Endereco.Logradouro?.Contains(enderecoFiltro, StringComparison.OrdinalIgnoreCase) ?? false) ||
                            (cliente.Endereco.Numero?.Contains(enderecoFiltro, StringComparison.OrdinalIgnoreCase) ?? false) ||
                            (cliente.Endereco.Bairro?.Contains(enderecoFiltro, StringComparison.OrdinalIgnoreCase) ?? false) ||
                            (cliente.Endereco.Cidade?.Contains(enderecoFiltro, StringComparison.OrdinalIgnoreCase) ?? false) ||
                            (cliente.Endereco.UF?.Contains(enderecoFiltro, StringComparison.OrdinalIgnoreCase) ?? false)
                        )
                    ).ToList();
                }

                clientes = todosClientes;
                errorMessage = string.Empty; // Limpa mensagens de erro
            }
            catch (Exception ex)
            {
                errorMessage = $"Erro ao carregar clientes: {ex.Message}";
                Console.WriteLine(errorMessage);
            }
        }


        protected async Task OnSearch(string search)
        {
            searchQuery = search;
            await LoadClientes(); // Atualiza a lista de clientes com base na pesquisa
        }

        protected void AddButtonClick()
        {
            // Ação ao clicar no botão "Adicionar"
            NavigationManager.NavigateTo("/cadastrar-cliente"); // Redireciona para a página de cadastro de cliente
        }

        protected void OnRowSelect(ClienteDTO cliente)
        {
            // Ação ao selecionar uma linha (cliente)
        }

        protected void EditarCliente(ClienteDTO cliente)
        {
            // Implementar lógica de edição de cliente
        }

        protected void ExcluirCliente(ClienteDTO cliente)
        {
            // Implementar lógica de exclusão de cliente
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

        protected string FormatarCNPJ(string cnpj)
        {
            if (cnpj.Length == 14)
            {
                return $"{cnpj.Substring(0, 2)}.{cnpj.Substring(2, 3)}.{cnpj.Substring(5, 3)}/{cnpj.Substring(8, 4)}-{cnpj.Substring(12)}";
            }
            else
            {
                return "CNPJ inválido";
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
            string fileName = $"Clientes_{DateTime.Now:yyyyMMdd_HHmmss}";

            try
            {
                // Verifique se há dados
                if (clientes == null || !clientes.Any())
                {
                    NotificationService.Notify(NotificationSeverity.Error, "Erro", "Não há dados para exportar.");
                    return;
                }

                // Chama o serviço para exportação com base no formato selecionado
                var fileBytes = await exportacaoApiService.ExportarAsync(clientes, format, fileName);

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

using Microsoft.AspNetCore.Components;
using PIMFazendaUrbanaAPI.DTOs;
using PIMFazendaUrbanaRadzen.Services;
using Radzen;

namespace PIMFazendaUrbanaRadzen.Components.Pages.Producao
{
    public partial class CadastrarProducao
    {
        /* COMENTADO ENQUANTO NÃO COLOCA OS APISERVICE E CONTROLLERS

        //[Inject]
        //public ProducaoApiService<CProducaoDTO> ProducaoApiService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public NotificationService NotificationService { get; set; }

        protected ProducaoDTO producao;
        protected bool errorVisible;


        protected override async Task OnInitializedAsync()
        {
            producao = new ProducaoDTO; // Inicializa um novo objeto de produção
        }

        protected async Task FormSubmit()
        {
            try
            {

                producao.StatusAtivo = true; // Define StatusAtivo como true por padrão

                Console.WriteLine($"Chamando ApiService: CreateAsync" + " hora atual: " + DateTime.Now);
                var response = await ProducaoApiService.CreateAsync(producao); // Chama ApiService
                Console.WriteLine("Retornou de ApiService: Create Async" + " hora atual: " + DateTime.Now);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Navegando para /producao");
                    // Redireciona para a página de producao e exibe mensagem de sucesso
                    NavigationManager.NavigateTo("/producao");
                    NotificationService.Notify(NotificationSeverity.Success, "Sucesso", "Produção cadastrado com sucesso!", duration: 5000);
                }
                else
                {
                    // Exibe mensagem de erro caso o status não seja de sucesso
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    NotificationService.Notify(NotificationSeverity.Error, "Erro", $"Falha ao cadastrar produção: {errorMessage}", duration: 5000);
                }
            }
            catch (Exception ex)
            {
                errorVisible = true; // Exibe mensagem de erro
                Console.WriteLine($"Erro ao cadastrar produção: {ex.Message}");
            }
        }


        protected async Task CancelButtonClick()
        {
            // Redireciona para a página de producao
            NavigationManager.NavigateTo("/producao");
        }

        */
    }
}

using Microsoft.AspNetCore.Components;
using PIMFazendaUrbanaAPI.DTOs;

namespace PIMFazendaUrbanaRadzen.Components.Pages
{
    public partial class Login
    {
        private LoginDTO loginDto = new LoginDTO();
        private string error = string.Empty;
        private bool errorVisible = false;

        private async Task OnLoginClick()
        {
            errorVisible = false; // reseta a visibilidade da mensagem de erro

            var errorMessage = await authService.LoginAsync(loginDto); // obtém a mensagem de erro

            if (string.IsNullOrEmpty(errorMessage))
            {
                // login bem-sucedido
                navigationManager.NavigateTo("/"); // redireciona após o login
            }
            else
            {
                // exibir mensagem de erro
                error = errorMessage;
                errorVisible = true;
            }
        }

        private async Task LoginParaTesteGerente()
        {
            loginDto.UserName = "isabela.f";
            loginDto.Password = "I9#bE2%mN";
            await OnLoginClick();
        }

        private async Task LoginParaTesteFuncionario()
        {
            loginDto.UserName = "alice.s";
            loginDto.Password = "A1!eB3@cD";
            await OnLoginClick();
        }

        private void OnResetPassword()
        {
            // Implementar lógica para redefinição de senha
        }
    }
}

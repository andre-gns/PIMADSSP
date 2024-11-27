using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;

    public CustomAuthenticationStateProvider(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        Console.WriteLine("\nGetAuthenticationStateAsync chamado");

        //var token = await _localStorage.GetItemAsync<string>("authToken");

        // Realize a chamada de JS interop após a renderização
        var token = await GetAuthTokenAsync();
        Console.WriteLine($"\nToken lido do localStorage: {token}");

        Console.WriteLine("\nTESTE 1");

        if (string.IsNullOrEmpty(token))
        {
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity()); // Usuário anônimo
            return new AuthenticationState(anonymousUser); // Retorna o estado de autenticação
        }

        Console.WriteLine("\nTESTE 2");
        Console.WriteLine(token);

        var handler = new JwtSecurityTokenHandler(); // Cria um manipulador de token JWT
        var jsonToken = handler.ReadToken(token) as JwtSecurityToken; // Lê o token JWT

        Console.WriteLine("\nTESTE 3");

        // Adicionando as claims do JWT no ClaimsPrincipal
        var identity = new ClaimsIdentity(jsonToken?.Claims ?? Enumerable.Empty<Claim>(), "jwt"); // Cria um ClaimsIdentity com as claims do token
        var user = new ClaimsPrincipal(identity); // Cria um ClaimsPrincipal com o ClaimsIdentity

        Console.WriteLine("\nTESTE 4");

        Console.WriteLine(identity); // Exibe o usuário autenticado no console
        Console.WriteLine(user); // Exibe o usuário autenticado no console

        return new AuthenticationState(user); // Retorna o estado de autenticação
    }

    public async Task<string> GetAuthTokenAsync()
    {
        var token = await _localStorage.GetItemAsync<string>("authToken");
        Console.WriteLine($"\nGetAuthTokenAsync retornou: {token}");
        return token;
    }


    public void NotifyUserAuthentication(string token)
    {
        Console.WriteLine($"\nNotifyUserAuthentication chamado com token: {token}");
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

        // Cria uma lista de claims a partir do token
        var identity = new ClaimsIdentity(jsonToken?.Claims ?? Enumerable.Empty<Claim>(), "jwt");
        var user = new ClaimsPrincipal(identity);

        Console.WriteLine($"\nClaims do usuário: {string.Join(", ", user.Claims.Select(c => c.Type + ":" + c.Value))}");

        // Atualiza o estado de autenticação
        var authState = Task.FromResult(new AuthenticationState(user));
        NotifyAuthenticationStateChanged(authState);
    }

    public void NotifyUserLogout()
    {
        var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
        var authState = Task.FromResult(new AuthenticationState(anonymousUser));
        NotifyAuthenticationStateChanged(authState);
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using PIMFazendaUrbanaLib;
using PIMFazendaUrbanaAPI.DTOs;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace PIMFazendaUrbanaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IFuncionarioService _funcionarioService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration; // Para acessar as configurações

        public AuthController(IFuncionarioService funcionarioService, IMapper mapper, IConfiguration configuration)
        {
            _funcionarioService = funcionarioService;
            _mapper = mapper;
            _configuration = configuration; // Inicializa a configuração
        }

        // Método para autenticar um funcionário
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO loginDto)
        {
            try
            {
                var funcionario = _funcionarioService.AutenticarFuncionario(loginDto.UserName, loginDto.Password);
                if (funcionario == null)
                {
                    return Unauthorized(); // Retorna 401 se a autenticação falhar
                }

                // Mapeia Funcionario para FuncionarioDTO usando AutoMapper
                var funcionarioSessionDto = _mapper.Map<FuncionarioSessionDTO>(funcionario);

                // Gerar um token JWT
                var token = GerarToken(funcionarioSessionDto);
                return Ok(new { Token = token, Funcionario = funcionarioSessionDto }); // Retorna código 200 com token e funcionário
            }
            catch (AuthenticationException ex)
            {
                return Unauthorized(new { message = ex.Message }); // Retorna 401 com mensagem personalizada
            }
            catch (UserInactiveException ex)
            {
                return Unauthorized(new { message = ex.Message }); // Retorna 401 com mensagem personalizada
            }
            catch (Exception ex)
            {
                // Log detalhado do erro
                Console.WriteLine($"Erro ao autenticar: {ex.Message}\n{ex.StackTrace}");
                return StatusCode(500, new { message = "Erro interno ao autenticar. Tente novamente mais tarde." });
            }
        }

        // Método para gerar um token JWT
        private string GerarToken(FuncionarioSessionDTO funcionarioSessionDto)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, funcionarioSessionDto.Usuario),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, funcionarioSessionDto.Cargo) // Adiciona o cargo como uma claim
            };

            // Use a chave do appsettings.json
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"], // Usa o Issuer do appsettings.json
                audience: _configuration["Jwt:Audience"], // Usa o Audience do appsettings.json
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

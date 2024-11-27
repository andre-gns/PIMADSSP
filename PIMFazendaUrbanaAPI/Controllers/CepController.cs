using PIMFazendaUrbanaAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using AutoMapper;

namespace PIMFazendaUrbanaRadzen.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CepController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        // Injetando HttpClient e AutoMapper via dependência
        public CepController(HttpClient httpClient, IMapper mapper)
        {
            _httpClient = httpClient;
            _mapper = mapper;
        }

        [HttpGet("get")]
        public async Task<ActionResult<EnderecoDTO>> RetornarEndereco(string cep)
        {
            try
            {
                var json = await _httpClient.GetStringAsync($"https://viacep.com.br/ws/{cep}/json");

                var enderecoViaCep = JsonConvert.DeserializeObject<EnderecoViaCepDTO>(json);

                if (enderecoViaCep == null || enderecoViaCep.erro)
                {
                    return NotFound(new { message = "CEP não encontrado." });
                }

                // Converte o EnderecoViaCepDTO para EnderecoDTO usando AutoMapper
                var enderecoDTO = _mapper.Map<EnderecoDTO>(enderecoViaCep);

                return Ok(enderecoDTO);
            }
            catch (HttpRequestException)
            {
                return StatusCode(500, new { message = "Erro ao acessar o serviço de CEP." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }


    }
}

using Microsoft.AspNetCore.Mvc;
using PIMFazendaUrbanaLib;
using PIMFazendaUrbanaAPI.DTOs;
using AutoMapper;

namespace PIMFazendaUrbanaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CultivoController : ControllerBase
    {
        private readonly ICultivoService _cultivoService;
        private readonly IMapper _mapper; // Adiciona o AutoMapper

        // O controlador utiliza a interface ICultivoService para acessar as operações de Cultivo
        public CultivoController(ICultivoService cultivoService, IMapper mapper)
        {
            _cultivoService = cultivoService;
            _mapper = mapper; // Inicializa o AutoMapper
        }

        [HttpGet("filtrados")]
        public ActionResult<List<CultivoDTO>> ListarCultivosFiltrados(string search)
        {
            try
            {
                var cultivo = _cultivoService.ListarCultivosComFiltros(search);
                var cultivoDto = _mapper.Map<List<CultivoDTO>>(cultivo); // Mapeia Cultivo para CultivoDTO
                return Ok(cultivoDto); // Retorna a lista de Cultivo filtrados como resposta
            }
            catch (Exception ex)
            {
                // Log detalhado do erro
                Console.WriteLine($"Erro ao listar Cultivos: {ex.Message}\n{ex.StackTrace}");
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }


        // Método para listar Cultivo ativos
        [HttpGet("ativos")]
        public IActionResult ListarCultivoAtivos()
        {
            try
            {
                var cultivo = _cultivoService.ListarCultivosAtivos(); 
                var cultivoDto = _mapper.Map<List<CultivoDTO>>(cultivo); // Mapeia Cultivo para CultivoDTO
                return Ok(cultivoDto);
            }
            catch (Exception ex)
            {
                // Log detalhado do erro
                Console.WriteLine($"Erro ao listar Cultivo: {ex.Message}\n{ex.StackTrace}");
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }


        // Método para cadastrar um Cultivo
        [HttpPost("cadastrar")]
        public IActionResult CadastrarCultivo([FromBody] CultivoDTO cultivoDto)
        {
            try
            {
                var cultivo = _mapper.Map<Cultivo>(cultivoDto); // Mapeia Cultivo para CultivoDTO
                _cultivoService.CadastrarCultivo(cultivo); // Chama o serviço para cadastrar o Cultivo
                return Ok(new { message = "Cultivo cadastrado com sucesso." });
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { errors = ex.Errors }); // Retorna erros de validação
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }

        // Método para alterar um Cultivo
        [HttpPut("alterar")]
        public IActionResult AlterarCultivo([FromBody] CultivoDTO cultivoDto)
        {
            try
            {
                var cultivo = _mapper.Map<Cultivo>(cultivoDto); // Mapeia CultivoDTO para Cultivo
                _cultivoService.AlterarCultivo(cultivo); // Chama o serviço para alterar o Cultivo
                return Ok(new { message = "Cultivo alterado com sucesso." });
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { errors = ex.Errors }); // Retorna erros de validação
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }

        // Método para excluir um Cultivo (exclusão lógica)
        [HttpDelete("excluir/{id}")]
        public IActionResult ExcluirCultivo(int id)
        {
            try
            {
                _cultivoService.ExcluirCultivo(id); // Chama o serviço para excluir o Cultivo
                return Ok(new { message = "Cultivo excluído com sucesso." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }

        // Método para buscar um Cultivo por ID
        [HttpGet("{id}")]
        public IActionResult ConsultarCultivoPorId(int id)
        {
            try
            {
                var cultivo = _cultivoService.ConsultarCultivoPorID(id);
                var cultivoDto = _mapper.Map<CultivoDTO>(cultivo); // Mapeia Cultivo para CultivoDTO
                return Ok(cultivoDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }

    }
}

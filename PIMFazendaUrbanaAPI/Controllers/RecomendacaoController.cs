using Microsoft.AspNetCore.Mvc;
using PIMFazendaUrbanaLib;
using PIMFazendaUrbanaAPI.DTOs;
using AutoMapper;

namespace PIMFazendaUrbanaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecomendacaoController : ControllerBase
    {
        private readonly IRecomendacaoService _recomendacaoService;
        private readonly IMapper _mapper;

        public RecomendacaoController(IRecomendacaoService recomendacaoService, IMapper mapper)
        {
            _recomendacaoService = recomendacaoService;
            _mapper = mapper;
        }

        [HttpGet("gerar")]
        public ActionResult<List<CultivoDTO>> GetRecomendacoes(string regiao,string estacao, bool ambienteControlado)
        {
            try
            {
                var recomendacoes = _recomendacaoService.GerarRecomendacao(regiao, estacao, ambienteControlado);
                var recomendacoesDto = _mapper.Map<List<CultivoDTO>>(recomendacoes);
                return Ok(recomendacoesDto);
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

    }
}

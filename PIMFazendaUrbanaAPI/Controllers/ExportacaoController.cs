//using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PIMFazendaUrbanaAPI.DTOs;
using PIMFazendaUrbanaAPI.Services;

namespace PIMFazendaUrbanaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExportacaoController : Controller
    {
        private readonly IExportacaoService _exportacaoService;

        public ExportacaoController(IExportacaoService exportacaoService)
        {
            _exportacaoService = exportacaoService;
        }

        [HttpPost("exportar")]
        public IActionResult Exportar([FromBody] ExportacaoRequestDTO request)
        {
            // logar os dados da request no console
            Console.WriteLine($"Dados recebidos: {Newtonsoft.Json.JsonConvert.SerializeObject(request)}");

            try
            {
                // Verificar os dados recebidos
                if (request?.Dados == null || !request.Dados.Any())
                {
                    return BadRequest(new { message = "Dados não fornecidos ou estão vazios." });
                }

                var arquivo = _exportacaoService.Exportar(request.Dados, request.Formato);

                string contentType = request.Formato.ToLower() == "xlsx"
                    ? "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    : "text/csv";

                return File(arquivo, contentType, request.NomeArquivo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }
    }


}

using Microsoft.AspNetCore.Mvc;
using PIMFazendaUrbanaLib;
using PIMFazendaUrbanaAPI.DTOs;
using AutoMapper;

namespace PIMFazendaUrbanaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FuncionarioController : ControllerBase
    {
        private readonly IFuncionarioService _funcionarioService;
        private readonly IMapper _mapper; // Adiciona o AutoMapper

        // O controlador utiliza a interface IFuncionarioService para acessar as operações de cliente
        public FuncionarioController(IFuncionarioService funcionarioService, IMapper mapper)
        {
            _funcionarioService = funcionarioService;
            _mapper = mapper; // Inicializa o AutoMapper
        }

        [HttpGet("filtrados")]
        public ActionResult<List<FuncionarioDTO>> ListarFuncionariosFiltrados(string search)
        {
            try
            {
                var funcionarios = _funcionarioService.ListarFuncionariosComFiltros(search);
                var funcionariosDto = _mapper.Map<List<FuncionarioDTO>>(funcionarios); // Mapeia Funcionario para FuncionarioDTO
                return Ok(funcionariosDto); // Retorna a lista de funcionarios filtrados como resposta
            }
            catch (Exception ex)
            {
                // Log detalhado do erro
                Console.WriteLine($"Erro ao listar funcionarios: {ex.Message}\n{ex.StackTrace}");
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }

        // Método para listar funcionarios ativos
        [HttpGet("ativos")]
        public IActionResult ListarFuncionariosAtivos()
        {
            try
            {
                var funcionarios = _funcionarioService.ListarFuncionariosAtivos();
                var funcionariosDto = _mapper.Map<List<FuncionarioDTO>>(funcionarios); // Mapeia Funcionario para FuncionarioDTO
                return Ok(funcionariosDto);
            }
            catch (Exception ex)
            {
                // Log detalhado do erro
                Console.WriteLine($"Erro ao listar funcionarios: {ex.Message}\n{ex.StackTrace}");
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }

        // Método para cadastrar um funcionario
        [HttpPost("cadastrar")]
        public IActionResult CadastrarFuncionario([FromBody] FuncionarioDTO funcionarioDto)
        {
            try
            {
                var funcionario = _mapper.Map<Funcionario>(funcionarioDto); // Mapeia FuncionarioDTO para Funcionario
                _funcionarioService.CadastrarFuncionario(funcionario); // Chama o serviço para cadastrar o funcionario
                return Ok(new { message = "Funcionario cadastrado com sucesso." });
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

        // Método para alterar um funcionario
        [HttpPut("alterar")]
        public IActionResult AlterarFuncionario([FromBody] FuncionarioDTO funcionarioDTO)
        {
            try
            {
                var funcionario = _mapper.Map<Funcionario>(funcionarioDTO); // Mapeia FuncionarioDTO para Funcionario
                _funcionarioService.AlterarFuncionario(funcionario); // Chama o serviço para alterar o funcionario
                return Ok(new { message = "Funcionario alterado com sucesso." });
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

        // Método para excluir um funcionario (exclusão lógica)
        [HttpDelete("excluir/{id}")]
        public IActionResult ExcluirFuncionario(int id)
        {
            try
            {
                _funcionarioService.ExcluirFuncionario(id); // Chama o serviço para excluir o funcionario
                return Ok(new { message = "Funcionario excluído com sucesso." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }

        // Método para buscar um funcionario por ID
        [HttpGet("{id}")]
        public IActionResult ConsultarFuncionarioPorId(int id)
        {
            try
            {
                var funcionario = _funcionarioService.ConsultarFuncionarioPorID(id);
                var funcionarioDto = _mapper.Map<FuncionarioDTO>(funcionario); // Mapeia Funcionario para FuncionarioDTO
                return Ok(funcionarioDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }

    }
}

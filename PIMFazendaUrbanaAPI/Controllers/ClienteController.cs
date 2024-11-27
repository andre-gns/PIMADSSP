using Microsoft.AspNetCore.Mvc;
using PIMFazendaUrbanaLib;
using PIMFazendaUrbanaAPI.DTOs;
using AutoMapper;

namespace PIMFazendaUrbanaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper; // Adiciona o AutoMapper

        // O controlador utiliza a interface IClienteService para acessar as operações de cliente
        public ClienteController(IClienteService clienteService, IMapper mapper)
        {
            _clienteService = clienteService;
            _mapper = mapper; // Inicializa o AutoMapper
        }

        [HttpGet("filtrados")]
        public ActionResult<List<ClienteDTO>> ListarClientesFiltrados(string search)
        {
            try
            {
                var clientes = _clienteService.ListarClientesComFiltros(search);
                var clientesDto = _mapper.Map<List<ClienteDTO>>(clientes); // Mapeia Cliente para ClienteDTO
                return Ok(clientesDto); // Retorna a lista de clientes filtrados como resposta
            }
            catch (Exception ex)
            {
                // Log detalhado do erro
                Console.WriteLine($"Erro ao listar clientes: {ex.Message}\n{ex.StackTrace}");
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }


        // Método para listar clientes ativos
        [HttpGet("ativos")]
        public IActionResult ListarClientesAtivos()
        {
            try
            {
                var clientes = _clienteService.ListarClientesAtivos();
                var clientesDto = _mapper.Map<List<ClienteDTO>>(clientes); // Mapeia Cliente para ClienteDTO
                return Ok(clientesDto);
            }
            catch (Exception ex)
            {
                // Log detalhado do erro
                Console.WriteLine($"Erro ao listar clientes: {ex.Message}\n{ex.StackTrace}");
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }


        // Método para cadastrar um cliente
        [HttpPost("cadastrar")]
        public IActionResult CadastrarCliente([FromBody] ClienteDTO clienteDto)
        {
            try
            {
                var cliente = _mapper.Map<Cliente>(clienteDto); // Mapeia ClienteDTO para Cliente
                _clienteService.CadastrarCliente(cliente); // Chama o serviço para cadastrar o cliente
                return Ok(new { message = "Cliente cadastrado com sucesso." });
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

        // Método para alterar um cliente
        [HttpPut("alterar")]
        public IActionResult AlterarCliente([FromBody] ClienteDTO clienteDto)
        {
            try
            {
                var cliente = _mapper.Map<Cliente>(clienteDto); // Mapeia ClienteDTO para Cliente
                _clienteService.AlterarCliente(cliente); // Chama o serviço para alterar o cliente
                return Ok(new { message = "Cliente alterado com sucesso." });
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

        // Método para excluir um cliente (exclusão lógica)
        [HttpDelete("excluir/{id}")]
        public IActionResult ExcluirCliente(int id)
        {
            try
            {
                _clienteService.ExcluirCliente(id); // Chama o serviço para excluir o cliente
                return Ok(new { message = "Cliente excluído com sucesso." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }

        // Método para buscar um cliente por id
        [HttpGet("{id}")]
        public IActionResult ConsultarClientePorId(int id)
        {
            try
            {
                var cliente = _clienteService.ConsultarClientePorID(id);
                var clienteDto = _mapper.Map<ClienteDTO>(cliente); // Mapeia Cliente para ClienteDTO
                return Ok(clienteDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }

    }
}

using DesafioTerra.Application.Dto;
using DesafioTerra.Application.Dto.Response;
using DesafioTerra.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DesafioTerra.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AntCorrupcaoController : ControllerBase
    {
        private readonly IAntCorrupcaoService _service;

        public AntCorrupcaoController(IAntCorrupcaoService service)
        {
            _service = service;
        }

        [HttpPost("criar_repositorio")]
        [ProducesResponseType(typeof(CriacaoRepositorioResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CriacaoRepositorioResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CriacaoRepositorioResponse>> CriarRepositorio([FromBody] RepositorioDTO repositorioDTO)
        {

            if (!ModelState.IsValid)
                return BadRequest("todos os campos são obrigatórios.");

            try
            {
                var response = await _service.CriarRepositorio(repositorioDTO);

                return Ok(response);

            }
            catch (Exception _error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao criar repositório: " + _error.Message);
            }
        }

        [HttpGet("listar_branches")]
        [ProducesResponseType(typeof(BranchResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BranchResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BranchResponse>> ListarBranches(string usuario, string repositorio, string token)
        {

            if (!ModelState.IsValid)
                return BadRequest("todos os campos são obrigatórios.");

            try
            {
                var response = await _service.ListarBranchs(usuario, repositorio, token);

                return Ok(response);

            }
            catch (Exception _error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao listar branches do repositório - Erro: " + _error.Message);
            }
        }

        [HttpGet("listar_webhooks")]
        [ProducesResponseType(typeof(WebhookResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(WebhookResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<WebhookResponse>> ListarWebhooks(string usuario, string repositorio, string token)
        {

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(repositorio) || string.IsNullOrEmpty(token))
            {
                return BadRequest(new ErrorResponse("Todos os campos são obrigatórios."));
            }

            try
            {
                var response = await _service.ListarWebhooks(usuario, repositorio, token);

                return Ok(response);

            }
            catch (Exception _error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse("Erro ao listar Webhooks do repositório.", _error));
            }
        }


    }
}

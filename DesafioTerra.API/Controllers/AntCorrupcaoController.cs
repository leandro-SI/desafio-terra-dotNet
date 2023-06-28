using DesafioTerra.Application.Dto;
using DesafioTerra.Application.Dto.Request;
using DesafioTerra.Application.Dto.Response;
using DesafioTerra.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DesafioTerra.API.Controllers
{

    /// <summary>
    /// API de Integração com API REST do GitHub.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AntCorrupcaoController : ControllerBase
    {
        private readonly IAntCorrupcaoService _service;

        /// <summary>
        /// Construtor para injetar as dependências de serviço.
        /// </summary>
        public AntCorrupcaoController(IAntCorrupcaoService service)
        {
            _service = service;
        }

        /// <summary>
        /// Cria um novo repositório.
        /// </summary>
        /// <param name="repositorioDTO">Dados do repositório a ser criado.</param>
        /// <returns>Resposta da criação do repositório.</returns>
        [HttpPost("criar_repositorio")]
        [ProducesResponseType(typeof(CriacaoRepositorioResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CriacaoRepositorioResponse>> CriarRepositorio([FromBody] RepositorioDTO repositorioDTO)
        {

            if (!ModelState.IsValid)
                return BadRequest(new ErrorResponse("Todos os campos são obrigatórios."));

            try
            {
                var response = await _service.CriarRepositorio(repositorioDTO);

                return Ok(response);

            }
            catch (Exception _error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse("Erro ao criar o repositório.", _error));
            }
        }

        /// <summary>
        /// Lista as branches de um repositório.
        /// </summary>
        /// <param name="usuario">Nome do usuário/proprietário do repositório.</param>
        /// <param name="repositorio">Nome do repositório.</param>
        /// <param name="token">Token de autenticação.</param>
        /// <returns>Resposta com as branches do repositório.</returns>
        [HttpGet("listar_branches")]
        [ProducesResponseType(typeof(BranchResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BranchResponse>> ListarBranches(string usuario, string repositorio, string token)
        {

            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(repositorio) || string.IsNullOrWhiteSpace(token))
                return BadRequest(new ErrorResponse("Todos os campos são obrigatórios."));

            try
            {
                var response = await _service.ListarBranchs(usuario, repositorio, token);

                return Ok(response);

            }
            catch (Exception _error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse("Erro ao listar as branches do repositório.", error));
            }
        }

        /// <summary>
        /// Lista os webhooks de um repositório.
        /// </summary>
        /// <param name="usuario">Nome do usuário/proprietário do repositório.</param>
        /// <param name="repositorio">Nome do repositório.</param>
        /// <param name="token">Token de autenticação.</param>
        /// <returns>Resposta com os webhooks do repositório.</returns>
        [HttpGet("listar_webhooks")]
        [ProducesResponseType(typeof(WebhookResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<WebhookResponse>> ListarWebhooks(string usuario, string repositorio, string token)
        {

            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(repositorio) || string.IsNullOrWhiteSpace(token))
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
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse("Erro ao listar os webhooks do repositório.", error));
            }
        }

        /// <summary>
        /// Adiciona um webhook com os tipos de eventos aceitos. Tipos de Eventos aceitos (exemplos) - 'push' e/ou 'pull_request'
        /// </summary>
        /// <param name="webhookDTO">Dados do webhook a ser adicionado.</param>
        /// <returns>Resposta da adição do webhook.</returns>
        [HttpPost("adicionar_webhook")]
        [ProducesResponseType(typeof(AdicionarWebhookResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AdicionarWebhookResponse>> AdicionarWebhook([FromBody] WebhookDTO webhookDTO)
        {

            if (!ModelState.IsValid)
                return BadRequest(new ErrorResponse("Todos os campos são obrigatórios."));

            string[] eventosAceitos = { "push", "pull_request" };

            var eventosNaoAceitos = webhookDTO.Eventos.Except(eventosAceitos);

            if (eventosNaoAceitos.Any())
                return BadRequest("Tipo de evento de webhook não é aceito!");

            try
            {
                var response = await _service.AdicionarWebhook(webhookDTO);

                return Ok(response);

            }
            catch (Exception _error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse("Erro ao adicionar o webhook.", _error));

            }
        }

        /// <summary>
        /// Atualiza o webhook com os tipos de eventos aceitos. Tipos de Eventos aceitos (exemplos) - 'push' e/ou 'pull_request'
        /// </summary>
        /// <param name="webhookRequest">Dados do webhook a ser atualizado.</param>
        /// <returns>Resposta da atualização do webhook.</returns>
        [HttpPatch("atualizar_webhook")]
        [ProducesResponseType(typeof(AtualizarWebhookResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AtualizarWebhookResponse>> AtualizarWebhook([FromBody] AtualizarWebhookRequest webhookRequest)
        {

            if (!ModelState.IsValid)
            return BadRequest(new ErrorResponse("Todos os campos são obrigatórios."));

            string[] eventosAceitos = { "push", "pull_request" };

            var eventosNaoAceitos = webhookRequest.Eventos.Except(eventosAceitos);

            if (eventosNaoAceitos.Any())
                return BadRequest("Tipo de evento de webhook não é aceito!");

            try
            {
                var response = await _service.AtualizarWebhook(webhookRequest);

                return Ok(response);

            }
            catch (Exception _error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse("Erro ao atualizar webhook.", _error));

            }
        }


    }
}

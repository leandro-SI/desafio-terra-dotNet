﻿using DesafioTerra.Application.Dto;
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


    }
}
﻿using AutoFixture;
using DesafioTerra.Application.Dto;
using DesafioTerra.Application.Dto.Request;
using DesafioTerra.Application.Dto.Response;
using DesafioTerra.Application.Services;
using DesafioTerra.Application.Services.Interfaces;
using DesafioTerra.Domain.Models;
using Microsoft.Extensions.Configuration;
using Moq;
using Newtonsoft.Json.Linq;
using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DesafioTerra.Application.Tests.Services
{
    public class AntCorrupcaoServiceTests
    {

        [Fact]
        public async Task CriarRepositorio_StatusCode200_ReturnsSucessResponse()
        {
            var novoRepositorio = new RepositorioDTO()
            {
                Nome = "repositorio",
                Descricao = "descricao",
                Token = "meu token"
            };

            var expectedResponse = new CriacaoRepositorioResponse
            {
                Sucesso = true,
                Mensagem = "Repositório criado com sucesso!"
            };

            var http = new HttpClient(new HttpMessageHandlerMock(HttpStatusCode.OK));

            var service = new AntCorrupcaoService(http);
            var result = await service.CriarRepositorio(novoRepositorio);

            // Assert
            Assert.Equal(expectedResponse.Sucesso, result.Sucesso);
            Assert.Equal(expectedResponse.Mensagem, result.Mensagem);

            //Assert.True(result.Sucesso);

        }

        [Fact]
        public async Task ListarBranches_StatusCode200_ReturnsSucessResponse()
        {

            string owner = "leandro-user";
            string repo = "meu-repositorio";
            string token = "meu_token";

            var expectedResponse = new
            {
                Sucesso = true,
                Mensagem = "Sucesso",
                Branchs = new List<BranchList>()
            };

            var http = new HttpClient(new HttpMessageHandlerMock(HttpStatusCode.OK));

            var service = new AntCorrupcaoService(http);
            var result = await service.ListarBranchs(owner, repo, token);

            // Assert
            Assert.Equal(expectedResponse.Sucesso, result.Sucesso);
            Assert.Equal(expectedResponse.Mensagem, result.Mensagem);

        }

        [Fact]
        public async Task ListarWebhooks_StatusCode200_ReturnsSucessResponse()
        {

            string owner = "leandro-user";
            string repo = "meu-repositorio";
            string token = "meu_token";

            var expectedResponse = new
            {
                Sucesso = true,
                Mensagem = "Sucesso",
                Webhooks = new List<WebhooksList>()
            };

            var http = new HttpClient(new HttpMessageHandlerMock(HttpStatusCode.OK));

            var service = new AntCorrupcaoService(http);
            var result = await service.ListarWebhooks(owner, repo, token);

            // Assert
            Assert.Equal(expectedResponse.Sucesso, result.Sucesso);
            Assert.Equal(expectedResponse.Mensagem, result.Mensagem);

        }

        [Fact]
        public async Task CriarWebhook_StatusCode200_ReturnsSucessResponse()
        {
            string token = "meu token";

            WebhookDTO webhookDTO = new WebhookDTO()
            {
                Usuario = "meu proprietario",
                Repositorio = "meu repositorio",
                Ativo = true,
                Token = token,
                Eventos = new string[] { "push", "pull_request" }
            };

            var expectedResponse = new CriacaoRepositorioResponse
            {
                Sucesso = true,
                Mensagem = "Sucesso"
            };

            var http = new HttpClient(new HttpMessageHandlerMock(HttpStatusCode.OK));

            var service = new AntCorrupcaoService(http);
            var result = await service.AdicionarWebhook(webhookDTO);

            // Assert
            Assert.Equal(expectedResponse.Sucesso, result.Sucesso);
            Assert.Equal(expectedResponse.Mensagem, result.Mensagem);

            //Assert.True(result.Sucesso);

        }

        [Fact]
        public async Task AtualizarWebhook_StatusCode200_ReturnsSucessResponse()
        {
            string token = "meu token";

            AtualizarWebhookRequest webhookDTO = new AtualizarWebhookRequest()
            {
                Usuario = "meu proprietario",
                Repositorio = "meu repositorio",
                Ativo = true,
                HookId = 767467,
                Token = token,
                Eventos = new string[] { "push", "pull_request" }
            };

            var expectedResponse = new AtualizarWebhookResponse
            {
                Sucesso = true,
                Mensagem = "Sucesso"
            };

            var http = new HttpClient(new HttpMessageHandlerMock(HttpStatusCode.OK));

            var service = new AntCorrupcaoService(http);
            var result = await service.AtualizarWebhook(webhookDTO);

            // Assert
            Assert.Equal(expectedResponse.Sucesso, result.Sucesso);
            Assert.Equal(expectedResponse.Mensagem, result.Mensagem);

            //Assert.True(result.Sucesso);

        }

    }
}

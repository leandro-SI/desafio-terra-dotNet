using DesafioTerra.Application.Dto;
using DesafioTerra.Application.Dto.Response;
using DesafioTerra.Application.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DesafioTerra.Application.Services
{
    public class AntCorrupcaoService : IAntCorrupcaoService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public AntCorrupcaoService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<CriacaoRepositorioResponse> CriarRepositorio(RepositorioDTO repositorioDTO)
        {
            try
            {
                var novoRepositorio = new
                {
                    name = repositorioDTO.Nome,
                    description = repositorioDTO.Descricao,
                };

                using (HttpClient httpClient = _httpClientFactory.CreateClient("RepositorioAPI"))
                {
                    httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {repositorioDTO.Token}");
                    httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd("request");

                    string apiUrl = httpClient.BaseAddress.ToString();

                    string repositorioJson = JsonConvert.SerializeObject(novoRepositorio);

                    HttpContent requestBody = new StringContent(repositorioJson, Encoding.UTF8, "application/json");

                    var response = await httpClient.PostAsync(apiUrl, requestBody);

                    if (response.IsSuccessStatusCode)
                    {
                        return new CriacaoRepositorioResponse { Sucesso = true, Mensagem = "Repositório criado com sucesso!" };
                    }
                    else
                    {
                        return new CriacaoRepositorioResponse { Sucesso = false, Mensagem = "Falha ao criar o repositório. Status de resposta: " + response.StatusCode };
                    }
                }

            }
            catch (Exception _error)
            {
                throw new Exception(_error.Message);
            }

        }

    }
}

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
        private readonly IConfiguration _configuration;

        public AntCorrupcaoService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<CriacaoRepositorioResponse> CriarRepositorio(RepositorioDTO repositorioDTO)
        {
            try
            {
                string apiUrl = _configuration.GetSection("Bases:Repositorio_Url").Value;

                var novoRepositorio = new
                {
                    name = repositorioDTO.Nome,
                    description = repositorioDTO.Descricao,
                };

                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {repositorioDTO.Token}");
                    httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd("request");

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

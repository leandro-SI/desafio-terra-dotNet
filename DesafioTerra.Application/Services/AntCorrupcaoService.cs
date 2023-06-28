﻿using DesafioTerra.Application.Dto;
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
        private readonly HttpClient _httpClient;

        public AntCorrupcaoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
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

                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {repositorioDTO.Token}");
                _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                _httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd("request");

                string apiUrl = "https://api.github.com/user/repos";

                string repositorioJson = JsonConvert.SerializeObject(novoRepositorio);

                HttpContent requestBody = new StringContent(repositorioJson, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(apiUrl, requestBody);

                _httpClient.Dispose();

                if (response.IsSuccessStatusCode)
                {
                    return new CriacaoRepositorioResponse { Sucesso = true, Mensagem = "Repositório criado com sucesso!" };
                }
                else
                {
                    return new CriacaoRepositorioResponse { Sucesso = false, Mensagem = "Falha ao criar o repositório. Status de resposta: " + response.StatusCode };
                }
                

            }
            catch (Exception _error)
            {
                throw new Exception(_error.Message);
            }

        }

        public async Task<BranchResponse> ListarBranchs(string usuario, string repositorio, string token)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                _httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd("request");

                string apiUrl = $"https://api.github.com/repos/{usuario}/{repositorio}/branches";

                HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(apiUrl);

                string responseString = await httpResponseMessage.Content.ReadAsStringAsync();

                BranchResponse response = new BranchResponse();

                if (httpResponseMessage.IsSuccessStatusCode)
                {                    
                    response.Branchs = new List<BranchList>();
                    response.Sucesso = true;
                    response.Mensagem = "Sucesso";

                    response.Branchs = JsonConvert.DeserializeObject<List<BranchList>>(responseString);

                    return response;
                }
                else
                {
                    response.Branchs = null;
                    response.Sucesso = false;
                    response.Mensagem = "Resource not found.";

                    return response;
                }

            }
            catch (Exception _error)
            {
                throw new Exception(_error.Message);
            }
        }
    }
}

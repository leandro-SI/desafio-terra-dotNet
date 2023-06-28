using DesafioTerra.Application.Dto;
using DesafioTerra.Application.Dto.Request;
using DesafioTerra.Application.Dto.Response;
using DesafioTerra.Application.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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

        public async Task<WebhookResponse> ListarWebhooks(string usuario, string repositorio, string token)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                _httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd("request");

                string apiUrl = $"https://api.github.com/repos/{usuario}/{repositorio}/hooks";

                HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(apiUrl);

                string responseString = await httpResponseMessage.Content.ReadAsStringAsync();

                WebhookResponse response = new WebhookResponse();

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    response.Webhooks = new List<WebhooksList>();
                    response.Sucesso = true;
                    response.Mensagem = "Sucesso";

                    response.Webhooks = JsonConvert.DeserializeObject<List<WebhooksList>>(responseString);

                    return response;
                }
                else
                {
                    response.Webhooks = null;
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

        public async Task<AdicionarWebhookResponse> AdicionarWebhook(WebhookDTO webhookDTO)
        {
            try
            {
                var configuracao = new
                {
                    url = "https://example.com/webhook",
                    content_type = "json",
                    insecure_ssl = "0"
                };

                var cabecalho = new Dictionary<string, string>();

                cabecalho.Add("X-GitHub-Api-Version", "2022-11-28");

                var novoWebhook = new
                {
                    owner = webhookDTO.Usuario,
                    repo = webhookDTO.Repositorio,
                    name = "web",
                    active = webhookDTO.Ativo,
                    events = webhookDTO.Eventos,
                    config = configuracao,
                    headers = cabecalho
                };

                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {webhookDTO.Token}");
                _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/vnd.github+json"));
                _httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd("request");

                string apiUrl = $"https://api.github.com/repos/{webhookDTO.Usuario}/{webhookDTO.Repositorio}/hooks";

                string webhookJson = JsonConvert.SerializeObject(novoWebhook);

                HttpContent requestBody = new StringContent(webhookJson, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(apiUrl, requestBody);

                _httpClient.Dispose();

                AdicionarWebhookResponse webhookResponseJson = new AdicionarWebhookResponse();

                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();

                    webhookResponseJson.Sucesso = true;
                    webhookResponseJson.Mensagem = "Sucesso";

                    webhookResponseJson.Webhook = JsonConvert.DeserializeObject<WebhooksList>(responseString);

                    return webhookResponseJson;

                }
                else
                {
                    webhookResponseJson.Sucesso = false;
                    webhookResponseJson.Mensagem = "Erro ao adicionar Webhook: Erro - " + response.StatusCode;
                    webhookResponseJson.Webhook = null;

                    return webhookResponseJson;
                }

            }
            catch (Exception _error)
            {
                throw new Exception(_error.Message);
            }
        }

        public async Task<AtualizarWebhookResponse> AtualizarWebhook(AtualizarWebhookRequest webhookRequest)
        {
            try
            {

                var updateWebhook = new
                {
                    owner = webhookRequest.Usuario,
                    repo = webhookRequest.Repositorio,
                    hook_id = webhookRequest.HookId,
                    active = webhookRequest.Ativo,
                    add_events = webhookRequest.Eventos
                };

                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {webhookRequest.Token}");
                _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/vnd.github+json"));
                _httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd("request");

                string apiUrl = $"https://api.github.com/repos/{webhookRequest.Usuario}/{webhookRequest.Repositorio}/hooks/{webhookRequest.HookId}";

                string webhookJson = JsonConvert.SerializeObject(updateWebhook);

                HttpContent requestBody = new StringContent(webhookJson, Encoding.UTF8, "application/json");

                var response = await _httpClient.PatchAsync(apiUrl, requestBody);

                _httpClient.Dispose();

                AtualizarWebhookResponse webhookResponseJson = new AtualizarWebhookResponse();

                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();

                    webhookResponseJson.Sucesso = true;
                    webhookResponseJson.Mensagem = "Sucesso";

                    webhookResponseJson.Webhook = JsonConvert.DeserializeObject<AtualizarWebhookList>(responseString);

                    return webhookResponseJson;

                }
                else
                {
                    webhookResponseJson.Sucesso = false;
                    webhookResponseJson.Mensagem = "Erro ao atualizar Webhook: Erro - " + response.StatusCode;
                    webhookResponseJson.Webhook = null;

                    return webhookResponseJson;
                }

            }
            catch (Exception _error)
            {
                throw new Exception(_error.Message);
            }
        }
    }
}

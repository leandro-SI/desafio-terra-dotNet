using DesafioTerra.Application.Dto;
using DesafioTerra.Application.Dto.Request;
using DesafioTerra.Application.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioTerra.Application.Services.Interfaces
{
    public interface IAntCorrupcaoService
    {
        Task<CriacaoRepositorioResponse> CriarRepositorio(RepositorioDTO repositorioDTO);
        Task<BranchResponse> ListarBranchs(string usuario, string repositorio, string token);
        Task<WebhookResponse> ListarWebhooks(string usuario, string repositorio, string token);
        Task<AdicionarWebhookResponse> AdicionarWebhook(WebhookDTO webhookDTO);
        Task<AtualizarWebhookResponse> AtualizarWebhook(AtualizarWebhookRequest webhookRequest);
    }
}

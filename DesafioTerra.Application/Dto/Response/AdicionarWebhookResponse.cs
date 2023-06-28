using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioTerra.Application.Dto.Response
{
    public class AdicionarWebhookResponse
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public WebhooksList Webhook { get; set; }

    }
}

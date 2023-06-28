using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioTerra.Application.Dto.Response
{
    public class WebhookResponse
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public List<WebhooksList> Webhooks { get; set; }
    }

    public class WebhooksList
    {
        public string Type { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public string[] Events { get; set; }
    }
}

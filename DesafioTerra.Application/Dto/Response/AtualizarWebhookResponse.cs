using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioTerra.Application.Dto.Response
{
    public class AtualizarWebhookResponse
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public AtualizarWebhookList Webhook { get; set; }
    }

    public class AtualizarWebhookList
    {
        public string Type { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public string[] Events { get; set; }
        public DateTime Updated_at { get; set; }
        public DateTime Created_at { get; set; }
    }
}

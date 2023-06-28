using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioTerra.Application.Dto.Request
{
    public class AtualizarWebhookRequest
    {
        [Required]
        public string Usuario { get; set; }
        [Required]
        public string Repositorio { get; set; }
        [Required]
        public long HookId { get; set; }
        [Required]
        public string Token { get; set; }
        [Required]
        public bool Ativo { get; set; }
        public string[] Eventos { get; set; }
    }
}

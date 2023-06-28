using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioTerra.Application.Dto
{
    public class WebhookDTO
    {
        [Required]
        public string Usuario { get; set; }
        [Required]
        public string Repositorio { get; set; }
        [Required]
        public bool Ativo { get; set; }
        [Required]
        public string Token { get; set; }
        [Required]
        public string[] Eventos { get; set; }

    }

    public static class Eventos
    {
        public static readonly string PUSH = "push";
        public static readonly string PULL_REQUEST = "pull_request";

    }
}

using AutoMapper;
using DesafioTerra.Application.Dto;
using DesafioTerra.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioTerra.Application.Profiles
{
    public class AntCorrupcaoProfile : Profile
    {
        public AntCorrupcaoProfile()
        {
            CreateMap<Repositorio, RepositorioDTO>().ReverseMap();
            CreateMap<Branch, BranchDTO>().ReverseMap();
            CreateMap<Webhook, WebhookDTO>().ReverseMap();
        }
    }
}

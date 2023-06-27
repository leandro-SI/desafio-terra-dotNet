﻿using DesafioTerra.Application.Dto;
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
    }
}
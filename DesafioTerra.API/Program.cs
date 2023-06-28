using DesafioTerra.Application.Services;
using DesafioTerra.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IAntCorrupcaoService, AntCorrupcaoService>();

builder.Services.AddHttpClient("RepositorioAPI", options =>
{
    options.BaseAddress = new Uri(builder.Configuration.GetSection("Bases:Repositorio_Url").Value);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API de integração com API REST do GitHub",
        Version = "v1",
        Description = "Applicação Desafio Terra - Backend.",
        Contact = new OpenApiContact
        {
            Name = "Leandro Cesar de Almeida",
            Email = "leandro.almeida@uvvnet.com.br",
            Url = new Uri("https://github.com/leandro-SI"),
        },
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

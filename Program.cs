using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using EcommerceSuplementos.Infrastructure.Data;
using EcommerceSuplementos.Infrastructure.Repositories;
using EcommerceSuplementos.Domain.Interfaces.Repositories;
using EcommerceSuplementos.Api.Services;
using EcommerceSuplementos.Domain.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// ==========================================================
// 1️⃣ Controllers + Swagger
// ==========================================================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Suplementos API",
        Version = "v1",
        Description = "API para gerenciamento de suplementos",
        Contact = new OpenApiContact
        {
            Name = "Seu Nome",
            Email = "seuemail@email.com"
        }
    });
});

// ==========================================================
// 2️⃣ Banco de dados
// ==========================================================
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// ==========================================================
// 3️⃣ Repositories
// ==========================================================
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IAvaliacaoRepository, AvaliacaoRepository>();
builder.Services.AddScoped<ICarrinhoRepository, CarrinhoRepository>();

// ==========================================================
// 4️⃣ Services
// ==========================================================
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IAvaliacaoService, AvaliacaoService>();
builder.Services.AddScoped<ISimulaçãoService, SimulacaoService>();
builder.Services.AddScoped<ICarrinhoService, CarrinhoService>();

// ==========================================================
// 5️⃣ CORS
// ==========================================================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// ==========================================================
// 6️⃣ Build
// ==========================================================
var app = builder.Build();

// ==========================================================
// 7️⃣ Middlewares
// ==========================================================

// 👇 PRIMEIRO: handler de erro
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "text/plain";

        var exceptionHandlerPathFeature =
            context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature>();

        if (exceptionHandlerPathFeature?.Error != null)
        {
            await context.Response.WriteAsync(exceptionHandlerPathFeature.Error.ToString());
        }
    });
});

// Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Suplementos API v1");
    c.RoutePrefix = "swagger";
});

app.UseCors("AllowAll");

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// 🔥 ESSENCIAL PRO RENDER
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Run($"http://0.0.0.0:{port}");
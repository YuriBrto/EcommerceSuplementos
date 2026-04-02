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

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Digite: Bearer {seu-token-jwt}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// ==========================================================
// 2️⃣ Banco de dados
// ==========================================================
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ==========================================================
// 3️⃣ Repositories
// ==========================================================
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IAvaliacaoRepository, AvaliacaoRepository>();

// ==========================================================
// 4️⃣ Services
// ==========================================================
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IAvaliacaoService, AvaliacaoService>();
builder.Services.AddScoped<ISimulaçãoService, SimulacaoService>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// ==========================================================
// 5️⃣ Build
// ==========================================================
var app = builder.Build();


// ==========================================================
// 6️⃣ Middlewares
// ==========================================================
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Suplementos API v1");
    c.RoutePrefix = "swagger";
});

app.UseCors("AllowAll");
//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();